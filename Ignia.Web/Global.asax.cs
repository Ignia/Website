﻿/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ignia.Topics;
using Ignia.Topics.Data.Caching;
using Ignia.Topics.Data.Sql;
using Ignia.Topics.Web;
using Ignia.Topics.Web.Mvc;

namespace Ignia.Web {

  /*============================================================================================================================
  | CLASS: GLOBAL
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides default configuration for the application, including any special processing that needs to happen relative to
  ///   application events (such as <see cref="Application_Start"/> or <see cref="System.Web.HttpApplication.Error"/>.
  /// </summary>
  public class Global : HttpApplication {

    /*==========================================================================================================================
    | METHOD: APPLICATION START (EVENT HANDLER)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides initial configuration for the application, including registration of MVC routes via the
    ///   <see cref="RouteConfig"/> class.
    /// </summary>
    void Application_Start(object sender, EventArgs e) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Initialize Topic Repository
      >-------------------------------------------------------------------------------------------------------------------------
      | ### NOTE JJC071517: In order to maintain forward compatibility with dependency injection, the legacy ASP.NET Web Forms
      | support requires that the TopicRepository be configured with a concrete instance of an ITopicRepository instance.
      | (Previously, this relied instead on the ASP.NET Provider Model, which prevented injection of e.g. connection strings and
      | thus established hard-coded dependencies on e.g. ConfigurationManager)
      \-----------------------------------------------------------------------------------------------------------------------*/
      var connectionString                      = ConfigurationManager.ConnectionStrings["TopicsServer"].ConnectionString;
      var sqlTopicRepository                    = new SqlTopicRepository(connectionString);
      var cachedTopicRepository                 = new CachedTopicRepository(sqlTopicRepository);

      //Preload data to ensure it's available to subsequent applications
      cachedTopicRepository.Load();

      #pragma warning disable CS0618
      TopicRepository.DataProvider              = cachedTopicRepository;
      #pragma warning restore CS0618

      /*------------------------------------------------------------------------------------------------------------------------
      | Register controller factory
      \-----------------------------------------------------------------------------------------------------------------------*/
      ControllerBuilder.Current.SetControllerFactory(
        new IgniaControllerFactory()
      );

      /*------------------------------------------------------------------------------------------------------------------------
      | Register view engine
      \-----------------------------------------------------------------------------------------------------------------------*/
      ViewEngines.Engines.Insert(0, new TopicViewEngine());

      /*------------------------------------------------------------------------------------------------------------------------
      | Register routes
      \-----------------------------------------------------------------------------------------------------------------------*/
      RouteConfig.RegisterRoutes(RouteTable.Routes);

    }

    /*==========================================================================================================================
    | METHOD: REGISTER ROUTES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes (non-MVC) URL routes and maps them to the appropriate page handlers.
    /// </summary>
    /// <param name="routes">
    ///   The route collection for the server, typically passed from the <see cref="System.Web.HttpApplication"/> class.
    /// </param>
    void RegisterRoutes(RouteCollection routes) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Ignore ASPX pages
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.IgnoreRoute("{WebPage}.aspx/{*pathInfo}");

      /*------------------------------------------------------------------------------------------------------------------------
      | Add default and legacy routes
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.MapPageRoute("Edit",       "Edit/{*directory}",    "~/!Admin/Topics/Default.aspx");

      /*------------------------------------------------------------------------------------------------------------------------
      | Ignore ASPX pages
      \-----------------------------------------------------------------------------------------------------------------------*/
      routes.Add(
        "TopicUrl",
        new Route(
          "{namespace}/{*path}",
          new TopicsRouteHandler()
        )
      );

    }

  }

}