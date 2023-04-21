using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCMS.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer("Server=10.7.10.155,1433;Database=CoWorking_DEV;User Id=coworking;Password=$BPcoworking2020;", b => b.MigrationsAssembly("CoreCMS.Persistence"));
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
