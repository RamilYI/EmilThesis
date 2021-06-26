using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;

namespace EmilThesis.Models
{
    /// <summary>
    /// Входные данные временного ряда.
    /// </summary>
    public class TimeSeriesInputParameters
    {
        public ObservableCollection<TimeSeriesItem> InputTimeSeries = new ObservableCollection<TimeSeriesItem>();

        public double Alpha;

        public int M;
    }
}