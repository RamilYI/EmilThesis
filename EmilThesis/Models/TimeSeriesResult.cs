using System.Collections.Generic;
using System.Windows.Documents;

namespace EmilThesis.Models
{
    /// <summary>
    /// Выходные данные временного ряда.
    /// </summary>
    public class TimeSeriesResult
    {
        /// <summary>
        /// Временной ряд при простом экспоненциальном сглаживании.
        /// </summary>
        public IEnumerable<double> S1 = new List<double>();

        /// <summary>
        /// Временной ряд при двойном экспоненциальном сглаживании.
        /// </summary>
        public IEnumerable<double> S2 = new List<double>();

        /// <summary>
        /// Линейная модель.
        /// </summary>
        public IEnumerable<double> LinearModel = new List<double>();
    }
}