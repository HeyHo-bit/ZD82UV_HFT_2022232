using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Repository
{
    public class GenreRepository : Repository<Genre>, IRepository<Genre>
    {
        public GenreRepository(SongDbContext ctx) : base(ctx)
        {
        }

        public override Genre Read(int id)
        {
            return ctx.Genres.FirstOrDefault(t => t.GenreId == id);
        }

        public override void Update(Genre item)
        {
            var old = Read(item.GenreId);
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
