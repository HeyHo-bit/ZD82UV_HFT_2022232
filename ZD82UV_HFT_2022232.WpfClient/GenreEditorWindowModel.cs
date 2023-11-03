using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;
using System.Windows;
using ZD82UV_HFT_2022232.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ZD82UV_HFT_2022232.WpfClient
{
    internal class GenreEditorWindowModel: ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Genre> Genres { get; set; }

        private Genre selectedGenre;

        public Genre SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                if (value != null)
                {
                    selectedGenre = new Genre()
                    {
                        GenreKind = value.GenreKind,
                        GenreId = value.GenreId
                    };
                    OnPropertyChanged();
                    (DeleteGenreCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateGenreCommand { get; set; }

        public ICommand DeleteGenreCommand { get; set; }

        public ICommand UpdateGenreCommand { get; set; }

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


        public GenreEditorWindowModel()
        {
            if (!IsInDesignMode)
            {
                Genres = new RestCollection<Genre>("http://localhost:4273/", "Genre", "hub");
                CreateGenreCommand = new RelayCommand(() =>
                {
                    Genres.Add(new Genre()
                    {
                        GenreKind = SelectedGenre.GenreKind
                    });
                });

                UpdateGenreCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Genres.Update(SelectedGenre);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteGenreCommand = new RelayCommand(() =>
                {
                    Genres.Delete(SelectedGenre.GenreId);
                },
                () =>
                {
                    return SelectedGenre != null;
                });
                SelectedGenre = new Genre();

                OpendWindow = new RelayCommand(() =>
                {
                    new NonCrudWindow().ShowDialog();
                });

                OpendWindowBand = new RelayCommand(() =>
                {
                    new BandEditor().ShowDialog();
                });



                OpendWindowLabel = new RelayCommand(() =>
                {
                    new LabelEditorWindowxaml().ShowDialog();
                });
            }

        }
    }
}
