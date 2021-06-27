using EmilThesis.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using EmilThesis.Common;

namespace EmilThesis.Helpers
{
    /// <summary>
    /// Читатель csv файла с данными временного ряда.
    /// </summary>
    public class TimeSeriesReader
    {
        private ObservableCollection<TimeSeriesItem> inputTimeSeries;

        public void ReadFile(string fileName)
        {
            using (var readFile = new StreamReader(fileName))
            {
                string line;
                readFile.ReadLine();
                var regex = new Regex(";(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                while ((line = readFile.ReadLine()) != null)
                {
                    var columns = regex.Split(line);
                    if (columns.Length != 2 || columns.Any(x => x.Equals(string.Empty)))
                    {
                        continue;
                    }

                    var date = columns[0].ParseInt();
                    var value = columns[1].ParseDouble();
                    var timeSeriesItem = new TimeSeriesItem(date, value);
                    this.inputTimeSeries.Add(timeSeriesItem);
                }
            }
        }

        public TimeSeriesReader(ObservableCollection<TimeSeriesItem> inputTimeSeries)
        {
            this.inputTimeSeries = inputTimeSeries;
        }
    }
}