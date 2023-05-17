using System;
using System.Linq;
using ConsoleTools;
using ZD82UV_HFT_2022232.Logic;
using ZD82UV_HFT_2022232.Models;
using ZD82UV_HFT_2022232.Repository;

namespace ZD82UV_HFT_2022232.Client
{
    internal class Program
    {
        static BandLogic bandLogic;
        static GenreLogic genreLogic;
        static LabelLogic labelLogic;
        static SongLogic songLogic;

        static void Create(string entity)
        {
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Band")
            {
                var items = bandLogic.ReadAll();
                Console.WriteLine("Id" + "\t" + "Name");
                foreach (var item in items)
                {
                    Console.WriteLine(item.BandId + "\t" + item.BandName);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.WriteLine(entity + " update");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.WriteLine(entity + " delete");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            //SongDbContext db = new SongDbContext();
            //IRepository<Song> repo = new SongRepository(new SongDbContext());

            //var items = repo.ReadAll().ToArray();
            //var items = db.Songs.ToArray();

            //var item = songLogic.LabelRevenu();
            //var item = songLogic.ReadAll();

            var ctx = new SongDbContext();

            var songRepo = new SongRepository(ctx);
            var genreRepo = new GenreRepository(ctx);
            var bandRepo = new BandRepository(ctx);
            var labelRepo = new LabelRepository(ctx);

            songLogic = new SongLogic(songRepo);
            genreLogic = new GenreLogic(genreRepo);
            bandLogic = new BandLogic(bandRepo);
            labelLogic = new LabelLogic(labelRepo);

            //var item = songLogic.YearStatistics().ToArray();
            //var items = songLogic.LabelRevenu();
            //var thing = songLogic.TopLabel();
            //var thing2 = songLogic.BestSong();
            //var thing3 = genreLogic.MostSong();
            //;

            var bandSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Band"))
                .Add("Create", () => Create("Band"))
                .Add("Delete", () => Delete("Band"))
                .Add("Update", () => Update("Band"))
                .Add("Exit", ConsoleMenu.Close);

            var genreSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Genre"))
                .Add("Create", () => Create("Genre"))
                .Add("Delete", () => Delete("Genre"))
                .Add("Update", () => Update("Genre"))
                .Add("Exit", ConsoleMenu.Close);

            var labelSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Label"))
                .Add("Create", () => Create("Label"))
                .Add("Delete", () => Delete("Label"))
                .Add("Update", () => Update("Label"))
                .Add("Exit", ConsoleMenu.Close);

            var songSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Song"))
                .Add("Create", () => Create("Song"))
                .Add("Delete", () => Delete("Song"))
                .Add("Update", () => Update("Song"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Songs", () => songSubMenu.Show())
                .Add("Actors", () => bandSubMenu.Show())
                .Add("Genres", () => genreSubMenu.Show())
                .Add("Labels", () => labelSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();


        }
    }
}
