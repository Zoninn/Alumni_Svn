//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Alumini.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event_Faqs
    {
        public int FaqId { get; set; }
        public int Eventid { get; set; }
        public string Faqs { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool Status { get; set; }
    
        public virtual Def_Events Def_Events { get; set; }
    }
}
