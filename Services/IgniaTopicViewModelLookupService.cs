/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Topics Library
\=============================================================================================================================*/
using Ignia.Web.Models;
using OnTopic.Editor.AspNetCore.Infrastructure;

namespace Ignia.Web.Services {

  /*============================================================================================================================
  | CLASS: IGNIA TOPIC VIEW MODEL LOOKUP SERVICE
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides a mapping between string and class names to be used when mapping <see cref="Topic"/> to a <see
  ///   cref="TopicViewModel"/> or derived class.
  /// </summary>
  public class IgniaTopicViewModelLookupService : EditorViewModelLookupService {

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
      AddOrReplace(typeof(NavigationTopicViewModel));

    }

  } // Class
} // Namespace