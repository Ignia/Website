/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia
| Project       Website
\=============================================================================================================================*/
using Ignia.Topics.ViewModels;

namespace Ignia.Web.Models {

  /*============================================================================================================================
  | VIEW MODEL: NAVIGATION TOPIC
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a strongly-typed data transfer object for feeding views with information about the navigation.
  /// </summary>
  /// <remarks>
  ///   No topics are expected to have a <c>Navigation</c> content type. Instead, this view model is expected to be manually
  ///   constructed by the <see cref="LayoutController"/>.
  /// </remarks>
  public class NavigationTopicViewModel : PageTopicViewModel {

    public TopicViewModelCollection<NavigationTopicViewModel> Children { get; set; }
    public bool IsSelected(string uniqueKey) => uniqueKey.StartsWith(this.UniqueKey);

  } // Class

} // Namespace