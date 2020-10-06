/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Web.Models;

namespace Ignia.Web.Services {

  /*============================================================================================================================
  | CLASS: IGNIA TOPIC VIEW MODEL LOOKUP SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a mapping between string and class names to be used when mapping <see cref="Topic"/> to a <see
  ///   cref="TopicViewModel"/> or derived class.
  /// </summary>
  public class IgniaTopicViewModelLookupService : OnTopic.ViewModels.TopicViewModelLookupService {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Instantiates a new instance of the <see cref="IgniaTopicViewModelLookupService"/>.
    /// </summary>
    /// <returns>A new instance of the <see cref="IgniaTopicViewModelLookupService"/>.</returns>
    internal IgniaTopicViewModelLookupService() : base() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Add GoldSim specific view models
      \-----------------------------------------------------------------------------------------------------------------------*/
      Add(typeof(ArticleTopicViewModel));
      Add(typeof(HomeTopicViewModel));
      Add(typeof(LibraryIndexTopicViewModel));
      Add(typeof(NavigationViewModel));
      Add(typeof(ProcessTopicViewModel));
      Add(typeof(ResourceTopicViewModel));
      Add(typeof(StandardTopicViewModel));
      Add(typeof(TechnologyAdvisoryTopicViewModel));

      /*------------------------------------------------------------------------------------------------------------------------
      | Override Ignia topics
      \-----------------------------------------------------------------------------------------------------------------------*/
      Replace(typeof(NavigationTopicViewModel));

      /*------------------------------------------------------------------------------------------------------------------------
      | Function: Replace
      \-----------------------------------------------------------------------------------------------------------------------*/
      void Replace(Type type) {
        if (Contains(type.Name)) {
          Remove(type.Name);
        }
        Add(type);
      }

    }

  } // Class

} // Namespace