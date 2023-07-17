using Multiple_Linear_Regression.Work_WIth_Files;
using Multiple_Linear_Regression.Work_WIth_Files.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Multiple_Linear_Regression.Forms {
    public partial class FileRegressors : Form {
        IFileService fileService;
        IDialogService dialogService;

        private List<List<string>> AllRows { get; }
        private List<string> RegressorsNames { get; }
        private List<Model> Models { get; }

        private bool isSaved = false;

        public FileRegressors(List<string> regressorsNames, List<Model> models, List<List<string>> allRows,
                              string formName, string infoForForm) {

            RegressorsNames = new List<string>(regressorsNames);
            Models = new List<Model>(models);
            AllRows = new List<List<string>>(allRows);

            fileService = new ExcelFileService();
            dialogService = new DefaultDialogService();

            InitializeComponent();
            this.CenterToScreen();

            this.Text = formName;

            StartSetRows();
            helpImitationContorl.ToolTipText = infoForForm;
            saveAsDataFileMenu.ToolTipText = StepsInfo.SaveImitationResults;
        }

        /// <summary>
        /// Set predicted values for regressors into the data grid
        /// </summary>
        private void StartSetRows() {
            // Create headers row
            List<string> headers = new List<string>(RegressorsNames);
            foreach (var model in Models) {
                headers.Add(model.RegressantName);
            }

            // Set header
            OperationsWithControls.SetDataGVColumnHeaders(headers, regressorsFromFileDataGrid, false);

            Dictionary<string, List<double>> allRegressors = new Dictionary<string, List<double>>();

            // Fill all regressors values from input data
            for (int col = 0; col < AllRows[0].Count; col++) {
                string regressorName = AllRows[0][col];
                allRegressors.Add(regressorName, new List<double>());

                for (int row = 1; row < AllRows.Count; row++) {
                    allRegressors[regressorName].Add(Convert.ToDouble(AllRows[row][col]));
                }
            }

            // Find predict for each row of regressors from file
            for (int row = 0; row < AllRows.Count - 1; row++) {
                List<string> nextRow = new List<string>();
                Dictionary<string, double> regressorsRowValues = new Dictionary<string, double>();

                foreach (var regressorName in RegressorsNames) {
                    double value = 0;

                    // If it's pairwise factor the multiply the factors
                    if (regressorName.Contains(" & ")) {
                        string[] pairwiseRegressors = regressorName.Split(new string[] { " & " }, StringSplitOptions.None);
                        double factor1 = allRegressors[pairwiseRegressors[0]][row];
                        double factor2 = allRegressors[pairwiseRegressors[1]][row];
                        value = factor1 * factor2;
                    }
                    else {
                        value = allRegressors[regressorName][row];
                    }

                    regressorsRowValues.Add(regressorName, value);
                }

                // Add regressors value to the next row
                foreach (var regressorValue in regressorsRowValues.Values) {
                    nextRow.Add(Math.Round(regressorValue, 2).ToString());
                }

                // For each model add regressant value to the next row
                foreach (var model in Models) {
                    nextRow.Add(Math.Round(OperationsWithModels.CalcModelValue(model, regressorsRowValues), 2).ToString());
                }

                regressorsFromFileDataGrid.Rows.Add(nextRow.ToArray());
            }
        }

        private void saveAsDataFileMenu_Click(object sender, EventArgs e) {
            try {
                if (dialogService.SaveFileDialog() == true) {
                    fileService.Save(dialogService.FilePath, AllRows);
                    isSaved = true;
                }
            }
            catch (Exception ex) {
                dialogService.ShowMessage(ex.Message);
            }
        }

        /// <summary>
        /// Before closing the form, check that the information has been saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileRegressors_FormClosing(object sender, FormClosingEventArgs e) {
            if (!isSaved) {
                UserWarningForm acceptForm = new UserWarningForm(StepsInfo.UserWarningFormClosingRegressorsFromFile);
                acceptForm.ShowDialog();

                if (!acceptForm.AcceptAction) {
                    e.Cancel = true;
                }
            }
        }

        private void exitFormMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
