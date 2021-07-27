using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Autofac;
using Sharpetor.UILib;

namespace Sharpetor.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            MainWindowViewModel context = ContainerFactory.Container.Resolve<MainWindowViewModel>();
            context.LeftPanelDataContext.MainWindow = this;
           
            DataContext = context;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void NewProjectClick(object sender, RoutedEventArgs e)
        {
            ContainerFactory.Container
                .Resolve<NewProjectView>()
                .ShowDialog(this);
        }
    }
}
