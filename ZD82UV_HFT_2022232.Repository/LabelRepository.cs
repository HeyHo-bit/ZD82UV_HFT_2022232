using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Repository
{
    internal class LabelRepository : Repository<Label>, IRepository<Label>
    {
        public LabelRepository(SongDbContext ctx) : base(ctx)
        {
        }

        public override Label Read(int id)
        {
            return ctx.Labels.FirstOrDefault(t => t.LabelId == id);
        }

        public override void Update(Label item)
        {
            var old = Read(item.LabelId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
