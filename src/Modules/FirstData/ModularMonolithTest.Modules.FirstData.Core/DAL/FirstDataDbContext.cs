using Microsoft.EntityFrameworkCore;
using ModularMonolithTest.Modules.FirstData.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.FirstData.Core.DAL
{
    internal class FirstDataDbContext : DbContext, IFirstDataDbContext
    {
        public DbSet<Entities.FirstData> FirstDatas { get; set; }
        public DbSet<FourthData> FourthDatas { get; set; }

        public FirstDataDbContext(DbContextOptions<FirstDataDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("first-data");
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
