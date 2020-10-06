/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;

namespace Ignia.Web.Models
{

  /*============================================================================================================================
  | VIEW MODEL: RESOURCE TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about a <c>Resource</c> topic.
  /// </summary>
  public class ResourceTopicViewModel : TopicViewModel {

    public string Description { get; set; }
    public string Url { get; set; }

  } // Class

} // Namespace