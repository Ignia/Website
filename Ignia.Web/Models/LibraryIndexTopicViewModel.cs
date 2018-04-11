/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using System.Linq;
using Ignia.Topics.ViewModels;
using Ignia.Topics.Mapping;

namespace Ignia.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: LIBRARY INDEX TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>LibraryIndex</c> topic.
  /// </summary>
  public class LibraryIndexTopicViewModel : PageTopicViewModel {

    /*==========================================================================================================================
    | GENERIC PROPERTIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    [Recurse(Relationships.Children)]
    [Relationship("Children", RelationshipType.Children)]
    public TopicViewModelCollection<LibraryIndexTopicViewModel> Indexes { get; set; }

    [Relationship("Children", RelationshipType.Children)]
    public TopicViewModelCollection<ArticleTopicViewModel> Articles { get; set; }

    /*==========================================================================================================================
    | GET ARTICLE COUNT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Calculates the total number of <see cref="ArticleTopicViewModel"/> Topics available from <code>Articles</code> and
    ///   children of <code>Indexes</code>.
    /// </summary>
    /// <returns>
    ///   The total number of <see cref="ArticleTopicViewModel"/> Topics.
    /// </returns>
    public int GetArticleCount() {
      return Articles.Count + Indexes.Sum(i => i.GetArticleCount());
    }

    /*==========================================================================================================================
    | GET ALL ARTICLES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a collection of articles assembled from the <see cref="Articles"/> and <see cref="Indexes"/> collections.
    /// </summary>
    /// <returns>A consolidated list of articles.</returns>
    public TopicViewModelCollection<ArticleTopicViewModel> GetAllArticles() {
      return new TopicViewModelCollection<ArticleTopicViewModel>(
        Articles.Union(
          Indexes.SelectMany(i => i.GetAllArticles())
        )
      );
    }

  } // Class

} // Namespace
