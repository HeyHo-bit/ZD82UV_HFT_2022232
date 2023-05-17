using System.Linq;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Logic
{
    public interface IBandLogic
    {
        void Create(Band item);
        void Delete(int id);
        Band Read(int id);
        IQueryable<Band> ReadAll();
        void Update(Band item);
    }
}