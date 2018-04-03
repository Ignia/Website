/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using System.Linq;
using System.Collections.Generic;
using Ignia.Topics.ViewModels;

namespace Ignia.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: LIBRARY INDEX TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>LibraryIndex</c> topic.
  /// </summary>
  public class LibraryIndexTopicViewModel : PageTopicViewModel {

    /*==========================================================================================================================
    | PRIVATE VARIABLES
    \-------------------------------------------------------------------------------------------------------------------------*/

    /*==========================================================================================================================
    | GENERIC PROPERTIES
    \-------------------------------------------------------------------------------------------------------------------------*/
    public virtual TopicViewModelCollection<ArticleTopicViewModel> Processes { get; set; }
    public virtual TopicViewModelCollection<ArticleTopicViewModel> Standards { get; set; }
    public virtual TopicViewModelCollection<ArticleTopicViewModel> Technologies { get; set; }

    /*==========================================================================================================================
    | GET CATEGORY HEADING
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns the Article(s) category heading, based on the category (Content Type) key.
    /// </summary>
    /// <param name="categoryKey">String corresponding to the Content Type for the set of articles.</param>
    /// <returns>The title/heading string for the category.</returns>
    public string GetCategoryHeading(string categoryKey) {
      switch (categoryKey) {
        case "Technologies"     : return "Technology Advisories";
        case "Standards"        : return "Development Standards";
        case "Processes"        : return "Business and Development Processes";
        default                 : return "Articles";
      }
    }

    /*==========================================================================================================================
    | GET ALL APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a consolidated list of <i>all</i> articles from the corresponding properties.
    /// </summary>
    /// <returns>A consolidated list of articles.</returns>
    public TopicViewModelCollection<ArticleTopicViewModel> GetAllArticles() {
      return new TopicViewModelCollection<ArticleTopicViewModel>(
        Processes.Concat(Standards).Concat(Technologies).Distinct().ToList()
      );
    }

    /*==========================================================================================================================
    | GET CATEGORIZED APPLICATIONS
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Returns a dictionary of applications keyed by <see cref="Categories"/>.
    /// </summary>
    /// <returns>A consolidated list of applications.</returns>
    public Dictionary<string, TopicViewModelCollection<ArticleTopicViewModel>> GetCategorizedArticles() {
      var categorizedApplications = new Dictionary<string, TopicViewModelCollection<ArticleTopicViewModel>> {
        { nameof(Processes), Processes },
        { nameof(Standards), Standards },
        { nameof(Technologies), Technologies }
      };
      return categorizedApplications;
    }

  } // Class

} // Namespace
