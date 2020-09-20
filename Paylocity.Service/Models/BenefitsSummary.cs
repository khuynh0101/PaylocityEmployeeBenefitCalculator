using System.ComponentModel.DataAnnotations;

namespace Paylocity.Service.Models
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