/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ignia.Web.Controllers;
using Ignia.Topics.Mapping;
using Ignia.Topics.Web;
using Ignia.Topics.Web.Mvc;
using Ignia.Topics.Web.Mvc.Controllers;
using Ignia.Topics;
using Ignia.Topics.Repositories;

namespace Ignia.Web {

  /*============================================================================================================================
  | CLASS: CONTROLLER FACTORY
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the default homepage for the site.
  /// </summary>
  class IgniaControllerFactory : DefaultControllerFactory {

    /*==========================================================================================================================
    | PRIVATE INSTANCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            ITypeLookupService              _typeLookupService              = null;
    private readonly            ITopicMappingService            _topicMappingService            = null;
    private readonly            ITopicRepository                _topicRepository                = null;
    private readonly            Topic                           _rootTopic                      = null;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="IgniaControllerFactory"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    public IgniaControllerFactory() : base() {
      #pragma warning disable CS0618
      _topicRepository          = TopicRepository.DataProvider;
      _typeLookupService        = new IgniaTopicViewModelLookupService();
      _topicMappingService      = new TopicMappingService(_topicRepository, _typeLookupService);
      _rootTopic                = TopicRepository.RootTopic;
      #pragma warning restore CS0618
    }

    /*==========================================================================================================================
    | GET CONTROLLER INSTANCE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Overrides the factory method for creating new instances of controllers.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Register
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mvcTopicRoutingService = new MvcTopicRoutingService(
        _topicRepository,
        requestContext.HttpContext.Request.Url,
        requestContext.RouteData
      );

      // Set default controller
      if (controllerType == null) {
        controllerType = typeof(FallbackController);
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Resolve
      \-----------------------------------------------------------------------------------------------------------------------*/
      switch (controllerType.Name) {

        case (nameof(RedirectController)):
          return new RedirectController(_topicRepository);

        case (nameof(SitemapController)):
          return new SitemapController(_topicRepository);

        case (nameof(ErrorController)):
          return new ErrorController();

        case nameof(TopicController):
          return new TopicController(_topicRepository, mvcTopicRoutingService, _topicMappingService);

        default:
          return base.GetControllerInstance(requestContext, controllerType);

      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Release
      \-----------------------------------------------------------------------------------------------------------------------*/
      // There are no resources to release

    }

  } // Class

} // Namespace
