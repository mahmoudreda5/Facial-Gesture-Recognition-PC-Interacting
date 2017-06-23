using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialGestureRecognitionPCInteracting {
    interface Neural {

        //main action functions to be done by neural network

        void train(List<FacialGesture> trainingSet);  //neural netork training
        double test(List<FacialGesture> testingSet);  //neural network testing
        bool classify(FacialGesture pattern);  //classify pattern


    }
}
