using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using EmilThesis.Calc;
using EmilThesis.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace EmilThesis.ViewModels
{
    /// <summary>
    /// Вьюмодель основного окна.
    /// </summary>
    public class MainViewModel : BindableBase, INotifyDataErrorInfo
    {
        #region Поля и свойства

        /// <summary>
        /// Входные данные временного ряда.
        /// </summary>
        private readonly TimeSeriesInputParameters timeSeriesInputParamters;

        /// <summary>
        /// Анализатор временных рядов.
        /// </summary>
        private readonly TimeSeriesAnalyzer timeSeriesAnalyzer;

        public double Alpha
        {
            get => this.timeSeriesInputParamters.Alpha;
            set
            {
                this.timeSeriesInputParamters.Alpha = value;
                ValidateNum(this.timeSeriesInputParamters.Alpha, nameof(this.Alpha));
                RaisePropertyChanged();
            }
        }

        public int M
        {
            get => this.timeSeriesInputParamters.M;
            set
            {
                this.timeSeriesInputParamters.M = value;
                RaisePropertyChanged();
            }
        }

        public DelegateCommand CalcCommand { get; }

        #endregion

        #region INotifyDataErrorInfo

        private void ValidateNum(double? propValue, string propName)
        {
            ClearErrors(propName);
            if (propValue <= 0.0)
            {
                AddError(propName, "Значение должно превышать нуля!");
            }
        }

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public IEnumerable GetErrors(string propertyName) => _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;

        public bool HasErrors => _errorsByPropertyName.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(propertyName);
            }
        }

        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        #endregion

        #region Методы

        private void CalcCommandHandler()
        {
            this.timeSeriesInputParamters.InputTimeSeries = new List<double>()
            {
                622, 620, 621, 630, 636, 650, 666, 670, 676, 684,
                696, 705, 707, 718, 731, 745, 758, 773, 787, 807,
                828, 844, 870, 894, 920, 938, 962, 990, 1020, 1050
            };

            if (!this.timeSeriesInputParamters.InputTimeSeries.Any())
            {
                MessageBox.Show("Введите исходные данные в редакторе временного ряда!");
                return;
            }

            var result = this.timeSeriesAnalyzer.Calc(this.timeSeriesInputParamters);
        }

        #endregion


        #region Конструктор

        public MainViewModel()
        {
            this.timeSeriesInputParamters = new TimeSeriesInputParameters();
            this.timeSeriesAnalyzer = new TimeSeriesAnalyzer();
            this.Alpha = 0.0;
            this.M = 0;
            this.CalcCommand = new DelegateCommand(this.CalcCommandHandler);
        }

        #endregion
    }
}