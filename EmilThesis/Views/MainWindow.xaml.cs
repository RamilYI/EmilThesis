using System.Windows;
using EmilThesis.ViewModels;
using MahApps.Metro.Controls;

namespace EmilThesis.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
        }
    }
}
