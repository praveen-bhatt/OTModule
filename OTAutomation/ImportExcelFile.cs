using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;

namespace OTAutomation
{
    public class ImportExcelFile
    {
        private double OT10Hrs = 0;
        private double OT12Hrs = 0;

        public ImportExcelFile()
        {
            OT10Hrs = Convert.ToDouble(ConfigurationManager.AppSettings["10HrsOT"]);

            OT12Hrs = Convert.ToDouble(ConfigurationManager.AppSettings["12HrsOT"]);
        }

        public static DataTable FillExcelData(string destinationPath, DataTable _dtTemp)
        {
            string extension = Path.GetExtension(destinationPath);
            DataTable _dt = null;

            string conString = string.Empty;
            if (extension.Equals(".xlsx"))
            { conString = string.Format(ConfigurationManager.AppSettings["ConStrOleDb12"].ToString().Trim(), destinationPath); }
            else { conString = string.Format(ConfigurationManager.AppSettings["ConStrOleDb04"].ToString().Trim(), destinationPath); }

            OleDbConnection oleDbcon = null;
            try
            {
                using (oleDbcon = new OleDbConnection(conString))
                {
                    using (OleDbCommand ocmd = new OleDbCommand("select * from [Rpt$]", oleDbcon))
                    {
                        oleDbcon.Open();
                        using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(ocmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                oleDbDataAdapter.Fill(_dtTemp);
                                _dt = _dtTemp;
                            }
                        }
                    }
                }
            }
            catch { throw; }
            finally { if (oleDbcon != null) { if (oleDbcon.State == ConnectionState.Open) { oleDbcon.Close(); } } oleDbcon = null; }
            return _dt;
        }

        /// <summary>
        /// Process and parse the input excel file into DataTable. 
        /// </summary>
        /// <param name="destinationPath">Path of the file as string.</param>
        public static DataTable GetExcelData(string destinationPath)
        {
            string extension = Path.GetExtension(destinationPath);
            DataTable _dt = null;

            string conString = string.Empty;
            if (extension.Equals(".xlsx"))
            { conString = string.Format(ConfigurationManager.AppSettings["ConStrOleDb12"].ToString().Trim(), destinationPath); }
            else { conString = string.Format(ConfigurationManager.AppSettings["ConStrOleDb04"].ToString().Trim(), destinationPath); }

            OleDbConnection oleDbcon = null;
            try
            {
                using (oleDbcon = new OleDbConnection(conString))
                {
                    using (OleDbCommand ocmd = new OleDbCommand("select * from [Rpt$]", oleDbcon))
                    {
                        oleDbcon.Open();
                        using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(ocmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                oleDbDataAdapter.Fill(ds);
                                _dt = ds.Tables[0];
                            }
                        }
                    }
                }
            }
            catch { throw; }
            finally { if (oleDbcon != null) { if (oleDbcon.State == ConnectionState.Open) { oleDbcon.Close(); } } oleDbcon = null; }
            return _dt;
        }

        /// <summary>
        /// Parse the DataTable to get the employee data. 
        /// </summary>
        /// <param name="dt">dt as DataTable to parse.</param>
        /// <returns>Returns as list of Employee.</returns>
        public static ExcelData ProcessExcelData(DataTable dt)
        {
            ExcelData excelData = new ExcelData();
            excelData.Employees = new List<Employee>();
            Employee _Employee = null;

            if (dt.Rows.Count > 0)
            {
                excelData.Mgr = Convert.ToString(dt.Rows[0][0]);
                excelData.ReportDate = Convert.ToString(dt.Rows[1][0]);
                excelData.WeekFirstDay = Convert.ToDateTime(dt.Rows[4][7]);
                excelData.WeekSecondDay = Convert.ToDateTime(dt.Rows[4][9]);
                excelData.WeekThirdDay = Convert.ToDateTime(dt.Rows[4][11]);
                excelData.WeekForthDay = Convert.ToDateTime(dt.Rows[4][13]);
                excelData.WeekFifthDay = Convert.ToDateTime(dt.Rows[4][15]);
                excelData.WeekSixthDay = Convert.ToDateTime(dt.Rows[4][17]);
                excelData.WeekSeventhDay = Convert.ToDateTime(dt.Rows[4][19]);

                foreach (DataRow row in dt.Rows)
                {
                    if (dt.Rows.IndexOf(row) > 5)
                    {
                        _Employee = new Employee();

                        if (!row.IsNull(0))
                        {
                            _Employee.Id = Convert.ToString(row[0]);
                        }
                        else
                        {
                            break;
                        }

                        _Employee.WeekFirstDayHours = Convert.ToDouble(row[7]);
                        _Employee.WeekSecondDayHours = Convert.ToDouble(row[9]);
                        _Employee.WeekThirdDayHours = Convert.ToDouble(row[11]);
                        _Employee.WeekFourthDayHours = Convert.ToDouble(row[13]);
                        _Employee.WeekFifthDayHours = Convert.ToDouble(row[15]);
                        _Employee.WeekSixthDayHours = Convert.ToDouble(row[17]);
                        _Employee.WeekSeventhDayHours = Convert.ToDouble(row[19]);
                        _Employee.TotalHours = Convert.ToDouble(row[20]);
                        excelData.Employees.Add(_Employee);
                    }
                }
            }

            return excelData;
        }
    }
}
