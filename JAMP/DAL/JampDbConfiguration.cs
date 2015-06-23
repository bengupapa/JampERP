//Added namespaces
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using JAMP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JAMP.DAL
{
    public class JampDbConfiguration
    {
        public JampDbConfiguration(DbModelBuilder modelBuilder)
        {
            #region Product configurations
            // Product has 1 category, Categories have many product records
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Category)
                 .WithMany(c => c.ProductList)
                 .HasForeignKey(p => p.CategoryID);

            // Product has 1 business, Business have many product records
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Business)
                 .WithMany(b => b.Products)
                .HasForeignKey(b => b.BusinessID);

            // Product has 1 Employee, Employee have many product records
            modelBuilder.Entity<Product>()
                .HasRequired(p => p.Employee)
                 .WithMany(e => e.Products)
                .HasForeignKey(p => p.EmployeeID)
                .WillCascadeOnDelete(false);
            #endregion

            #region Employee configurations
            // Business has many employee, an Employee has 1 business 
            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Business)
                 .WithMany(b => b.Employees)
                .HasForeignKey(e => e.BusinessID);

            // Employee has 1 SettingUser, Employee has 1 SettingUser
            modelBuilder.Entity<SettingUser>()
                .HasKey(s => s.EmployeeID);

            modelBuilder.Entity<Employee>()
                .HasRequired(e => e.Settings)
                .WithRequiredPrincipal(s => s.Employee);

            #endregion

            #region Category configurations
            // Business has many categories of products, a Category has 1 business 
            modelBuilder.Entity<Category>()
                .HasRequired(e => e.Business)
                 .WithMany(b => b.Categories)
                 .HasForeignKey(e => e.BusinessID)
                 .WillCascadeOnDelete(false);
            #endregion

            #region Supplier + Account configurations
            // Business has many Suppliers, a Supplier has 1 business 
            modelBuilder.Entity<Supplier>()
                .HasRequired(s => s.Business)
                 .WithMany(b => b.Suppliers)
                 .HasForeignKey(s => s.BusinessID)
                 .WillCascadeOnDelete(false);

            // Employee has many Suppliers, a Suppliers has 1 Employee 
            modelBuilder.Entity<Supplier>()
                .HasRequired(s => s.Employee)
                 .WithMany(e => e.Suppliers)
                 .HasForeignKey(s => s.EmployeeID)
                 .WillCascadeOnDelete(false);

            // Supplier has 1 SupplierAccount, SupplierAccount has 1 Customer
            modelBuilder.Entity<SupplierAccount>()
                 .HasKey(sa => sa.SupplierID);

            modelBuilder.Entity<Supplier>()
               .HasRequired(s => s.SupplierAccount)
               .WithRequiredPrincipal(s => s.Supplier);

            #endregion

            #region SupplierPayments configurations
            // SupplierAccount has many SupplierPayment, an SupplierPayment has 1 SupplierAccount 
            modelBuilder.Entity<SupplierPayment>()
                .HasRequired(b => b.SupplierAccount)
                .WithMany(b => b.SupplierPayments)
                .HasForeignKey(s => s.SupplierID)
                .WillCascadeOnDelete(false);

            // Employee has many SupplierPayment, a SupplierPayment has 1 Employee 
            modelBuilder.Entity<SupplierPayment>()
                .HasRequired(b => b.Employee)
                .WithMany(c => c.SupplierPayments)
                .HasForeignKey(s => s.EmployeeID)
                .WillCascadeOnDelete(false);

            #endregion

            #region Customer + Account configurations
            // Business has many Customers, a Customer has 1 business 
            modelBuilder.Entity<Customer>()
                .HasRequired(s => s.Business)
                 .WithMany(b => b.Customers)
                 .HasForeignKey(s => s.BusinessID)
                 .WillCascadeOnDelete(false);

            // Cusotmer has 1 Employee, an Employee has many Customer
            modelBuilder.Entity<Customer>()
                .HasRequired(b => b.Employee)
                .WithMany(c => c.Customers)
                .HasForeignKey(s => s.EmployeeID)
                .WillCascadeOnDelete(false);

            // Customer has 1 CustomerAccount, CustomerAccount has 1 Customer
            modelBuilder.Entity<CustomerAccount>()
                .HasKey(t => t.CustomerID);

            modelBuilder.Entity<Customer>()
                .HasRequired(t => t.CustomerAccount)
                .WithRequiredPrincipal(t => t.Customer);

            #endregion

            #region CustomerPayments configurations
            // CustomerAccount has many AccountPayments, an AccountPayments has 1 CustomerAccount 
            modelBuilder.Entity<CustomerPayment>()
                .HasRequired(b => b.CustomerAccount)
                .WithMany(b => b.CustomerPayments)
                .HasForeignKey(s => s.CustomerID)
                .WillCascadeOnDelete(false);

            // CustomerPayment has 1 Employee, an Employee has many CustomerPayment
            modelBuilder.Entity<CustomerPayment>()
                .HasRequired(b => b.Employee)
                .WithMany(c => c.CustomerPayments)
                .HasForeignKey(s => s.EmployeeID)
                .WillCascadeOnDelete(false);
            #endregion

            #region Incident configurations
            // Incident has 1 business, a business have many incidents 
            modelBuilder.Entity<Incident>()
                .HasRequired(i => i.Business)
                .WithMany(b => b.Incidents)
                .HasForeignKey(b => b.BusinessID);

            // Incident has 1 employee, an employee have many incidents 
            modelBuilder.Entity<Incident>()
                .HasRequired(i => i.Employee)
                .WithMany(e => e.Incidents)
                .HasForeignKey(i => i.EmployeeID)
                .WillCascadeOnDelete(false); ;
            #endregion

            #region Sales configurations
            // Sale has 1 employee, an employee has many sales
            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.Employee)
                 .WithMany(e => e.Sales)
                 .HasForeignKey(s => s.EmployeeID)
                 .WillCascadeOnDelete(false);

            // Sale has 1 business, an business has many sales
            modelBuilder.Entity<Sale>()
                .HasRequired(s => s.Business)
                 .WithMany(b => b.Sales)
                 .HasForeignKey(s => s.BusinessID)
                 .WillCascadeOnDelete(false);

            // Sale has 1 business, an business has many sales
            modelBuilder.Entity<Sale>()
                .HasOptional(s => s.Customer)
                 .WithMany(c => c.Sales)
                 .HasForeignKey(s => s.CustomerID)
                 .WillCascadeOnDelete(false);

            #endregion

            #region Order configurations
            // Order has 1 supplier, a supplier has many orders
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Supplier)
                 .WithMany(s => s.Orders)
                 .HasForeignKey(o => o.SupplierID);

            // Order has 1 business, a Business has many orders
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Business)
                 .WithMany(s => s.Orders)
                 .HasForeignKey(o => o.BusinessID);

            // Order has 1 employee, a employee has many orders
            modelBuilder.Entity<Order>()
                .HasRequired(o => o.Employee)
                 .WithMany(e => e.Orders)
                 .HasForeignKey(o => o.EmployeeID)
                 .WillCascadeOnDelete(false);

            #endregion

            #region Delivery configurations
            // Delivery has 1 order, a order has many Deliveries
            modelBuilder.Entity<Delivery>()
                .HasRequired(d => d.Order)
                 .WithMany(o => o.Deliveries)
                 .HasForeignKey(d => d.OrderID)
                 .WillCascadeOnDelete(false); 

            // Delivery has 1 employee, a employee has many Deliveries
            modelBuilder.Entity<Delivery>()
                .HasRequired(d => d.Employee)
                 .WithMany(e => e.Deliveries)
                 .HasForeignKey(d => d.EmployeeID)
                 .WillCascadeOnDelete(false); 

            #endregion

            #region Device configurations
            // Device has 1 Employee, Employee have many Device records
            modelBuilder.Entity<Device>()
                .HasRequired(d => d.Employee)
                 .WithMany(e => e.Devices)
                 .HasForeignKey(d => d.EmployeeID)
                 .WillCascadeOnDelete(false);

            // EntityUpdate has 1 Device, Device have many EntityUpdate records
            modelBuilder.Entity<EntityUpdate>()
                .HasRequired(eu => eu.Device)
                 .WithMany(d => d.Updates)
                 .HasForeignKey(eu => eu.DeviceID)
                 .WillCascadeOnDelete(true); 
            #endregion

            // Bridging Configurations
            #region ProductOrders configuration
            //ProductOrders has 1 Products, Products have many ProductOrders records
            modelBuilder.Entity<ProductOrder>()
                .HasRequired(po => po.Product)
                .WithMany(p => p.ProductOrders)
                .HasForeignKey(po => po.ProductID)
                .WillCascadeOnDelete(false);

            //ProductOrders has 1 Orders, Orders have many ProductOrders records
            modelBuilder.Entity<ProductOrder>()
                .HasRequired(po => po.Order)
                .WithMany(o => o.ProductOrders)
                .HasForeignKey(po => po.OrderID)
                .WillCascadeOnDelete(false);
            #endregion

            #region ProductIncidents configuration
            //ProductOrders has 1 Products, Products have many ProductOrders records
            modelBuilder.Entity<ProductIncident>()
                .HasRequired(pi => pi.Product)
                .WithMany(p => p.ProductIncidents)
                .HasForeignKey(pi => pi.ProductID)
                .WillCascadeOnDelete(false);

            //ProductOrders has 1 Orders, Orders have many ProductOrders records
            modelBuilder.Entity<ProductIncident>()
                .HasRequired(pi => pi.Incident)
                .WithMany(i => i.ProductIncidents)
                .HasForeignKey(pi => pi.IncidentID)
                .WillCascadeOnDelete(false);
            #endregion

            #region ProductDeliveries configuration
            //ProductDeliveries has 1 Products, Products have many ProductDeliveries records
            modelBuilder.Entity<ProductDelivery>()
                .HasRequired(pd => pd.Product)
                .WithMany(p => p.ProductDeliveries)
                .HasForeignKey(pd => pd.ProductID)
                .WillCascadeOnDelete(false);

            //ProductDeliveries has 1 Delivery, Delivery have many ProductDeliveries records
            modelBuilder.Entity<ProductDelivery>()
                .HasRequired(pd => pd.Delivery)
                .WithMany(d => d.ProductDeliveries)
                .HasForeignKey(pd => pd.DeliveryID)
                .WillCascadeOnDelete(false);
            #endregion

            #region SalesItems configuration
            //SalesList has 1 Products, Products have many SalesList records
            modelBuilder.Entity<SalesItem>()
                .HasRequired(sl => sl.Product)
                .WithMany(p => p.SalesItems)
                .HasForeignKey(sl => sl.ProductID)
                .WillCascadeOnDelete(false);

            //SalesList has 1 Sale, Sale have many SalesList records
            modelBuilder.Entity<SalesItem>()
                .HasRequired(sl => sl.Sale)
                .WithMany(s => s.SalesItems)
                .HasForeignKey(sl => sl.SaleID)
                .WillCascadeOnDelete(false);
            #endregion
        }
    }
}