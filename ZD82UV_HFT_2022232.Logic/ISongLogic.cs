using System.Collections.Generic;
using System.Linq;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Logic
{
    public interface ISongLogic
    {
        void Create(Song item);
        void Delete(int id);
        IQueryable<LabelReve> LabelRevenu();
        Song Read(int id);
        IQueryable<Song> ReadAll();
        void Update(Song item);
        IEnumerable<YearInfo> YearStatistics();
    }
}