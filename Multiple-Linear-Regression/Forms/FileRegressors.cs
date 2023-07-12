using Multiple_Linear_Regression.Work_WIth_Files;
using Multiple_Linear_Regression.Work_WIth_Files.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Multiple_Linear_Regression.Forms {
    public partial class FileRegressors : Form {
        IFileService fileService;
        IDialogService dialogService;

        private List<List<string>> AllRows { get; set; }

        private BackgroundWorker resizeWorker = new BackgroundWorker();
        private OperationsWithControls operationsWithControls = new OperationsWithControls();

        private bool isResizeNeeded = false;

        private bool isSaved = false;

        private const int SIZE_DIFF_GV_FORM_WIDTH = 42;

        private const int SIZE_DIFF_GV_FORM_HEIGHT = 99;

        public FileRegressors(List<List<string>> rows, string formName, string infoForForm) {
            AllRows = new List<List<string>>(rows);

            fileService = new ExcelFileService();
            dialogService = new DefaultDialogService();

            InitializeComponent();
            this.CenterToScreen();

            this.Text = formName;

            StartSetRows();
            helpImitationContorl.ToolTipText = infoForForm;
            saveAsDataFileMenu.ToolTipText = StepsInfo.SaveImitationResults;

            // Run background worker for resizing components on form
            resizeWorker.DoWork += new DoWorkEventHandler(DoResizeComponents);
            resizeWorker.WorkerSupportsCancellation = true;
            resizeWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Set rows into data grid
        /// </summary>
        private void StartSetRows() {
            // Set header
            operationsWithControls.SetDataGVColumnHeaders(AllRows[0], regressorsFromFileDataGrid, false);

            // Set all other rows
            for (int i = 1; i < AllRows.Count; i++) { 
                regressorsFromFileDataGrid.Rows.Add(AllRows[i].ToArray());
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
            if (isSaved) {
                resizeWorker.CancelAsync();
            }
            else {
                UserWarningForm acceptForm = new UserWarningForm(StepsInfo.UserWarningFormClosingRegressorsFromFile);
                acceptForm.ShowDialog();

                if (acceptForm.AcceptAction) {
                    resizeWorker.CancelAsync();
                }
                else {
                    e.Cancel = true;
                }
            }
        }

        private void FileRegressors_Resize(object sender, EventArgs e) {
            isResizeNeeded = true;
        }

        private void exitFormMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Resize all form components
        /// </summary>
        private void DoResizeComponents(object sender, DoWorkEventArgs e) {
            // Check if resizeWorker has been stopped
            if (resizeWorker.CancellationPending) {
                e.Cancel = true;
            }
            else {
                while (true) {
                    System.Threading.Thread.Sleep(50);
                    if (isResizeNeeded) {
                        int formWidth = this.Width;
                        int formHeight = this.Height;

                        regressorsFromFileDataGrid.Invoke(new Action<Size>((size) => regressorsFromFileDataGrid.Size = size),
                            new Size(formWidth - SIZE_DIFF_GV_FORM_WIDTH, formHeight - SIZE_DIFF_GV_FORM_HEIGHT));


                        isResizeNeeded = false;
                    }
                }
            }
        }
    }
}
