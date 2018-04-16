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

  /**
   * Re-fire menu-dependent functions on resize
   */
  var resizeTimer;
  $(window).on('resize', function(e) {
    clearTimeout(resizeTimer);
    resizeTimer = setTimeout(function() {
      setMenuLinks();
    }, 250);
  });

  /**
   * Set active class on Library menu item if within the Library
   */
  if (window.location.pathname.toLowerCase().indexOf('library') >= 0) {
    $('#LibraryLink, #LibraryLinkSmallScreen').addClass('active');
  }

  /**
   * Sets the active state of the tapped-on navigation item;
   * closes the off-canvas menu on tap of one of the navigation items
   */
  $('#PrimaryNavigation ul li a, #PrimaryNavigationSmallScreen ul li a').click(function(event) {

    // Establish variables
    var
      menuItem                  = '#' + $(this).attr('id'),
      target                    = $(this).attr('href'),
      isLibraryItem             = (target.indexOf('Library') >= 0 || target.indexOf('/Web/Home/') >= 0);

    // Set the active navigation item
    setActiveNavigation(menuItem);

    // Smooth scroll to the target panel
    if (isLibraryItem === false) {
      Foundation.SmoothScroll.scrollToLoc(
        target,
        {
          duration              : 1000,
          threshold             : 25,
          offset                : ($('#Header').height() - 20)
        },
        function () {
          setActiveNavigation(menuItem);
        }
      );
    }

    // Force close of the small screen menu
    if (menuItem.indexOf("SmallScreen") >= 0) {
      closeOffCanvasMenu();
    }

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
function setActiveNavigation(menuItem) {
  'use strict';
  $('#PrimaryNavigation ul li a, #PrimaryNavigationSmallScreen ul li a').removeClass('active');
  if (typeof menuItem !== 'undefined' && menuItem.length) {
    $(menuItem).addClass('active');
  }
};

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
  * Sets the active state of the tapped-on navigation item;
  * closes the off-canvas menu on tap of one of the navigation items
  */
function closeOffCanvasMenu() {
  'use strict';
  setTimeout(function() {
    $('#PrimaryNavigationSmallScreen').foundation('close');
  }, 250);
};
