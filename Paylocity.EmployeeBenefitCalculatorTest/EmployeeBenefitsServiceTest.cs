using Paylocity.Data.Repositories;
using Paylocity.Service;
using Paylocity.Service.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Paylocity.EmployeeBenefitCalculatorTest
{
    [TestClass]
    public class EmployeeBenefitsServiceTest
    {
        [TestMethod]
        public void CalculateBenefitsCostForEmployeeWithoutLetterA_ShouldReturn1000()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "test";
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.IsNotNull(benefitsSummary);
            Assert.AreEqual(benefitsSummary.EmployeeCost, 1000, "Employee Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 1000, "Employee Total Cost incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeWithLetterA_ShouldReturn900()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "atest";
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.EmployeeCost, 900, "Employee Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 900, "Employee Total Cost incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeAndDependentsWithoutLetterA_ShouldReturnDependentCostOf500_And_TotalCostOf1500()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "test";
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "michelle" });
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.DependentsCost, 500, "Dependent Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 1500, "Employee Total Cost incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeAndDependentsWithLetterA_ShouldReturnDependentCostOf450_And_TotalCostOf1450()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "test";
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "amichelle" });
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.DependentsCost, 450, "Dependent Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 1450, "Employee Total Cost incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeAndTwoDependentsWithoutLetterA_ShouldReturnDependentCostOf1000_And_TotalCostOf2000()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "test";
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "michelle" });
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "tom" });
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.DependentsCost, 1000, "Dependent Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 2000, "Employee Total Cost incorrect");
        }

        [TestMethod]
        public void CalculateBenefitsCostForEmployeeAndTwoDependentsWithLetterA_ShouldReturnDependentCostOf900_And_TotalCostOf1800()
        {
            Mock<Employee> mockEmployee = new Mock<Employee>();
            mockEmployee.Object.Name = "test";
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Apple" });
            mockEmployee.Object.Dependents.Add(new Dependent() { Name = "Ariana" });
            BenefitsSummary benefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(mockEmployee.Object);

            Assert.AreEqual(benefitsSummary.DependentsCost, 900, "Dependent Cost incorrect");
            Assert.AreEqual(benefitsSummary.TotalCost, 1900, "Employee Total Cost incorrect");
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
                    if (benefit.DependentCost != 1000)
                        return false;
                    if (benefit.EmployeeCost != 1000)
                        return false;
                }
                return true; 
            });

            bool isSaved = new EmployeeBenefitsService(mockRepository.Object).Save(mockEmployee.Object);

            Assert.IsTrue(isSaved, "View Model not mapped to Data Model correctly");
        }
    }
}
