namespace OTAutomation
{
    partial class Main
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
            this.btnUploadFiles = new System.Windows.Forms.Button();
            this.dgvFilesName = new System.Windows.Forms.DataGridView();
            this.lblMessage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesName)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUploadFiles
            // 
            this.btnUploadFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUploadFiles.Location = new System.Drawing.Point(71, 42);
            this.btnUploadFiles.Name = "btnUploadFiles";
            this.btnUploadFiles.Size = new System.Drawing.Size(133, 27);
            this.btnUploadFiles.TabIndex = 0;
            this.btnUploadFiles.Text = "Upload Files";
            this.btnUploadFiles.UseVisualStyleBackColor = true;
            this.btnUploadFiles.Click += new System.EventHandler(this.btnUploadFiles_Click);
            // 
            // dgvFilesName
            // 
            this.dgvFilesName.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFilesName.Location = new System.Drawing.Point(71, 118);
            this.dgvFilesName.Name = "dgvFilesName";
            this.dgvFilesName.RowHeadersWidth = 32;
            this.dgvFilesName.Size = new System.Drawing.Size(287, 150);
            this.dgvFilesName.TabIndex = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(71, 90);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(146, 17);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "List of files to Process";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 280);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.dgvFilesName);
            this.Controls.Add(this.btnUploadFiles);
            this.Name = "Main";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFilesName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadFiles;
        private System.Windows.Forms.DataGridView dgvFilesName;
        private System.Windows.Forms.Label lblMessage;
    }
}

