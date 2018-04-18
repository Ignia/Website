/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using System;
using System.Linq;
using Ignia.Topics.Mapping;
using Ignia.Topics.ViewModels;

namespace Ignia.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: ARTICLE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Article</c> topic.
  /// </summary>
  /// <remarks>
  ///   It is not expected that any topics will directly implement the <c>Article</c> content type that corresponds
  ///   to this view model. Instead, it provides a base schema definition for, e.g.,
  ///   <see cref="TechnologyAdvisoryTopicViewModel"/>.
  /// </remarks>
  public class ArticleTopicViewModel : PageTopicViewModel {

    public bool IsFeatured { get; set; }
    public string Author { get; set; }
    public string PublishDate { get; set; }
    public string Abstract { get; set; }

    [Metadata("Authors")]
    public TopicViewModelCollection<LookupListItemTopicViewModel> Authors { get; set; }

    /*==========================================================================================================================
    | GET AUTHOR TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Looks up an author from the <see cref="Authors"/> collection based on the <see
    ///   cref="LookupListItemTopicViewModel.Key"/> and returns the corresponding <see
    ///   cref="LookupListItemTopicViewModel.Title"/>.
    /// </summary>
    /// <param name="author">The <c>Key</c> for the author.</param>
    /// <returns>The title corresponding to the author <c>Key</c>.</returns>
    public string GetAuthorTitle(string author) {
      if (Authors.Count > 0) {
        return Authors.Where(t => t.Key.Equals(author)).FirstOrDefault().Title;
      }
      return "Ignia, LLC";
    }

    /*==========================================================================================================================
    | GET FORMATTED DATETIME VALUE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a date and/or time value based on the provided string value and date/time format
    /// </summary>
    /// <param name="value">String representation of the date/time Attribute value.</param>
    /// <param name="format">Specified date/time string format for return value.</param>
    /// <returns>String representation of the date and/or time, corresponding to the format provided.</returns>
    public string GetFormattedDateTimeValue(string value, string format) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      string dateTimeOutput     = "";
      DateTime dateTimeValue;

      /*------------------------------------------------------------------------------------------------------------------------
      | Verify DateTime value and set it to the output string based on the specified format
      \-----------------------------------------------------------------------------------------------------------------------*/
      if (DateTime.TryParse(value, out dateTimeValue)) {
        dateTimeOutput          = dateTimeValue.ToString(format);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Return output
      \-----------------------------------------------------------------------------------------------------------------------*/
      return dateTimeOutput;

    }

  } // Class

} // Namespace
