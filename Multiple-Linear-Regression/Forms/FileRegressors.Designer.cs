namespace Multiple_Linear_Regression.Forms {
    partial class FileRegressors {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileRegressors));
            this.regressorsFromFileDataGrid = new System.Windows.Forms.DataGridView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.workWithFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsDataFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitFormMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpImitationContorl = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.regressorsFromFileDataGrid)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // regressorsFromFileDataGrid
            // 
            this.regressorsFromFileDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regressorsFromFileDataGrid.Location = new System.Drawing.Point(12, 40);
            this.regressorsFromFileDataGrid.Name = "regressorsFromFileDataGrid";
            this.regressorsFromFileDataGrid.ReadOnly = true;
            this.regressorsFromFileDataGrid.RowHeadersWidth = 51;
            this.regressorsFromFileDataGrid.RowTemplate.Height = 24;
            this.regressorsFromFileDataGrid.Size = new System.Drawing.Size(1045, 412);
            this.regressorsFromFileDataGrid.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.workWithFileMenuItem,
            this.exitFormMenuItem,
            this.helpImitationContorl});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(1069, 28);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // workWithFileMenuItem
            // 
            this.workWithFileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsDataFileMenu});
            this.workWithFileMenuItem.Name = "workWithFileMenuItem";
            this.workWithFileMenuItem.Size = new System.Drawing.Size(59, 24);
            this.workWithFileMenuItem.Text = "Файл";
            // 
            // saveAsDataFileMenu
            // 
            this.saveAsDataFileMenu.Name = "saveAsDataFileMenu";
            this.saveAsDataFileMenu.Size = new System.Drawing.Size(201, 26);
            this.saveAsDataFileMenu.Text = "Сохранить как...";
            this.saveAsDataFileMenu.Click += new System.EventHandler(this.saveAsDataFileMenu_Click);
            // 
            // exitFormMenuItem
            // 
            this.exitFormMenuItem.Name = "exitFormMenuItem";
            this.exitFormMenuItem.Size = new System.Drawing.Size(67, 24);
            this.exitFormMenuItem.Text = "Выход";
            this.exitFormMenuItem.Click += new System.EventHandler(this.exitFormMenuItem_Click);
            // 
            // helpImitationContorl
            // 
            this.helpImitationContorl.Name = "helpImitationContorl";
            this.helpImitationContorl.Size = new System.Drawing.Size(83, 24);
            this.helpImitationContorl.Text = "Помощь";
            // 
            // FileRegressors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 464);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.regressorsFromFileDataGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileRegressors";
            this.Text = "Имитация управления";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileRegressors_FormClosing);
            this.Resize += new System.EventHandler(this.FileRegressors_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.regressorsFromFileDataGrid)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView regressorsFromFileDataGrid;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem workWithFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsDataFileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitFormMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpImitationContorl;
    }
}