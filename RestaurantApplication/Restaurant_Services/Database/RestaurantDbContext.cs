using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Services.Database
{
    public partial class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext()
        {
            
        }
        public RestaurantDbContext(DbContextOptions options): base(options) { }

        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Table> Tables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source= .; Initial Catalog=Restoran; user=sa; Password=Konjic1981; TrustServerCertificate=True");

            }
        }
    }
}
