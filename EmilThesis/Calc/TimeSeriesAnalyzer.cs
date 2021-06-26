using System;
using System.Collections.Generic;
using System.Linq;
using EmilThesis.Models;

namespace EmilThesis.Calc
{
    /// <summary>
    /// Анализатор временных рядов.
    /// </summary>
    public class TimeSeriesAnalyzer
    {
        /// <summary>
        /// Рассчитать.
        /// </summary>
        /// <param name="timeSeriesInputParamters">Входные данные.</param>
        /// <returns>Результат.</returns>
        public TimeSeriesResult Calc(TimeSeriesInputParameters timeSeriesInputParamters)
        {
            var x = timeSeriesInputParamters.InputTimeSeries;
            var n = x.Count;
            var t = x.Select(x => x.Date).ToList();

            var sum_x = 0.0; //вычисление коэффициентов a0 и a1
            var sum_y = 0.0;
            var sum_xx = 0.0;
            var sum_xy = 0.0;
            for (int i = 0; i < n; i++)
            {
                sum_x += x[i].Date;
                sum_y += x[i].Value;
                sum_xx += ((double)x[i].Date * x[i].Date);
                sum_xy += (x[i].Date * x[i].Value);
            }
            double a1 = (n * sum_xy - sum_x * sum_y) / (n * sum_xx - sum_x * sum_x);
            double a0 = (sum_y - a1 * sum_x) / n;

            var timeSeriesResult = new TimeSeriesResult();
            var s1 = new double[n];
            var s2 = new double[n];
            linearModel(x.Select(x => x.Value).ToArray(), timeSeriesInputParamters.Alpha, ref s1, ref s2, a0, a1, 0);
            timeSeriesResult.S1 = t.Zip(s1, (first, second) => new TimeSeriesItem((int)first, second)).ToList();
            timeSeriesResult.S2 = t.Zip(s2, (first, second) => new TimeSeriesItem((int)first, second)).ToList();
            var lm = linearModel(x.Select(x => x.Value).ToArray(), timeSeriesInputParamters.Alpha, ref s1, ref s2, a0, a1, timeSeriesInputParamters.M);
            timeSeriesResult.LinearModel = t.Zip(lm, (first, second) => new TimeSeriesItem((int)first, second)).ToList();
            timeSeriesResult.PredictedValues = timeSeriesResult.LinearModel.TakeLast(timeSeriesInputParamters.M);
            return timeSeriesResult;
        }

        static double M(double[] a)
        {
            double res = 0.0;
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                res += a[i];
            }
            res = res / n;
            return res;
        }

        static double dispersion(double[] a)
        {
            double res = 0.0;
            int n = a.Length;
            double avg = M(a);
            for (int i = 0; i < n; i++)
            {
                res += Math.Pow((a[i] - avg), 2);
            }
            res = res / n;
            return res;
        }

        static double[] constModel(double[] a, double alpha)
        {
            double[] res = new double[a.Length];
            double beta = 1 - alpha;

            double average = 0.0;
            for (int i = 0; i < 5; i++)
            {
                average += a[i];
            }
            average /= 5;

            res[0] = average;
            for (int i = 0; i < a.Length - 1; i++)
            {
                res[i + 1] = alpha * a[i] + beta * res[i];
            }
            return res;
        }

        static double error(double[] a, double[] predictions, double degree)
        {
            double res = 0.0;
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                res += Math.Pow((a[i] - predictions[i]), 2);
            }
            res = res / (n - degree - 1);
            return res;
        }

        static double[] linearModel(double[] a, double alpha, ref double[] s1, ref double[] s2, double a0, double a1, int m)
        {
            int n = a.Length;
            double beta = 1 - alpha;
            s1[0] = a0 - (beta / alpha) * a1;
            s2[0] = a0 - (2 * beta / alpha) * a1;

            double[] a0t = new double[n - m];
            double[] a1t = new double[n - m];
            double[] xt = new double[n];

            a0t[0] = a0;
            a1t[0] = a1;
            double x_t = a0 - a1;
            xt[0] = x_t;

            for (int i = 1; i < n - m; i++)
            {
                s1[i] = alpha * a[i] + beta * s1[i - 1];
                s2[i] = alpha * s1[i] + beta * s2[i - 1];
                xt[i] = (a0t[i - 1] + a1t[i - 1]);
                x_t = xt[i];
                a0t[i] = a[i] + beta * beta * (x_t - a[i]);
                a1t[i] = a1t[i - 1] + alpha * alpha * (x_t - a[i]);
            }

            double a0_last = a0t[n - m - 1]; //оценки коэффициентов Ai для последнего вычисленного значения xt
            double a1_last = a1t[n - m - 1];
            for (int i = 0; i < m; i++)
            {
                xt[n - m + i] = a0_last + (i + 1) * a1_last; //i+1 - количество интервалов с последнего наблюдавшегося значения
            }
            return xt;
        }
    }
}