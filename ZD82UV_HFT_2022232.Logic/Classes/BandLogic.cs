using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZD82UV_HFT_2022232.Models;
using ZD82UV_HFT_2022232.Repository;

namespace ZD82UV_HFT_2022232.Logic
{
    public class BandLogic : IBandLogic
    {
        IRepository<Band> repo;

        public BandLogic(IRepository<Band> repo)
        {
            this.repo = repo;
        }


        public void Create(Band item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Band Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Band> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Band item)
        {
            this.repo.Update(item);
        }
    }
}
