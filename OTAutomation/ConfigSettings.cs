using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OTAutomation
{
    public partial class ConfigSettings : Form
    {
        DataTable dt = null;
        ExeConfigurationFileMap configFileMap = null;

        public ConfigSettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void ConfigSettings_Load(object sender, EventArgs e)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string configFile = System.IO.Path.Combine(appPath, "OTAutomation.exe.config");
            configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            dt = new DataTable();
            dt.Columns.Add("ConfigKey");
            dt.Columns.Add("ConfigValue");

            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                DataRow dr = dt.NewRow();
                dr["ConfigKey"] = key;
                dr["ConfigValue"] = Convert.ToString(config.AppSettings.Settings[key].Value);
                dt.Rows.Add(dr);
            }

            dgvConfigList.DataSource = dt;
            dgvConfigList.AutoGenerateColumns = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    string key = Convert.ToString(item[0]);
                    configuration.AppSettings.Settings[key].Value = Convert.ToString(item[1]);
                }
            }

            configuration.Save();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
