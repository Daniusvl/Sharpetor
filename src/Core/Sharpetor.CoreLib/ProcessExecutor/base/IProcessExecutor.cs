using System;

namespace Sharpetor.CoreLib
{
  public interface IProcessExecutor
  {
    string ExecuteCommand(string argument);
    
    void ExecuteCommand(string argument, Action<string> handler);
  }
}