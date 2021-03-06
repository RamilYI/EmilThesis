using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using EmilThesis.Common;
using EmilThesis.Views;

namespace EmilThesis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var container = ContainerConfig.Configure();
            var mainView = container.Resolve<MainWindow>();
            mainView.Closed += (o, args) => { this.Shutdown(); };
            mainView.Show();
        }
    }
}
