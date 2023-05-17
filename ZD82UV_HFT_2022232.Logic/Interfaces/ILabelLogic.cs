using System.Linq;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Logic
{
    public interface ILabelLogic
    {
        void Create(Label item);
        void Delete(int id);
        Label Read(int id);
        IQueryable<Label> ReadAll();
        void Update(Label item);
    }
}