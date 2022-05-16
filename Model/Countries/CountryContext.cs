using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEFAsyncWPF.Model.Countries
{
    public class CountryContext : DbContext
    {
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Continent> Continents { get; set; } = null!;
        public DbSet<EconomicUnion> EconomicUnions { get; set; } = null!;
        public DbSet<MilitaryUnion> MilitaryUnions { get; set; } = null!;
        public DbSet<Interaction> Interactions { get; set; } = null!;
        public DbSet<EconomicInteraction> EconomicInteractions { get; set; } = null!;
        public DbSet<MeetInteraction> MeetInteractions { get; set; } = null!;
        public DbSet<GDP> GDPs { get; set; } = null!;

        public CountryContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
            System.Diagnostics.Debug.WriteLine(Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString(), "CountriesDB.db"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source={Path.Combine(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString(), "CountriesDB.db")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().OwnsOne(c => c.GDP);
        }
    }
}
