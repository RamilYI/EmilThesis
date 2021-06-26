using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using EmilThesis.Calc;
using EmilThesis.Models;
using EmilThesis.Views;
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

        public ResultTableViewModel ResultTable { get; set; }

        public PlotsViewModel Plots { get; set; }

        public DelegateCommand CalcCommand { get; }

        public DelegateCommand OpenTimeSeriesEditorCommand { get; }

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
            if (!this.timeSeriesInputParamters.InputTimeSeries.Any())
            {
                MessageBox.Show("Введите исходные данные в редакторе временного ряда!");
                return;
            }

            var result = this.timeSeriesAnalyzer.Calc(this.timeSeriesInputParamters);
            this.ResultTable = new ResultTableViewModel(result);
            this.Plots = new PlotsViewModel(result, this.timeSeriesInputParamters);
            RaisePropertyChanged(nameof(this.ResultTable));
            RaisePropertyChanged(nameof(this.Plots));
        }

        private void OpenTimeSeriesEditorCommandHandler()
        {
            var timeSeriesEditor = new TimeSeriesEditor(new TimeSeriesEditorViewModel(this.timeSeriesInputParamters.InputTimeSeries));
            timeSeriesEditor.ShowDialog();
        }

        #endregion


        #region Конструктор

        public MainViewModel()
        {
            this.timeSeriesInputParamters = new TimeSeriesInputParameters();
            this.timeSeriesAnalyzer = new TimeSeriesAnalyzer();
            this.Alpha = 0.0;
            this.M = 0;
            this.timeSeriesInputParamters.InputTimeSeries = new ObservableCollection<TimeSeriesItem>()
            {
                new(1, 622), new(2, 620), new(3, 621), new(4, 630), new(5,636), new(6,650), new(7,666), new(8,670),
                new(9,676), new(10,684), new(11,696), new(12,705), new(13,707), new(14,718), new(15,731), new(16,745),
                new(17,758), new(18,773), new(19,787), new(20,807),
                new(21,828), new(22,844), new(23,870), new(24,894), new(25,920), new(26,938), new(27,962), new(28,990),
                new(29,1020), new(30,1050)
            };
            this.CalcCommand = new DelegateCommand(this.CalcCommandHandler);
            this.OpenTimeSeriesEditorCommand = new DelegateCommand(this.OpenTimeSeriesEditorCommandHandler);
        }

        #endregion
    }
}