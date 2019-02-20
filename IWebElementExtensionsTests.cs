using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace ASI.SeleniumExtensions
{
  [TestFixture]
  // ReSharper disable once InconsistentNaming
  public class IWebElementExtensionsTests
  {
    [Test]
    public void ToDataTable_NotTableElement()
    {
      // Arrange
      var element = Mock.Of<IWebElement>(e =>
        e.TagName == "body");

      // Act
      // ReSharper disable once ConvertToLocalFunction
      TestDelegate handler = () => element.ToDataTable();

      // Assert
      Assert.That(handler, Throws.ArgumentException);
    }

    [Test]
    public void ToDataTable_CorrectColumnCount()
    {
      // Arrange
      const string html = "<table class=\"wikitable\"><tbody><tr><th> Date </th><th> Build Number </th><th> Modification </th><th> Dependencies</th></tr><tr><td> N/A </td><td> TBA </td><td> Initial Release </td><td> <a href=\"/wiki/Environment_Management\" title=\"Environment Management\">Environment Management</a> TBA</td></tr></tbody></table>";

      var element = new MockWebElement(html);

      // Act
      var table = element.ToDataTable();

      // Assert
      Assert.That(table, Is.Not.Null);
      Assert.That(table.Columns.Count, Is.EqualTo(4));
    }

    [Test]
    public void ToDataTable_WithTHeadAndTBodyElement()
    {
      // Arrange
      const string html = "<table><thead><tr><th>Item 1</th><th>Item 2</th><th>Item 3</th></tr></thead><tbody><tr><td>1.1</td><td>2.1</td><td>3.1</td></tr><tr><td>1.2</td><td>2.2</td><td>3.2</td></tr></tbody></table>";
      var element = new MockWebElement(html);

      // Act
      var table = element.ToDataTable();

      // Assert
      Assert.That(table.Columns.Count, Is.EqualTo(3));
      Assert.That(table.Columns[1].Caption, Is.EqualTo("Item 2"));
      Assert.That(((SearchableElement)table.Rows[1][1]).InnerText, Is.EqualTo("2.2"));
    }

    [Test]
    public void ToDataTable_WithTHeadWithoutTrAndTBodyElement()
    {
      // Arrange
      const string html = "<table><thead><th>Item 1</th><th>Item 2</th><th>Item 3</th></thead><tbody><tr><td>1.1</td><td>2.1</td><td>3.1</td></tr><tr><td>1.2</td><td>2.2</td><td>3.2</td></tr></tbody></table>";

      var element = new MockWebElement(html);

      // Act
      var table = element.ToDataTable();

      // Assert
      Assert.That(table.Columns.Count, Is.EqualTo(3));
      Assert.That(table.Columns[1].Caption, Is.EqualTo("Item 2"));
      Assert.That(((SearchableElement)table.Rows[1][1]).InnerText, Is.EqualTo("2.2"));
    }

    [Test]
    public void ToDataTable_WithoutTheadOrTBodyElement()
    {
      // Arrange
      const string html = "<table><tr><th>Item 1</th><th>Item 2</th><th>Item 3</th></tr><tr><td>1.1</td><td>2.1</td><td>3.1</td></tr><tr><td>1.2</td><td>2.2</td><td>3.2</td></tr></table>";

      var element = new MockWebElement(html);

      // Act
      var table = element.ToDataTable();

      // Assert
      Assert.That(table.Columns.Count, Is.EqualTo(3));
      Assert.That(table.Columns[1].Caption, Is.EqualTo("Item 2"));
      Assert.That(((SearchableElement)table.Rows[1][1]).InnerText, Is.EqualTo("2.2"));
    }

    [Test]
    public void ToDataTable_WithTBodyElement()
    {
      // Arrange
      const string html = "<table><tbody><tr><th>Item 1</th><th>Item 2</th><th>Item 3</th></tr><tr><td>1.1</td><td>2.1</td><td>3.1</td></tr><tr><td>1.2</td><td>2.2</td><td>3.2</td></tr></tbody></table>";

      var element = new MockWebElement(html);

      // Act
      var table = element.ToDataTable();

      // Assert
      Assert.That(table.Columns.Count, Is.EqualTo(3));
      Assert.That(table.Columns[1].Caption, Is.EqualTo("Item 2"));
      Assert.That(((SearchableElement)table.Rows[1][1]).InnerText, Is.EqualTo("2.2"));
    }

    [Test]
    public void ToDataTable_WithoutHeaderRowElement()
    {
      // Arrange
      const string html = "<table><tr><td>Item 1</td><td>Item 2</td><td>Item 3</td></tr><tr><td>1.1</td><td>2.1</td><td>3.1</td></tr><tr><td>1.2</td><td>2.2</td><td>3.2</td></tr></table>";

      var element = new MockWebElement(html);

      // Act
      var table = element.ToDataTable(true);

      // Assert
      Assert.That(table.Columns.Count, Is.EqualTo(3));
      Assert.That(table.Columns[1].Caption, Is.EqualTo("Item 2"));
      Assert.That(((SearchableElement)table.Rows[1][1]).InnerText, Is.EqualTo("2.2"));
    }

    [Test]
    public void ToDataTable_WithoutHeaderRow()
    {
      // Arrange
      const string html = "<table><tr><td>1.1</td><td>2.1</td><td>3.1</td></tr></tr><tr><td>1.2</td><td>2.2</td><td>3.2</td></tr></table>";

      var element = new MockWebElement(html);

      // Act
      var table = element.ToDataTable();

      // Assert
      Assert.That(table.Columns.Count, Is.EqualTo(3));
      Assert.That(((SearchableElement)table.Rows[0][0]).InnerText, Is.EqualTo("1.1"));
      Assert.That(((SearchableElement)table.Rows[1][1]).InnerText, Is.EqualTo("2.2"));
    }

    [Test]
    public void ToSearchableDocument()
    {
      // Arrange
      var element = new MockWebElement("<div/><div/>");

      // Act
      var searchable = element.ToSearchableDocument();

      // Assert
      Assert.That(searchable, Is.Not.Null);
    }

    [Test]
    public void ToSearchableElement()
    {
      // Arrange
      var element = new MockWebElement("<div/>");

      // Act
      var searchable = element.ToSearchableElement();

      // Assert
      Assert.That(searchable, Is.Not.Null);
    }

    [Test]
    public void OuterHtml()
    {
      // Arrange
      var element = Mock.Of<IWebElement>();

      // Act
      var _ = element.OuterHtml();

      // Assert
    }
  }
}
