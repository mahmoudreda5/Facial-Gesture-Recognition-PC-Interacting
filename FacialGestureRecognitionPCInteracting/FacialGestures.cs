using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FacialGestureRecognitionPCInteracting {
    static class FacialGestures {

        //load and preprocess FacialGesture dataSet
        public static string FACIAL_GESTURES_PATH;

        //FacialGesture DataSet Information
        public const int facialGestureClassesNumber = 4;
        public const int facialGestureSamplesPerClass = 20;
        public const int facialGestureTotalSamplesNumber = facialGestureClassesNumber * facialGestureSamplesPerClass;
        public const int facialGestureTrainingSamplesPerClass = 15;
        public const int facialGestureTestingSamplesPerClass = 5;

        //private static bool normalized = false;

        static string[] extensions = new string[] {".pts"};
        static string[] classes = new string[] { "Closing Eyes", "Looking Down", "Looking Front", "Looking Left" };

        //flags
        const int TRAIN = 1;
        const int TEST = 2;
        public static bool dataLoaded = false;

        //FacialGesture DataSet containers
        //--capacity: Capacity is the number of elements that the List<T> can store before resizing is required
        public static List<FacialGesture> trainingFacialGestures = 
            new List<FacialGesture>(facialGestureTrainingSamplesPerClass * facialGestureClassesNumber);
        public static List<FacialGesture> testingFacialGestures = 
            new List<FacialGesture>(facialGestureTestingSamplesPerClass * facialGestureClassesNumber);

        //FacialGestures DataSet loading and preprocessing (normalization, shuffling)

        //load FacialGesture DataSet
        static void loadFacialGestures(/**/) {

            if(trainingFacialGestures != null && trainingFacialGestures.Count != 0)
                trainingFacialGestures.Clear();
            if(testingFacialGestures != null && testingFacialGestures.Count != 0)
                testingFacialGestures.Clear();

            //load training and testing data
            string[] trainTestDirectories = Directory.GetDirectories(FacialGestures.FACIAL_GESTURES_PATH);

            for(int i = 0; i < trainTestDirectories.Length; i++) {
                string[] directories = trainTestDirectories[i].Split('\\');
                if(directories[directories.Length - 1].Equals("Training Dataset"))
                    loadFacialGesturesSamples(FacialGestures.TRAIN, FacialGestures.FACIAL_GESTURES_PATH + "\\"
                        + directories[directories.Length - 1]);
                else if(directories[directories.Length - 1].Equals("Testing Dataset"))
                    loadFacialGesturesSamples(FacialGestures.TEST, FacialGestures.FACIAL_GESTURES_PATH + "\\"
                        + directories[directories.Length - 1]);
            }
        }


        static void loadFacialGesturesSamples(int trainTestFlag, string rootDirectoryPath) {
            //load training and testing data
            string[] FacialGesturesClasses = Directory.GetDirectories(rootDirectoryPath);

            for(int i = 0; i < FacialGesturesClasses.Length; i++) {
                string directoryPath = "";
                double[] desiredClass = Enumerable.Repeat((double) 0, facialGestureClassesNumber).ToArray<double>();

                string[] directories = FacialGesturesClasses[i].Split('\\');
                for(int j = 0; j < classes.Length; j++){
                    if(directories[directories.Length - 1].Equals(classes[j])){
                        directoryPath = FacialGesturesClasses[i];
                        desiredClass[j] = 1;
                    }
                }

                //if(directories[directories.Length - 1].Equals("Closing Eyes")) {
                //    directoryPath = rootDirectoryPath + "\\" + directories[directories.Length - 1];
                //    desiredClass[0] = 1;
                //} else if(directories[directories.Length - 1].Equals("Looking Down")) {
                //    directoryPath = rootDirectoryPath + "\\" + directories[directories.Length - 1];
                //    desiredClass[1] = 1;
                //} else if(directories[directories.Length - 1].Equals("Looking Front")) {
                //    directoryPath = rootDirectoryPath + "\\" + directories[directories.Length - 1];
                //    desiredClass[2] = 1;
                //} else if(directories[directories.Length - 1].Equals("Looking Left")) {
                //    directoryPath = rootDirectoryPath + "\\" + directories[directories.Length - 1];
                //    desiredClass[3] = 1;
                //}

                if(directoryPath != "") {
                    //load data
                    FileInfo[] FacialGesturesPts = getFiles(directoryPath);
                    for(int j = 0; j < FacialGesturesPts.Length; j++) {
                        //read features
                        FileStream FacialGestureStream = new FileStream(FacialGesturesPts[j].FullName, FileMode.Open, FileAccess.Read);
                        StreamReader FacialGestureReader = new StreamReader(FacialGestureStream, Encoding.UTF8);

                        bool startReadPts = false;
                        string line = "";
                        int pointIndex = 0;
                        PointF[] featuresPoints = new PointF[FacialGesture.facialGestureFeaturesPerSample + 1];
                        while((line = FacialGestureReader.ReadLine()) != null){

                            if(line.Equals("}"))
                                break;

                            if(startReadPts) {
                                string[] point = line.Split(' ');
                                featuresPoints[pointIndex++] = new PointF(float.Parse(point[0]), float.Parse(point[1]));
                            }

                            if(line.Equals("{"))
                                startReadPts = true;
                            
                        }

                        //get features
                        double[] features = new double[FacialGesture.facialGestureFeaturesPerSample];
                        for(int k = 0 , index = 0; k < featuresPoints.Length; k++, index++) {
                            if(k == FacialGesture.topOfNose) { index--; continue; }
                            features[index] = eculdeanDistance(featuresPoints[FacialGesture.topOfNose], featuresPoints[k]);
                        }

                        if(trainTestFlag == FacialGestures.TRAIN)
                            trainingFacialGestures.Add(new FacialGesture(features, desiredClass));
                        else if(trainTestFlag == FacialGestures.TEST)
                            testingFacialGestures.Add(new FacialGesture(features, desiredClass));
                    }
                    
                }
            }
        }

        //get all files at directory folder
        static FileInfo[] getFiles(string directoryPath) {

            //get selected folder files
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            IEnumerable<FileInfo> filesInfoEnumerable = directoryInfo.EnumerateFiles();

            FileInfo[] filesInfo = filesInfoEnumerable.Where(file => extensions.Contains(file.Extension)).ToArray<FileInfo>();

            return filesInfo;
        }

        static double eculdeanDistance(PointF firstPoint, PointF secondPoint) {
            return Math.Sqrt((secondPoint.X - firstPoint.X) * (secondPoint.X - firstPoint.X) +
                (secondPoint.Y - firstPoint.Y) * (secondPoint.Y - firstPoint.Y));
        }

        public static void process(string FACIAL_GESTURES_PATH) {

            FacialGestures.FACIAL_GESTURES_PATH = FACIAL_GESTURES_PATH;

            //load and preprocess
            loadFacialGestures();

            trainingFacialGestures = Preprocessing.normalize(trainingFacialGestures);
            testingFacialGestures = Preprocessing.normalize(testingFacialGestures);

            trainingFacialGestures = Preprocessing.shuffle(trainingFacialGestures);
            testingFacialGestures = Preprocessing.shuffle(testingFacialGestures);

            dataLoaded = true;
        }
    }

    class FacialGesture {

        //FacialGesture object Information
        public const int facialGestureFeaturesPerSample = 19;
        public const int topOfNose = 13;  //zero based

        //members
        public double[] FacialGestureFeatures;
        public double[] desiredClass;  //desiredClass of each sample
        public double actualClass;  //actualClass of each sample
        public double error;  //error of each sample

        public FacialGesture(double[] FacialGestureFeatures, double[] desiredClass) {
            this.FacialGestureFeatures = FacialGestureFeatures;
            this.desiredClass = desiredClass;
        }

    }

    class Preprocessing {

        //normalization
        public static List<FacialGesture> normalize(List<FacialGesture> data) {

            double[] featuresMean = new double[FacialGesture.facialGestureFeaturesPerSample];
            double[] featuresMax = new double[FacialGesture.facialGestureFeaturesPerSample];

            for(int i = 0; i < FacialGesture.facialGestureFeaturesPerSample; i++)
                featuresMean[i] = 0;

            //cal featuresMean
            for(int i = 0; i < data.Count; i++)
                for(int j = 0; j < FacialGesture.facialGestureFeaturesPerSample; j++)
                    featuresMean[j] += data[i].FacialGestureFeatures[j];

            for(int i = 0; i < FacialGesture.facialGestureFeaturesPerSample; i++)
                featuresMean[i] /= data.Count;

            //-mean, cal featuresMax
            for(int i = 0; i < data.Count; i++)
                for(int j = 0; j < FacialGesture.facialGestureFeaturesPerSample; j++) {
                    data[i].FacialGestureFeatures[j] -= featuresMean[j];

                    if(i == 0)
                        featuresMax[j] = Math.Abs(data[i].FacialGestureFeatures[j]);
                    else
                        featuresMax[j] = Math.Max(featuresMax[j], Math.Abs(data[i].FacialGestureFeatures[j]));
                }

            // /max
            for(int i = 0; i < data.Count; i++)
                for(int j = 0; j < FacialGesture.facialGestureFeaturesPerSample; j++)
                    data[i].FacialGestureFeatures[j] /= featuresMax[j];

            return data;

        }

        //shuffling
        public static List<FacialGesture> shuffle(List<FacialGesture> data) {
            Random random = new Random();

            for(int i = 0; i < data.Count; i++) {
                //generate random index
                int randomIndex = random.Next(data.Count);

                //swap with current index
                FacialGesture temp = data[i];
                data[i] = data[randomIndex];
                data[randomIndex] = temp;
            }

            return data;
        }

    }

}
