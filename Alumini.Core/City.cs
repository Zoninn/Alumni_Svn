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
    
    public partial class City
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public City()
        {
            this.UserDetails = new HashSet<UserDetail>();
            this.UserDetails1 = new HashSet<UserDetail>();
        }
    
        public int Id { get; set; }
        public string CityName { get; set; }
        public Nullable<int> Stateid { get; set; }
        public Nullable<int> DisctirctId { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual District District { get; set; }
        public virtual State State { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDetail> UserDetails1 { get; set; }
    }
}
