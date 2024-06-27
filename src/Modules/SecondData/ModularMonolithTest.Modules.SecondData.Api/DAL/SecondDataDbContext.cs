using Microsoft.EntityFrameworkCore;
using ModularMonolithTest.Modules.SecondData.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.SecondData.Api.DAL
{
    internal class SecondDataDbContext : DbContext, ISecondDataDbContext
    {
        public DbSet<Entities.SecondData> SecondDatas { get; set; }
        public DbSet<Entities.FirstData> FirstDatas { get; set; }
        public DbSet<Entities.OtherEntities.ThirdData> ThirdDatas { get; set; }  
        public SecondDataDbContext(DbContextOptions<SecondDataDbContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("second-data");
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
