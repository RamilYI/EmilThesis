using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
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
                if (value < 0)
                {
                    MessageBox.Show("Значение не может быть отрицательным!");
                    return;
                }

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