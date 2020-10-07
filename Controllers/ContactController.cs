/*==============================================================================================================================
| Author        Ignia, LLC
| Client        Ignia, LLC
| Project       Website
\=============================================================================================================================*/
using System;
using System.Net.Mail;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Ignia.Web.Controllers {

  /*============================================================================================================================
  | CLASS: CONTACT CONTROLLER
  \---------------------------------------------------------------------------------------------------------------------------*/
  /// <summary>
  ///   Provides access to the default homepage for the site.
  /// </summary>
  public class ContactController : Controller {

    /*==========================================================================================================================
    | INDEX
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Placeholder / dummy view
    /// </summary>
    public ActionResult Index() => View();

    /*==========================================================================================================================
    | SEND CONTACT REQUEST
    \-------------------------------------------------------------------------------------------------------------------------*/
    /// <summary>
    ///   Handles data submitted on the contact form; sends form data as email to info@ignia.com.
    /// </summary>
    public JsonResult SendContactRequest() {

      /*------------------------------------------------------------------------------------------------------------------------
      | Establish variables
      \-----------------------------------------------------------------------------------------------------------------------*/
      HttpContext.Request.Form.TryGetValue("contactName", out var fromName);
      HttpContext.Request.Form.TryGetValue("contactEmail", out var fromEmail);
      HttpContext.Request.Form.TryGetValue("contactMessage", out var messageBody);

      if (!String.IsNullOrWhiteSpace(fromName) && !String.IsNullOrWhiteSpace(fromEmail) && !String.IsNullOrWhiteSpace(messageBody)) {
        Console.WriteLine("Fields: name = " + fromName + "; email = " + fromEmail + "; message = " + messageBody);

        /*------------------------------------------------------------------------------------------------------------------------
        | Send out email
        \-----------------------------------------------------------------------------------------------------------------------*/
        var mailBody = new StringBuilder();
        mailBody.Append("<div style=\"text-align: center; padding-top: 0px; padding-right: 0px; padding-bottom: 50px; padding-left: 0px;\"><img src=\"http://test.ignia.com/Images/Logo.png\" alt=\"Ignia\" /></div>");
        mailBody.Append("<h1 style=\"font-family: Arial,sans-serif; font-weight: normal;\">Information request from <a href=\"mailto:" + fromEmail + "?Subject=Thank%20you%20for%20inquiring%20about%20Ignia\" style=\"color: #EF4A24;\">" + fromName + "</a></h1>");
        mailBody.Append("<h2 style=\"font-family: Arial,sans-serif; font-weight: normal;\">Message:</h2>");
        mailBody.Append("<div style=\"font-family: Arial,sans-serif; font-size: 16px; line-height: 18px; padding-top: 15px;\">" + messageBody + "</div>");

        var smtp                = new SmtpClient();
        var mailMessage         = new MailMessage(new MailAddress(fromEmail), new MailAddress("info@Ignia.com")) {
          Subject               = "Contact from Ignia.com",
          IsBodyHtml            = true,
          Body                  = "<html><head><title>Email Message from Ignia.com</title></head><body style=\"background-color: #FFF; padding-top: 50px; padding-right: 35px; padding-bottom: 50px; padding-left: 35px; margin-top: 0px; margin-right: 0px; margin-bottom: 0px; margin-left: 0px; border: 1px solid #F0AD20;\"> " + mailBody + "</body></html>"
        };

        smtp.Send(mailMessage);

        return Json("Success");

      }
      else {
        return Json("Fields empty");
      }

    }


  } // Class
} // Namespace