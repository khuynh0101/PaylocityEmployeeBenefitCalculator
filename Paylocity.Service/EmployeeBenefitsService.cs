using System;
using System.Linq;
using Paylocity.Common;
using Paylocity.Data.Repositories;
using ViewModel = Paylocity.Service.Models;

namespace Paylocity.Service
{
    public class EmployeeBenefitsService : IEmployeeBenefitsService
    {
        private IEmployeeBenefitsRepository _repository;
        
        public EmployeeBenefitsService(IEmployeeBenefitsRepository repository)
        {
            _repository = repository;
        }
        public bool Save(ViewModel.Employee employeeModel)
        {
            Data.Employee employeeData = new Data.Employee() { Name = employeeModel.Name };

            foreach(var dependents in employeeModel.Dependents)
            {
                employeeData.Dependents.Add(new Data.Dependent() { Name = dependents.Name });
            }
            employeeData.Benefits.Add(new Data.Benefit() { DependentCost = employeeModel.BenefitsSummary.DependentsCost, EmployeeCost = employeeModel.BenefitsSummary.EmployeeCost });

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

            benefitsSummary.EmployeeCost = (employeeModel.Name.StartsWith("A", StringComparison.OrdinalIgnoreCase)) ?
                ((100 - Constants.DiscountPercentAmount) / 100 * Constants.EmployeeBenefitsCost) : Constants.EmployeeBenefitsCost;

            benefitsSummary.DependentsCost = employeeModel.Dependents.Sum(x => x.Name.StartsWith("A", StringComparison.OrdinalIgnoreCase)
                                             ? ((100 - Constants.DiscountPercentAmount) / 100 * Constants.DependentBenefitsCost) : Constants.DependentBenefitsCost);

            benefitsSummary.TotalCost = benefitsSummary.EmployeeCost + benefitsSummary.DependentsCost;

            return benefitsSummary;
        }
    }
}