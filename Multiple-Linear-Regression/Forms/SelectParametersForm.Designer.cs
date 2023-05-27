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
            this.SuspendLayout();
            // 
            // listAvailabelFactors
            // 
            this.listAvailabelFactors.FormattingEnabled = true;
            this.listAvailabelFactors.Location = new System.Drawing.Point(287, 41);
            this.listAvailabelFactors.Margin = new System.Windows.Forms.Padding(2);
            this.listAvailabelFactors.Name = "listAvailabelFactors";
            this.listAvailabelFactors.Size = new System.Drawing.Size(136, 238);
            this.listAvailabelFactors.TabIndex = 0;
            this.listAvailabelFactors.DoubleClick += new System.EventHandler(this.listAvailabelFactors_DoubleClick);
            // 
            // listSelectedFactors
            // 
            this.listSelectedFactors.FormattingEnabled = true;
            this.listSelectedFactors.Location = new System.Drawing.Point(32, 41);
            this.listSelectedFactors.Margin = new System.Windows.Forms.Padding(2);
            this.listSelectedFactors.Name = "listSelectedFactors";
            this.listSelectedFactors.Size = new System.Drawing.Size(136, 238);
            this.listSelectedFactors.TabIndex = 1;
            this.listSelectedFactors.DoubleClick += new System.EventHandler(this.listSelectedFactors_DoubleClick);
            // 
            // labelAvailableFactors
            // 
            this.labelAvailableFactors.AutoSize = true;
            this.labelAvailableFactors.Location = new System.Drawing.Point(284, 25);
            this.labelAvailableFactors.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAvailableFactors.Name = "labelAvailableFactors";
            this.labelAvailableFactors.Size = new System.Drawing.Size(129, 13);
            this.labelAvailableFactors.TabIndex = 2;
            this.labelAvailableFactors.Text = "Доступные показатели:";
            // 
            // labelSelectedFactors
            // 
            this.labelSelectedFactors.AutoSize = true;
            this.labelSelectedFactors.Location = new System.Drawing.Point(29, 25);
            this.labelSelectedFactors.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSelectedFactors.Name = "labelSelectedFactors";
            this.labelSelectedFactors.Size = new System.Drawing.Size(131, 13);
            this.labelSelectedFactors.TabIndex = 3;
            this.labelSelectedFactors.Text = "Выбранные показатели:";
            // 
            // allToAvailableList
            // 
            this.allToAvailableList.Location = new System.Drawing.Point(213, 169);
            this.allToAvailableList.Name = "allToAvailableList";
            this.allToAvailableList.Size = new System.Drawing.Size(29, 24);
            this.allToAvailableList.TabIndex = 11;
            this.allToAvailableList.Text = ">>";
            this.allToAvailableList.UseVisualStyleBackColor = true;
            this.allToAvailableList.Click += new System.EventHandler(this.allToAvailableList_Click);
            // 
            // allToSelectList
            // 
            this.allToSelectList.Location = new System.Drawing.Point(213, 139);
            this.allToSelectList.Name = "allToSelectList";
            this.allToSelectList.Size = new System.Drawing.Size(29, 24);
            this.allToSelectList.TabIndex = 10;
            this.allToSelectList.Text = "<<";
            this.allToSelectList.UseVisualStyleBackColor = true;
            this.allToSelectList.Click += new System.EventHandler(this.allToSelectList_Click);
            // 
            // toAvailableList
            // 
            this.toAvailableList.Location = new System.Drawing.Point(213, 85);
            this.toAvailableList.Name = "toAvailableList";
            this.toAvailableList.Size = new System.Drawing.Size(29, 24);
            this.toAvailableList.TabIndex = 9;
            this.toAvailableList.Text = ">";
            this.toAvailableList.UseVisualStyleBackColor = true;
            this.toAvailableList.Click += new System.EventHandler(this.toAvailableList_Click);
            // 
            // toSelectList
            // 
            this.toSelectList.Location = new System.Drawing.Point(213, 55);
            this.toSelectList.Name = "toSelectList";
            this.toSelectList.Size = new System.Drawing.Size(29, 24);
            this.toSelectList.TabIndex = 8;
            this.toSelectList.Text = "<";
            this.toSelectList.UseVisualStyleBackColor = true;
            this.toSelectList.Click += new System.EventHandler(this.toSelectList_Click);
            // 
            // acceptSelectedFactorsButton
            // 
            this.acceptSelectedFactorsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.acceptSelectedFactorsButton.Location = new System.Drawing.Point(180, 246);
            this.acceptSelectedFactorsButton.Margin = new System.Windows.Forms.Padding(2);
            this.acceptSelectedFactorsButton.Name = "acceptSelectedFactorsButton";
            this.acceptSelectedFactorsButton.Size = new System.Drawing.Size(94, 34);
            this.acceptSelectedFactorsButton.TabIndex = 12;
            this.acceptSelectedFactorsButton.Text = "Принять";
            this.acceptSelectedFactorsButton.UseVisualStyleBackColor = true;
            this.acceptSelectedFactorsButton.Click += new System.EventHandler(this.acceptSelectedFactorsButton_Click);
            // 
            // SelectParametersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 306);
            this.Controls.Add(this.acceptSelectedFactorsButton);
            this.Controls.Add(this.allToAvailableList);
            this.Controls.Add(this.allToSelectList);
            this.Controls.Add(this.toAvailableList);
            this.Controls.Add(this.toSelectList);
            this.Controls.Add(this.labelSelectedFactors);
            this.Controls.Add(this.labelAvailableFactors);
            this.Controls.Add(this.listSelectedFactors);
            this.Controls.Add(this.listAvailabelFactors);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(485, 345);
            this.Name = "SelectParametersForm";
            this.Text = "SelectParametersForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectParametersForm_FormClosing);
            this.Resize += new System.EventHandler(this.SelectParametersForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}