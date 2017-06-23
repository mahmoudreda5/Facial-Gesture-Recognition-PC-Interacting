using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialGestureRecognitionPCInteracting {
    class K_MeansClustering {

        public static List<MyPair<double[] /*centroid*/, double /*variance*/>> clusterProcess(k_Means trainingData) {
            //apply k-means clustering on trainingSet and return each cluster center and variance
            if(trainingData.clustersNumber <= trainingData.trainingSet.Count) {
                bool stop = false, initialeStep = true;
                while(!stop) {
                    //cluster process
                    stop = cluster(trainingData, initialeStep);

                    //loop over clusters
                    for(int i = 0; i < trainingData.clusters.Count; i++)
                        trainingData.clusters[i].first = updateCenteroids(trainingData, i);

                    initialeStep = false;
                }
            }

            //till now we have centroids, need to calculate variance
            for(int i = 0; i < trainingData.clustersNumber; i++) {
                //calculate variance of each cluster
                double variance = 0;
                for(int j = 0; j < trainingData.clusters[i].second.Count; j++)
                    variance += ecludeanDistance(trainingData.clusters[i].second[j].FacialGestureFeatures, trainingData.clusters[i].first);

                trainingData.clusteringResult.Add(new MyPair<double[], double>(trainingData.clusters[i].first, variance));

            }


            return trainingData.clusteringResult;  //mlk4 clusters 3ndy :'D
        }

        static double ecludeanDistance(double[] features, double[] center) {
            //calculate ecludeanDistance
            double result = 0;

            for(int i = 0; i < FacialGesture.facialGestureFeaturesPerSample; i++)
                result += (center[i] - features[i]) * (center[i] - features[i]);

            return Math.Sqrt(result);

        }

        static int minIndex(double[] distances) {
            //return index of min distance
            int minInd = 0;
            for(int i = 1; i < distances.Length; i++)
                if(distances[i] < distances[minInd]) minInd = i;

            return minInd;
        }

        static bool cluster(k_Means trainingData, bool initialStep) {
            bool stop = true;

            if(initialStep)
                //loop over training data
                for(int i = 0; i < trainingData.trainingSet.Count; i++) {
                    double[] centersED = new double[trainingData.clustersNumber];
                    //cluster
                    for(int j = 0; j < trainingData.clustersNumber; j++)
                        centersED[j] = ecludeanDistance(trainingData.trainingSet[i].FacialGestureFeatures,
                            trainingData.clusters[j].first);

                    //pick min ED
                    int minInd = minIndex(centersED);

                    //add current sample to cluster with minInd
                    trainingData.clusters[minInd].second.Add(trainingData.trainingSet[i]);

                    stop = false;
                } else  //loop over samples at clusters
                for(int i = 0; i < trainingData.clustersNumber; i++) {
                    //cluster
                    for(int j = 0; j < trainingData.clusters[i].second.Count; j++) {

                        double[] centersED = new double[trainingData.clustersNumber];
                        //cluster
                        for(int k = 0; k < trainingData.clustersNumber; k++)
                            centersED[k] = ecludeanDistance(trainingData.clusters[i].second[j].FacialGestureFeatures,
                                trainingData.clusters[k].first);

                        //pick min ED
                        int minInd = minIndex(centersED);

                        if(minInd != i) {
                            //not in same cluster
                            //add current sample to cluster with minInd and remove it from old cluster
                            trainingData.clusters[minInd].second.Add(trainingData.clusters[i].second[j]);
                            trainingData.clusters[i].second.RemoveAt(j);

                            stop = false;
                        }
                    }
                }

            return stop;
        }

        static double[] updateCenteroids(k_Means trainingData, int clusterIndex) {
            //each center is the mean of the cluster
            double[] newClusterCenter = Enumerable.Repeat((double) 0, FacialGesture.facialGestureFeaturesPerSample).ToArray<double>();
            for(int i = 0; i < trainingData.clusters[clusterIndex].second.Count; i++)
                for(int j = 0; j < FacialGesture.facialGestureFeaturesPerSample; j++)
                    newClusterCenter[j] += trainingData.clusters[clusterIndex].second[i].FacialGestureFeatures[j];

            for(int j = 0; j < FacialGesture.facialGestureFeaturesPerSample; j++)
                newClusterCenter[j] /= trainingData.clusters[clusterIndex].second.Count;

            return newClusterCenter;
        }
    }

    class k_Means {

        //members////////////////////////////////////////////////////////////////////////////
        public int clustersNumber;

        public List<FacialGesture> trainingSet;
        public List<MyPair<double[] /*centroid*/, List<FacialGesture> /*cluster samples*/>> clusters;
        public List<MyPair<double[] /*centroid*/, double /*variance*/>> clusteringResult;
        /////////////////////////////////////////////////////////////////////////////////////

        public k_Means(int clustersNumber, List<FacialGesture> trainingData) {
            this.clustersNumber = clustersNumber;
            this.trainingSet = trainingData;

            clusters = new List<MyPair<double[], List<FacialGesture>>>(this.clustersNumber);
            clusteringResult = new List<MyPair<double[], double>>(this.clustersNumber);
            //initiale centers and clusters sizes
            for(int i = 0; i < this.clustersNumber; i++)
                clusters.Add(new MyPair<double[], List<FacialGesture>>(trainingData[i].FacialGestureFeatures,
                    new List<FacialGesture>(trainingData.Count / clustersNumber /*t2rebn m4 exactly*/)));

        }

    }

    class MyPair<T1, T2> {

        public T1 first;
        public T2 second;

        public MyPair(T1 first, T2 second) {
            this.first = first;
            this.second = second;
        }

    }
}
