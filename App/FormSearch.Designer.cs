namespace App
{
    partial class FormSearch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextSearch = new System.Windows.Forms.TextBox();
            this.SearchLabel = new System.Windows.Forms.Label();
            this.ResultBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.luceneCheck = new System.Windows.Forms.CheckBox();
            this.luceneButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextSearch
            // 
            this.TextSearch.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextSearch.Location = new System.Drawing.Point(162, 36);
            this.TextSearch.Name = "TextSearch";
            this.TextSearch.Size = new System.Drawing.Size(463, 26);
            this.TextSearch.TabIndex = 1;
            this.TextSearch.TextChanged += new System.EventHandler(this.TextSearch_TextChanged);
            // 
            // SearchLabel
            // 
            this.SearchLabel.AutoSize = true;
            this.SearchLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SearchLabel.Location = new System.Drawing.Point(37, 39);
            this.SearchLabel.Name = "SearchLabel";
            this.SearchLabel.Size = new System.Drawing.Size(107, 18);
            this.SearchLabel.TabIndex = 2;
            this.SearchLabel.Text = "Search Query:";
            // 
            // ResultBox
            // 
            this.ResultBox.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ResultBox.FormattingEnabled = true;
            this.ResultBox.HorizontalScrollbar = true;
            this.ResultBox.ItemHeight = 22;
            this.ResultBox.Location = new System.Drawing.Point(162, 144);
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.Size = new System.Drawing.Size(627, 268);
            this.ResultBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(89, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Result:";
            // 
            // SearchButton
            // 
            this.SearchButton.Enabled = false;
            this.SearchButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SearchButton.Location = new System.Drawing.Point(658, 32);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(131, 33);
            this.SearchButton.TabIndex = 5;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // luceneCheck
            // 
            this.luceneCheck.AutoSize = true;
            this.luceneCheck.Enabled = false;
            this.luceneCheck.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.luceneCheck.Location = new System.Drawing.Point(162, 90);
            this.luceneCheck.Name = "luceneCheck";
            this.luceneCheck.Size = new System.Drawing.Size(164, 22);
            this.luceneCheck.TabIndex = 6;
            this.luceneCheck.Text = "Use Lucene Search";
            this.luceneCheck.UseVisualStyleBackColor = true;
            // 
            // luceneButton
            // 
            this.luceneButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.luceneButton.Location = new System.Drawing.Point(417, 78);
            this.luceneButton.Name = "luceneButton";
            this.luceneButton.Size = new System.Drawing.Size(208, 45);
            this.luceneButton.TabIndex = 7;
            this.luceneButton.Text = "Upload from DB to Lucene";
            this.luceneButton.UseVisualStyleBackColor = true;
            this.luceneButton.Click += new System.EventHandler(this.luceneButton_Click);
            // 
            // FormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 454);
            this.Controls.Add(this.luceneButton);
            this.Controls.Add(this.luceneCheck);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ResultBox);
            this.Controls.Add(this.SearchLabel);
            this.Controls.Add(this.TextSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextSearch;
        private System.Windows.Forms.Label SearchLabel;
        private System.Windows.Forms.ListBox ResultBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.CheckBox luceneCheck;
        private System.Windows.Forms.Button luceneButton;
    }
}