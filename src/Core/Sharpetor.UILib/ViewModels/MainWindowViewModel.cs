using System;

namespace Sharpetor.UILib
{
    public class MainWindowViewModel : BaseViewModel
    {
        public LeftPanelViewModel LeftPanelDataContext { get; set; }
    
        public MainWindowViewModel(LeftPanelViewModel leftPanelDataContext)
        {
            LeftPanelDataContext = leftPanelDataContext;
        }
    }
}