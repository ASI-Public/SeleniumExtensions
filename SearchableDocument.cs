using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using OpenQA.Selenium;

namespace ASI.SeleniumExtensions
{
  public class SearchableDocument
  {
    public SearchableDocument(IWebElement webElement)
      : this(webElement.OuterHtml())
    {
    }

    public SearchableDocument(string document)
    {
      PageSource = new HtmlDocument();
      PageSource.LoadHtml(document);
    }

    private HtmlDocument PageSource { get; }
    public SearchableElement BaseElement => FindElements(By.XPath("//*"))[0];

    public IReadOnlyList<SearchableElement> Children => PageSource.DocumentNode.ChildNodes
      .Where(child => child.NodeType == HtmlNodeType.Element)
      .Select(child => new SearchableElement(child)).ToList();

    public IReadOnlyList<SearchableElement> FindElements(By by)
    {
      var request = by.ToString().Split(new[] {':'}, 2);

      switch (request[0])
      {
        case "By.Id":
          return FindElementsByXPath($"//*[@id='{request[1].Trim()}']");
        case "By.ClassName[Contains]":
          return FindElementsByXPath($"//*[contains(concat(' ', @class, ' '), ' {request[1].Trim()} ')]");
        case "By.XPath":
          return FindElementsByXPath(request[1].Trim());
        case "By.TagName":
          return FindElementsByXPath($"//{request[1].Trim()}");
        case "By.LinkText":
          return FindElementsByXPath($"//*[.='{request[1].Trim()}']");
        case "By.PartialLinkText":
          return FindElementsByXPath($"//*[contains(text(),'{request[1].Trim()}')]");
        case "By.Name":
          return FindElementsByXPath($"//*[@name='{request[1].Trim()}']");
        default:
          return new SearchableElement[0];
      }
    }

    private IReadOnlyList<SearchableElement> FindElementsByXPath(string path)
    {
      return PageSource.DocumentNode.SelectNodes(path.Trim())?.Select(node =>
               new SearchableElement(node)).ToList() ??
             (IReadOnlyList<SearchableElement>) new SearchableElement[0];
    }
  }
}