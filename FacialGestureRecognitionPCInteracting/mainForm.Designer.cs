namespace FacialGestureRecognitionPCInteracting {
    partial class mainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HiddenNeurons = new System.Windows.Forms.TextBox();
            this.HiddenLayers = new System.Windows.Forms.TextBox();
            this.Accuracy = new System.Windows.Forms.TextBox();
            this.ConMatrix = new System.Windows.Forms.DataGridView();
            this.Bias = new System.Windows.Forms.CheckBox();
            this.LearningAlgorithms = new System.Windows.Forms.ComboBox();
            this.Test = new System.Windows.Forms.Button();
            this.Train = new System.Windows.Forms.Button();
            this.DataSet = new System.Windows.Forms.Button();
            this.ActivationGroup = new System.Windows.Forms.GroupBox();
            this.Signum = new System.Windows.Forms.RadioButton();
            this.HyperbolicTangentSigmoid = new System.Windows.Forms.RadioButton();
            this.Step = new System.Windows.Forms.RadioButton();
            this.Sigmoid = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.MSEThreshold = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LearningRate = new System.Windows.Forms.TextBox();
            this.EpochsNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ConMatrix)).BeginInit();
            this.ActivationGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 73;
            this.label2.Text = "HiddenNeurons";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(272, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 72;
            this.label1.Text = "HiddenLayers";
            // 
            // HiddenNeurons
            // 
            this.HiddenNeurons.Location = new System.Drawing.Point(362, 220);
            this.HiddenNeurons.Name = "HiddenNeurons";
            this.HiddenNeurons.Size = new System.Drawing.Size(100, 20);
            this.HiddenNeurons.TabIndex = 71;
            // 
            // HiddenLayers
            // 
            this.HiddenLayers.Location = new System.Drawing.Point(362, 193);
            this.HiddenLayers.Name = "HiddenLayers";
            this.HiddenLayers.Size = new System.Drawing.Size(100, 20);
            this.HiddenLayers.TabIndex = 70;
            // 
            // Accuracy
            // 
            this.Accuracy.Location = new System.Drawing.Point(12, 488);
            this.Accuracy.Name = "Accuracy";
            this.Accuracy.Size = new System.Drawing.Size(100, 20);
            this.Accuracy.TabIndex = 69;
            // 
            // ConMatrix
            // 
            this.ConMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ConMatrix.Location = new System.Drawing.Point(12, 264);
            this.ConMatrix.Name = "ConMatrix";
            this.ConMatrix.Size = new System.Drawing.Size(450, 218);
            this.ConMatrix.TabIndex = 68;
            // 
            // Bias
            // 
            this.Bias.AutoSize = true;
            this.Bias.Location = new System.Drawing.Point(560, 28);
            this.Bias.Name = "Bias";
            this.Bias.Size = new System.Drawing.Size(46, 17);
            this.Bias.TabIndex = 66;
            this.Bias.Text = "Bias";
            this.Bias.UseVisualStyleBackColor = true;
            // 
            // LearningAlgorithms
            // 
            this.LearningAlgorithms.FormattingEnabled = true;
            this.LearningAlgorithms.Items.AddRange(new object[] {
            "Multi Layer Perceptron (BackProbagation)",
            "Radial Baisis Functions (Least Mean Square)"});
            this.LearningAlgorithms.Location = new System.Drawing.Point(229, 94);
            this.LearningAlgorithms.Name = "LearningAlgorithms";
            this.LearningAlgorithms.Size = new System.Drawing.Size(312, 21);
            this.LearningAlgorithms.TabIndex = 63;
            this.LearningAlgorithms.Text = "Learning Algorithm";
            // 
            // Test
            // 
            this.Test.Location = new System.Drawing.Point(18, 219);
            this.Test.Name = "Test";
            this.Test.Size = new System.Drawing.Size(75, 23);
            this.Test.TabIndex = 62;
            this.Test.Text = "Test";
            this.Test.UseVisualStyleBackColor = true;
            this.Test.Click += new System.EventHandler(this.Test_Click);
            // 
            // Train
            // 
            this.Train.Location = new System.Drawing.Point(18, 177);
            this.Train.Name = "Train";
            this.Train.Size = new System.Drawing.Size(75, 23);
            this.Train.TabIndex = 61;
            this.Train.Text = "Train";
            this.Train.UseVisualStyleBackColor = true;
            this.Train.Click += new System.EventHandler(this.train_Click);
            // 
            // DataSet
            // 
            this.DataSet.Location = new System.Drawing.Point(137, 27);
            this.DataSet.Name = "DataSet";
            this.DataSet.Size = new System.Drawing.Size(75, 23);
            this.DataSet.TabIndex = 60;
            this.DataSet.Text = "Data Set";
            this.DataSet.UseVisualStyleBackColor = true;
            this.DataSet.Click += new System.EventHandler(this.dataSet_Click);
            // 
            // ActivationGroup
            // 
            this.ActivationGroup.Controls.Add(this.Signum);
            this.ActivationGroup.Controls.Add(this.HyperbolicTangentSigmoid);
            this.ActivationGroup.Controls.Add(this.Step);
            this.ActivationGroup.Controls.Add(this.Sigmoid);
            this.ActivationGroup.Location = new System.Drawing.Point(12, 12);
            this.ActivationGroup.Name = "ActivationGroup";
            this.ActivationGroup.Size = new System.Drawing.Size(105, 115);
            this.ActivationGroup.TabIndex = 59;
            this.ActivationGroup.TabStop = false;
            this.ActivationGroup.Text = "Activation Group";
            // 
            // Signum
            // 
            this.Signum.AutoSize = true;
            this.Signum.Enabled = false;
            this.Signum.Location = new System.Drawing.Point(6, 42);
            this.Signum.Name = "Signum";
            this.Signum.Size = new System.Drawing.Size(68, 17);
            this.Signum.TabIndex = 3;
            this.Signum.Text = "SIGNUM";
            this.Signum.UseVisualStyleBackColor = true;
            // 
            // HyperbolicTangentSigmoid
            // 
            this.HyperbolicTangentSigmoid.AutoSize = true;
            this.HyperbolicTangentSigmoid.Location = new System.Drawing.Point(6, 88);
            this.HyperbolicTangentSigmoid.Name = "HyperbolicTangentSigmoid";
            this.HyperbolicTangentSigmoid.Size = new System.Drawing.Size(93, 17);
            this.HyperbolicTangentSigmoid.TabIndex = 5;
            this.HyperbolicTangentSigmoid.TabStop = true;
            this.HyperbolicTangentSigmoid.Text = "HYPERBOLIC";
            this.HyperbolicTangentSigmoid.UseVisualStyleBackColor = true;
            // 
            // Step
            // 
            this.Step.AutoSize = true;
            this.Step.Enabled = false;
            this.Step.Location = new System.Drawing.Point(6, 19);
            this.Step.Name = "Step";
            this.Step.Size = new System.Drawing.Size(53, 17);
            this.Step.TabIndex = 1;
            this.Step.TabStop = true;
            this.Step.Text = "STEP";
            this.Step.UseVisualStyleBackColor = true;
            // 
            // Sigmoid
            // 
            this.Sigmoid.AutoSize = true;
            this.Sigmoid.Location = new System.Drawing.Point(6, 65);
            this.Sigmoid.Name = "Sigmoid";
            this.Sigmoid.Size = new System.Drawing.Size(71, 17);
            this.Sigmoid.TabIndex = 4;
            this.Sigmoid.TabStop = true;
            this.Sigmoid.Text = "SIGMOID";
            this.Sigmoid.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(438, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 13);
            this.label5.TabIndex = 79;
            this.label5.Text = "MeanSquareError";
            // 
            // MSEThreshold
            // 
            this.MSEThreshold.Location = new System.Drawing.Point(441, 29);
            this.MSEThreshold.Name = "MSEThreshold";
            this.MSEThreshold.Size = new System.Drawing.Size(100, 20);
            this.MSEThreshold.TabIndex = 78;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "LearningRate";
            // 
            // LearningRate
            // 
            this.LearningRate.Location = new System.Drawing.Point(335, 29);
            this.LearningRate.Name = "LearningRate";
            this.LearningRate.Size = new System.Drawing.Size(100, 20);
            this.LearningRate.TabIndex = 75;
            // 
            // EpochsNumber
            // 
            this.EpochsNumber.Location = new System.Drawing.Point(229, 29);
            this.EpochsNumber.Name = "EpochsNumber";
            this.EpochsNumber.Size = new System.Drawing.Size(100, 20);
            this.EpochsNumber.TabIndex = 74;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 76;
            this.label3.Text = "epochs";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(119, 494);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 80;
            this.label6.Text = "Accuracy";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 520);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MSEThreshold);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.LearningRate);
            this.Controls.Add(this.EpochsNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HiddenNeurons);
            this.Controls.Add(this.HiddenLayers);
            this.Controls.Add(this.Accuracy);
            this.Controls.Add(this.ConMatrix);
            this.Controls.Add(this.Bias);
            this.Controls.Add(this.LearningAlgorithms);
            this.Controls.Add(this.Test);
            this.Controls.Add(this.Train);
            this.Controls.Add(this.DataSet);
            this.Controls.Add(this.ActivationGroup);
            this.Name = "mainForm";
            this.Text = "mainForm";
            ((System.ComponentModel.ISupportInitialize)(this.ConMatrix)).EndInit();
            this.ActivationGroup.ResumeLayout(false);
            this.ActivationGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox HiddenNeurons;
        private System.Windows.Forms.TextBox HiddenLayers;
        private System.Windows.Forms.TextBox Accuracy;
        private System.Windows.Forms.DataGridView ConMatrix;
        private System.Windows.Forms.CheckBox Bias;
        private System.Windows.Forms.ComboBox LearningAlgorithms;
        private System.Windows.Forms.Button Test;
        private System.Windows.Forms.Button Train;
        private System.Windows.Forms.Button DataSet;
        private System.Windows.Forms.GroupBox ActivationGroup;
        private System.Windows.Forms.RadioButton Signum;
        private System.Windows.Forms.RadioButton HyperbolicTangentSigmoid;
        private System.Windows.Forms.RadioButton Step;
        private System.Windows.Forms.RadioButton Sigmoid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox MSEThreshold;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox LearningRate;
        private System.Windows.Forms.TextBox EpochsNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;

    }
}

