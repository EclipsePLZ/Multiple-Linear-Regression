using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Linear_Regression {
    internal interface IDialogService {
        /// <summary>
        /// Show the message
        /// </summary>
        /// <param name="message">Message</param>
        void ShowMessage(string message);

        /// <summary>
        /// Path to file
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Open File Dialog
        /// </summary>
        /// <returns>Result of opening the file</returns>
        bool OpenFileDialog();

        /// <summary>
        /// Save File Dialog
        /// </summary>
        /// <returns>Result of saving the file</returns>
        bool SaveFileDialog();
    }
}
