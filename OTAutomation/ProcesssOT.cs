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
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace OTAutomation
{
    /// <summary>
    /// Represents filds and method of ProcessOT form. 
    /// </summary>
    public partial class ProcesssOT : Form
    {
        #region Private Fields
        Hashtable fileNames = null;
        List<Employee> employees = null;
        Hashtable EmployeeCollection = null;
        DateTime period;
        double WeeklyWorkingHours = 0;
        Dictionary<int, ReportDateRange> weekDateRange = null;
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ProcesssOT()
        {
            InitializeComponent();
            WeeklyWorkingHours = Convert.ToDouble(ConfigurationManager.AppSettings["WeeklyWorkingHours"]);
            EmployeeCollection = new Hashtable();

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
            pnlWeekNumber.Controls.Clear();
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
            int firstDay = 0;
            int lastDay = 0;
            int weekNumber = 0;
            fileNames = new Hashtable();
            weekDateRange = new Dictionary<int, ReportDateRange>();

            for (int i = 0; i < weeksInMonth; i++)
            {
                Button btnWeek = new Button();
                btnWeek.Tag = i;
                btnWeek.Name = "btnWeek" + (i + 1);
                btnWeek.Text = "Week" + (i + 1);
                //btnWeek.Anchor = AnchorStyles.None;
                btnWeek.Left = _PreviousButtonWidth + 20;

                btnWeek.Click += btn_Click;
                weekNumber = i + 1;
                GetWeekFirstAndLast(firstOfMonth, weekNumber, out firstDay, out lastDay);

                //Add week date range like 1 - 7 etc.
                ReportDateRange dateRange = new ReportDateRange();
                dateRange.WeekStartDate = firstDay;
                dateRange.WeekEndDate = lastDay;
                weekDateRange.Add(i, dateRange);

                Label lblWeek = new Label();
                lblWeek.Name = "lblWeek" + (i + 1);
                lblWeek.Text = "Days: " + firstDay.ToString() + "-" + lastDay.ToString();
                lblWeek.Width = 72;
                lblWeek.Left = _PreviousButtonWidth + 28;
                pnlFileUploader.Controls.Add(btnWeek);
                pnlWeekNumber.Controls.Add(lblWeek);
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
                    bool correctDateFormat = Convert.ToBoolean(ConfigurationManager.AppSettings["CorrectDateFormat"]);
                    bool readFromExcel = Convert.ToBoolean(ConfigurationManager.AppSettings["ReadFromExcel"]);
                    //Correct the date data of excel file.
                    if (correctDateFormat)
                    {
                        dtTemp = DateCorrection(openFileDialog.FileName, dtTemp);
                    }

                    if (readFromExcel)
                    {
                        dtTemp = ReadFromExcel(openFileDialog.FileName, dtTemp);
                    }

                    frmModel _model = new frmModel();
                    _model.ModelData = dtTemp;

                    if (_model.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ExcelData _EmpExcelFile = ImportExcelFile.ProcessExcelData(_model.ModelData);

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

        private static DataTable DateCorrection(string _filename, DataTable _table)
        {

            DataTable dt = _table.Clone();

            foreach (DataColumn item in dt.Columns)
            {
                item.DataType = typeof(string);
            }


            for (int rows = 0; rows < _table.Rows.Count - 1; rows++)
            {
                DataRow dr = dt.NewRow();
                for (int cols = 0; cols < _table.Columns.Count - 1; cols++)
                {

                    dr[cols] = _table.Rows[rows][cols];

                }
                dt.Rows.Add(dr);
            }


            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel._Worksheet _sheet = null;
            try
            {
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Open(_filename);
                _sheet = (Excel._Worksheet)wb.ActiveSheet;

                //string data = string.Empty;
                int row = Convert.ToInt32(ConfigurationManager.AppSettings["ExcelDateRow"].ToString().Trim());
                int rowSetting = Convert.ToInt32(ConfigurationManager.AppSettings["ExcelDateRowAdjustment"].ToString().Trim());

                for (int col = 1; col <= 22; col++)
                {
                    //Debug.Assert(!(row == 4 && col == 7), "checked!!");
                    var cellValue = Convert.ToString((_sheet.Cells[row, col] as Excel.Range).Value);
                    dt.Rows[row - rowSetting][col - 1] = cellValue;

                    //if (cellValue.Trim().Length > 0) { data += "   " + cellValue; }
                }

                //MessageBox.Show(data);
                wb.Close();
                excelApp.Quit();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { excelApp = null; wb = null; _sheet = null; GC.Collect(); }

            return dt;
        }

        private static DataTable ReadFromExcel(string _filename, DataTable _table)
        {

            DataTable dt = _table.Clone();

            foreach (DataColumn item in dt.Columns)
            {
                item.DataType = typeof(string);
            }


            for (int rows = 0; rows < _table.Rows.Count - 1; rows++)
            {
                DataRow dr = dt.NewRow();
                for (int cols = 0; cols < _table.Columns.Count - 1; cols++)
                {

                    dr[cols] = _table.Rows[rows][cols];

                }
                dt.Rows.Add(dr);
            }


            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel._Worksheet _sheet = null;
            try
            {
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Open(_filename);
                _sheet = (Excel._Worksheet)wb.ActiveSheet;

                int rowSetting = Convert.ToInt32(ConfigurationManager.AppSettings["ExcelDateRowAdjustment"].ToString().Trim());

                for (int dtRow = 2; dtRow < dt.Rows.Count; dtRow++)
                {
                    for (int col = 1; col <= 22; col++)
                    {
                        //Debug.Assert(!(row == 4 && col == 7), "checked!!");
                        var cellValue = Convert.ToString((_sheet.Cells[dtRow, col] as Excel.Range).Value);
                        dt.Rows[dtRow - rowSetting][col - 1] = cellValue;

                        //if (cellValue.Trim().Length > 0) { data += "   " + cellValue; }
                    }
                }

                //MessageBox.Show(data);
                wb.Close();
                excelApp.Quit();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally { excelApp = null; wb = null; _sheet = null; GC.Collect(); }

            return dt;
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
                bool processWeeklyFile = Convert.ToBoolean(ConfigurationManager.AppSettings["ProcessWeeklyFile"]);

                if (!processWeeklyFile)
                {
                    if (fileNames.ContainsValue(null))
                    {
                        MessageBox.Show("Please choose all the files corresponding to each week!");
                        return;
                    }
                }

                employees = new List<Employee>();

                foreach (ExcelData employeeCollection in EmployeeCollection.Values)
                {
                    foreach (Employee emp in employeeCollection.Employees)
                    {
                        ExporExcelFile.SetEmployeeOTHours(emp, WeeklyWorkingHours, 0);
                        employees.Add(emp);
                    }
                }

                var employeesTotal = employees.GroupBy(m => m.Id).Select(m => new Employee { Id = m.Key, Ot1 = m.Sum(c => c.Ot1), Ot2 = m.Sum(c => c.Ot2), Ot3 = m.Sum(c => c.Ot3), TotalHours = m.Sum(c => c.TotalHours) }).ToList();

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


        private void GetWeekFirstAndLast(DateTime date, int weekNumber, out int firstDay, out int lastDay)
        {
            var firstDayOfMonth = (int)date.DayOfWeek;

            var weekLastDay = (7 * weekNumber) - firstDayOfMonth;
            var weekFirstDay = weekLastDay - 6;

            var lastDate = date.AddDays(weekLastDay);

            if (weekFirstDay <= 0)
                firstDay = 1;
            else
                firstDay = weekFirstDay;

            if (lastDate.Month > date.Month)
                lastDay = DateTime.DaysInMonth(date.Year, date.Month);
            else
                lastDay = weekLastDay;
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ConfigSettings configSettings = new ConfigSettings();

            if (configSettings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                configSettings.Close();
            }
        }

        static void ShowConfig()
        {
            // For read access you do not need to call OpenExeConfiguraton
            foreach (string key in ConfigurationManager.AppSettings)
            {
                string value = ConfigurationManager.AppSettings[key];
                Console.WriteLine("Key: {0}, Value: {1}", key, value);
            }
        }
    }
}
