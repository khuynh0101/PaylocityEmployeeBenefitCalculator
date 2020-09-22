using Paylocity.Common;
using Paylocity.Data.Repositories;
using ViewModel = Paylocity.Service.Models;
using DataModel = Paylocity.Data;
using System;
using System.Linq;

namespace Paylocity.Service
{
    public class EmployeeBenefitsService : IEmployeeBenefitsService
    {
        private IEmployeeBenefitsRepository _repository;
        private decimal _percentDiscount = (100.00m - Constants.DiscountPercentAmount) / 100.00m;
        public EmployeeBenefitsService(IEmployeeBenefitsRepository repository)
        {
            _repository = repository;
        }

        public bool Save(ViewModel.Employee employeeModel)
        {
            //mapping the ViewModel object to DataModel object
            DataModel.Employee employeeData = new DataModel.Employee() { Name = employeeModel.Name };
            //just in case website doesn't send the employee's benefits information
            if (employeeModel.BenefitsSummary == null)
                employeeModel.BenefitsSummary = CalculateBenefitsCost(employeeModel);

            foreach (var dependents in employeeModel.Dependents)
            {
                employeeData.Dependents.Add(new DataModel.Dependent() { Name = dependents.Name });
            }
            employeeData.Benefits.Add(new DataModel.Benefit() { DependentCost = employeeModel.BenefitsSummary.DependentsCost, EmployeeCost = employeeModel.BenefitsSummary.EmployeeCost });
      
            bool isSaved = _repository.SaveEmployeeBenefits(employeeData);
            return isSaved;
        }

        
        /// <summary>
        /// Business logic to calculate the benefit cost
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ViewModel.BenefitsSummary CalculateBenefitsCost(ViewModel.Employee employeeModel)
        {
            ViewModel.BenefitsSummary benefitsSummary = new ViewModel.BenefitsSummary();

            benefitsSummary.TotalSalary = Constants.TotalPayCheck * Constants.PayCheckAmount;

            //using ternary operator-discount if name start with "a" or "A"
            benefitsSummary.EmployeeCost = (employeeModel.Name.StartsWith("A", StringComparison.OrdinalIgnoreCase)) ?
                (_percentDiscount * Constants.EmployeeBenefitsCost) : Constants.EmployeeBenefitsCost;

            benefitsSummary.DependentsCost = employeeModel.Dependents.Sum(x => x.Name.StartsWith("A", StringComparison.OrdinalIgnoreCase)
                                             ? (_percentDiscount * Constants.DependentBenefitsCost) : Constants.DependentBenefitsCost);
            
            benefitsSummary.TotalCost = benefitsSummary.EmployeeCost + benefitsSummary.DependentsCost;

            benefitsSummary.CostPerPayCheck = benefitsSummary.TotalCost / Constants.TotalPayCheck;

            return benefitsSummary;
        }
    }
}