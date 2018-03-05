/**
 * MENU / NAVIGATION
 *
 * @file Functions associated with the primary navigation, including menu smooth scroll and highlight functionality.
 * @author Katherine Trunkey (katherine.trunkey@ignia.com)
 */

$(function () {
  'use strict';

  /**
   * Establish variables
   */
  var
    topOffset                   = $('#Header').height(),
    headingBuffer               = $('#Introduction header:first-child').height(),
    menuElement                 = $('#PrimaryNavigation li a:not("#LibraryLink")');

  if ($(window).width() < 960) {
    menuElement                 = $('#PrimaryNavigationSmallScreen li a:not("#LibraryLinkSmallScreen")');
  }

  /**
   * Initializes Foundation smooth scrolling for menu items
   */
  var
    smoothScrollOptions         = {
      animationDuration         : 1000,
      offset                    : (topOffset - 25)
    },
    menuSmoothScroll            = new Foundation.SmoothScroll(menuElement, smoothScrollOptions);

  /**
   * Fires navigation highlight reset on initialization
   */
  resetNavHighlight();

  /**
   * Highlights the "active" navigation as the corresponding panel comes into view
   */
  $(window).scroll(function() {

    var scrollPosition          = $(window).scrollTop() + topOffset + headingBuffer;

    // Highlight 'About (01)' item
    if (scrollPosition >= $("#Introduction").offset().top && scrollPosition <= $("#Services").offset().top) {
      resetNavHighlight();
      $('#IntroductionAnchor, #IntroductionAnchorSmallScreen').addClass('active');
    }

    // Highlight 'Services (02)' item
    if (scrollPosition >= $("#Services").offset().top && scrollPosition <= $("#ClientHighlights").offset().top) {
      resetNavHighlight();
      $('#ServicesAnchor, #ServicesAnchorSmallScreen').addClass('active');
    }

    // Highlight 'Clients (03)' item
    if (scrollPosition >= $("#ClientHighlights").offset().top && scrollPosition <= $("#Contact").offset().top) {
      resetNavHighlight();
      $('#ClientHighlightsAnchor, #ClientHighlightsAnchorSmallScreen').addClass('active');
    }

    // Highlight 'Contact (04)' item
    if (scrollPosition >= $('#Contact').offset().top) {
      resetNavHighlight();
      $('#ContactAnchor, #ContactAnchorSmallScreen').addClass('active');
    }

  });

  /**
   * Sets the active state of the tapped-on navigation item;
   * closes the off-canvas menu on tap of one of the navigation items
   */
  $('#PrimaryNavigationSmallScreen ul li a').click(function (event) {
    resetNavHighlight();
    $(this).addClass('active');
    closeOffCanvasMenu();
  });

  /**
   * Workaround for non-responsive close button
   * ### TODO: Resolve underlying issue
   */
  $('#PrimaryNavigationSmallScreen .close-button').click(function (event) {
    closeOffCanvasMenu();
  });

});

/**
 * Removes the "active" class from all primary navigation items
 */
function resetNavHighlight() {
  'use strict';
  $('#PrimaryNavigation ul li a, #PrimaryNavigationSmallScreen ul li a').removeClass('active');
};

/**
  * Sets the active state of the tapped-on navigation item;
  * closes the off-canvas menu on tap of one of the navigation items
  */
function closeOffCanvasMenu() {
  'use strict';
  setTimeout(function() {
    $('#PrimaryNavigationSmallScreen').foundation('close');
  }, 250);
};
