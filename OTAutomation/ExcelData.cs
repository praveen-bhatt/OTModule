using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OTAutomation
{
    public class ExcelData
    {
        /// <summary>
        /// Define location.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Define Manager.
        /// </summary>
        public string Mgr { get; set; }

        /// <summary>
        /// Define report date.
        /// </summary>
        public string ReportDate { get; set; }

        /// <summary>
        /// Define week first day.
        /// </summary>
        public DateTime WeekFirstDay { get; set; }

        /// <summary>
        /// Define week second day.
        /// </summary>
        public DateTime WeekSecondDay { get; set; }

        /// <summary>
        /// Define week third day.
        /// </summary>
        public DateTime WeekThirdDay { get; set; }

        /// <summary>
        /// Define week forth day.
        /// </summary>
        public DateTime WeekForthDay { get; set; }

        /// <summary>
        /// Define week fifth day.
        /// </summary>
        public DateTime WeekFifthDay { get; set; }

        /// <summary>
        /// Define week sixth day.
        /// </summary>
        public DateTime WeekSixthDay { get; set; }

        /// <summary>
        /// Define week seventh day.
        /// </summary>
        public DateTime WeekSeventhDay { get; set; }


        /// <summary>
        /// Define list of employees.
        /// </summary>
        public List<Employee> Employees { get; set; }
    }
}
