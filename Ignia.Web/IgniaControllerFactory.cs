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
      _topicMappingService      = new TopicMappingService(_topicRepository);
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
      if (controllerType == typeof(RedirectController)) {
        return new RedirectController(topicRepository);
      }

      if (controllerType == typeof(SitemapController)) {
        return new SitemapController(topicRepository, null);
      }

      if (controllerType == typeof(ErrorController)) {
        return new ErrorController();
      }

      if (controllerType == typeof(LayoutController)) {
        return new LayoutController(topicRepository, mvcTopicRoutingService, topicMappingService);
      }

      if (controllerType == typeof(TopicController)) {
        return new TopicController(topicRepository, mvcTopicRoutingService, topicMappingService);
      }

      return base.GetControllerInstance(requestContext, controllerType);

      /*------------------------------------------------------------------------------------------------------------------------
      | Release
      \-----------------------------------------------------------------------------------------------------------------------*/
      // There are no resources to release

    }

  } // Class
} // Namespace
