using System;
using System.Windows.Forms;

namespace Multiple_Linear_Regression.Forms {
    public partial class UserWarningForm : Form {
        public bool AcceptAction { get; private set; }

        public UserWarningForm(string message) {
            InitializeComponent();
            labelWarning.Anchor = AnchorStyles.Top;
            labelWarning.Margin = new Padding(20, 5, 5, 5);
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
