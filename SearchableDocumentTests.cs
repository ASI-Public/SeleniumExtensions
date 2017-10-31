using NUnit.Framework;
using OpenQA.Selenium;

namespace ASI.SeleniumExtensions
{
  [TestFixture, Category("SearchableDocument")]
  public class SearchableDocumentTests
  {
    [Test]
    public void CreateSearchableDocument()
    {
      // Arrange
      const string document = "<!DOCTYPE html><html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var children = searchableDocument.Children;

      // Assert
      Assert.That(searchableDocument, Is.Not.Null);
      Assert.That(children.Count, Is.EqualTo(1));
    }

    [Test]
    public void FindElement_ById_OneElement()
    {
      // Arrange
      const string document = "<!DOCTYPE html><html><head><title id=\"elementId\">Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.Id("elementId"));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("title"));
    }

    [Test]
    public void FindElement_ByClassName_OneElement()
    {
      // Arrange
      const string document = "<!DOCTYPE html><html><head><title class=\"elementClass\">Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.ClassName("elementClass"));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("title"));
    }

    [Test]
    public void FindElement_ByXPathType_OneElement()
    {
      // Arrange
      const string xpath = "//title";
      const string document = "<!DOCTYPE html><html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.XPath(xpath));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("title"));
    }

    [Test]
    public void FindElement_ByXPathAttribute_OneElement()
    {
      // Arrange
      const string xpath = "//*[@name='elementName']";
      const string document = "<!DOCTYPE html><html><head><title name=\"elementName\">Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.XPath(xpath));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("title"));
    }

    [Test]
    public void FindElement_ByXPathText_OneElement()
    {
      // Arrange
      const string xpath = "//title[.='Page Title']";
      const string document = "<!DOCTYPE html><html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.XPath(xpath));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("title"));
    }

    [Test]
    public void FindElement_ByXPathTypeAndAttribute_OneElement()
    {
      // Arrange
      const string xpath = "//title[@id='correctTitle']";
      const string document = "<!DOCTYPE html><html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><title id=\"correctTitle\">Page Title</title><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.XPath(xpath));

      // Assert
      Assert.That(element[0].Id, Is.EqualTo("correctTitle"));
    }

    [Test]
    public void FindElement_ByXPathNestedType_OneElement()
    {
      // Arrange
      const string xpath = "//body/title";
      const string document = "<!DOCTYPE html><html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><title id=\"correctTitle\">Page Title</title><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.XPath(xpath));

      // Assert
      Assert.That(element[0].Id, Is.EqualTo("correctTitle"));
    }

    [Test]
    public void FindElement_ByXPathTypeAndIndex_OneElement()
    {
      // Arrange
      const string xpath = "//p[2]";
      const string document = "<!DOCTYPE html><html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><title id=\"correctTitle\">Page Title</title><p>This is a paragraph.</p><p>This is another paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.XPath(xpath));

      // Assert
      Assert.That(element[0].InnerText, Is.EqualTo("This is another paragraph."));
    }

    [Test]
    public void FindElement_ByXPath_AllElements()
    {
      // Arrange
      const string xpath = "//*";
      const string document = "<html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.XPath(xpath));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("html"));
    }

    [Test]
    public void FindElement_ByTagName_OneElement()
    {
      // Arrange
      const string document = "<html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.TagName("h1"));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("h1"));
    }

    [Test]
    public void FindElement_ByLinkText_OneElement()
    {
      // Arrange
      const string document = "<html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.LinkText("This is a Heading"));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("h1"));
    }

    [Test]
    public void FindElement_ByPartialLinkText_OneElement()
    {
      // Arrange
      const string document = "<html><head><title>Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.PartialLinkText("Heading"));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("h1"));
    }

    [Test]
    public void FindElement_ByName_OneElement()
    {
      // Arrange
      const string document = "<!DOCTYPE html><html><head><title name=\"elementName\">Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var element = searchableDocument.FindElements(By.Name("elementName"));

      // Assert
      Assert.That(element[0].ElementType, Is.EqualTo("title"));
    }

    [Test]
    public void FindElement_ByName_NoElements()
    {
      // Arrange
      const string document = "<!DOCTYPE html><html><head><title name=\"elementName\">Page Title</title></head><body><h1>This is a Heading</h1><p>This is a paragraph.</p></body></html> ";
      var searchableDocument = new SearchableDocument(document);

      // Act
      var elements = searchableDocument.FindElements(By.Name("nonExistentName"));

      // Assert
      Assert.That(elements, Is.Empty);
    }
  }
}
