using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Sharpetor.UI.Views
{
    public partial class LeftPanel : UserControl
    {
        public LeftPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
