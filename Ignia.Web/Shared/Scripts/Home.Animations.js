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
    topOffset                   = $('#Header').height(),
    slides                      = $('.panel').not('.splash'),
    sceneController             = new ScrollMagic.Controller();

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

});
