using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace OTAutomation
{
    public partial class frmModel : Form
    {
        public DataTable ModelData
        {
            get;
            set;
        }

        public frmModel()
        {
            InitializeComponent();
        }

        private void frmModel_Load(object sender, EventArgs e)
        {
            try
            {
                if (ModelData != null)
                {
                    grdOvertimeData.DataSource = ModelData;
                    grdOvertimeData.AutoGenerateColumns = true;

                    if (Convert.ToBoolean(ConfigurationManager.AppSettings["AutoScreenAdjustment"]))
                    {
                        this.Width = (Screen.PrimaryScreen.WorkingArea.Size.Width * Convert.ToInt32(ConfigurationManager.AppSettings["ScreenHeightAdjustment"])) / 100;
                        this.Height = (Screen.PrimaryScreen.WorkingArea.Size.Width * Convert.ToInt32(ConfigurationManager.AppSettings["ScreenWidthAdjustment"])) / 100;
                    }
                }
            }
            catch 
            {
                throw;
            }
           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Hide();
        }
    }
}
