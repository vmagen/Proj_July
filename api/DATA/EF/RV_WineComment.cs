//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class RV_WineComment
    {
        public int id { get; set; }
        public string text { get; set; }
        public System.DateTime date { get; set; }
        public string email { get; set; }
        public int wineId { get; set; }
        public Nullable<int> type { get; set; }
        public Nullable<int> rate { get; set; }
    
        public virtual RV_User RV_User { get; set; }
        public virtual RV_Wine RV_Wine { get; set; }
    }
}
