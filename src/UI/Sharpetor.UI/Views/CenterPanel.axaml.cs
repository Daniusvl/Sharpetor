using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Sharpetor.UI.Views
{
    public partial class CenterPanel : UserControl
    {
        public CenterPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
