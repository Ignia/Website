/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.Mapping;
using Ignia.Topics.ViewModels;
using System.Linq;

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
    | GET CATEGORY TITLE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Looks up an author from the <see cref="Authors"/> collection based on the <see
    ///   cref="LookupListItemTopicViewModel.UniqueKey"/> and returns the corresponding <see
    ///   cref="LookupListItemTopicViewModel.Title"/>.
    /// </summary>
    /// <param name="author">The <c>UniqueKey</c> for the author.</param>
    /// <returns>The title corresponding to the author <c>UniqueKey</c>.</returns>
    public string GetAuthorTitle(string author) => Authors.Where(t => t.UniqueKey.Equals(author)).FirstOrDefault().Title;

  } // Class

} // Namespace
