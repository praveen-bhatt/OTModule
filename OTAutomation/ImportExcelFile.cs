using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;

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

        /// <summary>
        /// Process and parse the input excel file. 
        /// </summary>
        /// <param name="destinationPath">Path of the file as string.</param>
        public static void ImportExcelData(List<Employee> employees, string destinationPath)
        {
            string extension = Path.GetExtension(destinationPath);
            string conString = string.Empty;
            //string destinationPath = "D:\\ExportNew\\Input Excel Report.xlsx";//Server.MapPath("~//Export//" + filePath);
            if (extension.Equals(".xlsx"))
            {
                conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            else
            {
                conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destinationPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }

            OleDbConnection oleDbcon = null;

            // connectionstring to connect to the Excel Sheet
            try
            {
                //After connecting to the Excel sheet here we are selecting the data 
                //using select statement from the Excel sheet
                using (oleDbcon = new OleDbConnection(conString))
                {
                    using (OleDbCommand ocmd = new OleDbCommand("select * from [Rpt$]", oleDbcon))
                    {
                        oleDbcon.Open();  //Here [Sheet1$] is the name of the sheet in the Excel file where the data is present
                        using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(ocmd))
                        {
                            using (DataSet ds = new DataSet())
                            {
                                oleDbDataAdapter.Fill(ds);

                                foreach (DataRow row in ds.Tables[0].Rows)
                                {
                                    if (ds.Tables[0].Rows.IndexOf(row) > 5)
                                    {
                                        Employee employee = new Employee();

                                        if (!row.IsNull(0))
                                        {
                                            employee.Id = Convert.ToString(row[0]);
                                        }
                                        else
                                        {
                                            break;
                                        }

                                        employee.TotalHours = Convert.ToDouble(row[20]);
                                        employees.Add(employee);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (oleDbcon != null) { if (oleDbcon.State == ConnectionState.Open) { oleDbcon.Close(); } }
                oleDbcon = null;
            }
        }

        public static DataTable GetExcelData(string destinationPath)
        {
            string extension = Path.GetExtension(destinationPath);
            DataTable _dt = null;

            string conString = string.Empty;
            if (extension.Equals(".xlsx"))
            { conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + destinationPath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\""; }
            else { conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + destinationPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\""; }

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

        public static List<Employee> ProcessExcelData(DataTable dt)
        {
            List<Employee> employees = new List<Employee>();
            Employee _Employee = null;

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

                    _Employee.TotalHours = Convert.ToDouble(row[20]);
                    employees.Add(_Employee);
                }
            }
            return employees;
        }

        private void SetEmployeeOTHours(ref Employee employee, double weeklyWorkingHours, double totalLeaves)
        {
            employee.TotalOverTimeHours = employee.TotalHours - weeklyWorkingHours;

            if (employee.TotalOverTimeHours > OT10Hrs + OT12Hrs)
            { 

            }
        }
    }
}
