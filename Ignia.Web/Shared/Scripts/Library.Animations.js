/**
 * LIBRARY: ANIMATIONS
 *
 * @file Client-side functionality defining animations for Library pages, such as the pinning of the Article metadata.
 * @author Katherine Trunkey (katherine.trunkey@ignia.com)
 */

$(function () {
  'use strict';

  /**
   * Establish variables
   */
  var
    topOffset                   = $('#Header').outerHeight(),
    mainContentHeight           = ($(window).height() - topOffset),
    sceneController             = new ScrollMagic.Controller();

  /**
   * Set scenes for tablet and larger screens
   */
  if ($(window).width() > 767) {

    // Article metadata pin
    var articleMetadataPinScene = new ScrollMagic.Scene({
      triggerElement            : '#References',
      triggerHook               : 0,
      offset                    : (-topOffset - 84),
      duration                  : ($('#ArticleBody').height() - $('#References').height() -64)
    }).setPin('#References', { pushFollowers: false }).addTo(sceneController);

  }

});