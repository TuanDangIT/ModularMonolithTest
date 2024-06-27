using Microsoft.EntityFrameworkCore;
using ModularMonolithTest.Modules.FirstData.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularMonolithTest.Modules.FirstData.Core.DAL
{
    internal interface IFirstDataDbContext
    {
        DbSet<Entities.FirstData> FirstDatas { get; set; }
        DbSet<Entities.FourthData> FourthDatas { get; set; }
        Task<int> SaveChangesAsync();
    }
}
