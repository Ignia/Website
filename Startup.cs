/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia
| Project       Website
\=============================================================================================================================*/
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using OnTopic.AspNetCore.Mvc;
using OnTopic.Editor.AspNetCore;

using HeaderNames = Microsoft.Net.Http.Headers.HeaderNames;

namespace Ignia.Web {

  /*============================================================================================================================
  | CLASS: STARTUP
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Configures the application and sets up dependencies.
  /// </summary>
  public class Startup {

    /*==========================================================================================================================
    | CONSTRUCTOR
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Constructs a new instances of the <see cref="Startup"/> class. Accepts an <see cref="IConfiguration"/>.
    /// </summary>
    /// <param name="configuration">
    ///   The shared <see cref="IConfiguration"/> dependency.
    /// </param>
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment) {
      Configuration = configuration;
      HostingEnvironment = webHostEnvironment;
    }

    /*==========================================================================================================================
    | PROPERTY: CONFIGURATION
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a (public) reference to the application's <see cref="IConfiguration"/> service.
    /// </summary>
    public IConfiguration Configuration { get; }

    /*==========================================================================================================================
    | PROPERTY: HOSTING ENVIRONMENT
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides a (public) reference to the application's <see cref="IWebHostEnvironment"/> service.
    /// </summary>
    public IWebHostEnvironment HostingEnvironment { get; }

    /*==========================================================================================================================
    | METHOD: CONFIGURE SERVICES
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides configuration of services. This method is called by the runtime to bootstrap the server configuration.
    /// </summary>
    public void ConfigureServices(IServiceCollection services) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: MVC
      \-----------------------------------------------------------------------------------------------------------------------*/
      var mvcBuilder = services.AddControllersWithViews()

        //Add OnTopic support
        .AddTopicSupport()

        //Add OnTopic editor support
        .AddTopicEditor();

      //Conditionally add runtime compilation in development
      if (HostingEnvironment.IsDevelopment()) {
        mvcBuilder.AddRazorRuntimeCompilation();
      }

      /*------------------------------------------------------------------------------------------------------------------------
      | Register: Activators
      \-----------------------------------------------------------------------------------------------------------------------*/
      var activator = new IgniaActivator(Configuration, HostingEnvironment);

      services.AddSingleton<IControllerActivator>(activator);
      services.AddSingleton<IViewComponentActivator>(activator);


    }

    /*==========================================================================================================================
    | METHOD: CONFIGURE (APPLICATION)
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Provides configuration the application. This method is called by the runtime to bootstrap the application
    ///   configuration, including the HTTP pipeline.
    /// </summary>
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: Server defaults
      \-----------------------------------------------------------------------------------------------------------------------*/
      app.UseStaticFiles();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseCors("default");

      /*------------------------------------------------------------------------------------------------------------------------
      | Configure: MVC
      \-----------------------------------------------------------------------------------------------------------------------*/
      app.UseEndpoints(endpoints => {

        endpoints.MapTopicEditorRoute().RequireAuthorization(); // OnTopic/{action}/{**path}

        endpoints.MapTopicAreaRoute();                          // {area:exists}/{**path}
        endpoints.MapImplicitAreaControllerRoute();             // {area:exists}/{action=Index}
        endpoints.MapDefaultControllerRoute();                  // {controller=Home}/{action=Index}/{id?}
        endpoints.MapDefaultAreaControllerRoute();              // {area:exists}/{controller}/{action=Index}/{id?}

        endpoints.MapTopicRoute("Web");                         // Web/{**path}
        endpoints.MapTopicRoute("Library");                     // Library/{**path}
        endpoints.MapTopicRedirect();                           // Topic/{topicId}
        endpoints.MapControllers();

      });

    }

  } //Class
} //Namespace