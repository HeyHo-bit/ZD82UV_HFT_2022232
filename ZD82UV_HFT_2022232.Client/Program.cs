﻿using System;
using System.Linq;
using ZD82UV_HFT_2022232.Models;
using ZD82UV_HFT_2022232.Repository;

namespace ZD82UV_HFT_2022232.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SongDbContext db = new SongDbContext();
            var items = db.Songs.ToArray();
            ;
        }
    }
}
