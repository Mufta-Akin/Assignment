using System;
using MMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MMS.Data.Repositories
{
    // The Context is How EntityFramework communicates with the database
    // We define DbSet properties for each table in the database
    public class MovieDbContext :DbContext
    { 
        // create DbSets for various models
        public DbSet<User> Users { get; set; }
      
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }

        // Could use SqlServer using connection below if installed
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                /** use simple logging to log the sql commands issued by entityframework **/
                //.EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information)
	            .UseSqlite("Filename=data.db");
        }

        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}
