//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Benefit
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Nullable<decimal> EmployeeCost { get; set; }
        public Nullable<decimal> DependentCost { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
