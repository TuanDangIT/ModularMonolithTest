using Microsoft.EntityFrameworkCore;

namespace ModularMonolithTest.Modules.SecondData.Api.DAL
{
    internal interface ISecondDataDbContext
    {
        DbSet<Entities.SecondData> SecondDatas { get; set; }
        DbSet<Entities.FirstData> FirstDatas { get; set; }
        DbSet<Entities.OtherEntities.ThirdData> ThirdDatas { get; set; }
        Task<int> SaveChangesAsync();
    }
}