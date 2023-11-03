using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.WpfClient
{
    internal class NonCrudWindowModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Song> Songs { get; set; }
       

        //public RestCollection<RetriceCollection.BestSo> rest;
        //public RestCollection test = new RestCollection();

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        private string answer;
        public string Answer
        {
            get
            {
                return answer;

            }
            set
            {
                SetProperty(ref answer, value);
            }
        }
        public List<RetriceCollection.BestSo> bestsong { get; set; }
        public List<RetriceCollection.LabelReve> LayelRev { get; set; }

        public List<RetriceCollection.YearInfo> YearInfos { get; set; }

        public List<RetriceCollection.Topla> TopLabelList { get; set; }
        public List<RetriceCollection.MostSo> MostSongList { get; set; }


        public string inputbox { get; set; }
        public ICommand BestSong { get; set; }
        public ICommand LabelRevenu { get; set; }
        public ICommand YearStatistics { get; set; }
        public ICommand TopLabel { get; set; }
        public ICommand MostSong { get; set; }



        public NonCrudWindowModel()
        {
          
            if (!IsInDesignMode)
            {
                MostSong = new RelayCommand(() =>
                {
                    Answer = "";
                    MostSongList = new RestService("http://localhost:4273/").Get<RetriceCollection.MostSo>("/Stat/MostSong");
                    Answer = MostSongList.First().SongName + " : " + MostSongList.First().SongNumber;


                });

                TopLabel = new RelayCommand(() =>
                {
                    Answer = "";
                    TopLabelList = new RestService("http://localhost:4273/").Get<RetriceCollection.Topla>("/Stat/TopLabel");
                    Answer = "Top Label name: " + TopLabelList.First().LabelName + " SongCount: " + TopLabelList.First().SongCount + " Revenu: " + TopLabelList.First().Revenu + "M $";


                });


                YearStatistics = new RelayCommand(() =>
                {
                    Answer = "";
                    YearInfos = new RestService("http://localhost:4273/").GetCollection<RetriceCollection.YearInfo>(inputbox, "/Stat/YearStatistics");
                    foreach (var item in YearInfos)
                    {
                        if (item.Year == int.Parse(inputbox))
                        {
                            Answer += item.Year + ", Number of songs: " + item.SongNumber + ", Avarage rating: " + item.AvgRating + Environment.NewLine;
                        }
                        //Answer += item.Year + ", Number of songs: " + item.SongNumber + ", Avarage rating: " + item.AvgRating + Environment.NewLine;
                    }

                });

                LabelRevenu = new RelayCommand(() =>
                {
                    Answer = "";
                    LayelRev = new RestService("http://localhost:4273/").Get<RetriceCollection.LabelReve>("/Stat/LabelRevenu");
                    foreach (var item in LayelRev)
                    {
                        Answer += "Label Name: "+ item.LabelName + ": " + item.Revenu + "M $" + Environment.NewLine;
                    }
                });

                BestSong = new RelayCommand(() =>
                {
                    Answer = "";
                    bestsong = new RestService("http://localhost:4273/").Get<RetriceCollection.BestSo>("/Stat/BestSong");
                    Answer = "The best Song is: " + bestsong.First().SongName;


                });
 
            }

        }

    }
}
