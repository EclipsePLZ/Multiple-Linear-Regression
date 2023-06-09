﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiple_Linear_Regression.Forms {
    public partial class UserWarningForm : Form {
        public bool AcceptAction { get; private set; }

        public UserWarningForm(string message) {
            InitializeComponent();
            labelWarning.Text = message;
            this.CenterToScreen();
        }

        private void AcceptActionButton_Click(object sender, EventArgs e) {
            SendAnswer(true);
        }

        private void CancelActionButton_Click(object sender, EventArgs e) {
            SendAnswer(false);
        }

        private void SendAnswer(bool isAccept) {
            AcceptAction = isAccept;
            this.Close();
        }
    }
}
