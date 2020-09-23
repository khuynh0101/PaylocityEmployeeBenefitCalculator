using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Paylocity.Service.Models
{
    public class EmployeesDependents
    {
        [Display(Name ="Employee ID")]
        public int EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Dependents")]
        public List<Dependent> Dependents { get; set; }

        [Display(Name = "Employee Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalEmployeeCost { get; set; }

        [Display(Name = "Total Dependents Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalDependCost { get; set; }
    }
}
