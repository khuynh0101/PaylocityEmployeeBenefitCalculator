﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Paylocity.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class PaylocityEntities : DbContext
    {
        public PaylocityEntities()
            : base("name=PaylocityEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Benefit> Benefits { get; set; }
        public virtual DbSet<Dependent> Dependents { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
    
        public virtual ObjectResult<EmployeesAndDependentsCost> GetAllEmployeesAndDependentsCost()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EmployeesAndDependentsCost>("GetAllEmployeesAndDependentsCost");
        }
    }
}
