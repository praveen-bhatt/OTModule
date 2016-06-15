using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OTAutomation
{
    public class Employee
    {
        /// <summary>
        /// Define Employee Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Define Period.
        /// </summary>
        public string Period { get; set; }

        /// <summary>
        /// Define hour of first day of week. 
        /// </summary>
        public double WeekFirstDayHours { get; set; }

        /// <summary>
        /// Define hour of second day of week. 
        /// </summary>
        public double WeekSecondDayHours { get; set; }

        /// <summary>
        /// Define hour of third day of week. 
        /// </summary>
        public double WeekThirdDayHours { get; set; }

        /// <summary>
        /// Define hour of fourth day of week. 
        /// </summary>
        public double WeekFourthDayHours { get; set; }

        /// <summary>
        /// Define hour of fifth day of week. 
        /// </summary>
        public double WeekFifthDayHours { get; set; }

        /// <summary>
        /// Define hour of sixth day of week. 
        /// </summary>
        public double WeekSixthDayHours { get; set; }

        /// <summary>
        /// Define hour of seventh day of week. 
        /// </summary>
        public double WeekSeventhDayHours { get; set; }

        /// <summary>
        /// Define total working hours of employee in month. 
        /// </summary>
        public double TotalHours { get; set; }

        /// <summary>
        /// Define total leave of employee.
        /// </summary>
        public double TotalLeave { get; set; }

        /// <summary>
        /// Define annual leave taken by employee.
        /// </summary>
        public double AnnualLeave { get; set; }

        /// <summary>
        /// Define sick leave taken by employee.
        /// </summary>
        public double SickLeave { get; set; }

        /// <summary>
        /// Define other leave taken by employee.
        /// </summary>
        public double OtherLeave { get; set; }

        /// <summary>
        /// Define unpaid leave taken by employee.
        /// </summary>
        public double UnpaidLeave { get; set; }

        /// <summary>
        /// Define total overtime hours of employee.
        /// </summary>
        public double TotalOverTimeHours { get; set; }

        /// <summary>
        /// Define total hours under category 0.5
        /// </summary>
        public double Ot1 { get; set; }

        /// <summary>
        /// Define total hours under category 1.25
        /// </summary>
        public double Ot2 { get; set; }

        /// <summary>
        /// Define total hours under category 1.5
        /// </summary>
        public double Ot3 { get; set; }

        /// <summary>
        /// Define total hours under category fixed OT.
        /// </summary>
        public double Ot4 { get; set; }

        /// <summary>
        /// Define Ot5.
        /// </summary>
        public double Ot5 { get; set; }

        /// <summary>
        /// Define Ot6
        /// </summary>
        public double Ot6 { get; set; }
    }

    /// <summary>
    /// Represents properties to define the date range.
    /// </summary>
    public class ReportDateRange
    {
        public int WeekStartDate { get; set; }

        public int WeekEndDate { get; set; }
    }
}