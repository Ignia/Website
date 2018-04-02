/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
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

  } // Class

} // Namespace