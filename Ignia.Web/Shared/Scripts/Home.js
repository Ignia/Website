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
  var
    viewportWidth               = $(window).width(),
    topOffset                   = $('#Header').height(),
    headingBuffer               = $('#Introduction header:first-child').height();

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
   * Adds height to panels needing additional buffer for scroll-based functionality or animations (see Home.Animations.js), for
   * tablet or larger screens
   */
  if (viewportWidth > 767) {
    $('#Services').height($('#Services').height() * 1.25);
  }

  /**
   * Highlights the "active" navigation as the corresponding panel comes into view
   */
  $(window).scroll(function () {

    // Establish variables
    var
      menuItem,
      scrollPosition            = $(window).scrollTop() + topOffset + headingBuffer;

    // Highlight 'About (01)' item
    if (scrollPosition >= $("#Introduction").offset().top && scrollPosition <= $("#Services").offset().top) {
      menuItem                  = '#IntroductionAnchor, #IntroductionAnchorSmallScreen';
      setActiveNavigation(menuItem);
    }

    // Highlight 'Services (02)' item
    if (scrollPosition >= $("#Services").offset().top && scrollPosition <= $("#ClientHighlights").offset().top) {
      menuItem                  = '#ServicesAnchor, #ServicesAnchorSmallScreen';
      setActiveNavigation(menuItem);
    }

    // Highlight 'Clients (03)' item
    if (scrollPosition >= $("#ClientHighlights").offset().top && scrollPosition <= $("#Contact").offset().top) {
      menuItem                  = '#ClientHighlightsAnchor, #ClientHighlightsAnchorSmallScreen';
      setActiveNavigation(menuItem);
    }

    // Highlight 'Contact (04)' item
    if (scrollPosition >= $('#Contact').offset().top) {
      menuItem                  = '#ContactAnchor, #ContactAnchorSmallScreen';
      setActiveNavigation(menuItem);
    }

  });

  /**
   * Handles Services panel click functionality in order to fire off selected Service
   */
  $('.panel.services .categories.navigation ul li a').click(function(event) {

    // Prevent anchor-based navigation
    event.preventDefault();

    // Select appropriate Service
    selectService($(this).attr('href'));

  });

  /**
   * Monitors the scroll position within the Services panel, in order to highlight sections and swap out the content.
   */

  // Establish variables
  var
    servicesAreaTop             = ($('#Services').offset().top - topOffset),
    servicesAreaHeight          = ($('#Services').height()),
    servicesAreaSectionHeight   = (servicesAreaHeight / 3),
    servicesScrollRegion        = (servicesAreaTop + servicesAreaHeight),
    sectionCloudStart           = (servicesAreaTop + servicesAreaSectionHeight),
    sectionCmsStart             = (sectionCloudStart + servicesAreaSectionHeight);

  // Calculate selected service on scroll
  $(window).scroll(function() {

    var scrollPosition          = $(window).scrollTop();

    // Highlight Responsive Web Apps section
    if (scrollPosition >= servicesAreaTop && scrollPosition < sectionCloudStart) {
      selectService('#ResponsiveService');
    }

    // Highlight Cloud-based APIs section
    if (scrollPosition >= sectionCloudStart && scrollPosition < sectionCmsStart) {
      selectService('#CloudAPIService');
    }

    // Highlight Content Management Systems section
    if (scrollPosition >= sectionCmsStart && scrollPosition < servicesScrollRegion) {
      selectService('#CMSService');
    }

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
