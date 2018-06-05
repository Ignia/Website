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
    clientLogosDuration         = ($('#ClientHighlights').innerHeight() * 0.5),
    clientLogosTween            = TweenMax.staggerFromTo(
      clientLogosItem,
      3,
      { autoAlpha: 0, scale: 0 },
      { autoAlpha: 1, scale: 1, ease: SlowMo.ease.config(0.3, 0.4, false), autoCSS: true },
      0.25
    ),
    clientLogosScene            = new ScrollMagic.Scene({
      triggerElement            : '#ClientHighlights section.tagline h2.subheadline',
      triggerHook               : 1,
      offset                    : -48,
      duration                  : clientLogosDuration
    }).setTween(clientLogosTween).addTo(sceneController);

  /**
   * Gradually fades the Contact form and callout as the footer comes into view
   */
  var
    contactFormArea             = $('#Contact section'),
    contactFormAreaTween        = TweenMax.to(
      contactFormArea,
      0.9,
      { autoAlpha: 0.05, ease: Power0.easeNone }
    ),
    contactFormAreaScene        = new ScrollMagic.Scene({
      triggerElement            : '#Footer',
      triggerHook               : 1,
      duration                  : $('#Footer').height()
    }).setTween(contactFormAreaTween);
  if ($(window).width() > 959 && $('#Contact').height() < 1000) {
    // $('#Footer').addClass('transparent');
    // contactFormAreaScene.addTo(sceneController);
  }

  /**
   * Set panel scenes for tablet and larger screens
   */
  if ($(window).width() > 767) {

    // Establish variables
    var servicesPanelHeader     = '#Services header:first-child';

    // Services panel header pin
    var servicesHeaderScene     = new ScrollMagic.Scene({
      triggerElement            : '#Services',
      triggerHook               : 0,
      offset                    : -topOffset,
      duration                  : ($('#Services .tagline .heading-block').outerHeight())
    }).setPin(servicesPanelHeader, { pushFollowers: false }).addTo(sceneController);

    // Services panel categories animations
    $('#Services article').each(function () {
      var
        category                = '#' + $(this).attr('id'),
        categoryHeaderTween     = TweenMax.to(category + ' header h2 span', 0.075, {
          transform             : 'scale(1.1)',
          ease                  : Back.easeOut.config(2),
          repeat                : 1,
          yoyo                  : true
        }),
        sceneDuration           = ($(category).height() - 72);

      if (category === '#CMSService') {
        sceneDuration           = ($(category).height() - 104);
      }

      // Heading text scaling
      new ScrollMagic.Scene({
        triggerElement          : category,
        triggerHook             : 1,
        offset                  : (mainContentHeight/2 - 60)
      }).setTween(categoryHeaderTween).addIndicators().addTo(sceneController);

      // Highlight and heading pin
      new ScrollMagic.Scene({
        triggerElement          : category,
        triggerHook             : 1,
        offset                  : (mainContentHeight/2 - 60),
        duration                : sceneDuration
      }).setClassToggle(category, 'is-active').setPin(category + ' header h2', { pushFollowers: false }).addTo(sceneController);

    });

    // Contact panel pin
    var
      contactPanelAllowance     = ($('#Header').outerHeight() + $('#Contact > .container').outerHeight() + $('#Footer').outerHeight()),
      contactPinScene           = new ScrollMagic.Scene({
        triggerElement          : '#Contact',
        triggerHook             : 0,
        offset                  : -topOffset
      }).setPin('#Contact', { pushFollowers: true });
    if (contactPanelAllowance <= $(window).outerHeight()) {
      contactPinScene.addTo(sceneController);
    }

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
        'img#FactoryWidget',
        10,
        { y: '260px', ease: Linear.easeNone, force3D: true }
      ),
      codeFactoryScene          = new ScrollMagic.Scene({
        triggerElement          : '#FactoryWidget',
        triggerHook             : 1,
        offset                  : 10,
        duration                : '750'
      }).setTween(codefactoryTween).addTo(sceneController);
  }

});
