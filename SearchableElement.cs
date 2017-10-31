using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using OpenQA.Selenium;

namespace SeleniumExtensions
{
  public class SearchableElement
  {
    readonly HtmlNode _node;

    public SearchableElement(IWebElement webElement)
      : this(webElement.OuterHtml())
    {
    }

    public SearchableElement(string element) =>
      _node = HtmlNode.CreateNode(element);

    internal SearchableElement(HtmlNode node) =>
      _node = node;

    public string Id => _node.Id;

    public string Name => _node.Attributes.Contains("name") ?
      _node.Attributes["name"].Value : string.Empty;

    public string ElementType => _node.Name;

    public string InnerText => _node.InnerHtml;

    public string Text => _node.OuterHtml;

    public IReadOnlyList<SearchableElement> Children =>
      _node.ChildNodes.Select(c => new SearchableElement(c)).ToList();

    public IReadOnlyDictionary<string, string> Attributes =>
      _node.Attributes.ToDictionary(
        attribute => attribute.Name.EndsWith("/")
          ? attribute.Name.Substring(0, attribute.Name.Length - 1)
          : attribute.Name,
        attribute => attribute.Value);

    public IReadOnlyList<string> Classes => Attributes.TryGetValue("class", out var value)
      ? value.Trim().Length == 0
        ? (IReadOnlyList<string>)new string[0]
        : value.Split(' ').ToList()
      : new string[0];
  }
}
