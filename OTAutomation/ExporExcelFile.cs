using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Marshal = System.Runtime.InteropServices.Marshal;

namespace OTAutomation
{
    /// <summary>
    /// Represents method to export data into Excel File.
    /// </summary>
    public class ExporExcelFile
    {
        /// <summary>
        /// Used to create the Excel file and open the file.
        /// </summary>
        /// <param name="employees">employees a collection of employee.</param>
        public static void DisplayInExcel(List<Employee> employees, DateTime period)
        {
            Excel.Application excelApp = null;
            Excel._Worksheet workSheet = null;
            //Excel.Workbook workbook = null;

            try
            {
                excelApp = new Excel.Application();
                // Make the object visible.
                excelApp.Visible = true;

                // Create a new, empty workbook and add it to the collection returned 
                // by property Workbooks. The new workbook becomes the active workbook.
                // Add has an optional parameter for specifying a praticular template. 
                // Because no argument is sent in this example, Add creates a new workbook. 
                excelApp.Workbooks.Add();

                // This example uses a single workSheet. 
                workSheet = excelApp.ActiveSheet;

                // Establish column headings in cells A1 and B1.
                workSheet.Cells[1, "A"] = "EmCode";
                workSheet.Cells[1, "B"] = "Period";
                workSheet.Cells[1, "C"] = "Ot1";
                workSheet.Cells[1, "D"] = "Ot2";
                workSheet.Cells[1, "E"] = "Ot3";
                workSheet.Cells[1, "F"] = "Ot4";
                workSheet.Cells[1, "G"] = "Ot5";
                workSheet.Cells[1, "H"] = "Ot6";
                workSheet.Cells[1, "I"] = "Lv1";
                workSheet.Cells[1, "J"] = "Lv2";
                workSheet.Cells[1, "K"] = "Lv3";
                workSheet.Cells[1, "L"] = "Lv4";
                workSheet.Cells[1, "M"] = "Lv5";
                workSheet.Cells[1, "N"] = "Lv6";

                var row = 1;
                foreach (var employee in employees)
                {
                    row++;
                    workSheet.Cells[row, "A"] = employee.Id;
                    workSheet.Cells[row, "B"] = period;
                }

                var xlYourRange = workSheet.get_Range("B2").EntireColumn;
                xlYourRange.NumberFormat = "MMyyyy";

                var range = workSheet.get_Range("C1");
                range.AddComment("Hariharan Narayanan: Ot1");
            }
            catch
            {
                throw;
            }
            finally
            {
                Marshal.ReleaseComObject(workSheet);

                excelApp = null;
                workSheet = null;
                //workbook = null;
            }
        }

    }
}
