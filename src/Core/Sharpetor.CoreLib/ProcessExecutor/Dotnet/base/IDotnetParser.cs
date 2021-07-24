using System.Collections.Generic;

namespace Sharpetor.CoreLib
{
  public interface IDotnetParser
  {
    List<Template> TemplateListToTable(string template);
  }
}