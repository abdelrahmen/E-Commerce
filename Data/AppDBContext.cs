namespace E_Commerce.Data
{
    using E_Commerce.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Configuration;

    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(){}
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        // DbSet for your custom entities
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = Configuration.GetConnectionString("local");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

}
