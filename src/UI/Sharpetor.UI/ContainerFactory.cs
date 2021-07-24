using Autofac;
using Sharpetor.CoreLib;
using Sharpetor.UILib;
using Sharpetor.UI.Views;

namespace Sharpetor.UI
{
  public static class ContainerFactory
  {
    public static IContainer Container { get; }

    static ContainerFactory(){
      ContainerBuilder builder = new();
      
      builder.RegisterType<MainWindow>()
        .AsSelf();

      builder.RegisterType<NewProjectView>()
        .AsSelf();

      builder.RegisterType<MainWindowViewModel>()
        .AsSelf();

      builder.RegisterType<NewProjectViewModel>()
        .AsSelf();

      builder.RegisterType<DotnetExecutor>()
        .As<IProcessExecutor>();

      builder.RegisterType<DotnetParser>()
        .As<IDotnetParser>();
        
      Container = builder.Build();
    }
  }

}