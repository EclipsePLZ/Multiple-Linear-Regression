namespace Multiple_Linear_Regression {
    partial class SelectParametersForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectParametersForm));
            this.listAvailabelFactors = new System.Windows.Forms.ListBox();
            this.listSelectedFactors = new System.Windows.Forms.ListBox();
            this.labelAvailableFactors = new System.Windows.Forms.Label();
            this.labelSelectedFactors = new System.Windows.Forms.Label();
            this.allToAvailableList = new System.Windows.Forms.Button();
            this.allToSelectList = new System.Windows.Forms.Button();
            this.toAvailableList = new System.Windows.Forms.Button();
            this.toSelectList = new System.Windows.Forms.Button();
            this.acceptSelectedFactorsButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listAvailabelFactors
            // 
            this.listAvailabelFactors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listAvailabelFactors.FormattingEnabled = true;
            this.listAvailabelFactors.HorizontalScrollbar = true;
            this.listAvailabelFactors.Location = new System.Drawing.Point(286, 23);
            this.listAvailabelFactors.Margin = new System.Windows.Forms.Padding(2);
            this.listAvailabelFactors.Name = "listAvailabelFactors";
            this.listAvailabelFactors.Size = new System.Drawing.Size(181, 281);
            this.listAvailabelFactors.TabIndex = 0;
            this.listAvailabelFactors.DoubleClick += new System.EventHandler(this.listAvailabelFactors_DoubleClick);
            // 
            // listSelectedFactors
            // 
            this.listSelectedFactors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listSelectedFactors.FormattingEnabled = true;
            this.listSelectedFactors.HorizontalScrollbar = true;
            this.listSelectedFactors.Location = new System.Drawing.Point(2, 23);
            this.listSelectedFactors.Margin = new System.Windows.Forms.Padding(2);
            this.listSelectedFactors.Name = "listSelectedFactors";
            this.listSelectedFactors.Size = new System.Drawing.Size(180, 281);
            this.listSelectedFactors.TabIndex = 1;
            this.listSelectedFactors.DoubleClick += new System.EventHandler(this.listSelectedFactors_DoubleClick);
            // 
            // labelAvailableFactors
            // 
            this.labelAvailableFactors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelAvailableFactors.AutoSize = true;
            this.labelAvailableFactors.Location = new System.Drawing.Point(286, 5);
            this.labelAvailableFactors.Margin = new System.Windows.Forms.Padding(2, 5, 2, 3);
            this.labelAvailableFactors.Name = "labelAvailableFactors";
            this.labelAvailableFactors.Size = new System.Drawing.Size(129, 13);
            this.labelAvailableFactors.TabIndex = 2;
            this.labelAvailableFactors.Text = "Доступные показатели:";
            // 
            // labelSelectedFactors
            // 
            this.labelSelectedFactors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectedFactors.AutoSize = true;
            this.labelSelectedFactors.Location = new System.Drawing.Point(2, 5);
            this.labelSelectedFactors.Margin = new System.Windows.Forms.Padding(2, 5, 2, 3);
            this.labelSelectedFactors.Name = "labelSelectedFactors";
            this.labelSelectedFactors.Size = new System.Drawing.Size(131, 13);
            this.labelSelectedFactors.TabIndex = 3;
            this.labelSelectedFactors.Text = "Выбранные показатели:";
            // 
            // allToAvailableList
            // 
            this.allToAvailableList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.allToAvailableList.Location = new System.Drawing.Point(32, 131);
            this.allToAvailableList.Name = "allToAvailableList";
            this.allToAvailableList.Size = new System.Drawing.Size(29, 24);
            this.allToAvailableList.TabIndex = 11;
            this.allToAvailableList.Text = ">>";
            this.allToAvailableList.UseVisualStyleBackColor = true;
            this.allToAvailableList.Click += new System.EventHandler(this.allToAvailableList_Click);
            // 
            // allToSelectList
            // 
            this.allToSelectList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.allToSelectList.Location = new System.Drawing.Point(32, 101);
            this.allToSelectList.Name = "allToSelectList";
            this.allToSelectList.Size = new System.Drawing.Size(29, 24);
            this.allToSelectList.TabIndex = 10;
            this.allToSelectList.Text = "<<";
            this.allToSelectList.UseVisualStyleBackColor = true;
            this.allToSelectList.Click += new System.EventHandler(this.allToSelectList_Click);
            // 
            // toAvailableList
            // 
            this.toAvailableList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.toAvailableList.Location = new System.Drawing.Point(32, 50);
            this.toAvailableList.Name = "toAvailableList";
            this.toAvailableList.Size = new System.Drawing.Size(29, 24);
            this.toAvailableList.TabIndex = 9;
            this.toAvailableList.Text = ">";
            this.toAvailableList.UseVisualStyleBackColor = true;
            this.toAvailableList.Click += new System.EventHandler(this.toAvailableList_Click);
            // 
            // toSelectList
            // 
            this.toSelectList.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.toSelectList.Location = new System.Drawing.Point(32, 20);
            this.toSelectList.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.toSelectList.Name = "toSelectList";
            this.toSelectList.Size = new System.Drawing.Size(29, 24);
            this.toSelectList.TabIndex = 8;
            this.toSelectList.Text = "<";
            this.toSelectList.UseVisualStyleBackColor = true;
            this.toSelectList.Click += new System.EventHandler(this.toSelectList_Click);
            // 
            // acceptSelectedFactorsButton
            // 
            this.acceptSelectedFactorsButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.acceptSelectedFactorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptSelectedFactorsButton.Location = new System.Drawing.Point(2, 220);
            this.acceptSelectedFactorsButton.Margin = new System.Windows.Forms.Padding(2);
            this.acceptSelectedFactorsButton.Name = "acceptSelectedFactorsButton";
            this.acceptSelectedFactorsButton.Size = new System.Drawing.Size(90, 34);
            this.acceptSelectedFactorsButton.TabIndex = 12;
            this.acceptSelectedFactorsButton.Text = "Принять";
            this.acceptSelectedFactorsButton.UseVisualStyleBackColor = true;
            this.acceptSelectedFactorsButton.Click += new System.EventHandler(this.acceptSelectedFactorsButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelSelectedFactors, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelAvailableFactors, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.listSelectedFactors, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listAvailabelFactors, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(469, 306);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.acceptSelectedFactorsButton, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.allToAvailableList, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.toSelectList, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.toAvailableList, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.allToSelectList, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(187, 24);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.95918F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.04082F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(94, 268);
            this.tableLayoutPanel2.TabIndex = 14;
            // 
            // SelectParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 306);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SelectParametersForm";
            this.Text = "SelectParametersForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listAvailabelFactors;
        private System.Windows.Forms.ListBox listSelectedFactors;
        private System.Windows.Forms.Label labelAvailableFactors;
        private System.Windows.Forms.Label labelSelectedFactors;
        private System.Windows.Forms.Button allToAvailableList;
        private System.Windows.Forms.Button allToSelectList;
        private System.Windows.Forms.Button toAvailableList;
        private System.Windows.Forms.Button toSelectList;
        private System.Windows.Forms.Button acceptSelectedFactorsButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
    }
}