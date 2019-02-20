using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using OpenQA.Selenium;

namespace ASI.SeleniumExtensions
{
  // ReSharper disable once InconsistentNaming
  /// <summary>
  /// IWebElement Extension class
  /// </summary>
  public static class IWebElementExtensions
  {
    /// <summary>
    /// Converts an HTML table into a <see cref="DataTable"/>
    /// </summary>
    /// <param name="webElement">The table element to convert</param>
    /// <param name="forceHeaderRow">Force first row to be considered the header row</param>
    /// <exception cref="ArgumentException">Thrown when the target element is not an HTML table.</exception>
    [Pure]
    public static DataTable ToDataTable(this IWebElement webElement, bool forceHeaderRow = false)
    {
      if (string.Compare(webElement.TagName, "table", StringComparison.OrdinalIgnoreCase) != 0)
      {
        throw new ArgumentException("Not a table element", nameof(webElement));
      }

      return TryWebElementParse(webElement, forceHeaderRow);
    }

    private static DataTable TryWebElementParse(IWebElement webElement, bool forceHeaderRow)
    {
      var dataTable = new DataTable(webElement.GetAttribute("id"));

      var rows = webElement.FindElements(By.TagName("tr"));
      var headers = webElement.FindElements(By.TagName("th"));
      if (forceHeaderRow && !headers.Any())
      {
        headers = webElement.FindElements(By.XPath("//tr[1]/td"));
        rows = new ReadOnlyCollection<IWebElement>(rows.Skip(1).ToList());
      }

      if (webElement.FindElements(By.XPath("//tr/th")).Any())
        rows = new ReadOnlyCollection<IWebElement>(rows.Skip(1).ToList());


      dataTable.Columns.AddRange(headers.Select(h => new DataColumn(h.Text, typeof(SearchableElement))).ToArray());

      if (dataTable.Columns.Count == 0)
      {
        var count = rows.First().FindElements(By.TagName("td")).Count;
        dataTable.Columns.AddRange(Enumerable.Range(0, count).Select(_ =>
          new DataColumn(string.Empty, typeof(SearchableElement))).ToArray());
      }

      foreach (var row in rows)
      {
        var elems = row.FindElements(By.TagName("td"));
        var cells = new List<SearchableElement>();
        foreach (var elem in elems)
        {
          cells.Add(new SearchableElement(elem.Text));
        }

        dataTable.Rows.Add(cells.ToArray<object>());
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
