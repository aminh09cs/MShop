using DataAccess.MShop.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.MShop.EntitiesFramework
{
    public class MShopDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MShopDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder) { base.OnModelCreating(builder); }
        public DbSet<Post>? Post { get; set; }
    }

}
