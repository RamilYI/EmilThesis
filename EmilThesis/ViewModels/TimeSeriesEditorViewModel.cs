using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EmilThesis.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace EmilThesis.ViewModels
{
    /// <summary>
    /// Модель представления редактора временного ряда.
    /// </summary>
    public class TimeSeriesEditorViewModel : BindableBase
    {
        #region Поля и свойства

        private ObservableCollection<TimeSeriesItem> inputTimeSeries;

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

        #endregion

        #region Методы

        private void ImportCommandhandler()
        {
            throw new System.NotImplementedException();
        }

        private void RemoveCommandHandler()
        {
            this.TimeSeriesValues.Remove(this.TimeSeriesValues.Last());
        }

        private void AddCommandHandler()
        {
            this.TimeSeriesValues.Add(new TimeSeriesItem(0, 0.0));
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
        }

        #endregion
    }
}