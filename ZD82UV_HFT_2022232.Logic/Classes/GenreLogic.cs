using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZD82UV_HFT_2022232.Models;
using ZD82UV_HFT_2022232.Repository;

namespace ZD82UV_HFT_2022232.Logic
{
    public class GenreLogic : IGenreLogic
    {

        IRepository<Genre> repo;

        public GenreLogic(IRepository<Genre> repo)
        {
            this.repo = repo;
        }

        public void Create(Genre item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Genre Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Genre> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Genre item)
        {
            this.repo.Update(item);
        }

        //non-crud

        public IEnumerable<MostSo> MostSong()
        {
            return from x in this.repo.ReadAll()
                   group x by x.Song.SongTitle into g
                   select new MostSo()
                   {
                       SongName = g.Key,
                       SongNumber = g.Count()
                   };
        }
    }

    public class MostSo
    {
        public MostSo()
        {
        }

        public string SongName { get; set; }
        public int SongNumber { get; set; }
    }
}
