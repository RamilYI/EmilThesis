using System;
using System.Collections.Generic;
using System.Threading;
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

        public static double ParseDouble(this string text, double defaultValue = 0D)
        {
            var sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            if (string.IsNullOrEmpty(text))
            {
                return defaultValue;
            }

            text = text.Trim();
            text = text.Replace(".", sep);
            text = text.Replace(",", sep);

            return double.TryParse(text, out var v) ? v : defaultValue;
        }

        public static int ParseInt(this string text, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(text))
            {
                return defaultValue;
            }

            text = text.Trim();

            return int.TryParse(text, out var v) ? v : defaultValue;
        }
    }
}