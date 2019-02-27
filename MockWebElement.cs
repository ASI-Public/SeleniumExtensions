using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using OpenQA.Selenium;
using Scalpel;

namespace ASI.SeleniumExtensions
{
  [Remove]
  internal class MockWebElement : IWebElement
  {
    private readonly SearchableDocument _dom;

    public MockWebElement(string html)
    {
      _dom = new SearchableDocument(html);
    }

    public IWebElement FindElement(By by)
    {
      return new MockWebElement(_dom.FindElements(by).First().Text);
    }

    public ReadOnlyCollection<IWebElement> FindElements(By by)
    {
      var elems = _dom.FindElements(by);
      var list = elems.Select(elem => new MockWebElement(elem.Text)).Cast<IWebElement>().ToArray();

      return new ReadOnlyCollection<IWebElement>(list);
    }

    public void Clear()
    {
      throw new NotImplementedException();
    }

    public void SendKeys(string text)
    {
      throw new NotImplementedException();
    }

    public void Submit()
    {
      throw new NotImplementedException();
    }

    public void Click()
    {
      throw new NotImplementedException();
    }

    public string GetAttribute(string attributeName)
    {
      switch (attributeName)
      {
        case "outerHTML":
          return _dom.BaseElement.Text;
        case "innerHTML":
          return _dom.BaseElement.InnerText;
        default:
          return _dom.BaseElement.Attributes.ContainsKey(attributeName)
            ? _dom.BaseElement.Attributes[attributeName]
            : string.Empty;
      }
    }

    public string GetProperty(string propertyName)
    {
      return _dom.BaseElement.Attributes.ContainsKey(propertyName)
        ? _dom.BaseElement.Attributes[propertyName]
        : string.Empty;
    }

    public string GetCssValue(string propertyName)
    {
      return _dom.BaseElement.Classes.FirstOrDefault(c => c.Contains(propertyName));
    }

    public string TagName => _dom.BaseElement.ElementType;
    public string Text => _dom.BaseElement.InnerText;
    public bool Enabled { get; }
    public bool Selected { get; }
    public Point Location { get; }
    public Size Size { get; }
    public bool Displayed { get; }
  }
}