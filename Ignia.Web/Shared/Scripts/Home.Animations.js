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
   * Sets up overlay layer and opacity tweens for panels
   */
  $('article.panel').not('#Contact').each(function() {

    // Establish variables
    var
      panelId                   = $(this).attr('id'),
      panelOverlay              = $(this).find('span.overlay'),
      triggerPanel              = '#Introduction';

    // Conditionally set subsequent trigger panels
    if (panelId === 'Introduction') {
      triggerPanel              = '#Services';
    }
    if (panelId === 'Services') {
      triggerPanel              = '#ClientHighlights';
    }
    if (panelId === 'ClientHighlights') {
      triggerPanel              = '#Contact';
    }

    // Define tweens timeline
    var overlayTweens           = new TimelineMax()
      .add(TweenMax.to(panelOverlay, 0.1, { zIndex: 1, ease: Power0.easeNone }))
      .add(TweenMax.to(panelOverlay, 0.9, { autoAlpha: 0.8, ease: Power0.easeNone }, '-=0.05'));

    // Define scene
    var overlayScene            = new ScrollMagic.Scene({
      triggerElement            : triggerPanel,
      duration                  : mainContentHeight,
      triggerHook               : 1
    }).setTween(overlayTweens).addTo(sceneController);

  });


  /**
   * Creates scale and opacity tweens for featured client logos
   */
  var
    clientLogosItem             = $('#ClientHighlights section.logos ul li'),
    clientLogosDuration         = ($('#ClientHighlights').innerHeight() * 0.7),
    clientLogosTween            = TweenMax.staggerFromTo(
      clientLogosItem,
      3,
      { autoAlpha: 0, scale: 0 },
      { autoAlpha: 1, scale: 1, ease: SlowMo.ease.config(0.3, 0.4, false), autoCSS: true },
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
      triggerHook               : 0,
      offset                    : -topOffset,
      duration                  : servicesPinDuration
    }).setPin('#Services').addTo(sceneController);

    // Contact panel pin
    var contactPinScene         = new ScrollMagic.Scene({
      triggerElement            : '#Contact',
      triggerHook               : 0,
      offset                    : -topOffset
    }).setPin('#Contact', { pushFollowers: true }).addTo(sceneController);

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
      triggerHook               : 0,
      offset                    : -topOffset,
      duration                  : ($('#Introduction').height() - mainContentHeight)
    }).setPin(introductionPanelHeader, { pushFollowers: false }).addTo(sceneController);

    // Introduction panel code factory tween
    var
      codefactoryTween          = TweenMax.to(
        '#Introduction section.factory img',
        5,
        { top: '0', autoCSS: true, ease: SlowMo.ease.config(0.1, 0.1, false) }
      ),
      codeFactoryScene          = new ScrollMagic.Scene({
        triggerElement          : '#Introduction',
        triggerHook             : 0,
        offset                  : -topOffset,
        duration                : '660',
      }).setTween(codefactoryTween).addTo(sceneController);
  }

});
