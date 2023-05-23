namespace Multiple_Linear_Regression {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.WorkFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAllSteps = new System.Windows.Forms.ToolStripMenuItem();
            this.allTabs = new System.Windows.Forms.TabControl();
            this.loadDataTab = new System.Windows.Forms.TabPage();
            this.acceptFactorsButton = new System.Windows.Forms.Button();
            this.selectRegressorsButton = new System.Windows.Forms.Button();
            this.selectRegressantsButton = new System.Windows.Forms.Button();
            this.progressBarDataLoad = new System.Windows.Forms.ProgressBar();
            this.factorsData = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.WorkFileMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitAppMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAllStepsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.regressantsList = new System.Windows.Forms.ListBox();
            this.labelregressantsList = new System.Windows.Forms.Label();
            this.regressorsList = new System.Windows.Forms.ListBox();
            this.labelRegressorsList = new System.Windows.Forms.Label();
            this.clearSelectedFactorsButton = new System.Windows.Forms.Button();
            this.allTabs.SuspendLayout();
            this.loadDataTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.factorsData)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // WorkFileMenuItem
            // 
            this.WorkFileMenuItem.Name = "WorkFileMenuItem";
            this.WorkFileMenuItem.Size = new System.Drawing.Size(48, 20);
            this.WorkFileMenuItem.Text = "Файл";
            // 
            // OpenFile
            // 
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(180, 22);
            this.OpenFile.Text = "Открыть";
            // 
            // ExitApp
            // 
            this.ExitApp.Name = "ExitApp";
            this.ExitApp.Size = new System.Drawing.Size(54, 20);
            this.ExitApp.Text = "Выход";
            // 
            // helpAllSteps
            // 
            this.helpAllSteps.Name = "helpAllSteps";
            this.helpAllSteps.Size = new System.Drawing.Size(68, 20);
            this.helpAllSteps.Text = "Помощь";
            // 
            // allTabs
            // 
            this.allTabs.Controls.Add(this.loadDataTab);
            this.allTabs.Controls.Add(this.tabPage2);
            this.allTabs.Location = new System.Drawing.Point(236, 33);
            this.allTabs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.allTabs.Name = "allTabs";
            this.allTabs.SelectedIndex = 0;
            this.allTabs.Size = new System.Drawing.Size(1099, 512);
            this.allTabs.TabIndex = 3;
            // 
            // loadDataTab
            // 
            this.loadDataTab.Controls.Add(this.clearSelectedFactorsButton);
            this.loadDataTab.Controls.Add(this.acceptFactorsButton);
            this.loadDataTab.Controls.Add(this.selectRegressorsButton);
            this.loadDataTab.Controls.Add(this.selectRegressantsButton);
            this.loadDataTab.Controls.Add(this.progressBarDataLoad);
            this.loadDataTab.Controls.Add(this.factorsData);
            this.loadDataTab.Location = new System.Drawing.Point(4, 25);
            this.loadDataTab.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loadDataTab.Name = "loadDataTab";
            this.loadDataTab.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.loadDataTab.Size = new System.Drawing.Size(1091, 483);
            this.loadDataTab.TabIndex = 0;
            this.loadDataTab.Text = "Загрузка данных";
            this.loadDataTab.UseVisualStyleBackColor = true;
            // 
            // acceptFactorsButton
            // 
            this.acceptFactorsButton.Enabled = false;
            this.acceptFactorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptFactorsButton.Location = new System.Drawing.Point(904, 375);
            this.acceptFactorsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.acceptFactorsButton.Name = "acceptFactorsButton";
            this.acceptFactorsButton.Size = new System.Drawing.Size(147, 59);
            this.acceptFactorsButton.TabIndex = 13;
            this.acceptFactorsButton.Text = "Подтвердить";
            this.acceptFactorsButton.UseVisualStyleBackColor = true;
            this.acceptFactorsButton.Click += new System.EventHandler(this.acceptFactorsButton_Click);
            // 
            // selectRegressorsButton
            // 
            this.selectRegressorsButton.Enabled = false;
            this.selectRegressorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectRegressorsButton.Location = new System.Drawing.Point(884, 132);
            this.selectRegressorsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.selectRegressorsButton.Name = "selectRegressorsButton";
            this.selectRegressorsButton.Size = new System.Drawing.Size(184, 60);
            this.selectRegressorsButton.TabIndex = 12;
            this.selectRegressorsButton.Text = "Выбрать управляющие факторы";
            this.selectRegressorsButton.UseVisualStyleBackColor = true;
            this.selectRegressorsButton.Click += new System.EventHandler(this.selectRegressorsButton_Click);
            // 
            // selectRegressantsButton
            // 
            this.selectRegressantsButton.Enabled = false;
            this.selectRegressantsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectRegressantsButton.Location = new System.Drawing.Point(884, 43);
            this.selectRegressantsButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.selectRegressantsButton.Name = "selectRegressantsButton";
            this.selectRegressantsButton.Size = new System.Drawing.Size(184, 60);
            this.selectRegressantsButton.TabIndex = 11;
            this.selectRegressantsButton.Text = "Выбрать управляемые факторы";
            this.selectRegressantsButton.UseVisualStyleBackColor = true;
            this.selectRegressantsButton.Click += new System.EventHandler(this.selectRegressantsButton_Click);
            // 
            // progressBarDataLoad
            // 
            this.progressBarDataLoad.Location = new System.Drawing.Point(4, 455);
            this.progressBarDataLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progressBarDataLoad.Name = "progressBarDataLoad";
            this.progressBarDataLoad.Size = new System.Drawing.Size(843, 18);
            this.progressBarDataLoad.TabIndex = 10;
            this.progressBarDataLoad.Visible = false;
            // 
            // factorsData
            // 
            this.factorsData.AllowUserToAddRows = false;
            this.factorsData.AllowUserToDeleteRows = false;
            this.factorsData.AllowUserToResizeRows = false;
            this.factorsData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.factorsData.Location = new System.Drawing.Point(4, 4);
            this.factorsData.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.factorsData.Name = "factorsData";
            this.factorsData.ReadOnly = true;
            this.factorsData.RowHeadersWidth = 51;
            this.factorsData.Size = new System.Drawing.Size(843, 469);
            this.factorsData.TabIndex = 9;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(1091, 483);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WorkFileMenuItem2,
            this.ExitAppMenu,
            this.helpAllStepsMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(1339, 30);
            this.menuStrip.TabIndex = 4;
            this.menuStrip.Text = "menuStrip1";
            // 
            // WorkFileMenuItem2
            // 
            this.WorkFileMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFileMenu});
            this.WorkFileMenuItem2.Name = "WorkFileMenuItem2";
            this.WorkFileMenuItem2.Size = new System.Drawing.Size(59, 26);
            this.WorkFileMenuItem2.Text = "Файл";
            // 
            // OpenFileMenu
            // 
            this.OpenFileMenu.Name = "OpenFileMenu";
            this.OpenFileMenu.Size = new System.Drawing.Size(150, 26);
            this.OpenFileMenu.Text = "Открыть";
            this.OpenFileMenu.Click += new System.EventHandler(this.OpenFileMenu_Click);
            // 
            // ExitAppMenu
            // 
            this.ExitAppMenu.Name = "ExitAppMenu";
            this.ExitAppMenu.Size = new System.Drawing.Size(67, 26);
            this.ExitAppMenu.Text = "Выход";
            this.ExitAppMenu.Click += new System.EventHandler(this.ExitAppMenu_Click);
            // 
            // helpAllStepsMenu
            // 
            this.helpAllStepsMenu.Name = "helpAllStepsMenu";
            this.helpAllStepsMenu.Size = new System.Drawing.Size(83, 26);
            this.helpAllStepsMenu.Text = "Помощь";
            // 
            // regressantsList
            // 
            this.regressantsList.FormattingEnabled = true;
            this.regressantsList.ItemHeight = 16;
            this.regressantsList.Location = new System.Drawing.Point(16, 76);
            this.regressantsList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.regressantsList.Name = "regressantsList";
            this.regressantsList.Size = new System.Drawing.Size(211, 132);
            this.regressantsList.TabIndex = 5;
            // 
            // labelregressantsList
            // 
            this.labelregressantsList.AutoSize = true;
            this.labelregressantsList.Location = new System.Drawing.Point(16, 57);
            this.labelregressantsList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelregressantsList.Name = "labelregressantsList";
            this.labelregressantsList.Size = new System.Drawing.Size(161, 16);
            this.labelregressantsList.TabIndex = 6;
            this.labelregressantsList.Text = "Управляемые факторы:";
            // 
            // regressorsList
            // 
            this.regressorsList.FormattingEnabled = true;
            this.regressorsList.ItemHeight = 16;
            this.regressorsList.Location = new System.Drawing.Point(16, 256);
            this.regressorsList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.regressorsList.Name = "regressorsList";
            this.regressorsList.Size = new System.Drawing.Size(211, 276);
            this.regressorsList.TabIndex = 7;
            // 
            // labelRegressorsList
            // 
            this.labelRegressorsList.AutoSize = true;
            this.labelRegressorsList.Location = new System.Drawing.Point(16, 236);
            this.labelRegressorsList.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRegressorsList.Name = "labelRegressorsList";
            this.labelRegressorsList.Size = new System.Drawing.Size(162, 16);
            this.labelRegressorsList.TabIndex = 8;
            this.labelRegressorsList.Text = "Управляющие факторы:";
            // 
            // clearSelectedFactorsButton
            // 
            this.clearSelectedFactorsButton.Enabled = false;
            this.clearSelectedFactorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearSelectedFactorsButton.Location = new System.Drawing.Point(884, 250);
            this.clearSelectedFactorsButton.Margin = new System.Windows.Forms.Padding(4);
            this.clearSelectedFactorsButton.Name = "clearSelectedFactorsButton";
            this.clearSelectedFactorsButton.Size = new System.Drawing.Size(184, 35);
            this.clearSelectedFactorsButton.TabIndex = 14;
            this.clearSelectedFactorsButton.Text = "Очистить факторы";
            this.clearSelectedFactorsButton.UseVisualStyleBackColor = true;
            this.clearSelectedFactorsButton.Click += new System.EventHandler(this.clearSelectedFactorsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 546);
            this.Controls.Add(this.labelRegressorsList);
            this.Controls.Add(this.regressorsList);
            this.Controls.Add(this.labelregressantsList);
            this.Controls.Add(this.regressantsList);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.allTabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1354, 584);
            this.Name = "MainForm";
            this.Text = "Многомерная Линейная Регрессия";
            this.allTabs.ResumeLayout(false);
            this.loadDataTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.factorsData)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem WorkFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenFile;
        private System.Windows.Forms.ToolStripMenuItem ExitApp;
        private System.Windows.Forms.ToolStripMenuItem helpAllSteps;
        private System.Windows.Forms.TabControl allTabs;
        private System.Windows.Forms.TabPage loadDataTab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem WorkFileMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem OpenFileMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitAppMenu;
        private System.Windows.Forms.ToolStripMenuItem helpAllStepsMenu;
        private System.Windows.Forms.DataGridView factorsData;
        private System.Windows.Forms.ListBox regressantsList;
        private System.Windows.Forms.Label labelregressantsList;
        private System.Windows.Forms.ListBox regressorsList;
        private System.Windows.Forms.Label labelRegressorsList;
        private System.Windows.Forms.ProgressBar progressBarDataLoad;
        private System.Windows.Forms.Button acceptFactorsButton;
        private System.Windows.Forms.Button selectRegressorsButton;
        private System.Windows.Forms.Button selectRegressantsButton;
        private System.Windows.Forms.Button clearSelectedFactorsButton;
    }
}

