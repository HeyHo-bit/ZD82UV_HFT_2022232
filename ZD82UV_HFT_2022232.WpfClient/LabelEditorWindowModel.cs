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
    internal class LabelEditorWindowModel: ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }
        public RestCollection<Label> Labels { get; set; }

        private Label selectedLabel;

        public Label SelectedLabel
        {
            get { return selectedLabel; }
            set
            {
                if (value != null)
                {
                    selectedLabel = new Label()
                    {
                        LabelName = value.LabelName,
                        LabelId = value.LabelId
                    };
                    OnPropertyChanged();
                    (DeleteLabelCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateLabelCommand { get; set; }

        public ICommand DeleteLabelCommand { get; set; }

        public ICommand UpdateLabelCommand { get; set; }

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


        public LabelEditorWindowModel()
        {
            if (!IsInDesignMode)
            {
                Labels = new RestCollection<Label>("http://localhost:4273/", "Label", "hub");
                CreateLabelCommand = new RelayCommand(() =>
                {
                    Labels.Add(new Label()
                    {
                        LabelName = SelectedLabel.LabelName
                    });
                });

                UpdateLabelCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Labels.Update(SelectedLabel);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteLabelCommand = new RelayCommand(() =>
                {
                    Labels.Delete(SelectedLabel.LabelId);
                },
                () =>
                {
                    return SelectedLabel != null;
                });
                SelectedLabel = new Label();

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
   
            }

        }
    }
}
