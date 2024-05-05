using GLOproject.DataBase.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLOproject.DataBase
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<PickupPoint> PickupPoints { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }

        public AppDbContext() : base()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        private readonly string connectString = "Server=(local); Database=OnlineStore; Integrated Security=true; TrustServerCertificate=True";

        //private readonly string connectString = "Data Source=DBSRV\\GLO2023; Initial Catalog=WbProject; Integrated Security=true;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }
        private static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
            
                new Customer { ID = 1, Name = "Александр Смирнов", Email = "smirnov@example.com", TotalOrders = 1 },
                new Customer { ID = 2, Name = "Екатерина Иванова", Email = "ivanova@example.com", TotalOrders = 1 },
                new Customer { ID = 3, Name = "Максим Петров", Email = "petrov@example.com", TotalOrders = 1 },
                new Customer { ID = 4, Name = "Ольга Сидорова", Email = "sidorova@example.com", TotalOrders = 1 },
                new Customer { ID = 5, Name = "Иван Козлов", Email = "kozlov@example.com", TotalOrders = 1 }
            );
            modelBuilder.Entity<Order>().HasData(
            
                new Order { ID = 1, CustomerID = 1, OrderDate = new DateTime(2024, 2, 5), TotalAmount = 12000, PickupPointID = 1 },
                new Order { ID = 2, CustomerID = 2, OrderDate = new DateTime(2024, 2, 4), TotalAmount = 21000, PickupPointID = 2 },
                new Order { ID = 3, CustomerID = 3, OrderDate = new DateTime(2024, 2, 3), TotalAmount = 28000, PickupPointID = 3 },
                new Order { ID = 4, CustomerID = 4, OrderDate = new DateTime(2024, 2, 2), TotalAmount = 42000, PickupPointID = 4 },
                new Order { ID = 5, CustomerID = 5, OrderDate = new DateTime(2024, 2, 1), TotalAmount = 15000, PickupPointID = 5 },
                new Order { ID = 6, CustomerID = 2, OrderDate = new DateTime(2024, 2, 1), TotalAmount = 11000, PickupPointID = 2 }
            );
            modelBuilder.Entity<Employee>().HasData(
            
                new Employee { ID = 1, RoleID = 2, Name = "Максим Админов", Login = "1", Password = "1", PickupPointID = 1 },
                new Employee { ID = 2, RoleID = 3, Name = "Максим Работников", Login = "2", Password = "2", Salary = 15000, PickupPointID = 2 }
            );
            modelBuilder.Entity<OrderDetail>().HasData(
            
                new OrderDetail { ID = 1, OrderID = 1, ProductID = 4, Quantity = 4, Subtotal = 12000 },
                new OrderDetail { ID = 2, OrderID = 2, ProductID = 16, Quantity = 2, Subtotal = 20000 },
                new OrderDetail { ID = 3, OrderID = 3, ProductID = 8, Quantity = 14, Subtotal = 28000 },
                new OrderDetail { ID = 4, OrderID = 2, ProductID = 11, Quantity = 1, Subtotal = 1000 },
                new OrderDetail { ID = 5, OrderID = 4, ProductID = 20, Quantity = 6, Subtotal = 42000 },
                new OrderDetail { ID = 6, OrderID = 5, ProductID = 1, Quantity = 2, Subtotal = 15000 }
            );
            modelBuilder.Entity<PickupPoint>().HasData(
            
                new PickupPoint { ID = 1, Location = "ул. Ленина, 10", Turnover = 1, Rating = 3.9M },
                new PickupPoint { ID = 2, Location = "ул. Первомайская, 1", Turnover = 2, Rating = 4.2M },
                new PickupPoint { ID = 3, Location = "пр. Героев, 31", Turnover = 1, Rating = 3.2M },
                new PickupPoint { ID = 4, Location = "пл. Мира, 15", Turnover = 1, Rating = 5.0M },
                new PickupPoint { ID = 5, Location = "пр. Принцев, 30", Turnover = 1, Rating = 4.0M }
            );
            modelBuilder.Entity<Product>().HasData(
            
                new Product { ID = 1, Title = "Ноутбук Dell XPS 13", Price = 15000, Quantity = 10, SellerID = 1, Rating = 4.2m, CategoryID = 3 },
                new Product { ID = 2, Title = "Смартфон iPhone 12", Price = 10000, Quantity = 15, SellerID = 2, Rating = 3.2m, CategoryID = 4 },
                new Product { ID = 3, Title = "Телевизор Samsung QLED 55", Price = 1200, Quantity = 8, SellerID = 3, Rating = 5, CategoryID = 4 },
                new Product { ID = 4, Title = "Наушники Sony WH-1000XM4", Price = 3000, Quantity = 20, SellerID = 4, Rating = 4.5m, CategoryID = 4 },
                new Product { ID = 5, Title = "Кофемашина DeLonghi Magnifica", Price = 6000, Quantity = 12, SellerID = 5, Rating = 4.8m, CategoryID = 2 },
                new Product { ID = 6, Title = "Планшет Samsung Galaxy Tab S7", Price = 8000, Quantity = 18, SellerID = 1, Rating = 4.3m, CategoryID = 4 },
                new Product { ID = 7, Title = "Фотоаппарат Canon EOS R5", Price = 25000, Quantity = 5, SellerID = 2, Rating = 4.2m, CategoryID = 4 },
                new Product { ID = 8, Title = "Беспроводные наушники Apple AirPods Pro", Price = 2500, Quantity = 25, SellerID = 3, Rating = 4.2m, CategoryID = 4 },
                new Product { ID = 9, Title = "Умные часы Garmin Forerunner 945", Price = 6000, Quantity = 10, SellerID = 4, Rating = 4, CategoryID = 4 },
                new Product { ID = 10, Title = "Холодильник LG InstaView", Price = 15000, Quantity = 8, SellerID = 5, Rating = 4, CategoryID = 2 },
                new Product { ID = 11, Title = "Умные колонки Amazon Echo", Price = 1000, Quantity = 30, SellerID = 1, Rating = 3.3m, CategoryID = 4 },
                new Product { ID = 12, Title = "Видеокарта NVIDIA GeForce RTX 3080", Price = 12000, Quantity = 15, SellerID = 2, Rating = 4.5m, CategoryID = 3 },
                new Product { ID = 13, Title = "Фитнес трекер Fitbit Charge 4", Price = 1500, Quantity = 20, SellerID = 3, Rating = 4.8m, CategoryID = 4 },
                new Product { ID = 14, Title = "Соковыжималка Bosch MESM731M", Price = 2000, Quantity = 10, SellerID = 4, Rating = 4.3m, CategoryID = 2 },
                new Product { ID = 15, Title = "Ноутбук Apple MacBook Pro 13", Price = 18000, Quantity = 7, SellerID = 5, Rating = 3.8m, CategoryID = 3 },
                new Product { ID = 16, Title = "Квадрокоптер DJI Mavic Air 2", Price = 10000, Quantity = 12, SellerID = 1, Rating = 4, CategoryID = 4 },
                new Product { ID = 17, Title = "Монитор LG UltraGear 27GN950-B", Price = 7000, Quantity = 10, SellerID = 2, Rating = 3.2m, CategoryID = 3 },
                new Product { ID = 18, Title = "Кофеварка DeLonghi Dedica", Price = 1500, Quantity = 15, SellerID = 3, Rating = 5, CategoryID = 2 },
                new Product { ID = 19, Title = "Умный термостат Nest Learning Thermostat", Price = 2500, Quantity = 8, SellerID = 4, Rating = 4.2m, CategoryID = 2 },
                new Product { ID = 20, Title = "Смарт-часы Apple Watch Series 6", Price = 7000, Quantity = 6, SellerID = 5, Rating = 4.2m, CategoryID = 4 }
            );
            modelBuilder.Entity<Seller>().HasData(
            
                new Seller { ID = 1, Name = "Максим Продавакин", Email = "prodavakinmaks@example.com", Rating = 4.2m },
                new Seller { ID = 2, Name = "Зовов Семён", Email = "zovovsemen@example.com", Rating = 4.0m },
                new Seller { ID = 3, Name = "Гойдов Дмитрий", Email = "goydovdmit@example.com", Rating = 3.1m },
                new Seller { ID = 4, Name = "Руков Егор", Email = "egorruk@example.com", Rating = 4.8m },
                new Seller { ID = 5, Name = "Михайлова Александра", Email = "mikhaylovaaleksa@example.com", Rating = 3.9m }
            );
            modelBuilder.Entity<Category>().HasData(
            
                new Category { ID = 1, Name = "All" },
                new Category { ID = 2, Name = "Appliances" },
                new Category { ID = 3, Name = "Computers" },
                new Category { ID = 4, Name = "Electronics" }
            );
            modelBuilder.Entity<Role>().HasData(
            
                new Role { ID = 1, Name = "Guest" },
                new Role { ID = 2, Name = "Administrator" },
                new Role { ID = 3, Name = "Worker" }
            );
        }
    }
}