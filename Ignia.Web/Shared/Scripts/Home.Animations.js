/**
 * HOME: ANIMATIONS
 *
 * @file Client-side functionality defining animations for the home page, such as the panel wipes, "factory" movement, and
 * client logos.
 * @author Katherine Trunkey (katherine.trunkey@ignia.com)
 */

$(function() {
  'use strict';

  /**
   * Establish variables
   */
  var
    topOffset                   = $('#Header').outerHeight(),
    mainContentHeight           = ($(window).height() - topOffset),
    slides                      = $('.panel').not('.splash'),
    sceneController             = new ScrollMagic.Controller();

  /**
   * Sets up pin / section wipe scene for splash panel
   */
  var splashPinScene            = new ScrollMagic.Scene({
    triggerElement              : '#Introduction',
    triggerHook                 : 1
  }).setPin('#Splash', { pushFollowers: false }).addTo(sceneController);

  /**
   * Creates scale and opacity tweens for featured client logos
   */
  var
    clientLogosItem             = $('#ClientHighlights section.logos ul li'),
    clientLogosDuration         = ($('#ClientHighlights').innerHeight()*0.8),
    clientLogosTween            = TweenMax.staggerFromTo(
      clientLogosItem,
      3,
      { autoAlpha: 0, scale: 0 },
      { autoAlpha: 1, scale: 1, ease: Power2.easeIn, autoCSS: true },
      0.25
    ),
    clientLogosScene            = new ScrollMagic.Scene({
      triggerElement            : '#ClientHighlights section.logos',
      duration                  : clientLogosDuration,
      triggerHook               : 1,
    }).setTween(clientLogosTween).addTo(sceneController);

  /**
   * Set panel scenes for tablet and larger screens
   */
  if ($(window).width() > 767) {

    // Establish variables
    var servicesPinDuration     = $('#Services').height();
    if ($(window).height() > $(window).width()) {
      servicesPinDuration       = (servicesPinDuration * 0.85);
    }

    // Services panel pin
    var servicesPinScene        = new ScrollMagic.Scene({
      triggerElement            : '#Services',
      offset                    : -topOffset,
      duration                  : servicesPinDuration,
      triggerHook               : 0
    }).setPin('#Services').addTo(sceneController);

  }

  /**
   * Set panel scenes for large screens
   */
  if ($(window).width() > 959) {

    // Establish variables
    var introductionPanelHeader = '#Introduction header:first-child';

    // Introduction panel header pin
    var introHeaderScene        = new ScrollMagic.Scene({
      triggerElement            : '#Introduction',
      offset                    : -topOffset,
      triggerHook               : 0,
      duration                  : ($('#Introduction').height() - mainContentHeight)
    }).setPin(introductionPanelHeader, { pushFollowers: false }).addTo(sceneController);

  }

});
