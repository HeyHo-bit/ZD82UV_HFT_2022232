using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Repository
{
    public class SongRepository : Repository<Song>, IRepository<Song>
    {
        public SongRepository(SongDbContext ctx) : base(ctx)
        {
        }

        public override Song Read(int id)
        {
            return ctx.Songs.FirstOrDefault(t => t.SongId == id);
        }

        public override void Update(Song item)
        {
            var old = Read(item.SongId);
            if (old == null)
            {
                throw new ArgumentException("Item not exist..");
            }
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
