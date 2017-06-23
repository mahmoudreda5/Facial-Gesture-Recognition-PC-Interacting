using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialGestureRecognitionPCInteracting {
    interface LeastMeanSquare {

        //main actions functions of LMS
        double LMSForwardSignal(double[] features = null/*input features*/);
        void LMSUpdateWeights(double[] features = null /*previous layer output*/, double[] networkErrorSignal = null);
    }

    //interface MiniLeastMeanSquare {

    //}
}
