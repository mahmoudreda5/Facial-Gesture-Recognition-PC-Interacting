using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialGestureRecognitionPCInteracting{
    class NeuralNetwork : BackProbagation, RadialBasisFunctions, LeastMeanSquare {

        //members//////////////////////////////////////////////////
        //int NetworkHiddenLayersNumber;
        int NetworkTotalLayersNumber;  //Network number of layers

        int[] LayerNueronsNumber;  //number of neurons in each layer

        int inputSize;  //number of neurons of previous layer
        public int outputSize;  //number of neurons of next layer

        double[] desiredOutput;

        double learningRate;
        int activationType;

        int RBFLayerIndex;
        int learningAlgoritm;

        bool bias;  //bias flag

        List<NetworkLayer> layers;

        double[] inputPattern;
        public double[] networkOutput;
        double[] networkErrorSignal;

        k_Means KMean;
        List<double[]> centroids;
        double[] variances;
        //////////////////////////////////////////////////////////

        public NeuralNetwork(int NetworkTotalLayersNumber, int[] LayerNueronsNumber, /*int inputSize, int outputSize,*/
            int activationType, double learningRate, int learningAlgoritm, bool bias = false, int RBFLayerIndex = 0) {

            this.NetworkTotalLayersNumber = NetworkTotalLayersNumber;
            //this.NetworkHiddenLayersNumber = this.NetworkHiddenLayersNumber - 2 /*input and output*/;

            this.LayerNueronsNumber = LayerNueronsNumber;  //include input layer

            this.inputSize = LayerNueronsNumber[0 /*input*/];  //number of neurons of input layer
            this.outputSize = LayerNueronsNumber[NetworkTotalLayersNumber - 1];  //number of neurons of output layer

            this.activationType = activationType;
            this.learningRate = learningRate;

            //NeuralNetwork construction
            layers = new List<NetworkLayer>(this.NetworkTotalLayersNumber - 1 /*exclude input layer 34an mfeha4 processing*/);
            for(int i = 0; i < this.NetworkTotalLayersNumber - 2 /*for output layer*/; i++)
                //add hidden layer
                //layer input size = size of previous layer
                //layer output size = neurons number
                layers.Add(new NetworkLayer(LayerNueronsNumber[i], LayerNueronsNumber[i + 1], i, activationType,
                    learningRate, learningAlgoritm, bias));

            //output Layer
            layers.Add(new NetworkLayer(LayerNueronsNumber[NetworkTotalLayersNumber - 2], LayerNueronsNumber[NetworkTotalLayersNumber - 1],
                NetworkTotalLayersNumber - 2 /*exclude input layer*/, activationType, learningRate, learningAlgoritm, bias, true /*output layer*/));

            networkErrorSignal = new double[outputSize];

            this.RBFLayerIndex = RBFLayerIndex;
            this.learningAlgoritm = learningAlgoritm;

            this.bias = bias;

            this.KMean = new k_Means(LayerNueronsNumber[RBFLayerIndex + 1], FacialGestures.trainingFacialGestures);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void setNetworkInput(double[] inputPattern, double[] desiredOutput) {
            this.inputPattern = inputPattern;
            this.desiredOutput = desiredOutput;
        }

        double[] calculateNetworkErrorSignal() {
            //calculate output error signal
            for(int i = 0; i < outputSize; i++)
                this.networkErrorSignal[i] = desiredOutput[i] - networkOutput[i];

            return networkErrorSignal;
        }

        public double MLPForwardSignal(/*double[] features = null/*already exist*/) {

            //pattern is first layer input then each layer takes pervious layer output
            //0 firts hidden layer
            layers[0].MLPForwardSignal(inputPattern);
            for(int i = 1; i < NetworkTotalLayersNumber - 1 /*exclude input layer*/; i++)
                layers[i].MLPForwardSignal(layers[i - 1].layerOutput);

            networkOutput = layers[NetworkTotalLayersNumber - 2 /*output layer index*/].layerOutput;

            return -1;
        }

        public void MLPBackwardSignal() {
            //calculate output error signal
            calculateNetworkErrorSignal();

            for(int i = NetworkTotalLayersNumber - 2 /*exclude output*/; i >= 0; i--) {
                if(i == NetworkTotalLayersNumber - 2)
                    layers[i].MLPBackwardSignal(networkErrorSignal);
                else {
                    layers[i].MLPBackwardSignal(layers[i + 1] /*next layer*/);
                    layers[i + 1].MLPWeightsUpdate(layers[i].layerOutput);
                }
            }

            layers[0].MLPWeightsUpdate(inputPattern);

        }

        public void RBFForwardSignal(double[] features = null/*input features*/, List<double[]> ExistedCentroids = null,
            double[] ExistedVariances = null) {
            List<MyPair<double[] /*centroid*/, double /*variance*/>> clusteringResult =
                K_MeansClustering.clusterProcess(KMean);

            List<double[]> centroids = new List<double[]>(KMean.clustersNumber);
            double[] variances = new double[KMean.clustersNumber];

            for(int i = 0; i < KMean.clustersNumber; i++) {
                centroids.Add(clusteringResult[i].first);
                variances[i] = clusteringResult[i].second;
            }

            this.centroids = centroids;
            this.variances = variances;
            layers[RBFLayerIndex].RBFForwardSignal(inputPattern, centroids, variances);
        }

        public double LMSForwardSignal(double[] features = null/*input features*/) {
            //forward signal of output layer
            layers[RBFLayerIndex + 1 /*output layer*/].LMSForwardSignal(layers[RBFLayerIndex].layerOutput);
            networkOutput = layers[RBFLayerIndex + 1 /*output layer*/].layerOutput;

            return -1;
        }
        public void LMSUpdateWeights(double[] features = null/*previous layer output*/, double[] networkErrorSignal = null) {
            calculateNetworkErrorSignal();
            layers[NetworkTotalLayersNumber - 2 /*output layer*/].LMSUpdateWeights(layers[RBFLayerIndex].layerOutput, this.networkErrorSignal);
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}
