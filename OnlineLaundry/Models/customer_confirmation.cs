//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineLaundry.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class customer_confirmation
    {
        public int confirmation_id { get; set; }
        public Nullable<int> customer_id { get; set; }
        public int token { get; set; }
        public System.DateTime date_created { get; set; }
        public Nullable<byte> isValid { get; set; }
    
        public virtual customer customer { get; set; }
    }
}