namespace E_Commerce.Data
{
    using E_Commerce.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Configuration;

    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext() { }
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

            modelBuilder.Entity<IdentityRole>().HasData(seedRoles());

            modelBuilder.Entity<User>().HasData(seedSuperAdmin());

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "1", // RoleId for SuperAdmin
                    UserId = "1" // User Id for the default user
                }
            );

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var connectionString = Configuration.GetConnectionString("local");
            optionsBuilder.UseSqlServer(connectionString);
        }

        private List<IdentityRole> seedRoles()
        {
            return new List<IdentityRole> {
                new IdentityRole
                {
                    Id = "1",
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                }
            };
        }
        private User seedSuperAdmin()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            return new User
            {
                Id = "1",
                UserName = "TemporaryUsername",
                NormalizedUserName = "TEMPORARY-USERNAME",
                Email = "TemporaryEmail@example.com",
                NormalizedEmail = "TEMPORARYEMAIL@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "TemporaryPassword"),
                SecurityStamp = string.Empty
            };
        }
    }

}
