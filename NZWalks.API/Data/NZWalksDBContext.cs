using Microsoft.EntityFrameworkCore;
using NZWalks.API.Model.Domain;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    /*DbContext is a fundamental class in Entity Framework (EF), a popular Object-Relational Mapper (ORM) for .NET.
    It represents a session with the database, allowing you to query and save data.
    It is a bridge between your domain or entity classes and the database.*/
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions) { }

        //Creating collection so that it can create tables in database...
        //But we need to make a connection string as well..
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
