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
    
    public partial class CartLine
    {
        public Nullable<System.Guid> Id { get; set; }
        public int Quantity { get; set; }
        public System.Guid ProductId { get; set; }
        public System.Guid CustomerId { get; set; }
        public string VariantName { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}
