using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Autofac;
using Sharpetor.UILib;
using System;

namespace Sharpetor.UI.Views
{
    public partial class AddFileView : Window
    {
        public void SetGetName(Action<string> getName)
        {
            if (DataContext is AddFileViewModel ctx) ctx.GetName = getName;
        }

        public AddFileView()
        {
            InitializeComponent();

            AddFileViewModel context = ContainerFactory.Container.Resolve<AddFileViewModel>();
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
