using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialGestureRecognitionPCInteracting {

    class NetworkLayer : MiniBackProbagation, RadialBasisFunctions, LeastMeanSquare {

        //members//////////////////////////////////////////////////
        int inputSize;  //number of neurons of previous layer
        public int layerNeuronsNumber;  //layers number of neuron, outputSize        

        double learningRate;
        int activationType;

        //double desiredOutput;
        bool bias;

        int layerIndex;
        bool outputLayer;

        int learningAlgoritm;

        public List<Neuron> neurons;

        double[] layerInput;
        public double[] layerOutput;
        double[] layerGradients;

        List<double[]> centroids;
        double[] variances;
        ///////////////////////////////////////////////////////////

        public NetworkLayer(int inputSize, int layerNeuronsNumber, int layerIndex,
            int activationType, double learningRate, int learningAlgoritm = 0, bool bias = false, bool outputLayer = false) {

            this.inputSize = inputSize;
            this.layerNeuronsNumber = layerNeuronsNumber;

            this.learningRate = learningRate;
            this.activationType = activationType;

            this.layerIndex = layerIndex;
            this.outputLayer = outputLayer;

            //construct NetworkLayer
            neurons = new List<Neuron>(this.layerNeuronsNumber);
            for(int i = 0; i < this.layerNeuronsNumber; i++)
                if(!outputLayer)  //hidden Layers
                    neurons.Add(new Neuron(this.inputSize, this.layerNeuronsNumber, i, this.activationType, this.learningRate,
                       learningAlgoritm , bias));
                else neurons.Add(new Neuron(this.inputSize, this.layerNeuronsNumber, i, this.activationType, this.learningRate,
                    learningAlgoritm, bias, true));

            layerInput = new double[this.inputSize];
            layerOutput = new double[this.layerNeuronsNumber];
            layerGradients = new double[this.layerNeuronsNumber];

            this.bias = bias;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        //get input
        void setLayerInput(double[] layerInput) {
            this.layerInput = layerInput;  //input pattern
            //this.desiredOutput = desiredOutput;
        }


        public double MLPForwardSignal(double[] layerInput) {
            setLayerInput(layerInput);

            for(int i = 0; i < layerNeuronsNumber; i++)
                layerOutput[i] = neurons[i].MLPForwardSignal(layerInput);

            return -1;
        }

        public double MLPBackwardSignal(NetworkLayer nextlayer /*next layer*/) {
            for(int i = 0; i < layerNeuronsNumber; i++)
                layerGradients[i] = neurons[i].MLPBackwardSignal(nextlayer);

            return -1;
        }

        public double MLPBackwardSignal(double[] networkErrorSignal) {
            for(int i = 0; i < layerNeuronsNumber; i++)
                layerGradients[i] = neurons[i].MLPBackwardSignal(networkErrorSignal);

            return -1;
        }

        public void MLPWeightsUpdate(double[] features /*previous layer output*/) {
            for(int i = 0; i < layerNeuronsNumber; i++)
                neurons[i].MLPWeightsUpdate(layerInput);
        }

        void setLayerInput(double[] features /*input features*/, List<double[]> centroids, double[] variances){
            this.layerInput = features;
            this.centroids = centroids;
            this.variances = variances;
        }

        public void RBFForwardSignal(double[] features /*input features*/, List<double[]> centroids, double[] variances) {
            setLayerInput(features, centroids, variances);

            for(int i = 0; i < layerNeuronsNumber; i++)
                layerOutput[i] = neurons[i].RBFForwardSignal(features, centroids[i], variances[i]);
        }

        public double LMSForwardSignal(double[] features /*input features*/){
            setLayerInput(features);

            for(int i = 0; i < layerNeuronsNumber; i++)
                layerOutput[i] = neurons[i].LMSForwardSignal(features);

            return -1;
        }

        public void LMSUpdateWeights(double[] features /*previous layer output*/, double[] networkErrorSignal){
            for(int i = 0; i < layerNeuronsNumber; i++)
                neurons[i].LMSUpdateWeights(features, networkErrorSignal);

        }
        /////////////////////////////////////////////////////////////////////////////////////////////

    }
}
