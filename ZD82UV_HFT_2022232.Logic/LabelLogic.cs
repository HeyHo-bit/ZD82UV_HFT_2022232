using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZD82UV_HFT_2022232.Models;
using ZD82UV_HFT_2022232.Repository;

namespace ZD82UV_HFT_2022232.Logic
{
    public class LabelLogic
    {
        IRepository<Label> repo;

        public LabelLogic(IRepository<Label> repo)
        {
            this.repo = repo;
        }

        public void Create(Label item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Label Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Label> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Label item)
        {
            this.repo.Update(item);
        }
    }
}
