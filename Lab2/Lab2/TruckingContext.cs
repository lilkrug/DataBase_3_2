using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Lab2
{
    public partial class TruckingContext : DbContext
    {
        public TruckingContext()
            : base("name=Model1")
        {
        }

        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceType> ServiceType { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .Property(e => e.CityName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.CustomerName)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.RouteName)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.Distance)
                .HasPrecision(5, 1);

            modelBuilder.Entity<Route>()
                .Property(e => e.DeparturePoint)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.ArrivalPoint)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.ServiceType)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.RouteName)
                .IsUnicode(false);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Service)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ServiceType>()
                .Property(e => e.ServiceName)
                .IsUnicode(false);
        }
    }
}
