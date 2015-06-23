//Added namespaces
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using JAMP.Models;

namespace JAMP.DAL
{
    public class JampContext : DbContext
    {
        public JampContext()
            : base(nameOrConnectionString: "JampContext") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Use singular table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Database Configuration setup
            new JampDbConfiguration(modelBuilder);
        }

        // User Datasets
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SettingUser> SettingUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Business> Businesses { get; set; }
        // Inventory Datasets
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        // Supplier Datasets
        public DbSet<Order> Orders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierPayment> SupplierPayments { get; set; }
        public DbSet<SupplierAccount> SupplierAccounts { get; set; }
        // Customer Datasets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
        // Sales Datasets
        public DbSet<Sale> Sales { get; set; }
        // Update Datasets
        public DbSet<Device> Devices { get; set; }
        public DbSet<EntityUpdate> Updates { get; set; }

        // Bridging Datasets
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ProductIncident> ProductIncidents { get; set; }
        public DbSet<ProductDelivery> ProductDeliveries { get; set; } 
        public DbSet<SalesItem> SalesItems { get; set; }
    
    }
}