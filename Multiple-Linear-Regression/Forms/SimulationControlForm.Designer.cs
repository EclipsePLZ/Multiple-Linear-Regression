namespace Multiple_Linear_Regression.Forms {
    partial class SimulationControlForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationControlForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.workWithFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataFileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitFormMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpImitationContorl = new System.Windows.Forms.ToolStripMenuItem();
            this.regressorsSetDataGrid = new System.Windows.Forms.DataGridView();
            this.regressantsResultDataGrid = new System.Windows.Forms.DataGridView();
            this.labelRegressors = new System.Windows.Forms.Label();
            this.labelRegressants = new System.Windows.Forms.Label();
            this.checkMutualImpactFactors = new System.Windows.Forms.CheckBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regressorsSetDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regressantsResultDataGrid)).BeginInit();
            this.SuspendLayout();
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
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip1";
            // 
            // workWithFileMenuItem
            // 
            this.workWithFileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadDataFileMenu});
            this.workWithFileMenuItem.Name = "workWithFileMenuItem";
            this.workWithFileMenuItem.Size = new System.Drawing.Size(48, 20);
            this.workWithFileMenuItem.Text = "Файл";
            // 
            // loadDataFileMenu
            // 
            this.loadDataFileMenu.Name = "loadDataFileMenu";
            this.loadDataFileMenu.Size = new System.Drawing.Size(121, 22);
            this.loadDataFileMenu.Text = "Открыть";
            this.loadDataFileMenu.Click += new System.EventHandler(this.loadDataFileMenu_Click);
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
            // regressorsSetDataGrid
            // 
            this.regressorsSetDataGrid.AllowUserToAddRows = false;
            this.regressorsSetDataGrid.AllowUserToDeleteRows = false;
            this.regressorsSetDataGrid.AllowUserToResizeRows = false;
            this.regressorsSetDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regressorsSetDataGrid.Location = new System.Drawing.Point(12, 55);
            this.regressorsSetDataGrid.Name = "regressorsSetDataGrid";
            this.regressorsSetDataGrid.RowHeadersWidth = 51;
            this.regressorsSetDataGrid.Size = new System.Drawing.Size(403, 383);
            this.regressorsSetDataGrid.TabIndex = 13;
            this.regressorsSetDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.regressorsSetDataGrid_CellValueChanged);
            // 
            // regressantsResultDataGrid
            // 
            this.regressantsResultDataGrid.AllowUserToAddRows = false;
            this.regressantsResultDataGrid.AllowUserToDeleteRows = false;
            this.regressantsResultDataGrid.AllowUserToResizeRows = false;
            this.regressantsResultDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regressantsResultDataGrid.Location = new System.Drawing.Point(466, 55);
            this.regressantsResultDataGrid.Name = "regressantsResultDataGrid";
            this.regressantsResultDataGrid.ReadOnly = true;
            this.regressantsResultDataGrid.RowHeadersWidth = 51;
            this.regressantsResultDataGrid.Size = new System.Drawing.Size(322, 383);
            this.regressantsResultDataGrid.TabIndex = 14;
            // 
            // labelRegressors
            // 
            this.labelRegressors.AutoSize = true;
            this.labelRegressors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRegressors.Location = new System.Drawing.Point(10, 37);
            this.labelRegressors.Name = "labelRegressors";
            this.labelRegressors.Size = new System.Drawing.Size(194, 15);
            this.labelRegressors.TabIndex = 15;
            this.labelRegressors.Text = "Список управляющих факторов:";
            // 
            // labelRegressants
            // 
            this.labelRegressants.AutoSize = true;
            this.labelRegressants.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRegressants.Location = new System.Drawing.Point(464, 37);
            this.labelRegressants.Name = "labelRegressants";
            this.labelRegressants.Size = new System.Drawing.Size(194, 15);
            this.labelRegressants.TabIndex = 16;
            this.labelRegressants.Text = "Список управляемых факторов:";
            // 
            // checkMutualImpactFactors
            // 
            this.checkMutualImpactFactors.AutoSize = true;
            this.checkMutualImpactFactors.Location = new System.Drawing.Point(260, 25);
            this.checkMutualImpactFactors.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkMutualImpactFactors.Name = "checkMutualImpactFactors";
            this.checkMutualImpactFactors.Size = new System.Drawing.Size(164, 30);
            this.checkMutualImpactFactors.TabIndex = 17;
            this.checkMutualImpactFactors.Text = "Учитывать взаимовлияние\r\nуправляющих факторов";
            this.checkMutualImpactFactors.UseVisualStyleBackColor = true;
            // 
            // SimulationControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkMutualImpactFactors);
            this.Controls.Add(this.labelRegressants);
            this.Controls.Add(this.labelRegressors);
            this.Controls.Add(this.regressantsResultDataGrid);
            this.Controls.Add(this.regressorsSetDataGrid);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SimulationControlForm";
            this.Text = "Имитация управления";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimulationControlForm_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.SimulationControlForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.SimulationControlForm_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regressorsSetDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regressantsResultDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem workWithFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataFileMenu;
        private System.Windows.Forms.ToolStripMenuItem exitFormMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpImitationContorl;
        private System.Windows.Forms.DataGridView regressorsSetDataGrid;
        private System.Windows.Forms.DataGridView regressantsResultDataGrid;
        private System.Windows.Forms.Label labelRegressors;
        private System.Windows.Forms.Label labelRegressants;
        private System.Windows.Forms.CheckBox checkMutualImpactFactors;
    }
}