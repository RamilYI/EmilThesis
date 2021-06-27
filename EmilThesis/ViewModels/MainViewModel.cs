using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using EmilThesis.Calc;
using EmilThesis.Helpers;
using EmilThesis.Models;
using EmilThesis.Views;
using Microsoft.Win32;
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

        public DelegateCommand ExportCommand { get; set; }
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
        private readonly TimeSeriesResultExporter timeSeriesResultExporter = new TimeSeriesResultExporter();
        private TimeSeriesResult timeSeriesResult;

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

            this.timeSeriesResult = this.timeSeriesAnalyzer.Calc(this.timeSeriesInputParamters);
            this.ResultTable = new ResultTableViewModel(this.timeSeriesResult);
            this.Plots = new PlotsViewModel(this.timeSeriesResult, this.timeSeriesInputParamters);
            RaisePropertyChanged(nameof(this.ResultTable));
            RaisePropertyChanged(nameof(this.Plots));
        }

        private void OpenTimeSeriesEditorCommandHandler()
        {
            var timeSeriesEditor = new TimeSeriesEditor(new TimeSeriesEditorViewModel(this.timeSeriesInputParamters.InputTimeSeries));
            timeSeriesEditor.ShowDialog();
        }

        private void ExportCommandHandler()
        {
            if (this.timeSeriesResult == null)
            {
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV (Semicolon delimitted) (*.csv)|*.csv",
            };
            var result = saveFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                this.timeSeriesResultExporter.ReadFile(saveFileDialog.FileName, this.timeSeriesResult);
            }
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
            this.ExportCommand = new DelegateCommand(this.ExportCommandHandler);
            this.OpenTimeSeriesEditorCommand = new DelegateCommand(this.OpenTimeSeriesEditorCommandHandler);
            this.timeSeriesResultExporter = new TimeSeriesResultExporter();
        }

        #endregion
    }
}