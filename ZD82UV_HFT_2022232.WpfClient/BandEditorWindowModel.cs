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
    internal class BandEditorWindowModel: ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Band> Bands { get; set; }

        private Band selectedBand;

        public Band SelectedBand
        {
            get { return selectedBand; }
            set
            {
                if (value != null)
                {
                    selectedBand = new Band()
                    {
                        BandName = value.BandName,
                        BandId = value.BandId
                    };
                    OnPropertyChanged();
                    (DeleteBandCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateBandCommand { get; set; }

        public ICommand DeleteBandCommand { get; set; }

        public ICommand UpdateBandCommand { get; set; }

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


        public BandEditorWindowModel()
        {
            if (!IsInDesignMode)
            {
                Bands = new RestCollection<Band>("http://localhost:4273/", "Band", "hub");
                CreateBandCommand = new RelayCommand(() =>
                {
                    Bands.Add(new Band()
                    {
                        BandName = SelectedBand.BandName
                    });
                });

                UpdateBandCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Bands.Update(SelectedBand);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteBandCommand = new RelayCommand(() =>
                {
                    Bands.Delete(SelectedBand.BandId);
                },
                () =>
                {
                    return SelectedBand != null;
                });
                SelectedBand = new Band();

                OpendWindow = new RelayCommand(() =>
                {
                    new NonCrudWindow().ShowDialog();
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
