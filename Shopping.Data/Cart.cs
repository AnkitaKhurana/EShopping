//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shopping.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public System.Guid Id { get; set; }
        public System.Guid CartStatusId { get; set; }
        public System.Guid CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    
        public virtual CartLine CartLine { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
