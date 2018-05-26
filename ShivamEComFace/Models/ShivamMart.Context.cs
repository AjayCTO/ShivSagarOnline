﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShivamEComFace.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SHIVAMECommerceDBEntities : DbContext
    {
        public SHIVAMECommerceDBEntities()
            : base("name=SHIVAMECommerceDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductAttributesRelation> ProductAttributesRelations { get; set; }
        public virtual DbSet<ProductAttributeWithQuantity> ProductAttributeWithQuantities { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<UnitOfMeasure> UnitOfMeasures { get; set; }
        public virtual DbSet<ProductAttribute_view> ProductAttribute_view { get; set; }
        public virtual DbSet<ProductValues_view> ProductValues_view { get; set; }
        public virtual DbSet<GetAttribute> GetAttributes { get; set; }
    
        [DbFunction("SHIVAMECommerceDBEntities", "SplitString")]
        public virtual IQueryable<SplitString_Result> SplitString(string input, string character)
        {
            var inputParameter = input != null ?
                new ObjectParameter("Input", input) :
                new ObjectParameter("Input", typeof(string));
    
            var characterParameter = character != null ?
                new ObjectParameter("Character", character) :
                new ObjectParameter("Character", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<SplitString_Result>("[SHIVAMECommerceDBEntities].[SplitString](@Input, @Character)", inputParameter, characterParameter);
        }
    
        public virtual int GetProductsCount(string supplierId)
        {
            var supplierIdParameter = supplierId != null ?
                new ObjectParameter("SupplierId", supplierId) :
                new ObjectParameter("SupplierId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetProductsCount", supplierIdParameter);
        }
    
        public virtual int GetProductsDetail(string supplierId)
        {
            var supplierIdParameter = supplierId != null ?
                new ObjectParameter("SupplierId", supplierId) :
                new ObjectParameter("SupplierId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetProductsDetail", supplierIdParameter);
        }
    
        public virtual int SP_GetAllSortedProducts(Nullable<int> displayLength, Nullable<int> displayStart, string sortCol, string sortDir, string searchText, string supplierId)
        {
            var displayLengthParameter = displayLength.HasValue ?
                new ObjectParameter("DisplayLength", displayLength) :
                new ObjectParameter("DisplayLength", typeof(int));
    
            var displayStartParameter = displayStart.HasValue ?
                new ObjectParameter("DisplayStart", displayStart) :
                new ObjectParameter("DisplayStart", typeof(int));
    
            var sortColParameter = sortCol != null ?
                new ObjectParameter("SortCol", sortCol) :
                new ObjectParameter("SortCol", typeof(string));
    
            var sortDirParameter = sortDir != null ?
                new ObjectParameter("SortDir", sortDir) :
                new ObjectParameter("SortDir", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            var supplierIdParameter = supplierId != null ?
                new ObjectParameter("SupplierId", supplierId) :
                new ObjectParameter("SupplierId", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_GetAllSortedProducts", displayLengthParameter, displayStartParameter, sortColParameter, sortDirParameter, searchTextParameter, supplierIdParameter);
        }
    
        public virtual int SP_GetAllSortedProductsFrontFace(Nullable<int> displayLength, Nullable<int> displayStart, string sortCol, string sortDir, string searchText)
        {
            var displayLengthParameter = displayLength.HasValue ?
                new ObjectParameter("DisplayLength", displayLength) :
                new ObjectParameter("DisplayLength", typeof(int));
    
            var displayStartParameter = displayStart.HasValue ?
                new ObjectParameter("DisplayStart", displayStart) :
                new ObjectParameter("DisplayStart", typeof(int));
    
            var sortColParameter = sortCol != null ?
                new ObjectParameter("SortCol", sortCol) :
                new ObjectParameter("SortCol", typeof(string));
    
            var sortDirParameter = sortDir != null ?
                new ObjectParameter("SortDir", sortDir) :
                new ObjectParameter("SortDir", typeof(string));
    
            var searchTextParameter = searchText != null ?
                new ObjectParameter("SearchText", searchText) :
                new ObjectParameter("SearchText", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_GetAllSortedProductsFrontFace", displayLengthParameter, displayStartParameter, sortColParameter, sortDirParameter, searchTextParameter);
        }
    }
}
