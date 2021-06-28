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
    
    public partial class RV_Wine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RV_Wine()
        {
            this.RV_Competition = new HashSet<RV_Competition>();
            this.RV_Rate = new HashSet<RV_Rate>();
            this.RV_WineryCommand = new HashSet<RV_WineryCommand>();
            this.RV_WineComment = new HashSet<RV_WineComment>();
        }
    
        public int wineId { get; set; }
        public string wineName { get; set; }
        public string content { get; set; }
        public int price { get; set; }
        public string wineImgPath { get; set; }
        public string wineLabelPath { get; set; }
        public Nullable<int> categoryId { get; set; }
        public int wineryId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RV_Competition> RV_Competition { get; set; }
        public virtual RV_WineCategory RV_WineCategory { get; set; }
        public virtual RV_Winery RV_Winery { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RV_Rate> RV_Rate { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RV_WineryCommand> RV_WineryCommand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RV_WineComment> RV_WineComment { get; set; }
    }
}
