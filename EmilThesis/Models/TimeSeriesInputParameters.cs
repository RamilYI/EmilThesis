using System.Collections.Generic;
using System.Windows.Documents;

namespace EmilThesis.Models
{
    /// <summary>
    /// Входные данные временного ряда.
    /// </summary>
    public class TimeSeriesInputParameters
    {
        public IEnumerable<double> InputTimeSeries = new List<double>();

        public double Alpha;

        public int M;
    }
}