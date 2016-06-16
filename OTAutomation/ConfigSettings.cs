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
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            gbConfig.Controls.Clear();
            int keyNum = 1;

            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                Label lblConfig = new Label();
                lblConfig.Name = "lblConfig" + keyNum.ToString();
                lblConfig.Text = key;
                lblConfig.Left = 130;
                pnlConfig.Controls.Add(lblConfig);

                TextBox txtConfig = new TextBox();
                txtConfig.Name = "txtConfig" + keyNum.ToString();
                txtConfig.Text = Convert.ToString(config.AppSettings.Settings[key].Value);
                pnlConfig.Controls.Add(txtConfig);

                keyNum++;
            }
            //config.AppSettings.Settings["YourThing"].Value = "New Value";
            //config.Save(); 
        }

        private static void UpdateSetting(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
