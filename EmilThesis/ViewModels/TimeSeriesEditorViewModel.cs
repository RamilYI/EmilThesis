using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using EmilThesis.Helpers;
using EmilThesis.Models;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace EmilThesis.ViewModels
{
    /// <summary>
    /// Модель представления редактора временного ряда.
    /// </summary>
    public class TimeSeriesEditorViewModel : BindableBase
    {
        #region Поля и свойства

        private ObservableCollection<TimeSeriesItem> inputTimeSeries;
        private readonly TimeSeriesReader timeSeriesReader;

        public ObservableCollection<TimeSeriesItem> TimeSeriesValues
        {
            get => this.inputTimeSeries;
            set
            {
                this.inputTimeSeries = value;
            }
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand RemoveCommand { get; }
        public DelegateCommand ImportCommand { get; }
        public DelegateCommand ClearCommand { get; set; }

        #endregion

        #region Методы

        private void ImportCommandhandler()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV (Semicolon delimitted) (*.csv)|*.csv",
                InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectTemplates")
            };
            var result = openFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                this.timeSeriesReader.ReadFile(openFileDialog.FileName);
            }
        }

        private void RemoveCommandHandler()
        {
            if (this.TimeSeriesValues.Any())
            {
                this.TimeSeriesValues.Remove(this.TimeSeriesValues.Last());
            }
        }

        private void AddCommandHandler()
        {
            this.TimeSeriesValues.Add(new TimeSeriesItem(0, 0.0));
            RaisePropertyChanged(nameof(this.TimeSeriesValues));
        }

        private void ClearCommandHandler()
        {
            this.TimeSeriesValues.Clear();
            RaisePropertyChanged(nameof(this.TimeSeriesValues));
        }

        #endregion

        #region Конструктор

        public TimeSeriesEditorViewModel(ObservableCollection<TimeSeriesItem> inputTimeSeries)
        {
            this.inputTimeSeries = inputTimeSeries;
            this.AddCommand = new DelegateCommand(this.AddCommandHandler);
            this.RemoveCommand = new DelegateCommand(this.RemoveCommandHandler);
            this.ImportCommand = new DelegateCommand(this.ImportCommandhandler);
            this.ClearCommand = new DelegateCommand(this.ClearCommandHandler);
            this.timeSeriesReader = new TimeSeriesReader(inputTimeSeries);
        }

        #endregion
    }
}