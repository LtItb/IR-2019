namespace App
{
    partial class FormImport
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
            this.TextLink = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonImport = new System.Windows.Forms.Button();
            this.LabelSuccess = new System.Windows.Forms.Label();
            this.LabelError = new System.Windows.Forms.Label();
            this.LabelMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextLink
            // 
            this.TextLink.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TextLink.Location = new System.Drawing.Point(211, 88);
            this.TextLink.Name = "TextLink";
            this.TextLink.Size = new System.Drawing.Size(462, 26);
            this.TextLink.TabIndex = 0;
            this.TextLink.Text = "https://raw.githubusercontent.com/SergeyMirvoda/IR-2019/master/data/IMDB%20Movie%" +
    "20Titles.csv";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(85, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Link to CSV";
            // 
            // ButtonImport
            // 
            this.ButtonImport.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ButtonImport.Location = new System.Drawing.Point(291, 272);
            this.ButtonImport.Name = "ButtonImport";
            this.ButtonImport.Size = new System.Drawing.Size(250, 65);
            this.ButtonImport.TabIndex = 2;
            this.ButtonImport.Text = "Import";
            this.ButtonImport.UseVisualStyleBackColor = true;
            this.ButtonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // LabelSuccess
            // 
            this.LabelSuccess.AutoSize = true;
            this.LabelSuccess.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelSuccess.ForeColor = System.Drawing.Color.Maroon;
            this.LabelSuccess.Location = new System.Drawing.Point(347, 157);
            this.LabelSuccess.Name = "LabelSuccess";
            this.LabelSuccess.Size = new System.Drawing.Size(116, 29);
            this.LabelSuccess.TabIndex = 3;
            this.LabelSuccess.Text = "Success!";
            this.LabelSuccess.Visible = false;
            // 
            // LabelError
            // 
            this.LabelError.AutoSize = true;
            this.LabelError.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelError.ForeColor = System.Drawing.Color.Maroon;
            this.LabelError.Location = new System.Drawing.Point(373, 157);
            this.LabelError.Name = "LabelError";
            this.LabelError.Size = new System.Drawing.Size(71, 29);
            this.LabelError.TabIndex = 4;
            this.LabelError.Text = "Error";
            this.LabelError.Visible = false;
            // 
            // LabelMessage
            // 
            this.LabelMessage.AutoSize = true;
            this.LabelMessage.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabelMessage.ForeColor = System.Drawing.Color.Black;
            this.LabelMessage.Location = new System.Drawing.Point(85, 207);
            this.LabelMessage.Name = "LabelMessage";
            this.LabelMessage.Size = new System.Drawing.Size(81, 18);
            this.LabelMessage.TabIndex = 5;
            this.LabelMessage.Text = "Message: ";
            this.LabelMessage.Visible = false;
            // 
            // FormImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.LabelMessage);
            this.Controls.Add(this.LabelError);
            this.Controls.Add(this.LabelSuccess);
            this.Controls.Add(this.ButtonImport);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextLink);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextLink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonImport;
        private System.Windows.Forms.Label LabelSuccess;
        private System.Windows.Forms.Label LabelError;
        private System.Windows.Forms.Label LabelMessage;
    }
}