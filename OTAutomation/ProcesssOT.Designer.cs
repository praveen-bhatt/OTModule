namespace OTAutomation
{
    partial class ProcesssOT
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
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.pnlFileUploader = new System.Windows.Forms.Panel();
            this.btnImport = new System.Windows.Forms.Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFileList = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlWeekNumber = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtErrorMessage = new System.Windows.Forms.TextBox();
            this.gbErrorMessage = new System.Windows.Forms.GroupBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbErrorMessage.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbMonth
            // 
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(135, 16);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(116, 21);
            this.cbMonth.TabIndex = 0;
            // 
            // cbYear
            // 
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(299, 16);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(89, 21);
            this.cbYear.TabIndex = 1;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.Location = new System.Drawing.Point(81, 18);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(48, 17);
            this.lblMonth.TabIndex = 2;
            this.lblMonth.Text = "Month";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblYear.Location = new System.Drawing.Point(257, 16);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(36, 17);
            this.lblYear.TabIndex = 3;
            this.lblYear.Text = "Year";
            // 
            // pnlFileUploader
            // 
            this.pnlFileUploader.Location = new System.Drawing.Point(13, 142);
            this.pnlFileUploader.Name = "pnlFileUploader";
            this.pnlFileUploader.Size = new System.Drawing.Size(602, 29);
            this.pnlFileUploader.TabIndex = 4;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(522, 18);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "&Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblMonth);
            this.pnlHeader.Controls.Add(this.cbMonth);
            this.pnlHeader.Controls.Add(this.btnImport);
            this.pnlHeader.Controls.Add(this.lblYear);
            this.pnlHeader.Controls.Add(this.cbYear);
            this.pnlHeader.Controls.Add(this.groupBox2);
            this.pnlHeader.Location = new System.Drawing.Point(15, 80);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(600, 57);
            this.pnlHeader.TabIndex = 7;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(1, -1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(602, 127);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Import";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(521, 50);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 6;
            this.btnExport.Text = "&Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Your Selected Files:";
            // 
            // lblFileList
            // 
            this.lblFileList.AutoSize = true;
            this.lblFileList.Location = new System.Drawing.Point(7, 41);
            this.lblFileList.Name = "lblFileList";
            this.lblFileList.Size = new System.Drawing.Size(70, 13);
            this.lblFileList.TabIndex = 9;
            this.lblFileList.Text = "                     ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFileList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Location = new System.Drawing.Point(13, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 138);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export";
            // 
            // pnlWeekNumber
            // 
            this.pnlWeekNumber.Location = new System.Drawing.Point(13, 171);
            this.pnlWeekNumber.Name = "pnlWeekNumber";
            this.pnlWeekNumber.Size = new System.Drawing.Size(602, 29);
            this.pnlWeekNumber.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::OTAutomation.Properties.Resources.FedEx;
            this.pictureBox1.Location = new System.Drawing.Point(12, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(180, 51);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // txtErrorMessage
            // 
            this.txtErrorMessage.Location = new System.Drawing.Point(10, 15);
            this.txtErrorMessage.Multiline = true;
            this.txtErrorMessage.Name = "txtErrorMessage";
            this.txtErrorMessage.Size = new System.Drawing.Size(586, 55);
            this.txtErrorMessage.TabIndex = 11;
            // 
            // gbErrorMessage
            // 
            this.gbErrorMessage.Controls.Add(this.txtErrorMessage);
            this.gbErrorMessage.Location = new System.Drawing.Point(13, 343);
            this.gbErrorMessage.Name = "gbErrorMessage";
            this.gbErrorMessage.Size = new System.Drawing.Size(602, 74);
            this.gbErrorMessage.TabIndex = 12;
            this.gbErrorMessage.TabStop = false;
            this.gbErrorMessage.Text = "Error Message";
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(534, 9);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 10;
            this.btnSettings.Text = "&Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // ProcesssOT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 429);
            this.Controls.Add(this.gbErrorMessage);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.pnlWeekNumber);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pnlFileUploader);
            this.Name = "ProcesssOT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fedex Overtime Module";
            this.Load += new System.EventHandler(this.ProcesssOT_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbErrorMessage.ResumeLayout(false);
            this.gbErrorMessage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Panel pnlFileUploader;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFileList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel pnlWeekNumber;
        private System.Windows.Forms.TextBox txtErrorMessage;
        private System.Windows.Forms.GroupBox gbErrorMessage;
        private System.Windows.Forms.Button btnSettings;

    }
}