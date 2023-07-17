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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.regressorsFromFileDataGrid)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // regressorsFromFileDataGrid
            // 
            this.regressorsFromFileDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regressorsFromFileDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regressorsFromFileDataGrid.Location = new System.Drawing.Point(2, 2);
            this.regressorsFromFileDataGrid.Margin = new System.Windows.Forms.Padding(2);
            this.regressorsFromFileDataGrid.Name = "regressorsFromFileDataGrid";
            this.regressorsFromFileDataGrid.ReadOnly = true;
            this.regressorsFromFileDataGrid.RowHeadersWidth = 51;
            this.regressorsFromFileDataGrid.RowTemplate.Height = 24;
            this.regressorsFromFileDataGrid.Size = new System.Drawing.Size(667, 307);
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
            this.menuStrip.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip.ShowItemToolTips = true;
            this.menuStrip.Size = new System.Drawing.Size(671, 24);
            this.menuStrip.TabIndex = 6;
            this.menuStrip.Text = "menuStrip1";
            // 
            // workWithFileMenuItem
            // 
            this.workWithFileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsDataFileMenu});
            this.workWithFileMenuItem.Name = "workWithFileMenuItem";
            this.workWithFileMenuItem.Size = new System.Drawing.Size(48, 20);
            this.workWithFileMenuItem.Text = "Файл";
            // 
            // saveAsDataFileMenu
            // 
            this.saveAsDataFileMenu.Name = "saveAsDataFileMenu";
            this.saveAsDataFileMenu.Size = new System.Drawing.Size(163, 22);
            this.saveAsDataFileMenu.Text = "Сохранить как...";
            this.saveAsDataFileMenu.Click += new System.EventHandler(this.saveAsDataFileMenu_Click);
            // 
            // exitFormMenuItem
            // 
            this.exitFormMenuItem.Name = "exitFormMenuItem";
            this.exitFormMenuItem.Size = new System.Drawing.Size(54, 20);
            this.exitFormMenuItem.Text = "Выход";
            this.exitFormMenuItem.Click += new System.EventHandler(this.exitFormMenuItem_Click);
            // 
            // helpImitationContorl
            // 
            this.helpImitationContorl.Name = "helpImitationContorl";
            this.helpImitationContorl.Size = new System.Drawing.Size(68, 20);
            this.helpImitationContorl.Text = "Помощь";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.regressorsFromFileDataGrid, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(671, 311);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // FileRegressors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 335);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FileRegressors";
            this.Text = "Имитация управления";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileRegressors_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.regressorsFromFileDataGrid)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}