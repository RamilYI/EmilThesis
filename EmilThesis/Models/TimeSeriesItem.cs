using System.ComponentModel;
using Prism.Mvvm;

namespace EmilThesis.Models
{
    /// <summary>
    /// Единица временного ряда.
    /// </summary>
    public class TimeSeriesItem : BindableBase
    {
        private int date;
        public int Date
        {
            get => this.date;
            set
            {
                this.date = value;
                RaisePropertyChanged();
            }
        }

        private double value;
        public double Value
        {
            get => this.value;
            set
            {
                this.value = value;
                RaisePropertyChanged();
            }
        }

        public TimeSeriesItem(int date, double value)
        {
            this.Date = date;
            this.Value = value;
        }
    }
}