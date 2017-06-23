using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace FacialGestureRecognitionPCInteracting {

    //statci class for activation functions
    class Activation {
        //static member variables
        public const int STEP = 1;  //STEP(THRESHOLD) function
        public const int SIGNUM = 2;  //SIGNUM function
        public const int SIGMOID = 3;  //SIGMIOD function
        public const int HYPERBOLIC_TANGENT_SIGMOID = 4;  //HYPERBOLIC_TANGENT_SIGMOID function
        public const int SIGMOID_DERIVATIVE = 5;  //SIGMIOD_DERIVATIVE 
        public const int HYPERBOLIC_TANGENT_SIGMOID_DERIVATIVE = 6;  //HYPERBOLIC_TANGENT_SIGMOID_DERIVATIVE

        //default constructor

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        //activation function
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double/*result*/ activate(double net, int activationType, double thresh = 0,
            double slopeParam = 1, double tanhParam = 1) {
            //apply activation function

            //check the activation type
            //1 step, 2 signum, 3 sigmoid, 4 or any other integer hyperbolic tangent sigmoid
            //5 SIGMIOD_DERIVATIVE, 6 HYPERBOLIC_TANGENT_SIGMOID_DERIVATIVE
                if(activationType == Activation.STEP)  //STEP(THRESHOLD) function
                    return net >= thresh ? 1 : 0;
                else if(activationType == Activation.SIGNUM)  //SIGNUM function
                    if(net > thresh) return 1;
                    else if(net < thresh) return -1;
                    else return 0;
                else if(activationType == Activation.SIGMOID)  //SIGMIOD function
                    return 1 / (1 + Math.Exp(-1 * slopeParam * net));
                else if(activationType == Activation.HYPERBOLIC_TANGENT_SIGMOID)  //HYPERBOLIC_TANGENT_SIGMOID function
                    return (1 - Math.Exp(-1 * slopeParam * net)) / (1 + Math.Exp(-1 * slopeParam * net));
                else if(activationType == Activation.SIGMOID_DERIVATIVE) {
                    //SIGMIOD_DERIVATIVE
                    double sigmoidOutput = (1 - Math.Exp(-1 * slopeParam * net)) / (1 + Math.Exp(-1 * slopeParam * net));
                    return slopeParam * sigmoidOutput * (1 - sigmoidOutput);
                } else if(activationType == Activation.HYPERBOLIC_TANGENT_SIGMOID_DERIVATIVE) {
                    //HYPERBOLIC_TANGENT_SIGMOID_DERIVATIVE
                    double tanhOutput = (1 - Math.Exp(-1 * slopeParam * net)) / (1 + Math.Exp(-1 * slopeParam * net));
                    return tanhParam / slopeParam * (slopeParam - tanhOutput) * (slopeParam + tanhOutput);
                } else return -1;

        }

        public static double gaussianActivate(double[] features /*input features*/, double[] neuronCentroid, double neuronVariance) {
            //gaussian EXP[-(r = pattern - centroid)^2 / 2 * sigma^2]
            double summation = 0;
            for(int i = 0; i < FacialGesture.facialGestureFeaturesPerSample; i++)
                summation += (neuronCentroid[i] - features[i]) * (neuronCentroid[i] - features[i]);

            return Math.Exp((-1 * summation) / (2 * neuronVariance * neuronVariance));
            
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
