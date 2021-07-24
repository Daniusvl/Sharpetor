using System;
using System.Windows.Input;

namespace Sharpetor.UILib
{
  public class Command : ICommand
  {
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    public Command(Action<object> execute)
    {
      _execute = execute;
      _canExecute = parameter => true;
    }

    public Command(Action<object> execute, Func<object, bool> canExecute)
    {
      _execute = execute;
      _canExecute = canExecute;
    }

    public bool CanExecute(object parameter){
      return _canExecute?.Invoke(parameter) ?? false;
    }

    public void Execute(object parameter){
      _execute?.Invoke(parameter);
    }

    public event EventHandler CanExecuteChanged;
  }
}