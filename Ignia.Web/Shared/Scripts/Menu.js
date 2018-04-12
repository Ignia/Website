/**
 * MENU / NAVIGATION
 *
 * @file Functions associated with the primary navigation, including menu smooth scroll and highlight functionality.
 * @author Katherine Trunkey (katherine.trunkey@ignia.com)
 */

$(function () {
  'use strict';

  /**
   * Fire menu-dependent functions on initialization
   */
  setMenuLinks();
  setSmoothScrolling();

  /**
   * Re-fire menu-dependent functions on resize
   */
  var resizeTimer;
  $(window).on('resize', function(e) {
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(function() {
      setMenuLinks();
      setSmoothScrolling();
    }, 250);
  });

  /**
   * Sets the active state of the tapped-on navigation item;
   * closes the off-canvas menu on tap of one of the navigation items
   */
  $('#PrimaryNavigation ul li a, #PrimaryNavigationSmallScreen ul li a').click(function (event) {
    $('#PrimaryNavigation ul li a, #PrimaryNavigationSmallScreen ul li a').removeClass('active');
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
 * Re-associates menu item targets for smaller screens
 */
function getMenuElement() {
  if ($(window).width() < 960) {
    return $('#PrimaryNavigationSmallScreen li a:not("#LibraryLinkSmallScreen")');
  }
  return $('#PrimaryNavigation li a:not("#LibraryLink")');
}

/**
 * Prepends Homepage path to menu links on non-Home (Library) views
 */
function setMenuLinks() {
  $(getMenuElement()).each(function() {
    var
      href                      = $(this).attr('href'),
      hash                      = href.substring(href.indexOf('#')),
      homeHref                  = hash.replace('#', '/Web/Home/#');
    if (window.location.pathname.toLowerCase().indexOf('web/home') < 0) {
      $(this).attr('href', homeHref);
    }
    else {
      $(this).attr('href', hash);
    }
  });
}

/**
 * Initializes Foundation smooth scrolling for menu items
 */
function setSmoothScrolling() {
  if (window.location.pathname.toLowerCase().indexOf('web/home') >= 0) {
    var
      smoothScrollOptions       = {
        animationDuration       : 1000,
        offset                  : ($('#Header').height() - 25)
      },
      menuSmoothScroll          = new Foundation.SmoothScroll(getMenuElement(), smoothScrollOptions);
  }
}

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
