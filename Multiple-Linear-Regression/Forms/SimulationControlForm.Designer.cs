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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regressorsSetDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regressantsResultDataGrid)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.menuStrip.Size = new System.Drawing.Size(1011, 24);
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
            this.regressorsSetDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regressorsSetDataGrid.Location = new System.Drawing.Point(3, 43);
            this.regressorsSetDataGrid.Name = "regressorsSetDataGrid";
            this.regressorsSetDataGrid.RowHeadersWidth = 51;
            this.regressorsSetDataGrid.Size = new System.Drawing.Size(502, 473);
            this.regressorsSetDataGrid.TabIndex = 13;
            this.regressorsSetDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.regressorsSetDataGrid_CellValueChanged);
            // 
            // regressantsResultDataGrid
            // 
            this.regressantsResultDataGrid.AllowUserToAddRows = false;
            this.regressantsResultDataGrid.AllowUserToDeleteRows = false;
            this.regressantsResultDataGrid.AllowUserToResizeRows = false;
            this.regressantsResultDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.regressantsResultDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.regressantsResultDataGrid.Location = new System.Drawing.Point(3, 24);
            this.regressantsResultDataGrid.Name = "regressantsResultDataGrid";
            this.regressantsResultDataGrid.ReadOnly = true;
            this.regressantsResultDataGrid.RowHeadersWidth = 51;
            this.regressantsResultDataGrid.Size = new System.Drawing.Size(489, 492);
            this.regressantsResultDataGrid.TabIndex = 14;
            // 
            // labelRegressors
            // 
            this.labelRegressors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelRegressors.AutoSize = true;
            this.labelRegressors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRegressors.Location = new System.Drawing.Point(3, 16);
            this.labelRegressors.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.labelRegressors.Name = "labelRegressors";
            this.labelRegressors.Size = new System.Drawing.Size(194, 15);
            this.labelRegressors.TabIndex = 15;
            this.labelRegressors.Text = "Список управляющих факторов:";
            // 
            // labelRegressants
            // 
            this.labelRegressants.AutoSize = true;
            this.labelRegressants.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRegressants.Location = new System.Drawing.Point(3, 4);
            this.labelRegressants.Margin = new System.Windows.Forms.Padding(3, 4, 3, 2);
            this.labelRegressants.Name = "labelRegressants";
            this.labelRegressants.Size = new System.Drawing.Size(194, 15);
            this.labelRegressants.TabIndex = 16;
            this.labelRegressants.Text = "Список управляемых факторов:";
            // 
            // checkMutualImpactFactors
            // 
            this.checkMutualImpactFactors.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.checkMutualImpactFactors.AutoSize = true;
            this.checkMutualImpactFactors.Checked = true;
            this.checkMutualImpactFactors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkMutualImpactFactors.Location = new System.Drawing.Point(336, 2);
            this.checkMutualImpactFactors.Margin = new System.Windows.Forms.Padding(2);
            this.checkMutualImpactFactors.Name = "checkMutualImpactFactors";
            this.checkMutualImpactFactors.Size = new System.Drawing.Size(164, 30);
            this.checkMutualImpactFactors.TabIndex = 17;
            this.checkMutualImpactFactors.Text = "Учитывать взаимовлияние\r\nуправляющих факторов";
            this.checkMutualImpactFactors.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.regressorsSetDataGrid, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(508, 519);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.labelRegressors, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.checkMutualImpactFactors, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(502, 34);
            this.tableLayoutPanel2.TabIndex = 19;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.labelRegressants, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.regressantsResultDataGrid, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(495, 519);
            this.tableLayoutPanel3.TabIndex = 19;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(1011, 519);
            this.splitContainer1.SplitterDistance = 508;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 20;
            // 
            // SimulationControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 543);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SimulationControlForm";
            this.Text = "Имитация управления";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.regressorsSetDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regressantsResultDataGrid)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}