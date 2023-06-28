using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_CarsForSale.Models;
using System.Collections.Generic;

namespace MVC_CarsForSale.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Vans> Vans { get; set; }
        public DbSet<Bikes> Bikes { get; set; }
    }
}
