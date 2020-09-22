using Paylocity.Data.Repositories;
using Paylocity.Service;
using Paylocity.Service.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Paylocity.EmployeeBenefitCalculatorTest
{
    [TestClass]
    public class EmployeeBenefitsServiceTest
    {
        [TestMethod]
        public void CalculateBenefitsCostForEmployeeWithoutLetterA_ShouldReturn1000()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "Khuong";
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.IsNotNull(benefitsSummary);
            Assert.AreEqual(benefitsSummary.EmployeeCost, 1000.00m, "Employee Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 1000.00m, "Employee Total Cost incorrect");
            Assert.AreEqual(Math.Round(benefitsSummary.CostPerPayCheck, 2), 38.46m, "Cost per check is incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeWithLetterA_ShouldReturn900()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "Albert";
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.EmployeeCost, 900.00m, "Employee Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 900.00m, "Employee Total Cost incorrect");
            Assert.AreEqual(Math.Round(benefitsSummary.CostPerPayCheck, 2), 34.62m, "Cost per check is incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeAndDependentsWithoutLetterA_ShouldReturnDependentCostOf500_And_TotalCostOf1500()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "Khuong";
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Michelle" });
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.DependentsCost, 500.00m, "Dependent Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 1500.00m, "Employee Total Cost incorrect");
            Assert.AreEqual(Math.Round(benefitsSummary.CostPerPayCheck, 2), 57.69m, "Cost per check is incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeAndDependentsWithLetterA_ShouldReturnDependentCostOf450_And_TotalCostOf1450()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "Khuong";
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Adele" });
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.DependentsCost, 450.00m, "Dependent Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 1450.00m, "Employee Total Cost incorrect");
            Assert.AreEqual(Math.Round(benefitsSummary.CostPerPayCheck, 2), 55.77m, "Cost per check is incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeAndTwoDependentsWithoutLetterA_ShouldReturnDependentCostOf1000_And_TotalCostOf2000()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "Khuong";
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Michelle" });
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Apple" });
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.DependentsCost, 950.00m, "Dependent Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 1950.00m, "Employee Total Cost incorrect");
            Assert.AreEqual(Math.Round(benefitsSummary.CostPerPayCheck, 2), 75.00m, "Cost per check is incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeAndTwoDependentsWithLetterA_ShouldReturnDependentCostOf900_And_TotalCostOf1800()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "Khuong";
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Apple" });
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Ariana" });
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.DependentsCost, 900.00m, "Dependent Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 1900.00m, "Employee Total Cost incorrect");
            Assert.AreEqual(Math.Round(benefitsSummary.CostPerPayCheck, 2), 73.08m, "Cost per check is incorrect");
        }

        [TestMethod]
        public void SaveEmployeeBenefits_ShouldReturnBoolean()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "Khuong";
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Kylie" });
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Evie" });
            mockEmployee.Object.BenefitsSummary = new BenefitsSummary() { DependentsCost = 1000, EmployeeCost = 1000, TotalCost = 2000, TotalSalary = 52000 };

            Data.Employee employeeData = new Data.Employee() { Name = mockEmployee.Object.Name };
            foreach (var dependents in mockEmployee.Object.Dependents)
            {
                employeeData.Dependents.Add(new Data.Dependent() { Name = dependents.Name });
            }
            employeeData.Benefits.Add(new Data.Benefit() { DependentCost = mockEmployee.Object.BenefitsSummary.DependentsCost, EmployeeCost = mockEmployee.Object.BenefitsSummary.EmployeeCost });

            Mock<IEmployeeBenefitsRepository> mockRepository = new Mock<IEmployeeBenefitsRepository>();

            mockRepository.Setup(r => r.SaveEmployeeBenefits(It.IsAny<Data.Employee>())).Returns((Data.Employee target) => {
                if (target.Name != "Khuong")
                    return false;
                if (target.Dependents.Count != 2)
                    return false;
                foreach (var benefit in target.Benefits)
                {
                    if (benefit.DependentCost != 1000.00m)
                        return false;
                    if (benefit.EmployeeCost != 1000.00m)
                        return false;
                }
                return true; 
            });

            bool isSaved = new EmployeeBenefitsService(mockRepository.Object).Save(mockEmployee.Object);

            Assert.IsTrue(isSaved, "View Model not mapped to Data Model correctly");
        }
    }
}
