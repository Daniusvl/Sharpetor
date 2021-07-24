using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sharpetor.UILib
{
  public class BaseViewModel : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new (propertyName));
    }
  }
}