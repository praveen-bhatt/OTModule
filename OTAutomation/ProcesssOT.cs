using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using System.Collections;
using System.IO;

namespace OTAutomation
{
    /// <summary>
    /// Represents filds and method of ProcessOT form. 
    /// </summary>
    public partial class ProcesssOT : Form
    {
        #region Private Fields
        Hashtable fileNames = null; //new List<string>();
        List<Employee> employees = null;
        Hashtable EmployeeCollection = null;
        DateTime period;
        decimal WeeklyWorkingHours = 0;
        //log4net.ILog log = log4net.LogManager.GetLogger(typeof(ProcesssOT));
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProcesssOT()
        {
            InitializeComponent();
            WeeklyWorkingHours = Convert.ToDecimal(ConfigurationManager.AppSettings["WeeklyWorkingHours"]);
            EmployeeCollection = new Hashtable();
            employees = new List<Employee>();
        }

        /// <summary>
        /// Page load event.
        /// </summary>
        /// <param name="sender">sender as object.</param>
        /// <param name="e">e as EventArgs</param>
        private void ProcesssOT_Load(object sender, EventArgs e)
        {
            //log.Info("Exception occured.");
            BindMonthNames();
            BindYearNames();
        }

        /// <summary>
        /// Import button click event.
        /// </summary>
        /// <param name="sender">sender as object.</param>
        /// <param name="e">e as EventArgs</param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            pnlFileUploader.Controls.Clear();
            lblFileList.Text = string.Empty;

            if (fileNames != null) { fileNames = null; }

            int _Months = Convert.ToInt32(cbMonth.SelectedValue);
            int _Year = Convert.ToInt32(cbYear.SelectedValue);

            //extract the month
            int daysInMonth = DateTime.DaysInMonth(_Year, _Months);
            DateTime firstOfMonth = new DateTime(_Year, _Months, 1);
            period = firstOfMonth;
            //days of week starts by default as Sunday = 0
            int firstDayOfMonth = (int)firstOfMonth.DayOfWeek;
            int weeksInMonth = (int)Math.Ceiling((firstDayOfMonth + daysInMonth) / 7.0);
            int _PreviousButtonWidth = 0;

            fileNames = new Hashtable();

            for (int i = 0; i < weeksInMonth; i++)
            {
                Button btnWeek = new Button();
                btnWeek.Tag = i;
                btnWeek.Name = "btnWeek" + (i + 1);
                btnWeek.Text = "Week" + (i + 1);
                //btnWeek.Anchor = AnchorStyles.None;
                btnWeek.Left = _PreviousButtonWidth + 20;

                btnWeek.Click += btn_Click;
                pnlFileUploader.Controls.Add(btnWeek);
                fileNames.Add(i, null);

                if (pnlFileUploader.Controls.Count > 0)
                {
                    _PreviousButtonWidth = pnlFileUploader.Controls[i].Left + pnlFileUploader.Controls[i].Width;
                }
            }
        }

        /// <summary>
        /// Import button click event.
        /// </summary>
        /// <param name="sender">sender as object.</param>
        /// <param name="e">e as EventArgs</param>
        private void btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = "*.xlsx";
            openFileDialog.Filter = "Excel Files (xlsx,xls)|*.xlsx;*.xls";
            lblFileList.Text = string.Empty;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string extension = Path.GetExtension(openFileDialog.FileName);
                if (!(extension.Contains(".xls"))) { return; }  // Implement: Message

                try
                {
                    DataTable dtTemp = ImportExcelFile.GetExcelData(openFileDialog.FileName);

                    frmModel _model = new frmModel();
                    _model.ModelData = dtTemp;
                    if (_model.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        List<Employee> _EmpExcelFile =  ImportExcelFile.ProcessExcelData(_model.ModelData);

                        if (EmployeeCollection.ContainsKey(((Button)sender).Tag))
                        {
                            EmployeeCollection[((Button)sender).Tag] = _EmpExcelFile;
                        }
                        else { EmployeeCollection.Add(((Button)sender).Tag, _EmpExcelFile); }


                        fileNames[((Button)sender).Tag] = openFileDialog.FileName;
                    }
                    _model.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            for (int i = 0; i < fileNames.Count; i++)
            { lblFileList.Text += "Week-" + (i + 1).ToString() + ":    " + fileNames[i] + Environment.NewLine; }
        }

        /// <summary>
        /// Export button click event.
        /// </summary>
        /// <param name="sender">sender as object.</param>
        /// <param name="e">e as EventArgs</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                if (fileNames.ContainsValue(null))
                {
                    MessageBox.Show("Please choose all the files corresponding to each week!");
                    return;
                }
                else
                {
                    foreach (List<Employee> employeeCollection in EmployeeCollection.Values)
                    {
                        foreach (Employee emp in employeeCollection)
                        {
                            employees.Add(emp);
                        }
                    }
                }

                var employeesTotal = employees.GroupBy(m => m.Id).Select(m => new Employee { Id = m.Key, TotalHours = m.Sum(c => c.TotalHours) }).ToList();

                ExporExcelFile.DisplayInExcel(employeesTotal, period);
            }
            catch (System.Runtime.InteropServices.COMException comex)
            {
                MessageBox.Show(comex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// Bind month names with Month combo box. 
        /// </summary>
        private void BindMonthNames()
        {
            var months = new Dictionary<int, string>();

            for (int i = 1; i <= DateTime.Now.Month; i++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i);
                months.Add(i, monthName);
            }

            cbMonth.DataSource = months.ToList();
            cbMonth.DisplayMember = "Value";
            cbMonth.ValueMember = "Key";
        }

        /// <summary>
        /// Bind year names with Year combo box.
        /// </summary>
        private void BindYearNames()
        {
            int currentYear = DateTime.Now.Year;
            int lastYear = currentYear - 5;
            int nextYear = currentYear + 5;

            List<int> years = new List<int>();

            for (int year = currentYear; year > currentYear - 5; year--)
            {
                years.Add(year);
            }

            cbYear.DataSource = years;
        }
    }
}
