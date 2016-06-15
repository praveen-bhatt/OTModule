using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.IO;
//using System.Threading.Tasks;
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

        // <summary>
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
    }
}
