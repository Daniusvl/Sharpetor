using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Autofac;

namespace Sharpetor.UI.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
