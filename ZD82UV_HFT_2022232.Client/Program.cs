using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTools;
using ZD82UV_HFT_2022232.Models;


namespace ZD82UV_HFT_2022232.Client
{
    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            Console.WriteLine(entity + " create");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            if (entity == "Band")
            {
                List<Band> bands = rest.Get<Band>("band");
                foreach (var item in bands)
                {
                    Console.WriteLine(item.BandId + ": " + item.BandName);
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
            rest = new RestService("http://localhost:4273/", "song");


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
