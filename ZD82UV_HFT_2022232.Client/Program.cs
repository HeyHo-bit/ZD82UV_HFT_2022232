using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ConsoleTools;
using ZD82UV_HFT_2022232.Models;


namespace ZD82UV_HFT_2022232.Client
{
    internal class Program
    {
        static RestService rest;

        static void Create(string entity)
        {
            if (entity == "Band")
            {
                Console.Write("Enter Band Name: ");
                string name = Console.ReadLine();
                rest.Post(new Band() { BandName = name }, "band");
            }
            if (entity == "Genre")
            {
                Console.Write("Enter Genre: ");
                string genrekind = Console.ReadLine();
                rest.Post(new Genre() { GenreKind = genrekind }, "genre");
            }
            if (entity == "Label")
            {
                Console.Write("Enter Label Name: ");
                string name = Console.ReadLine();
                rest.Post(new Label() { LabelName = name }, "label");
            }
            if (entity == "Song")
            {
                Console.Write("Enter Song Name: ");
                string songtitle = Console.ReadLine();
                rest.Post(new Song() { SongTitle = songtitle }, "song");
            }

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

            if (entity == "Genre")
            {
                List<Genre> genres = rest.Get<Genre>("genre");
                foreach (var item in genres)
                {
                    Console.WriteLine(item.GenreId + ": " + item.GenreKind);
                }
            }
            Console.ReadLine();

            if (entity == "Label")
            {
                List<Label> labels = rest.Get<Label>("label");
                foreach (var item in labels)
                {
                    Console.WriteLine(item.LabelId + ": " + item.LabelName);
                }
            }
            Console.ReadLine();

            if (entity == "Song")
            {
                List<Song> songs = rest.Get<Song>("song");
                foreach (var item in songs)
                {
                    Console.WriteLine(item.SongId + ": " + item.SongTitle  + "," + item.Album + ","+ item.ReleaseDate + "," + item.Income + "," + item.Rating);
                }
            }
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            if (entity == "Band")
            {
                Console.Write("Enter Band's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Band one = rest.Get<Band>(id, "band");
                Console.Write($"New name [old: {one.BandName}]: ");
                string name = Console.ReadLine();
                one.BandName = name;
                rest.Put(one, "band");
            }
            if (entity == "Genre")
            {
                Console.Write("Enter Genre's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Genre one = rest.Get<Genre>(id, "Genre");
                Console.Write($"New genre [old: {one.GenreKind}]: ");
                string kind = Console.ReadLine();
                one.GenreKind = kind;
                rest.Put(one, "Genre");
            }
            if (entity == "Label")
            {
                Console.Write("Enter Label's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Label one = rest.Get<Label>(id, "label");
                Console.Write($"New name [old: {one.LabelName}]: ");
                string name = Console.ReadLine();
                one.LabelName = name;
                rest.Put(one, "label");
            }
            if (entity == "Song")
            {
                Console.Write("Enter Song's id to update: ");
                int id = int.Parse(Console.ReadLine());
                Song one = rest.Get<Song>(id, "song");
                Console.Write($"New name [old: {one.SongTitle}]: ");
                string name = Console.ReadLine();
                one.SongTitle = name;
                rest.Put(one, "song");
            }
        }
        static void Delete(string entity)
        {
            if (entity == "Band")
            {
                Console.Write("Enter Band's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "band");
            }
            if (entity == "Genre")
            {
                Console.Write("Enter Genre's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "genre");
            }
            if (entity == "Label")
            {
                Console.Write("Enter Label's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "label");
            }
            if (entity == "Song")
            {
                Console.Write("Enter Song's id to delete: ");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, "song");
            }
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
                .Add("Bands", () => bandSubMenu.Show())
                .Add("Genres", () => genreSubMenu.Show())
                .Add("Labels", () => labelSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();


        }
    }
}
