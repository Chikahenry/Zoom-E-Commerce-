using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Presentation.Data.Models;

namespace ZoomApi.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Presentation.Data.Models.Buyer> Buyer { get; set; }

        public DbSet<Presentation.Data.Models.Admin> Admins { get; set; }

        public DbSet<Presentation.Data.Models.BuyerInfo> BuyerInfos { get; set; }

        public DbSet<Presentation.Data.Models.Bike> Bikes { get; set; }

        public DbSet<Presentation.Data.Models.Category> Categories { get; set; }

        public DbSet<Presentation.Data.Models.OrderItem> OrderItems { get; set; }
    }
}
