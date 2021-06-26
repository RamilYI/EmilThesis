using System.Windows.Controls;
using EmilThesis.Models;

namespace EmilThesis.Common
{
    public static class Extensions
    {
        public static void SetToDataGridTSItemDisplayName(this DataGridAutoGeneratingColumnEventArgs eventArgs)
        {
            switch (eventArgs.PropertyName)
            {
                case nameof(TimeSeriesItem.Date):
                    eventArgs.Column.Header = "Дата";
                    break;

                case nameof(TimeSeriesItem.Value):
                    eventArgs.Column.Header = "Значение";
                    break;

                default:
                    break;
            }
        }
    }
}