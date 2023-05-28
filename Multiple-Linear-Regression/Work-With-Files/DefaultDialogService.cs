using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using Microsoft.Win32;

namespace Multiple_Linear_Regression.Work_WIth_Files {
    internal class DefaultDialogService : IDialogService {
        /// <summary>
        /// Path to file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Open File (excel, csv) Dialog
        /// </summary>
        /// <returns></returns>
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
        /// Show the message
        /// </summary>
        /// <param name="message">Message</param>
        public void ShowMessage(string message) {
            MessageBox.Show(message);
        }
    }
}
