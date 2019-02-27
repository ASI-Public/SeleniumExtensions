using NUnit.Framework;

namespace ASI.SeleniumExtensions
{
  [TestFixture]
  public class SearchableElementTests
  {
    [TestCase("<div attrib1=\"element1\" attrib2=\"element2\"/>",
      "element1", "element2", Category = "Attributes")]
    [TestCase("<div attrib1 attrib2=\"element2\"/>",
      "", "element2", Category = "Attributes")]
    [TestCase("<div attrib1=\"element1\" attrib2/>",
      "element1", "", Category = "Attributes")]
    [TestCase("<div attrib1 attrib2/>",
      "", "", Category = "Attributes")]
    public void ElementWithMultipleAttributes(string element,
      string attrib1Value, string attrib2Value)
    {
      // Arrange

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Attributes.Count, Is.EqualTo(2));
      Assert.That(searchableElement.Attributes["attrib1"], Is.EqualTo(attrib1Value));
      Assert.That(searchableElement.Attributes["attrib2"], Is.EqualTo(attrib2Value));
    }

    [Test]
    [Category("ElementType")]
    public void ElementTypeSelfClosing()
    {
      // Arrange
      const string element = "<div/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.ElementType, Is.EqualTo("div"));
    }

    [Test]
    [Category("ElementType")]
    public void ElementTypeUnclosed()
    {
      // Arrange
      const string element = "<div>text";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.ElementType, Is.EqualTo("div"));
    }

    [Test]
    [Category("ElementType")]
    public void ElementTypeWithChild()
    {
      // Arrange
      const string element = "<div><hr/></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.ElementType, Is.EqualTo("div"));
    }

    [Test]
    [Category("ElementType")]
    public void ElementTypeWithoutChild()
    {
      // Arrange
      const string element = "<div></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.ElementType, Is.EqualTo("div"));
    }

    [Test]
    [Category("ElementType")]
    public void ElementTypeWithText()
    {
      // Arrange
      const string element = "<div>thing</div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.ElementType, Is.EqualTo("div"));
    }

    [Test]
    [Category("Id")]
    public void ElementWithId()
    {
      // Arrange
      const string element = "<div id=\"elementid\"/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Id, Is.EqualTo("elementid"));
    }

    [Test]
    [Category("Classes")]
    public void ElementWithMultipleClasses()
    {
      // Arrange
      const string element = "<div class=\"elementclass1 elementclass2 elementclass3\"/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Classes.Count, Is.EqualTo(3));
      Assert.That(searchableElement.Classes[0], Is.EqualTo("elementclass1"));
      Assert.That(searchableElement.Classes[1], Is.EqualTo("elementclass2"));
      Assert.That(searchableElement.Classes[2], Is.EqualTo("elementclass3"));
    }

    [Test]
    [Category("Name")]
    public void ElementWithName()
    {
      // Arrange
      const string element = "<div name=\"elementname\"/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Name, Is.EqualTo("elementname"));
    }

    [Test]
    [Category("Children")]
    public void ElementWithNestedChildrenWithText()
    {
      // Arrange
      const string element = "<div><p attrib=\"pie\"><h>this is</h> <h1>text</h1></p><div>more stuff</div><hr></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Children.Count, Is.EqualTo(3));
      Assert.That(searchableElement.Children[0].ElementType, Is.EqualTo("p"));
      Assert.That(searchableElement.Children[1].ElementType, Is.EqualTo("div"));
      Assert.That(searchableElement.Children[2].ElementType, Is.EqualTo("hr"));
    }

    [Test]
    [Category("Children")]
    public void ElementWithOneChild()
    {
      // Arrange
      const string element = "<div><hr/></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Children.Count, Is.EqualTo(1));
      Assert.That(searchableElement.Children[0].ElementType, Is.EqualTo("hr"));
    }

    [Test]
    [Category("Children")]
    public void ElementWithOneChildWithText()
    {
      // Arrange
      const string element = "<div><p attrib=\"pie\">this is text</p></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Children.Count, Is.EqualTo(1));
      Assert.That(searchableElement.Children[0].ElementType, Is.EqualTo("p"));
    }

    [Test]
    [Category("Classes")]
    public void ElementWithOneClass()
    {
      // Arrange
      const string element = "<div class=\"elementclass\"/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Classes.Count, Is.EqualTo(1));
      Assert.That(searchableElement.Classes[0], Is.EqualTo("elementclass"));
    }

    [Test]
    [Category("Children")]
    public void ElementWithOneVoidChild()
    {
      // Arrange
      const string element = "<div><br></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Children.Count, Is.EqualTo(1));
      Assert.That(searchableElement.Children[0].ElementType, Is.EqualTo("br"));
    }

    [Test]
    [Category("Attributes")]
    public void ElementWithoutAttribute()
    {
      // Arrange
      const string element = "<div/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Attributes.Count, Is.EqualTo(0));
    }

    [Test]
    [Category("Children")]
    public void ElementWithoutChildren()
    {
      // Arrange
      const string element = "<div></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Classes.Count, Is.EqualTo(0));
    }

    [Test]
    [Category("Classes")]
    public void ElementWithoutClasses()
    {
      // Arrange
      const string element = "<div/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Classes.Count, Is.EqualTo(0));
    }

    [Test]
    [Category("Id")]
    public void ElementWithoutId()
    {
      // Arrange
      const string element = "<div/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Id, Is.EqualTo(""));
    }

    [Test]
    [Category("Name")]
    public void ElementWithoutName()
    {
      // Arrange
      const string element = "<div/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Name, Is.EqualTo(""));
    }

    [Test]
    [Category("Attributes")]
    public void ElementWithSingleSetAttribute()
    {
      // Arrange
      const string element = "<div attrib=\"element1\"/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Attributes.Count, Is.EqualTo(1));
      Assert.That(searchableElement.Attributes["attrib"], Is.EqualTo("element1"));
    }

    [Test]
    [Category("Attributes")]
    public void ElementWithSingleSetAttributeWithMultipleValues()
    {
      // Arrange
      const string element = "<div attrib=\"element1 element2\">thing</div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Attributes.Count, Is.EqualTo(1));
      Assert.That(searchableElement.Attributes["attrib"], Is.EqualTo("element1 element2"));
    }

    [Test]
    [Category("Attributes")]
    public void ElementWithSingleSetEmptyAttribute()
    {
      // Arrange
      const string element = "<div attrib=\"\"/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Attributes.Count, Is.EqualTo(1));
      Assert.That(searchableElement.Attributes["attrib"], Is.EqualTo(""));
    }

    [Test]
    [Category("Attributes")]
    public void ElementWithSingleStaticAttribute()
    {
      // Arrange
      const string element = "<div attrib>thing</div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Attributes.Count, Is.EqualTo(1));
      Assert.That(searchableElement.Attributes["attrib"], Is.EqualTo(""));
    }

    [Test]
    [Category("Attributes")]
    public void ElementWithSingleStaticAttributeSelfClosing()
    {
      // Arrange
      const string element = "<div attrib/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Attributes.Count, Is.EqualTo(1));
      Assert.That(searchableElement.Attributes["attrib"], Is.EqualTo(""));
    }

    [Test]
    [Category("Children")]
    public void ElementWithThreeChildrenWithText()
    {
      // Arrange
      const string element = "<div><p attrib=\"pie\">this is text</p><div>more stuff</div><hr></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Children.Count, Is.EqualTo(3));
      Assert.That(searchableElement.Children[0].ElementType, Is.EqualTo("p"));
      Assert.That(searchableElement.Children[1].ElementType, Is.EqualTo("div"));
      Assert.That(searchableElement.Children[2].ElementType, Is.EqualTo("hr"));
    }

    [Test]
    [Category("Children")]
    public void ElementWithTwoChild()
    {
      // Arrange
      const string element = "<div><hr/><br/></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Children.Count, Is.EqualTo(2));
      Assert.That(searchableElement.Children[0].ElementType, Is.EqualTo("hr"));
      Assert.That(searchableElement.Children[1].ElementType, Is.EqualTo("br"));
    }

    [Test]
    [Category("Children")]
    public void ElementWithTwoChildrenWithText()
    {
      // Arrange
      const string element = "<div><p attrib=\"pie\">this is text</p><div>more stuff</div></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Children.Count, Is.EqualTo(2));
      Assert.That(searchableElement.Children[0].ElementType, Is.EqualTo("p"));
      Assert.That(searchableElement.Children[1].ElementType, Is.EqualTo("div"));
    }

    [Test]
    [Category("InnerText")]
    public void InnerTextSelfClosing()
    {
      // Arrange
      const string element = "<div/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.InnerText, Is.EqualTo(""));
    }

    [Test]
    [Category("InnerText")]
    public void InnerTextUnclosed()
    {
      // Arrange
      const string element = "<div>text";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.InnerText, Is.EqualTo("text"));
    }

    [Test]
    [Category("InnerText")]
    public void InnerTextWithChild()
    {
      // Arrange
      const string element = "<div><hr/></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.InnerText, Is.EqualTo("<hr>"));
    }

    [Test]
    [Category("InnerText")]
    public void InnerTextWithoutChild()
    {
      // Arrange
      const string element = "<div></div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.InnerText, Is.EqualTo(""));
    }

    [Test]
    [Category("InnerText")]
    public void InnerTextWithText()
    {
      // Arrange
      const string element = "<div>thing</div>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.InnerText, Is.EqualTo("thing"));
    }

    [Test]
    public void NewSearchableElement()
    {
      // Arrange
      const string element = "<div/>";

      // Act
      var searchableElement = new SearchableElement(element);

      // Assert
      Assert.That(searchableElement.Text, Is.EqualTo(element));
    }
  }
}