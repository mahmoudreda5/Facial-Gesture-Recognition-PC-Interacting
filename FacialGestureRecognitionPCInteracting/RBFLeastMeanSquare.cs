using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialGestureRecognitionPCInteracting {
    class RBFLeastMeanSquare : Neural{

        //members///////////////////////////////////////////////
        int epochsNumber;   //ephocs number
        int activationType;  //activation type
        double LearningRate;  //learning rate

        int networkTotalLayersNumber;  //Network number of layers

        int[] layerNueronsNumber;  //number of neurons in each layer

        double MSEThreshold;  //mean square error threshold

        int RBFLayerIndex;
        int learningAlgoritm;
        bool bias;  //bias flag

        double meanSqaureError;  //meanErrorSquare
        int totalTestingError;  //totalTestingError
        double accuracy;  //network accuracy

        NeuralNetwork RBFNetwork;  //network structure, activation and learning algorithm
        /////////////////////////////////////////////////////////////////////////////////

        public RBFLeastMeanSquare(int networkTotalLayersNumber, int[] layerNueronsNumber, int epochsNumber, int activationType,
            double LearningRate, double MSEThreshold, int learningAlgoritm = 0, int RBFLayerIndex = 0, bool bias = false) {

            this.networkTotalLayersNumber = networkTotalLayersNumber;
            this.layerNueronsNumber = layerNueronsNumber;

            this.epochsNumber = epochsNumber;
            this.activationType = activationType;
            this.LearningRate = LearningRate;
            this.MSEThreshold = MSEThreshold;

            this.learningAlgoritm = learningAlgoritm;
            this.bias = bias;

            this.RBFLayerIndex = RBFLayerIndex;
            RBFNetwork = new NeuralNetwork(this.networkTotalLayersNumber, this.layerNueronsNumber, this.activationType,
                this.LearningRate, learningAlgoritm, this.bias, RBFLayerIndex);  //neural network construction MLP with backprobagation

        }
        /////////////////////////////////////////////////////////////////////////////////

        /////////////////////////////////////////////////////////////////////////////////

        public void train(List<FacialGesture> trainingSet) {
            //loop over epochs
            for(int i = 0; i < epochsNumber; i++) {
                //loop over samples to train network (update weights)
                for(int j = 0; j < trainingSet.Count; j++) {
                    //BackProbagation
                    //=> forward phase cal. actual output and error signal
                    //=> backward phase cal. local gradient and update weights

                    RBFNetwork.setNetworkInput(trainingSet[j].FacialGestureFeatures, trainingSet[j].desiredClass);  //network input
                    RBFNetwork.RBFForwardSignal();  //RBF forward signal=> from non linear to linear data
                    RBFNetwork.LMSForwardSignal();  //LMS backward signal=> output
                    RBFNetwork.LMSUpdateWeights();  //update weights
                }

                meanSqaureError = 0;
                for(int j = 0; j < trainingSet.Count; j++) {
                    //calculate mean square error of final epoch weights to stop
                    //forward phase only to get actual output
                    RBFNetwork.setNetworkInput(trainingSet[j].FacialGestureFeatures, trainingSet[j].desiredClass);  //network input
                    RBFNetwork.RBFForwardSignal();  //RBF forward signal=> from non linear to linear data
                    RBFNetwork.LMSForwardSignal();  //LMS backward signal=> output

                    //calculate mean square error for pattern
                    //patter class is neuron with max output
                    double[] outputSignal = RBFNetwork.networkOutput;

                    for(int k = 0; k < outputSignal.Length; k++)
                        meanSqaureError += (trainingSet[j].desiredClass[k] - outputSignal[k]) *
                            (trainingSet[j].desiredClass[k] - outputSignal[k]);

                }

                //check mean square error to stop training
                if(meanSqaureError <= MSEThreshold) break;  //training done...
            }
        }


        public double test(List<FacialGesture> testingSet) {
            accuracy = 0;

            //test network performance
            for(int i = 0; i < testingSet.Count; i++) {
                //BackProbagation
                //=> forward phase cal. actual output and error signal

                RBFNetwork.setNetworkInput(testingSet[i].FacialGestureFeatures, testingSet[i].desiredClass);  //network input
                RBFNetwork.RBFForwardSignal();  //RBF forward signal=> from non linear to linear data
                RBFNetwork.LMSForwardSignal();  //LMS backward signal=> output

                //check sample classification
                double[] outputSignal = RBFNetwork.networkOutput;
                int maxIndex = getMaxIndex(outputSignal);

                //set test sample actual class
                testingSet[i].actualClass = maxIndex + 1;

                if(testingSet[i].desiredClass[maxIndex] == 1) accuracy++;
            }

            return accuracy;
        }

        public bool classify(FacialGesture pattern) {
            return false;
        }

        int getMaxIndex(double[] outputSignal) {
            int maxIndex = 0;
            for(int k = 1; k < outputSignal.Length; k++)
                if(outputSignal[k] > outputSignal[maxIndex]) maxIndex = k;

            return maxIndex;
        }
        /////////////////////////////////////////////////////////////////////////////////

    }
}
