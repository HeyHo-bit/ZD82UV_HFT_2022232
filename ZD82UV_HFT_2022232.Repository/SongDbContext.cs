using System;
using Microsoft.EntityFrameworkCore;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Repository
{
    public class SongDbContext: DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public DbSet<Genre> Genres { get; set; }  
        public DbSet<Band> Bands { get; set; }
        public DbSet<Label> Labels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            if (!builder.IsConfigured)
            {
                string conn = @"";
                builder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(conn);
            }
        }
    }
}
