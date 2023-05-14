using System;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
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

        public SongDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            if (!builder.IsConfigured)
            {
                builder
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase("song");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Song>(song => song
                .HasOne(song => song.Label)
                .WithMany(label => label.Songs)
                .HasForeignKey(song => song.LabelId)
            .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<Band>()
                .HasMany(x => x.Songs)
                .WithMany(x => x.Bands)
                .UsingEntity<Genre>(
                    x => x.HasOne(x => x.Song)
                        .WithMany().HasForeignKey(x => x.SongId).OnDelete(DeleteBehavior.Cascade),
                    x => x.HasOne(x => x.Band)
                        .WithMany().HasForeignKey(x => x.BandId).OnDelete(DeleteBehavior.Cascade));


            modelBuilder.Entity<Genre>()
                .HasOne(r => r.Band)
                .WithMany(actor => actor.Genres)
                .HasForeignKey(r => r.BandId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Genre>()
                .HasOne(r => r.Song)
                .WithMany(song => song.Genres)
                .HasForeignKey(r => r.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            //SEED SONGS
            var song = new List<Song>()
            { 
                new Song { SongId = 1, SongTitle = "Deutschland",ReleaseDate = new DateTime(2019, 3, 28),  Album = "Untitled", LabelId = 1,Income =50 ,Rating = 4},
                new Song { SongId = 2, SongTitle = "Sunflower",ReleaseDate = new DateTime(2018, 9, 19),  Album = "Spider-Man: Into the Spider-Verse ", LabelId = 2, Income = 70, Rating = 4},
                new Song { SongId = 3, SongTitle = "Raining Blood",ReleaseDate = new DateTime(1986, 9, 7),  Album = "Reign in Blood", LabelId = 3,Income = 35, Rating = 3 },
                new Song { SongId = 4, SongTitle = "Radio",ReleaseDate = new DateTime(2019, 3, 28),  Album = "Untitled", LabelId = 1, Income = 25, Rating = 3},
                new Song { SongId = 5, SongTitle = "Gossip",ReleaseDate = new DateTime(2023, 1, 13),  Album = "Rush!", LabelId = 4, Income = 80,Rating =5 },
                new Song { SongId = 6, SongTitle = "GASOLINE",ReleaseDate = new DateTime(2023, 1, 13),  Album = "Rush!", LabelId = 4,Income = 75,Rating = 5 },


            };

            //SEED LABELS
            var label = new List<Label>
             {
                 new Label {LabelId = 1, LabelName="Universal" },
                 new Label {LabelId = 2, LabelName="Republic" },
                 new Label {LabelId = 3, LabelName="Def Jam" },
                 new Label {LabelId = 4, LabelName="Epic"}

             };

            //SEED BANDS
            var band = new List<Band>()
            { 
                new Band {BandId = 1, BandName = "Rammstein"},
                new Band {BandId = 2, BandName = "Post Malone"},
                new Band {BandId = 3, BandName = "Slayer"},
                new Band {BandId = 4, BandName = "Maneskin"},


            };
            //SEED GENRES
            var genre = new List<Genre>()
            {
                new Genre { GenreId = 1, GenreKind="Neue Deutsche Harte", SongId =1 ,BandId = 1},
                new Genre { GenreId = 2, GenreKind="Hip hop", SongId =2 ,BandId = 2},
                new Genre { GenreId = 3, GenreKind="Thrash metal", SongId =3 ,BandId = 3},
                new Genre { GenreId = 4, GenreKind="Neue Deutsche Harte", SongId =4 ,BandId = 1},
                new Genre { GenreId = 5, GenreKind="Hard rock", SongId =5 ,BandId = 4},
                new Genre { GenreId = 6, GenreKind="Hard rock", SongId =6 ,BandId = 4},

            };
            modelBuilder.Entity<Song>().HasData(song);
            modelBuilder.Entity<Label>().HasData(label);
            modelBuilder.Entity<Band>().HasData(band);
            modelBuilder.Entity<Genre>().HasData(genre);
        }
    }
}
