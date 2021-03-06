﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using System.Web.Mvc;
using System.Web.Routing;

namespace Ignia.Web {

  /*============================================================================================================================
  | CLASS: ROUTE CONFIGURATION
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides default routing configuration for MVC.
  /// </summary>
  public static class RouteConfig {

    /*==========================================================================================================================
    | METHOD: REGISTER ROUTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provided a <see cref="RouteCollection"/>, registers all routes associated with the application.
    /// </summary>
    /// <param name="routes">
    ///   The route collection for the server, typically passed from the <see cref="System.Web.HttpApplication"/> class.
    /// </param>
    public static void RegisterRoutes(RouteCollection routes) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Ignore requests to AXDs (handled by HttpHandler)
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      /*------------------------------------------------------------------------------------------------------------------------
      | Enable attribute-based routing
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapMvcAttributeRoutes();

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle legacy redirects
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapRoute(
        name: "LegacyRedirect",
        url: "Page/{pageId}",
        defaults: new { controller = "Redirect", action = "LegacyRedirect" }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle OnTopic redirects
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapRoute(
        name: "TopicRedirect",
        url: "Topic/{topicId}",
        defaults: new { controller = "Redirect", action = "Redirect" }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle OnTopic Web namespace
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapRoute(
        name: "WebTopics",
        url: "Web/{*path}",
        defaults: new { controller = "Topic", action = "Index", id = UrlParameter.Optional, rootTopic = "Web" }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle OnTopic Library namespace
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapRoute(
        name: "LibraryTopics",
        url: "Library/{*path}",
        defaults: new { controller = "Topic", action = "Index", path = UrlParameter.Optional, rootTopic = "Library" }
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Handle default route convention
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Fallback", action = "Index", id = UrlParameter.Optional }
      );

    }

  } //Class
} //Namespace
