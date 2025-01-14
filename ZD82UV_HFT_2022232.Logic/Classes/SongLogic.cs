﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using ZD82UV_HFT_2022232.Models;
using ZD82UV_HFT_2022232.Repository;

namespace ZD82UV_HFT_2022232.Logic
{
    public class SongLogic : ISongLogic
    {
        IRepository<Song> repo;

        public SongLogic(IRepository<Song> repo)
        {
            this.repo = repo;
        }

        //CRUD

        public void Create(Song item)
        {
            if (item.SongTitle.Length < 3)
            {
                throw new ArgumentException("title too short...");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Song Read(int id)
        {
            var song = this.repo.Read(id);
            if (song == null)
            {
                throw new ArgumentException("Song does not exists");
            }
            return song;
        }

        public IQueryable<Song> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Song item)
        {
            this.repo.Update(item);
        }

        //NON-CRUD 5 (1 in GenresLogic)

        public IEnumerable/*IEnumerable*/<LabelReve> LabelRevenu()
        {
            var labelRe = from song in this.repo.ReadAll()
                          group song by song.Label.LabelName into grp
                          select new LabelReve()
                          {
                              LabelName = grp.Key,
                              SongCount = grp.Count(),
                              Revenu = grp.Sum(c => c.Income)
                          };
            return labelRe;
        }

        public IEnumerable<YearInfo> YearStatistics()
        {
            return from x in this.repo.ReadAll()
                   group x by x.ReleaseDate.Year into g
                   select new YearInfo()
                   {
                       Year = g.Key,
                       AvgRating = g.Average(t => t.Rating),
                       SongNumber = g.Count()
                   };
        }

        public IEnumerable<Topla> TopLabel()
        {
            var toplabel = from song in this.repo.ReadAll()
                           group song by song.Label.LabelName into grp
                           select new Topla()
                           {
                               LabelName = grp.Key,
                               SongCount = grp.Count(),
                               Revenu = grp.Max(c => c.Income)
                           };
            return toplabel;
        }

        public IEnumerable<BestSo> BestSong()
        {
            var bestsong = from song in this.repo.ReadAll()
                           group song by song.SongTitle into grp
                           select new BestSo()
                           {
                               SongName = grp.Key,
                               SongCount = grp.Count(),
                               Rate = grp.Max(c => c.Rating)
                           };
            return bestsong;
        }


        //public double GetAVG()
        //{
        //    return SongRepository.ReadAll().Average(x => x.Income);
        //}

    }

      public class BestSo
    {
        public BestSo()
        {
        }

        public string SongName { get; set; }
        public int SongCount { get; set; }
        public int Rate { get; set; }
    }

    public class Topla
    {
        public Topla()
        {
        }

        public string LabelName { get; set; }
        public int SongCount { get; set; }
        public double Revenu { get; set; }
    }

    public class YearInfo
    {
        public YearInfo()
        {
        }

        public int Year { get; set; }
        public double AvgRating { get; set; }
        public int SongNumber { get; set; }

        public override bool Equals(object obj)
        {
            YearInfo b = obj as YearInfo;
            if (b == null)
            {
                return false;
            }
            else
            {
                return this.Year == b.Year
                    && this.AvgRating == b.AvgRating
                    && this.SongNumber == b.SongNumber;
            }
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Year, this.AvgRating, this.SongNumber);
        }
    }

    public class LabelReve
    {
        public LabelReve()
        {
        }

        public string LabelName { get; set; }
        public int SongCount { get; set; }

        public double Revenu { get; set; } 
    }
}
