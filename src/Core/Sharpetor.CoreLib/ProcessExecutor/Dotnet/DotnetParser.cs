using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Sharpetor.CoreLib
{
  public class DotnetParser : IDotnetParser
  {
    public List<Template> TemplateListToTable(string template)
    {
      template = template ?? throw new NullReferenceException(nameof(template));

      string trimmed = Regex.Replace(template, @" {2,}", "  ") ?? throw new NullReferenceException(nameof(trimmed));

      List<Template> output = new ();
      foreach(string line in trimmed.Split('\n'))
      {
        if(!string.IsNullOrEmpty(line) && !line.StartsWith("Templates") && !line.StartsWith("---"))
        {
          List<string> splited = line.Split("  ")
            .Where(s => !string.IsNullOrEmpty(s)).
            ToList();
          output.Add(new());
          for(int i = 0; i < splited.Count; i++){
            switch(i){
              case 0:
                output[output.Count-1].TemplateName = splited[i];
              break;
              case 1:
                output[output.Count-1].ShortName = splited[i];
              break;
              case 2:
                output[output.Count-1].Language = splited[i];
              break;
              case 3:
                output[output.Count-1].Tag = splited[i];
              break;
            }
          }
        }
      }
      return output;
    }
  }
}