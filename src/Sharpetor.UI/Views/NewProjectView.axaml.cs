using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Sharpetor.UILib;
using Autofac;
using System;

namespace Sharpetor.UI.Views
{
    public partial class NewProjectView : Window
    {
        public NewProjectView()
        {
            InitializeComponent();
            NewProjectViewModel context = ContainerFactory.Container.Resolve<NewProjectViewModel>();
            context.CloseThis = () => this.Close();
            DataContext = context;
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}