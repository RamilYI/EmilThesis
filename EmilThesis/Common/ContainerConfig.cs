using Autofac;
using EmilThesis.ViewModels;
using EmilThesis.Views;

namespace EmilThesis.Common
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<MainWindow>().AsSelf();
            return builder.Build();
        }
    }
}