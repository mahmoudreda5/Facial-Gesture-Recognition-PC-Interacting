using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacialGestureRecognitionPCInteracting {
    interface RadialBasisFunctions {

        //main actions methods of RadialBasisFunctions
        void RBFForwardSignal(double[] features = null/*input features*/, List<double[]> centroids = null, double[] variances = null);

    }

    interface MiniRadialBasisFunctions {

        //main actions methods of RadialBasisFunctions
        double RBFForwardSignal(double[] features /*input features*/, double[] centroid, double variance);
    }
}
