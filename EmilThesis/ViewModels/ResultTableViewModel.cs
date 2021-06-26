using System.Collections.Generic;
using System.Collections.ObjectModel;
using EmilThesis.Models;

namespace EmilThesis.ViewModels
{
    /// <summary>
    /// Модель представления результирующей таблицы.
    /// </summary>
    public class ResultTableViewModel
    {
        private readonly TimeSeriesResult result;

        public IEnumerable<TimeSeriesItem> S1 => this.result.S1;
        public IEnumerable<TimeSeriesItem> S2 => this.result.S2;
        public IEnumerable<TimeSeriesItem> LinearModel => this.result.LinearModel;
        public IEnumerable<TimeSeriesItem> PredictedValues => this.result.PredictedValues;

        public ResultTableViewModel(TimeSeriesResult result)
        {
            this.result = result;
        }
    }
}