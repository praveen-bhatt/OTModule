using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTAutomation
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnUploadFiles_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> fileList = null;
                fileList = openFileDialog.FileNames.ToList();

                dgvFilesName.DataSource = fileList.Select(x => new { FilesName = x }).ToList(); 
            }
        }
    }
}
