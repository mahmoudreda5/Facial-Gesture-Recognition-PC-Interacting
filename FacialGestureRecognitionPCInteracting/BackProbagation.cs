using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialGestureRecognitionPCInteracting {
    interface BackProbagation {
        //BackProbagation algorithm main actions/functions
        //to be applied by network components (network)


    }

    interface MiniBackProbagation {
        //BackProbagation algorithm main actions/functions
        //to be applied by network components (neuron, layer)

        double /*neuron output*/ MLPForwardSignal(double[] features /*input features*/);  //forward signal
        double MLPBackwardSignal(NetworkLayer nextlayer /*next layer*/);  //backward signal
        double MLPBackwardSignal(double[] networkErrorSignal /*output Error signal (e)*/);  //backward signal
        void MLPWeightsUpdate(double[] features /*previous layer output*/);  //update weights
    }
}
