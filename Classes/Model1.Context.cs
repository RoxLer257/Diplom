﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Diplom.Classes
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VSK_DBEntities : DbContext
    {
        public VSK_DBEntities()
            : base("name=VSK_DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ClientTypes> ClientTypes { get; set; }
        public virtual DbSet<DriverInsuranceHistory> DriverInsuranceHistory { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<HealthConditions> HealthConditions { get; set; }
        public virtual DbSet<LifeAndHealth> LifeAndHealth { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<Policies> Policies { get; set; }
        public virtual DbSet<PolicyStatuses> PolicyStatuses { get; set; }
        public virtual DbSet<PolicyTypes> PolicyTypes { get; set; }
        public virtual DbSet<Properties> Properties { get; set; }
        public virtual DbSet<PropertyTypes> PropertyTypes { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<VehicleMakes> VehicleMakes { get; set; }
        public virtual DbSet<VehicleModels> VehicleModels { get; set; }
        public virtual DbSet<Vehicles> Vehicles { get; set; }
    }
}
