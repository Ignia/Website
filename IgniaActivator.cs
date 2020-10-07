/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using System;
using Ignia.Web.Controllers;
using Ignia.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using OnTopic;
using OnTopic.AspNetCore.Mvc.Controllers;
using OnTopic.Data.Caching;
using OnTopic.Data.Sql;
using OnTopic.Editor.AspNetCore;
using OnTopic.Editor.AspNetCore.Controllers;
using OnTopic.Internal.Diagnostics;
using OnTopic.Mapping;
using OnTopic.Repositories;

namespace Ignia.Web {

  /*============================================================================================================================
  | CLASS: IGNIA ACTIVATOR
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Responsible for creating instances of factories in response to web requests. Represents the Composition Root for
  ///   Dependency Injection.
  /// </summary>
  public class IgniaActivator : IControllerActivator, IViewComponentActivator {

    /*==========================================================================================================================
    | PRIVATE INSTANCES
    \-------------------------------------------------------------------------------------------------------------------------*/
    private readonly            IConfiguration                  _configuration;
    private readonly            ITypeLookupService              _typeLookupService;
    private readonly            ITopicMappingService            _topicMappingService;
    private readonly            ITopicRepository                _topicRepository;
    private readonly            IWebHostEnvironment             _webHostEnvironment;
    private readonly            StandardEditorComposer          _standardEditorComposer;

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Establishes a new instance of the <see cref="IgniaActivator"/>, including any shared dependencies to be used
    ///   across instances of controllers.
    /// </summary>
    /// <remarks>
    ///   The constructor is responsible for establishing dependencies with the singleton lifestyle so that they are available
    ///   to all requests.
    /// </remarks>
    public IgniaActivator(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Verify dependencies
      \-----------------------------------------------------------------------------------------------------------------------*/
      Contract.Requires(configuration, nameof(configuration));
      Contract.Requires(webHostEnvironment, nameof(webHostEnvironment));

      /*------------------------------------------------------------------------------------------------------------------------
      | SAVE STANDARD DEPENDENCIES
      \-----------------------------------------------------------------------------------------------------------------------*/
          _configuration        = configuration;
          _webHostEnvironment   = webHostEnvironment;
      var connectionString      = configuration.GetConnectionString("OnTopic");
      var sqlTopicRepository    = new SqlTopicRepository(connectionString);
      var cachedTopicRepository = new CachedTopicRepository(sqlTopicRepository);

      /*------------------------------------------------------------------------------------------------------------------------
      | PRELOAD REPOSITORY
      \-----------------------------------------------------------------------------------------------------------------------*/
      _topicRepository          = cachedTopicRepository;
      _typeLookupService        = new IgniaTopicViewModelLookupService();
      _topicMappingService      = new TopicMappingService(_topicRepository, _typeLookupService);

      _topicRepository.Load();

      /*------------------------------------------------------------------------------------------------------------------------
      | INITIALIZE EDITOR COMPOSER
      \-----------------------------------------------------------------------------------------------------------------------*/
      _standardEditorComposer   = new StandardEditorComposer(_topicRepository, _webHostEnvironment);

    }

    /*==========================================================================================================================
    | METHOD: CREATE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Registers dependencies, and injects them into new instances of controllers in response to each request.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    public object Create(ControllerContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine controller type
      \-----------------------------------------------------------------------------------------------------------------------*/
      var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();

      /*------------------------------------------------------------------------------------------------------------------------
      | Register
      \-----------------------------------------------------------------------------------------------------------------------*/

      /*------------------------------------------------------------------------------------------------------------------------
      | Resolve
      \-----------------------------------------------------------------------------------------------------------------------*/
      return controllerType.Name switch {

        nameof(RedirectController) => new RedirectController(_topicRepository),

        nameof(SitemapController) => new SitemapController(_topicRepository),

        nameof(ErrorController) => new ErrorController(
          _topicRepository,
          _topicMappingService
        ),

        nameof(TopicController) => new TopicController(_topicRepository, _topicMappingService),

        nameof(EditorController) => new EditorController(_topicRepository, _topicMappingService),

      _ => throw new Exception($"Unknown controller {controllerType.Name}")

      };
    }

    /// <summary>
    ///   Registers dependencies, and injects them into new instances of view components in response to each request.
    /// </summary>
    /// <returns>A concrete instance of an <see cref="IController"/>.</returns>
    public object Create(ViewComponentContext context) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Determine view component type
      \-----------------------------------------------------------------------------------------------------------------------*/
      var viewComponentType = context.ViewComponentDescriptor.TypeInfo.AsType();

      /*------------------------------------------------------------------------------------------------------------------------
      | Resolve
      \-----------------------------------------------------------------------------------------------------------------------*/

      //Handle standard topic editor view components
      if (StandardEditorComposer.IsEditorComponent(viewComponentType)) {
        return _standardEditorComposer.ActivateEditorComponent(
          viewComponentType,
          _topicRepository
        );
      }

      //Handle Ignia-specific view components
      return viewComponentType.Name switch {
        _ => throw new Exception($"Unknown view component {viewComponentType.Name}")

      };
    }

    /*==========================================================================================================================
    | METHOD: RELEASE
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Responds to a request to release resources associated with a particular controller.
    /// </summary>
    public void Release(ControllerContext context, object controller) { }

    /// <summary>
    ///   Responds to a request to release resources associated with a particular view component.
    /// </summary>
    public void Release(ViewComponentContext context, object viewComponent) { }

  } // Class
} // Namespace