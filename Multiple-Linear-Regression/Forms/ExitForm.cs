using System;
using System.Windows.Forms;

namespace Multiple_Linear_Regression {
    public partial class ExitForm : Form {
        public ExitForm() {
            InitializeComponent();
            this.CenterToParent();
        }

        private void AcceptExitButton_Click(object sender, EventArgs e) {
            System.Windows.Forms.Application.Exit();
        }

        private void CancelExitButton_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
