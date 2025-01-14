﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ZD82UV_HFT_2022232.Models;

namespace ZD82UV_HFT_2022232.WpfClient
{
    internal class MainWindowViewModel : ObservableRecipient
    {
    
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Song> Songs { get; set; }

        private Song selectedSong;

        public Song SelectedSong
        {
            get { return selectedSong; }
            set
            {
                if (value != null)
                {
                    selectedSong = new Song()
                    {
                        SongTitle = value.SongTitle,
                        SongId = value.SongId
                    };
                    OnPropertyChanged();
                    (DeleteSongCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateSongCommand { get; set; }

        public ICommand DeleteSongCommand { get; set; }

        public ICommand UpdateSongCommand { get; set; }

        public ICommand OpendWindow { get; set; }
        public ICommand OpendWindowBand { get; set; }
        public ICommand OpendWindowGenre { get; set; }
        public ICommand OpendWindowLabel { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }


        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Songs = new RestCollection<Song>("http://localhost:4273/", "song", "hub");
                CreateSongCommand = new RelayCommand(() =>
                {
                    Songs.Add(new Song()
                    {
                        SongTitle = SelectedSong.SongTitle
                    });
                });

                UpdateSongCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Songs.Update(SelectedSong);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteSongCommand = new RelayCommand(() =>
                {
                    Songs.Delete(SelectedSong.SongId);
                },
                () =>
                {
                    return SelectedSong != null;
                });
                SelectedSong = new Song();

                OpendWindow = new RelayCommand(() =>
                {
                    new NonCrudWindow().ShowDialog();
                });

                OpendWindowBand = new RelayCommand(() =>
                {
                    new BandEditor().ShowDialog();
                });
                OpendWindowGenre = new RelayCommand(() =>
                {
                    new GenreEditorWindow().ShowDialog();
                }); 
                OpendWindowLabel = new RelayCommand(() =>
                {
                    new LabelEditorWindowxaml().ShowDialog();
                });
            }

        }
    
    }
}
