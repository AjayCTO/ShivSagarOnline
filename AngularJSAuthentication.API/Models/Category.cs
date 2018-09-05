//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AngularJSAuthentication.API.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Category
    {
        public Category()
        {
            this.Categories1 = new HashSet<Category>();
            this.Products = new HashSet<Product>();
        }
    
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ParentCategory { get; set; }
        public string CategoryImage { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Sort { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string SupplierCategoryType { get; set; }
        public string Discriminator { get; set; }
        public Nullable<int> Supplier_Id { get; set; }
        public bool IsTopCategory { get; set; }
    
        public virtual ICollection<Category> Categories1 { get; set; }
        public virtual Category Category1 { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}