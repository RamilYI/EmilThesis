using System.IO;
using System.Linq;
using System.Text;
using EmilThesis.Models;

namespace EmilThesis.Helpers
{
    public class TimeSeriesResultExporter
    {
        public void ReadFile(string fileName, TimeSeriesResult result)
        {
            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                var newLine = "Дата;S1;S2;Линейная модель;Прогнозированные значения";
                writer.WriteLine(newLine);
                for (var i = 0; i < result.S1.Count(); i++)
                {
                    newLine = $"{result.S1.ElementAtOrDefault(i)?.Date};{result.S1.ElementAtOrDefault(i)?.Value};{result.S2.ElementAtOrDefault(i)?.Value};{result.LinearModel.ElementAtOrDefault(i)?.Value};{result.PredictedValues.ElementAtOrDefault(i)?.Value}";
                    writer.WriteLine(newLine);
                }
            }
        }
    }
}