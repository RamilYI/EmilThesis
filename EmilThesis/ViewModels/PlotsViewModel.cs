using System.Collections.Generic;
using EmilThesis.Models;
using OxyPlot;
using OxyPlot.Series;

namespace EmilThesis.ViewModels
{
    public class PlotsViewModel
    {
        private readonly TimeSeriesResult result;
        private readonly TimeSeriesInputParameters inputParameters;

        public PlotModel S1 { get; } = new PlotModel();
        public PlotModel S2 { get; } = new PlotModel();
        public PlotModel LinearModel { get; } = new PlotModel();

        private void InitPlotModel(PlotModel plotModel, IEnumerable<TimeSeriesItem> results, string title)
        {
            var inputSeries = new LineSeries
            {
                Title = "Исходные данные"
            };

            foreach (var seriesItem in this.inputParameters.InputTimeSeries)
            {
                inputSeries.Points.Add(new DataPoint(seriesItem.Date, seriesItem.Value));
            }

            var series = new LineSeries
            {
                Title = title
            };

            foreach (var resItem in results)
            {
                series.Points.Add(new DataPoint(resItem.Date, resItem.Value));
            }

            plotModel.Series.Add(inputSeries);
            plotModel.Series.Add(series);
        }

        public PlotsViewModel(TimeSeriesResult result, TimeSeriesInputParameters inputParameters)
        {
            this.result = result;
            this.inputParameters = inputParameters;
            this.InitPlotModel(this.S1, this.result.S1, nameof(this.S1));
            this.InitPlotModel(this.S2, this.result.S2, nameof(this.S2));
            this.InitPlotModel(this.LinearModel, this.result.LinearModel, nameof(this.LinearModel));
        }
    }
}