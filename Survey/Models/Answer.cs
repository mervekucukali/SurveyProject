//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Survey.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Answer
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string PersonName { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string CreateBy { get; set; }
    }
}