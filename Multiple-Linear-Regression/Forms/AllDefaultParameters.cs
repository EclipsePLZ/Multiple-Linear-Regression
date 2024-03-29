﻿using System;
using System.Windows.Forms;

namespace Multiple_Linear_Regression.Forms {
    public partial class AllDefaultParameters : Form {

        public bool UsingDefaultParameters { get; private set; }
        public AllDefaultParameters() {
            InitializeComponent();
            this.CenterToScreen();
            UsingDefaultParameters = false;
        }

        private void acceptDefaultParametersButton_Click(object sender, EventArgs e) {
            SendAnswer(true);
        }

        private void cancelDefaultParametersButton_Click(object sender, EventArgs e) {
            SendAnswer(false);
        }

        private void SendAnswer(bool answer) {
            UsingDefaultParameters = answer;
            this.Close();
        }
    }
}
