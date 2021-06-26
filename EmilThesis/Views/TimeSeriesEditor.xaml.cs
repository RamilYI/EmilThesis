using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EmilThesis.Common;
using EmilThesis.Models;
using EmilThesis.ViewModels;

namespace EmilThesis.Views
{
    /// <summary>
    /// Interaction logic for TimeSeriesEditor.xaml
    /// </summary>
    public partial class TimeSeriesEditor : Window
    {
        public TimeSeriesEditor(TimeSeriesEditorViewModel timeSeriesEditorViewModel)
        {
            InitializeComponent();
            DataContext = timeSeriesEditorViewModel;
        }

        private void DataGrid_OnAutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.SetToDataGridTSItemDisplayName();
        }
    }
}
