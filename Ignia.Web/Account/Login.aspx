<%@ Page Language="C#" Title="Login" %>
<Script runat="server">
  /*==============================================================================================================================
  | Author        Ignia, LLC
  | Client        Ignia, LLC
  | Project       Website
  \=============================================================================================================================*/

  /*============================================================================================================================
  | PRIVATE VARIABLES
  \---------------------------------------------------------------------------------------------------------------------------*/
  private       string          _adminUsername  = Environment.GetEnvironmentVariable("AdminUsername");
  private       string          _adminPassword  = Environment.GetEnvironmentVariable("AdminPassword");

  /*============================================================================================================================
  | PAGE LOAD
  \---------------------------------------------------------------------------------------------------------------------------*/
  void Page_Load(Object Src, EventArgs E) {

    /*--------------------------------------------------------------------------------------------------------------------------
    | Ensure caching is disabled
    \-------------------------------------------------------------------------------------------------------------------------*/
    Response.Cache.SetCacheability(HttpCacheability.NoCache);

    /*--------------------------------------------------------------------------------------------------------------------------
    | Entry
    \-------------------------------------------------------------------------------------------------------------------------*/
    if (!IsPostBack) {
      if (Request.Cookies["MembershipAuthentication"] != null && Request.QueryString["ReturnUrl"] != null) {
        Response.Redirect(Request.QueryString["ReturnUrl"]);
      }
      Instructions.Text         = "The resource you have requested requires authentication. Please login.";
    }

  }

  /*==========================================================================================================================
  | VALIDATOR: AUTHENTICATEUSER
  \-------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Checks to determine if the user is valid; if they are, logs the user in.
  /// </summary>
  void AuthenticateUser(object source, ServerValidateEventArgs args) {

    /*------------------------------------------------------------------------------------------------------------------------
    | Ensure form is valid before proceeding
    \-----------------------------------------------------------------------------------------------------------------------*/
    if (!Page.IsValid) return;

    /*------------------------------------------------------------------------------------------------------------------------
    | Authenticate user and return to originally requested page
    \-----------------------------------------------------------------------------------------------------------------------*/
    string usernameInput        = Request.Form["Username"];
    string passwordInput        = Request.Form["Password"];
    bool isLoginValid           = (_adminUsername.ToLower() == usernameInput.ToLower() && _adminPassword == passwordInput);

    if (isLoginValid) {
      FormsAuthentication.SetAuthCookie(_adminUsername, RememberPassword.Checked);
      Response.Redirect(Request.QueryString["ReturnUrl"] ?? "/");
      args.IsValid              = true;
      return;
    }

    /*------------------------------------------------------------------------------------------------------------------------
    | Provide instructions
    \-----------------------------------------------------------------------------------------------------------------------*/
    Instructions.Text           = "Login failed: please check your username and password and try again.";
    Instructions.CssClass       = "error";

    /*------------------------------------------------------------------------------------------------------------------------
    | Reset validation
    \-----------------------------------------------------------------------------------------------------------------------*/
    args.IsValid                = false;

  }

</Script>
<!DOCTYPE html>
<html xmlns="https://www.w3.org/1999/xhtml" lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, height=device-height, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>Admin Login - Ignia</title>
    <link rel="stylesheet" type="text/css" href="/Shared/Styles/Style.css" />
    <style type="text/css">
      #SiteHeader {
        margin-bottom: 15px;
      }
      .header .logo {
        background: transparent none;
      }
      .logo img {
        padding: 1.5rem 0;
        width: 115px;
      }
      article {
        padding: 3rem 0;
      }
    </style>
  </head>
  <body>
    <form runat="server">

      <!-- Site Header Area -->
      <header id="SiteHeader" class="site header title-bar" role="banner" vocab="http://schema.org" typeof="WPHeader">
        <div class="logo centered">
          <!-- Logo -->
          <a href="/"><img src="/Images/Logo.png" alt="Ignia" class="logo" /></a>
        </div>
      </header>
      <!-- /Site Header Area -->

      <!-- Main Site Content Area -->
      <main id="MainContentArea" class="page content" role="main">
        <article itemscope itemtype="http://schema.org/WebPageElement" itemprop="mainContentOfPage" class="grid-container">

          <div class="grid-x grid-margin-x">

            <!-- Instructions -->
            <div class="cell text-center">
              <div class="callout primary" style="float: none; margin-left: 0;"><asp:Label ID="Instructions" RunAt="Server"></asp:Label></div>
            </div>

            <div class="cell large-6 large-offset-3">
              <label for="Username">Username:</label>
              <asp:TextBox ClientIDMode="Static" ID="Username" name="username" type="text" required runat="server" />

              <label for="Password">Password:</label>
              <asp:TextBox ClientIDMode="Static" ID="Password" name="password" type="password" required runat="server" />
              <asp:CustomValidator ControlToValidate="Password" OnServerValidate="AuthenticateUser" runat="server" />

              <asp:Checkbox ClientIDMode="Static" ID="RememberPassword" Text="Remember password on this machine" CssClass="checkbox" runat="server" />

              <br /><br />
              <asp:Button ID="SubmitButton" Text="Login" CssClass="button" CausesValidation="true" runat="server" />

            </div>

          </div>

        </article>
      </main>

    </form>
  </body>
</html>
