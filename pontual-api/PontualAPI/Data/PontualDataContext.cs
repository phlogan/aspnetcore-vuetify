using Microsoft.EntityFrameworkCore;
using PontualAPI.Config;
using PontualAPI.Models;

namespace PontualAPI.Data
{
    public class PontualDataContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public PontualDataContext(DbContextOptions<PontualDataContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ClienteConfig().Configure(modelBuilder.Entity<Cliente>());
        }
    }
}
