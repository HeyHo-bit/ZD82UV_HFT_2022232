using System;
using Moq;
using System.Collections.Generic;
using NUnit.Framework;
using ZD82UV_HFT_2022232.Logic;
using ZD82UV_HFT_2022232.Repository;
using ZD82UV_HFT_2022232.Models;
using System.Linq;

namespace ZD82UV_HFT_2022232.Test
{
    [TestFixture]
    public class SongLogicTester
    {
        SongLogic songlogic;
        SongLogic songlogic2;
        GenreLogic genreLogic;
        Mock<IRepository<Song>> mockSongRepository;
        Mock<IRepository<Song>> mockSongRepository2;
        Mock<IRepository<Genre>> mockGenreRepository;

        [SetUp]
        public void Init()
        {
             mockGenreRepository = new Mock<IRepository<Genre>>();
             mockSongRepository = new Mock<IRepository<Song>>();
             mockSongRepository2 = new Mock<IRepository<Song>>();

            var song = new List<Song>()
            {
                new Song { SongId = 1, SongTitle = "Test1",ReleaseDate =  new DateTime(2001, 04, 1),  Album = "testAlbum1", LabelId = 1,Income =100 ,Rating = 1},
                new Song { SongId = 2, SongTitle = "Test2",ReleaseDate = new DateTime(2000, 1, 1),  Album = "TestAlbum2", LabelId = 2, Income = 200, Rating = 2},

            }.AsQueryable();
            songlogic = new SongLogic(mockSongRepository.Object);

            var genre = new List<Genre>()
            {
                new Genre { GenreId = 1, GenreKind="test1kind", SongId =1 ,BandId = 1},
                new Genre { GenreId = 2, GenreKind="test1kind2", SongId =2 ,BandId = 2},


            }.AsQueryable();

            mockSongRepository2.Setup(m => m.ReadAll()).Returns(new List<Song>()
                            {
                new Song { SongId = 4, SongTitle = "Test1",ReleaseDate =  new DateTime(2001, 04, 1),  Album = "testAlbum1", LabelId = 1,Income =100 ,Rating = 1},
                new Song { SongId = 5, SongTitle = "Test2",ReleaseDate = new DateTime(2000, 1, 1),  Album = "TestAlbum2", LabelId = 2, Income = 200, Rating = 2},

            }.AsQueryable());
            songlogic2 = new SongLogic(mockSongRepository2.Object);

            genreLogic = new GenreLogic(mockGenreRepository.Object);

        }

        [Test]
        public void ReadTest()
        {
            var song = new Song() { SongId = 1 };
            //ACT
            try
            {
                //ACT
                songlogic.Read(song.SongId);
            }
            catch
            {

            }


            //ASSERT
            mockSongRepository.Verify(r => r.Read(song.SongId), Times.Once);
        }
        [Test]
        public void DeleteTest()
        {
            var song = new Song() { SongId = 3 };
            //ACT
            try
            {
                //ACT
                songlogic.Delete(song.SongId);
            }
            catch
            {

            }


            //ASSERT
            mockSongRepository.Verify(r => r.Delete(song.SongId), Times.Once);
        }

        [Test]
        public void CreateSongTestWithCorrectTitle()
        {
            var song = new Song() { SongTitle = "Gladiator" };

            //ACT
            songlogic.Create(song);

            //ASSERT
            mockSongRepository.Verify(r => r.Create(song), Times.Once);
        }
        [Test]
        public void CreateSongTestWithinCorrectID()
        {
            var song = new Song() { SongId = 1 };

            try
            {
                //ACT
                songlogic.Create(song);
            }
            catch
            {

            }

            //ASSERT
            mockSongRepository.Verify(r => r.Create(song), Times.Never);
        }

        [Test]
        public void CreateSongTestWithInCorrectTitle()
        {
            var song = new Song() { SongTitle = "8" };
            try
            {
                //ACT
                songlogic.Create(song);
            }
            catch
            {

            }

            //ASSERT
            mockSongRepository.Verify(r => r.Create(song), Times.Never);
        }

        [Test]
        public void BestSong()
        {
            //ACT
            var result = songlogic.BestSong();

            //ASSERT
            Assert.That(result, Is.EqualTo(songlogic.ReadAll()));
            ;
        }

        [Test]
        public void LabelRevenu()
        {
            //Act
            var result = songlogic.LabelRevenu().ToList();
            
            //ASSERT
            Assert.That(result, Is.EqualTo(songlogic.ReadAll()));

        }
        [Test]
        public void TopLabel()
        {
            //Act
            var result = songlogic.TopLabel().ToList();

            //ASSERT
            Assert.That(result, Is.EqualTo(songlogic.ReadAll()));

        }

        [Test]
        public void YearStatisticsTest()
        {
            var actual = songlogic2.YearStatistics().ToList();
            var expected = new List<YearInfo>()
            {
                new YearInfo()
                {
                    Year = 2001,
                    AvgRating = 1,
                    SongNumber = 1
                },
                new YearInfo()
                {
                    Year = 2000,
                    AvgRating = 2,
                    SongNumber = 1
                },
            };
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void MostSong()
        {
            //Act
            var result = genreLogic.MostSong().ToList();

            //ASSERT
            Assert.That(result, Is.EqualTo(genreLogic.ReadAll()));

        }

    }


}
