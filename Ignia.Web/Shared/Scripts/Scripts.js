"use strict";function resetNavHighlight(){$("#PrimaryNavigation ul li a").removeClass("active")}$(function(){var e=$(window),t=(document.documentElement,$("#Header").height()),o=$("#Introduction header:first-child").height(),a=(e.height(),new ScrollMagic.Controller,$("#PrimaryNavigation").height()-($("#Header").height()+1));e.width()<960&&$("#PrimaryNavigation").css("top",-a),resetNavHighlight(),e.scroll(function(){var a=e.scrollTop()+t+o;a>=$("#Introduction").offset().top&&a<=$("#Services").offset().top&&(resetNavHighlight(),$("#IntroductionAnchor").addClass("active")),a>=$("#Services").offset().top&&a<=$("#ClientHighlights").offset().top&&(resetNavHighlight(),$("#ServicesAnchor").addClass("active")),a>=$("#ClientHighlights").offset().top&&a<=$("#Contact").offset().top&&(resetNavHighlight(),$("#ClientHighlightsAnchor").addClass("active")),a>=$("#Contact").offset().top&&(resetNavHighlight(),$("#ContactAnchor").addClass("active"))});var i=new TimelineMax({paused:!0}).add(TweenMax.to("#PrimaryNavigation",.25,{top:0})),n=new TimelineMax({paused:!0}).add(TweenMax.to("#PrimaryNavigation .menu-toggle a.open",.15,{scale:0,opacity:0,ease:Power2.easeNone})).add(TweenMax.to("#PrimaryNavigation .menu-toggle a.close",.15,{scale:1,opacity:1,ease:Power2.easeNone})),r=new TimelineMax({paused:!0}).add(TweenMax.to("#PrimaryNavigation img.annotation",1,{opacity:1,ease:Power4.easeIn}));$("#PrimaryNavigation .menu-toggle a").on("click",function(e){e.preventDefault(),$(this).hasClass("open")?(i.play(),n.play(),$("#PrimaryNavigation ul").removeClass("closed").addClass("opened"),r.play()):(i.reverse(),n.reverse(),$("#PrimaryNavigation ul").removeClass("opened").addClass("closed"),r.reverse())}),$("#PrimaryNavigation ul li a, #MainContent").on("click",function(t){e.width()<960&&($("#PrimaryNavigation ul").removeClass("opened").addClass("closed"),i.reverse(),n.reverse(),r.reverse())})}),function(){var e=new ScrollMagic.Controller({globalSceneOptions:{duration:$(".panel").height(),triggerHook:.025,reverse:!0}});new ScrollMagic.Scene({triggerElement:"#Introduction"}).setClassToggle("#IntroductionAnchor","active").addTo(e),new ScrollMagic.Scene({triggerElement:"#Services"}).setClassToggle("#ServicesAnchor","active").addTo(e),new ScrollMagic.Scene({triggerElement:"#ClientHighlights"}).setClassToggle("#ClientHighlightsAnchor","active").addTo(e),new ScrollMagic.Scene({triggerElement:"#Contact"}).setClassToggle("#ContactAnchor","active").addTo(e);e.scrollTo(function(e){console.log("target in navController.scrollTo: "+e),TweenMax.to(window,.85,{scrollTo:{y:e,autoKill:!1},ease:Power0.easeNone})}),$('#PrimaryNavigation ul li a, #Splash a[href="#Introduction"], #ClientHighlights a[href="#Contact"]').on("click",function(t){var o=$("#Header").height(),a=$(this).attr("href"),i=$(a).offset().top,n=$(window).height()-$("#Header").height();t.preventDefault(),"#Introduction"===a?e.scrollTo(n):$(window).width()<768?e.scrollTo(i-o):e.scrollTo(a),window.history&&window.history.pushState&&history.pushState("",document.title,a)})}();