using System;
using System.IO;
using System.Collections.Generic;
using Sharpetor.CoreLib;
using Prism.Events;

namespace Sharpetor.UILib
{
  public class NewProjectViewModel : BaseViewModel
  {
    public Action CloseThis { get; set; }

    private readonly IProcessExecutor _dotnetExecutor;
    private readonly IEventAggregator _eventAggregator;

    public Command CreateProjectCmd{ get; }

    private void ExecuteCreateProject(object parameter)
    {
      if(!Directory.Exists(Path)){
        ErrorMessage = string.Empty;
        
        Directory.CreateDirectory(Path);
        
        //                             Remove "dotnet " from Command
        _dotnetExecutor.ExecuteCommand(Command.Substring(7));
        _eventAggregator.GetEvent<ProjectCreatedEvent>().Publish(Path);
        CloseThis?.Invoke();
      }
      else{
        ErrorMessage = "Specified directory already exists.";
      }
    }

    public NewProjectViewModel(IProcessExecutor dotnetExecutor, IDotnetParser dotnetParser, IEventAggregator eventAggregator)
    {
      _dotnetExecutor = dotnetExecutor ?? throw new NullReferenceException(nameof(dotnetExecutor));
      _eventAggregator = eventAggregator ?? throw new NullReferenceException(nameof(eventAggregator));

      string response = dotnetExecutor.ExecuteCommand("new -l");
      Templates = dotnetParser.TemplateListToTable(response) ?? throw new Exception();
      SelectedTemplate = Templates[0];

      Path = Directory.GetCurrentDirectory();
      SetCommand();

      CreateProjectCmd = new(ExecuteCreateProject);
    }

    private void SetCommand()
    {
      Command = $"dotnet new {template} -o {Path}";
    }

    private string errorMessage;

    public string ErrorMessage{
      get => errorMessage;
      set{
        errorMessage = value;
        OnPropertyChanged();
      }
    }

    private List<Template> templates;

    public List<Template> Templates{
      get => templates;
      set{
        templates = value;
        OnPropertyChanged();
      }
    }

    private Template selectedTemplate;

    public Template SelectedTemplate{
      get => selectedTemplate;
      set{
        selectedTemplate = value;
        Template = selectedTemplate.ShortName;
        OnPropertyChanged();
      }
    }

    private string command;
    
    public string Command{
      get => command;
      set{
        command = value;
        OnPropertyChanged();
      }
    }

    private string template;

    public string Template{
      get => template;
      set{
        template = value;
        SetCommand();
        OnPropertyChanged();
      }
    }

    private string path;

    public string Path{
      get => path;
      set{
        path = value;
        SetCommand();
        OnPropertyChanged();
      }
    }
  }
}