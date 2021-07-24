namespace Sharpetor.CoreLib
{
  public class Template
  {
    public string TemplateName { get; set; }

    public string ShortName { get; set; }

    public string Language { get; set; }

    public string Tag { get; set; }

    public Template() => 
      (TemplateName, ShortName, Language, Tag) = (string.Empty, string.Empty, string.Empty, string.Empty);

    public Template(string template, string shortName, string language, string tag) => 
      (TemplateName, ShortName, Language, Tag) = (template, shortName, language, tag);
  }
}