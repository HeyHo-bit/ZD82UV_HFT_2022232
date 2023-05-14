using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Repository
{
    public class BandRepository : Repository<Band>, IRepository<Band>
    {
        public BandRepository(SongDbContext ctx) : base(ctx)
        {
        }

        public override Band Read(int id)
        {
            return ctx.Bands.FirstOrDefault(t => t.BandId == id);
        }

        public override void Update(Band item)
        {
            var old = Read(item.BandId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
