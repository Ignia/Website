/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;

namespace Ignia.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: TECHNOLOGY ADVISORY TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>TechnologyAdvisory</c> topic.
  /// </summary>
  public class TechnologyAdvisoryTopicViewModel : ArticleTopicViewModel {

    public string Strengths { get; set; }
    public string Weaknesses { get; set; }
    public string Usage { get; set; }
    public string BestPractices { get; set; }
    public string Website { get; set; }
    public string Documentation { get; set; }
    public TopicViewModelCollection<ResourceTopicViewModel> Resources { get; set; }

  } // Class

} // Namespace