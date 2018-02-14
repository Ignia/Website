/**
 * HOME
 *
 * @file Client-side functionality specifically associated with the home page
 * @author Katherine Trunkey (katherine.trunkey@ignia.com)
 */

$(function() {
  'use strict';

  /**
   * Establish variables
   */
  var topOffset                 = $('#Header').height();

  /**
   * Initializes Foundation smooth scrolling for homepage navigation arrows
   */
  var
    smoothScrollOptions         = {
      animationDuration         : 750,
      offset                    : (topOffset - 25)
    },
    splashSmoothScroll          = new Foundation.SmoothScroll($('#SplashArrow'), smoothScrollOptions),
    clientsSmoothScroll         = new Foundation.SmoothScroll($('#ClientsArrow'), smoothScrollOptions);

});
