using System.Collections.Generic;
using System.Linq;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Logic
{
    public interface IGenreLogic
    {
        void Create(Genre item);
        void Delete(int id);
        IEnumerable<MostSo> MostSong();
        Genre Read(int id);
        IQueryable<Genre> ReadAll();
        void Update(Genre item);
    }
}