using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using OpenQA.Selenium;

namespace ASI.SeleniumExtensions
{
  // ReSharper disable once InconsistentNaming
  public static class IWebElementExtensions
  {
    /// <summary>
    /// Converts an HTML table into a <see cref="DataTable"/>
    /// </summary>
    /// <param name="webElement">The table element to convert</param>
    /// <exception cref="ArgumentException">Thrown when the target element is not an HTML table.</exception>
    [Pure]
    public static DataTable ToDataTable(this IWebElement webElement)
    {
      if (string.Compare(webElement.TagName, "table", StringComparison.OrdinalIgnoreCase) != 0)
      {
        throw new ArgumentException("Not a table element", nameof(webElement));
      }

      var dataTable = new DataTable();
      var html = webElement.OuterHtml();
      using (var reader = new StringReader(html))
      {
        var nodeNavigator = new HtmlNodeNavigator(reader);
        var headers = nodeNavigator.Select("//th").Cast<HtmlNodeNavigator>();
        dataTable.Columns.AddRange(headers.Select(h =>
          new DataColumn(h.CurrentNode.InnerText)).ToArray());

        var rows = nodeNavigator.Select("//tr").Cast<HtmlNodeNavigator>();
        if (dataTable.Columns.Count == 0)
        {
          var count = rows.First().CurrentNode.ChildNodes.Count(c => c.Name == "td");
          dataTable.Columns.AddRange(Enumerable.Range(0, count).Select(_ =>
            new DataColumn()).ToArray());
        }
        else
        {
          rows = rows.Skip(1);
        }

        foreach (var row in rows)
        {
          dataTable.Rows.Add(row.CurrentNode.ChildNodes.Where(c =>
            c.Name == "td").Select(c => (object)c.InnerHtml).ToArray());
        }
      }

      return dataTable;
    }

    /// <summary>
    /// Converts the element into a <see cref="SearchableDocument"/>
    /// </summary>
    /// <param name="webElement"></param>
    /// <returns></returns>
    [Pure]
    public static SearchableDocument ToSearchableDocument(this IWebElement webElement) =>
      new SearchableDocument(webElement);

    /// <summary>
    /// Converts the element into a <see cref="SearchableElement"/>
    /// </summary>
    /// <param name="webElement"></param>
    /// <returns></returns>
    [Pure]
    public static SearchableElement ToSearchableElement(this IWebElement webElement) =>
      new SearchableElement(webElement);

    /// <summary>
    /// Gets the Outer Html from the element
    /// </summary>
    /// <param name="webElement"></param>
    /// <returns></returns>
    [Pure]
    public static string OuterHtml(this IWebElement webElement) =>
      webElement.GetAttribute("outerHTML");
  }
}
