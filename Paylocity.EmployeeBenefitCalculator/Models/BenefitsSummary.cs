using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paylocity.EmployeeBenefitCalculator.Models
{
    public class BenefitsSummary
    {
        [Display(Name ="Employee Cost")]
        [DisplayFormat(DataFormatString ="{0:C}")]
        public decimal EmployeeCost { get; set; }

        [Display(Name = "Dependents Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal DependentsCost { get; set; }

        [Display(Name = "Total Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Total Salary")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalSalary { get; set; }
    }
}