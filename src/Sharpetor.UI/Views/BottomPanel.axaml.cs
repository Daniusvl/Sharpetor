using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Sharpetor.UI.Views
{
    public partial class BottomPanel : UserControl
    {
        public BottomPanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
