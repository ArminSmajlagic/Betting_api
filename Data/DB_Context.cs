using Betting_api.DB_Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace evona_hackathon.Data
{
    public class DB_Context : DbContext
    {
        private readonly IConfiguration config;

        public DB_Context(IConfiguration config) : base()
        {
            this.config = config;
        }

        public DbSet<Korisnik> korisnici { get; set; }
        public DbSet<Igra> igre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(config.GetConnectionString("sql_db"));
            }
        }
    }
}
