using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.Repository
{
    internal class GenreRepository : Repository<Genre>, IRepository<Genre>
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
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
