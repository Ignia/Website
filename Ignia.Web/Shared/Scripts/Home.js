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

  /**
   * Handles Services panel click functionality in order to fire off selected Service
   */
  $('.panel.services .categories.navigation ul li a').click(function(event) {

    // Prevent anchor-based navigation
    event.preventDefault();

    // Select appropriate Service
    selectService($(this).attr('href'));

  });

});

/**
 * Sets the "active" class on the Service category navigation as well as the associated details area on click of the category
 * navigation. Also moves the indicator arrow on the details area border.
 *
 * @param {string} selectedService - The HREF value for the selected service category.
 */
function selectService(selectedService) {
  'use strict';

  // Establish variables
  var arrowIndicator            = '.panel.services div.details:before, .panel.services div.details:after';

  // Set the "active" class on the navigation and associated service category content
  $('.panel.services .categories.navigation ul li a, .panel.services .details article').blur().removeClass('active');
  $('nav.categories.navigation li a[href="' + selectedService + '"], ' + selectedService).addClass('active');

  // Set position of indicator arrow on details area border
  if (selectedService === '#CloudAPIService') {
    $(arrowIndicator).css('left', '33%');
  }
  else if (selectedService === '#CMSService') {
    $(arrowIndicator).css('left', 'auto').css('right', '0');
  }
  else {
    $(arrowIndicator).css('left', '0');
  }

};
