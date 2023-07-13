using System.Windows.Forms;

namespace Multiple_Linear_Regression.Work_WIth_Files {
    internal class DefaultDialogService : IDialogService {
        /// <summary>
        /// Path to file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Open File (excel, csv) dialog
        /// </summary>
        /// <returns>Result of opening the file</returns>
        public bool OpenFileDialog() {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()) {
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx|CSV files (*.csv)|*.csv";
                openFileDialog.Title = "Choose the file";
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    FilePath = openFileDialog.FileName;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Save File (excel, csv) dialog
        /// </summary>
        /// <returns>Result of saving the file</returns>
        public bool SaveFileDialog() {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()) {
                saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx|CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Save the file as";
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    FilePath = saveFileDialog.FileName;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Show the message
        /// </summary>
        /// <param name="message">Message</param>
        public void ShowMessage(string message) {
            MessageBox.Show(message);
        }
    }
}
