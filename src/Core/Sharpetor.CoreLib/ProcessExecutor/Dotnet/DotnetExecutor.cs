using System;
using System.Diagnostics;

namespace Sharpetor.CoreLib
{
  public class DotnetExecutor : IProcessExecutor
  {
    public const string Dotnet = "dotnet";

    public string ExecuteCommand(string argument){
      string response = string.Empty;
      Action<object, DataReceivedEventArgs> readOutput = (sender, args) => {
        response += $"{args.Data}\n";
      };

      Process process = new(){
        StartInfo = new(){
          FileName = "dotnet",
          Arguments = argument,
          UseShellExecute = false,
          RedirectStandardOutput = true,
          RedirectStandardError = true
        }
      };
        process.OutputDataReceived += new(readOutput);
        process.ErrorDataReceived += new(readOutput);

      process.EnableRaisingEvents = true;
      process.Start();
      process.BeginOutputReadLine();
      process.BeginErrorReadLine();
      process.WaitForExit();
      return response;
    }

    public void ExecuteCommand(string argument, Action<string> handler){

    }
  }

}