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
        /// Временной ряд при экспоненциальном сглаживании первого порядка.
        /// </summary>
        public IEnumerable<TimeSeriesItem> S1 = new List<TimeSeriesItem>();

        /// <summary>
        /// Временной ряд при экспоненциальном сглаживании второго порядка.
        /// </summary>
        public IEnumerable<TimeSeriesItem> S2 = new List<TimeSeriesItem>();

        /// <summary>
        /// Линейная модель.
        /// </summary>
        public IEnumerable<TimeSeriesItem> LinearModel = new List<TimeSeriesItem>();

        /// <summary>
        /// Предсказанные значения.
        /// </summary>
        public IEnumerable<TimeSeriesItem> PredictedValues = new List<TimeSeriesItem>();
    }
}