namespace WCFReportingservice
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WCFEmployee")]
    public partial class WCFEmployee
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        public int EmployeeType { get; set; }

        public int? AnnualSalary { get; set; }

        public int? HourlyPay { get; set; }

        public int? HoursWorked { get; set; }
    }
}
