using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paylocity.Service.Models
{
    public class Employee
    {
        [Display(Name="Employee Name")]
        [Required(ErrorMessage ="Employee Name is required.")]
        public string Name { get; set; }

        [Display(Name="Dependents")]
        public List<Dependent> Dependents { get; set; } = new List<Dependent>();

        public BenefitsSummary BenefitsSummary { get; set; }
    }
}