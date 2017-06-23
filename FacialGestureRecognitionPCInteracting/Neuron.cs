using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialGestureRecognitionPCInteracting {

    class Neuron : MiniBackProbagation, MiniRadialBasisFunctions, LeastMeanSquare {

        //members//////////////////////////////////////////////////
        int inputSize;  //number of neurons of previous layer
        int outputSize;  //number of neurons of next layer
        double[] weights;  //random
        double[] deltaWeights;  //for weights update
        //double[] neuronInput; //input will be previous layer output

        double netInput;

        double neuronOutput;
        //double desiredOutput;
        bool bias;  //bias flag
        int biasValue;

        int learningAlgoritm;
        double learningRate;

        double[] neuronCentroid;  //RBF
        double neuronVariance;  //RBF, sigma
        //double radialBasisFunction;  //RBF result

        //double errorSignal;  //output neuron only
        double localGradient;

        int activationType;

        //int layerIndex;
        int neuronIndex;
        bool outputNeuron;

        static Random random = new Random();  //random weights for all neurons
        ///////////////////////////////////////////////////////////

        public Neuron(int inputSize, int outputSize, /*int layerIndex, */int neuronIndex, 
            int activationType, double learningRate, int learningAlgoritm = 0, bool bias = false, bool outputNeuron = false) {

            this.inputSize = inputSize;
            this.outputSize = outputSize;
            
            //neuronInput = new double[this.size];

            this.bias = bias;
            this.biasValue = bias ? 1 : 0;

            weights = new double[this.inputSize + this.biasValue];
            deltaWeights = new double[this.inputSize + this.biasValue];

            //random initial weights
            //Random random = new Random();
            for(int i = 0; i < inputSize; i++)
                weights[i] = random.NextDouble();

            //this.layerIndex = layerIndex;
            this.neuronIndex = neuronIndex;

            this.activationType = activationType;
            this.learningRate = learningRate;

            this.outputNeuron = outputNeuron;

        }

        //////////////////////////////////////////////////////////////////////////
        //netInput
        double neuronNetInput(double[] neuronInput /*previous layer output*/) {
            //calculate neuron netInput
            double net = 0;
            for(int i = 0; i < inputSize + biasValue; i++)
                if(i == 0 && bias)
                    net += weights[i] * 1 /*bias input*/;
                else net += weights[i] * neuronInput[i - biasValue];

            netInput = net;
            return netInput;
        }

        //neuronOutput
        double neuronActivation(/**/) {
            //activate netInput
            neuronOutput = Activation.activate(netInput, activationType);
            return neuronOutput;
        }

        public double /*neuron output*/ MLPForwardSignal(double[] neuronInput /*previous layer output*/) {
            neuronNetInput(neuronInput);
            return neuronActivation();
        }

        //public double neuronForwardSignal(double[] neuronInput /*previous layer output*/) {
        //    neuronNetInput(neuronInput);
        //    return neuronActivation();
        //}

        //update neuron weights
        void updateNeuronWeights(/**/) {
            //update weights
            for(int i = 0; i < inputSize + biasValue; i++)
                weights[i] += deltaWeights[i];
        }

        //calculate delta weights
        void calculateNeuronDeltaWeights(double[] neuronInput /*previous layer output*/) {
            //calculate deltaWeights for weights update
            for(int i = 0; i < inputSize + biasValue; i++)
                if(i == 0 && bias)
                    deltaWeights[i] = learningRate * localGradient * 1 /*bias input*/;
                else deltaWeights[i] = learningRate * localGradient * neuronInput[i - biasValue];
        }

        //calculate neuron local gradient
        double calculateNeuronLocalGradient(NetworkLayer nextlayer /*next layer*/) {
            //calculate neuron localGradient
            double weightedGradients = 0;
            for(int i = 0; i < nextlayer.layerNeuronsNumber; i++)
                weightedGradients += nextlayer.neurons[i].weights[neuronIndex + biasValue] * nextlayer.neurons[i].localGradient;

            localGradient = weightedGradients * Activation.activate(netInput, activationType + 2  /*derivative of same activation*/);
            return localGradient;
        }

        double calculateNeuronLocalGradient(double[] networkErrorSignal) {
            //calculate neuron localGradient
            localGradient = networkErrorSignal[neuronIndex] * 
                Activation.activate(netInput, activationType + 2  /*derivative of same activation*/);
            return localGradient;
        }

        public double MLPBackwardSignal(NetworkLayer nextlayer /*next layer*/) {
            return calculateNeuronLocalGradient(nextlayer);
        }

        public double MLPBackwardSignal(double[] networkErrorSignal) {
            return calculateNeuronLocalGradient(networkErrorSignal);
        }

        //public void neuronBackwordSignal(NetworkLayer nextlayer /*next layer*/) {
        //    calculateNeuronLocalGradient(nextlayer);
        //}

        //public void neuronBackwordSignal(double[] networkErrorSignal) {
        //    calculateNeuronLocalGradient(networkErrorSignal);
        //}

        public void MLPWeightsUpdate(double[] neuronInput /*previous layer output*/) {
            calculateNeuronDeltaWeights(neuronInput);
            updateNeuronWeights();
        }
        //public void updateNeuronWeights(double[] neuronInput /*previous layer output*/) {
        //    calculateNeuronDeltaWeights(neuronInput);
        //    updateNeuronWeights();
        //}

        void setInput(double[] neuronCentroid, double neuronVariance) {
            this.neuronCentroid = neuronCentroid;
            this.neuronVariance = neuronVariance;
        }



        public double RBFForwardSignal(double[] features /*input features*/, double[] neuronCentroid, double neuronVariance) {
            setInput(neuronCentroid, neuronVariance);

            //forward signal (gaussian activation)
            neuronOutput = Activation.gaussianActivate(features, neuronCentroid, neuronVariance);

            return neuronOutput;
        }

        void calculateNeuronDeltaWeights(double[] neuronInput /*previous layer output*/, double[] networkErrorSignal) {
            //calculate deltaWeights for weights update
            for(int i = 0; i < inputSize + biasValue; i++)
                if(i == 0 && bias)
                    deltaWeights[i] = learningRate * networkErrorSignal[neuronIndex] * 1 /*bias input*/;
                else deltaWeights[i] = learningRate * networkErrorSignal[neuronIndex] * neuronInput[i - biasValue];
        }

        public double LMSForwardSignal(double[] neuronInput /*input features*/) {
            neuronNetInput(neuronInput);
            return neuronActivation();
        }

        public void LMSUpdateWeights(double[] features /*previous layer output*/, double[] networkErrorSignal) {
            calculateNeuronDeltaWeights(features, networkErrorSignal);
            updateNeuronWeights();
        }
        //////////////////////////////////////////////////////////////////////////

    }
}
