using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacialGestureRecognitionPCInteracting {
    public partial class mainForm : Form {

        //members/////////////////////////////////////////////////////////////////
        MLPBackProbagation mlpBackProbagation;  //neural network
        RBFLeastMeanSquare rbfLeastMeanSquare;  //neural network
        /////////////////////////////////////////////////////////////////////////

        public mainForm() {
            InitializeComponent();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        //open search folder
        FolderBrowserDialog openFolder(/*no params*/) {
            //choose folder using FolderBrowserDialog
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();

            //show dailog
            DialogResult dialogResult = openFolderDialog.ShowDialog();

            //check choosen folder
            if(dialogResult != DialogResult.OK || String.IsNullOrWhiteSpace(openFolderDialog.SelectedPath))
                //no folder choosen
                return null;

            //return openFolderDialog object
            return openFolderDialog;
        }

        private void dataSet_Click(object sender, EventArgs e) {
            //chose data set folder then load dataset
            FolderBrowserDialog openFolderDialog = openFolder();
            if(openFolderDialog != null) {
                //load data set
                FacialGestures.process(openFolderDialog.SelectedPath);  //data loading and preprocessing
            }
        }

        private void train_Click(object sender, EventArgs e) {
            //check user byst3bt wla l2
            if(FacialGestures.dataLoaded) {
                if(EpochsNumber.Text != "" && LearningRate.Text != "" && MSEThreshold.Text != "" /*&&
                    HiddenLayers.Text != "" */&& HiddenNeurons.Text != "") {
                    //GUI inputs
                    int epochsNumber = int.Parse(EpochsNumber.Text);
                    double learningRate = double.Parse(LearningRate.Text);
                    double MseThreshold = double.Parse(MSEThreshold.Text);

                    int activationType = Sigmoid.Checked ? 3 : 4;  //3 for sigmoid and 4 for tanh 
                    int learningAlgorithm = LearningAlgorithms.SelectedIndex;  //0 for MLP and 1 for RBF

                    int networkTotalLayerNumber = 3 /*input RBF output*/;
                    if(learningAlgorithm == 0)
                        networkTotalLayerNumber = int.Parse(HiddenLayers.Text) + 2 /*input and output layers*/;

                    int[] layerNueronsNumber = Array.ConvertAll((FacialGesture.facialGestureFeaturesPerSample.ToString() /*input layer*/ + 
                        " " + HiddenNeurons.Text + " " + FacialGestures.facialGestureClassesNumber.ToString() /*output layer*/).Split(' '),
                        int.Parse);

                    bool bias = Bias.Checked;
                    int RBFLayerIndex = 0;

                    //check learning algorithm
                    if(learningAlgorithm == 0) {  //MLP(BP)
                        mlpBackProbagation = new MLPBackProbagation(networkTotalLayerNumber, layerNueronsNumber, epochsNumber,
                            activationType, learningRate, MseThreshold, learningAlgorithm, bias);

                        //train network
                        mlpBackProbagation.train(FacialGestures.trainingFacialGestures);
                    } else if(learningAlgorithm == 1){
                        //RBF(LMS)
                        rbfLeastMeanSquare = new RBFLeastMeanSquare(networkTotalLayerNumber, layerNueronsNumber, epochsNumber,
                            activationType, learningRate, MseThreshold, learningAlgorithm, RBFLayerIndex, bias);

                        //train network
                        rbfLeastMeanSquare.train(FacialGestures.trainingFacialGestures);
                    }

                }
                
            }
        }

        private void Test_Click(object sender, EventArgs e) {
            //get learning algorithm
            int learningAlgorithm = -LearningAlgorithms.SelectedIndex;  //0 for MLP and 1 for RBF

            //test network and get accuracy
            if(learningAlgorithm == 0 && mlpBackProbagation != null)
                //test now
                Accuracy.Text = mlpBackProbagation.test(FacialGestures.testingFacialGestures).ToString();
            else if(learningAlgorithm == 1 && rbfLeastMeanSquare != null)
                Accuracy.Text = rbfLeastMeanSquare.test(FacialGestures.testingFacialGestures).ToString();
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
