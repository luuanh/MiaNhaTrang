
/*
 * Version 1.0

 /**********************************************************************
     globalVar
 ***********************************************************************/
var globalVar  = window.globalVar || {};
    globalVar.curPage = "undefined";


// your variables here ========================================================
var mobile;
var isLanguage = 'en';
var hasTag = "";
var lang = "";
var page = "";
var isScrollPage = true;
var sh = window.innerHeight;
var sw = window.innerWidth;
var isSubPage = false;
var isSafari = navigator.vendor && navigator.vendor.indexOf('Apple') > -1 &&
               navigator.userAgent && !navigator.userAgent.match('CriOS');

//auto run after the page is full loaded.
$(document).ready(function(){


	var devMode = "live";// "local" or stage" or "live"
	var url;

	//switch(devMode){
	//	case "local":
	//		url = "http://localhost/"
	//		break;
	//	case "stage":
	//		url = "http://www.mianhatrang.com/stage/"
	//		break;
	//	case "live":
	//		url = "http://www.mianhatrang.com/"
	//		break;
	//}


	//detect ie version.
	var ieVersion = ui.detectIEVersion();
	if(ieVersion != false){
		trace("addClass IE"); 

		$('body').addClass("IE");

		if(ieVersion < 10){
			$('body').addClass("dontSupportThisBrowser");
			$('#checkIEVersion').addClass('show');
			return;
		}		
	}

	//------------------- for develope ----------------//
	//remove this code when live

		var count = 0;
		var total = 1;

		if($('.subPage').length > 0) {
			path ="../";
			isSubPage = true;
		}

		//load header
			//$("header").load(url + "header.html",checkLoad);

		//load footer
			//if($("footer").length > 0){
			//	total = 2;
			//	$("footer").load(url + "footer.html",checkLoad);
			//} 

		//load popup
			//$('#popUp').load(url + "popup.html",checkLoad);

		//load mia experience popup
			//$('#experience-popUp').load(url + "popup.html",checkLoad);
		//
    checkLoad();
		function checkLoad(){
			count++;
			if(count == total) init();
		}

	//-------------------end  for develope ----------------//


	//------------------- for live ----------------//
		// use this code for live
		//init();
	//------------------- end for live ----------------//




	function init(){
		// detect mobile
		mobile = (/iphone|ipad|ipod|android|blackberry|mini|windows\sce|palm/i.test(navigator.userAgent.toLowerCase()));
		// trace("mobile =" + mobile);

		// var $links = $("a");

		// $links.each(function(){
		// 	var href = $(this).attr("href");

		// 	if(isSafari){
		// 		if(href.indexOf("http") == -1 && href != "#"){

		// 			// Clear the href
		// 			$(this).attr("href", "");

		// 			// Strip andy folder steps
		// 			var newHref = href.replace("../", "");					

		// 			// Combine
		// 			href = url + newHref;


		// 			$(this).attr("href", href);
		// 		}
		// 	}
		// });

		$(".menuBtn-contact").on("click", handleContactClick);
		$('#bl-offer').on('click', handleOfferClick)

		// page array
			var pages = [
				[ '#homePage', homePage],
				[ '#roomPage', roomPage],
				[ '#sleepPage', sleepPage],
				[ '#dinePage', dinePage],
				[ '#unwindPage', unwindPage],
				[ '#enlightenPage', enlightenPage],
				[ '#galleryPage', galleryPage]	
							
			];

		//get current page
			for (var i = 0; i < pages.length; i++){
				if ($(pages[i][0]).length == 1)
				pages[i][1]();
			}

		//deny click event with href="#"
		$("a[href='#']").click(function(e){
		  	e.preventDefault();
		 })


		//call this function first time.
		ui.init();


		var mobileHover = function () {
			$('*').on('click', function () {
			    $(this).trigger('hover');
			})
		};

		if(mobile) mobileHover();
	}

});

// homePage ====================================================================
function homePage(){	

	var linkArr = ["home","wakeup","getwet","induldge","revive","enjoythenight", "fountain"];
	var curPage = 0;
	var height = window.innerHeight;
	var pageArr = [];
	var total=0;

	// Show popup!
	// ui.showExperiencePopUp();

	$(document).bind('touchmove', false);

	$('.logo').addClass("active");
	//
	$('.pageItem').each(function(e){
		pageArr.push($(this));
	})

	total = pageArr.length;

	//resize
	$( window ).resize(function() {
		height = window.innerHeight;
		ui.scrollFn(curPage*height);
	});

	//
	controlPage();

		//
	$('.experienceBtn').on('click',function(){
		nextPage();
	})

	$('.back-to-top').on('click',function(){
		nextPage();
	})


	function controlPage(){

		//arrow button
		var timeout;
		if(!mobile) $('.arrowBtn').on('click',nextPage);
		else $('.arrowBtn').on('touchstart',nextPage);


		callback();

		$('#fullpage').fullpage({
	        anchors: linkArr,
	        onLeave:function(anchorLink, index){
	        	curPage = index-1;
				callback();
	        },
	        afterLoad: function(anchorLink, index){
	            
	        }
	    });


		//remove videoBG on mobile
	    if(mobile && window.innerWidth<768){
	    	$('.videoBG ').remove();
	    }
	}

	//next page
	function nextPage(){
		trace("nextPage");
		curPage++;
		htmlAddress_setValue(linkArr[curPage]);
	}

	//pre page
	function prePage(){
		trace("prePage");
		curPage--;
		htmlAddress_setValue(linkArr[curPage]);
	}


	 function callback(){
		for(var i=0; i<pageArr.length; i++){
			var item = pageArr[i].find('.container');
			if(i == curPage){
				item.stop(true,true).delay(200).fadeIn(300);
			}else{
				item.stop(true,true).fadeOut(300);
			}
	
		}
	}	

	function addVideo(){
		trace("addVideo - curPage =" + curPage);
		var video = pageArr[curPage].find("video");
		var videourl = video.attr('videourl');
		var mp4 = videourl + ".mp4";
		var ogg = videourl + ".ogv";

		$(video).find('source').remove();
		video.append('<source src="' + mp4 +'" type="video/mp4"> <source src="' + ogg +'" type="video/ogg">');
	}


	///
	function htmlAddress_setAddress(){

		var hash = window.location.hash.substr(2);
	    if(hash == "") htmlAddress_setValue("home");
	    htmlAddress_changeAddresss();    

	    $.address.change(function(event){
	     	htmlAddress_changeAddresss();
	  	}) 

	  	
	}

	//changeAddresss
	function htmlAddress_changeAddresss(){
		var pagename = htmlAddress_getPagename();
		trace("pagename =" + pagename);

		    if(globalVar.curPage != pagename){
		       curPage = getPageID(pagename);
		       if(curPage == -1) curPage = 0;
		       movePage();
		    }
		    globalVar.curPage = pagename;
	}

	function getPageID(pagename){
		for(var i=0; i<linkArr.length; i++){
    		if(pagename == "/" +  linkArr[i]) return i;
    	}

    	return -1;
	}
}

// sleepPage ====================================================================
function sleepPage(){
	$('.menuBtn-sleep').addClass("active");

	$(".spo-details").on("click", handleSeeDetails);
	$(".inquire-btn").on("click", handleInquireClick);
	$(".special-offer-bug").on("click", handleSpecOffersBugClick);

	$(window).on("scroll", handlePageScroll);
}

function handleSpecOffersBugClick(e){
	ui.scrollFn($(".special-offers").offset().top);
}

function handlePageScroll(e){
	var $specialOffers = $(".special-offers");
	var margin = $specialOffers.offset().top - ($specialOffers.height());
	if($(window).scrollTop() >= margin){
		$(".special-offer-bug").addClass("hide-offer-bug");
	}else{
		$(".special-offer-bug").removeClass("hide-offer-bug");
	}
}

function handleSeeDetails(e){
	var $item = $(this).parent();
	var $desc = $item.find(".spo-desc");
	var $excerpt = $item.find(".spo-excerpt");

	if($desc.hasClass("hide-me")){
		$(this).html("HIDE DETAILS -");
		$desc.removeClass("hide-me");
		$excerpt.addClass("hide-me");
	}else{
		$desc.addClass("hide-me");
		$excerpt.removeClass("hide-me");
		$(this).html("SEE DETAILS +");
	}
}

function handleInquireClick(e){
	var $spoItem =$(this) .parent();
	var inquiryTitle = $spoItem.attr("data-item-title");

	$(".core .title").html("OFFER INQUIRY");
	$(".core .subtitle").html(inquiryTitle);

	ui.showPopUp();
}

function handleContactClick(e){
	ui.showPopUp();

	$(".core .title").html("CONTACT US");
	$(".core .subtitle").html("General Customer Request");
}

function handleOfferClick(e) {
	ui.showExperiencePopUp();
}

// roomPage ====================================================================
function roomPage(){

	// if(isSafari){
		// adjustHeaderFooterImagesSrc();
	// }

	initCarousel();

	if(mobile && window.innerWidth >=768){
		$('body').addClass('touchScreen');
	}
}










// Carousel Controls

var carouselItemLength;
var activeCarouselItemIndex = 0;
var carouselInterval;
var carouselInactivityTimer;



function initCarousel(){
	 carouselItemLength = $(".carousel-item").length;
	

	// Carousel Init
	$(".carousel-item").on("click", handleCarouselItemClick).eq(activeCarouselItemIndex).addClass("active-carousel-item");



	// Dot Init
	for(var i=0; i <= carouselItemLength-1; i++){
		var newDot = "<div class='dot' data-index='" + i + "'></div>"
		$(".dots-nav").append(newDot);
	}

	// 
	$(".dot").on("click", handleCarouselDotSelected);
	$(".dot[data-index='" + activeCarouselItemIndex + "']").addClass("dot-active");




	// Timeout
	initCarouselRotation();
}


function initCarouselRotation(){
	carouselInterval = setInterval(function(){
		if(activeCarouselItemIndex >= carouselItemLength - 1){
			activeCarouselItemIndex = 0;
		}else{
			activeCarouselItemIndex++;
		}

		$(".carousel-item").removeClass("active-carousel-item").eq(activeCarouselItemIndex).addClass("active-carousel-item");

		// Dot
		$(".dot").removeClass("dot-active");
		$(".dot[data-index='" + activeCarouselItemIndex + "']").addClass("dot-active");

	}, 3250);
}


function handleCarouselItemClick(e){

	delayCarouselRotation();

	if(activeCarouselItemIndex >= carouselItemLength - 1){
		activeCarouselItemIndex = 0;
	}else{
		activeCarouselItemIndex++;
	}

	$(".carousel-item").removeClass("active-carousel-item").eq(activeCarouselItemIndex).addClass("active-carousel-item");

	// Dot
	$(".dot").removeClass("dot-active");
	$(".dot[data-index='" + activeCarouselItemIndex + "']").addClass("dot-active");
}



function delayCarouselRotation(){
	// Clear carouselInterval
	clearInterval(carouselInterval);

	// Reset carouselInactivityTimer
	if(carouselInactivityTimer != undefined){
		clearTimeout(carouselInactivityTimer);
	}

	// Start/Restart carouselInactivityTimer
	carouselInactivityTimer = setTimeout(function(){
		initCarouselRotation();
	}, 8000);
}



function handleCarouselDotSelected(e){

	var dotIndex = $(this).attr("data-index");

	activeCarouselItemIndex = dotIndex;

	// Clear all active dots
	$(".dot").removeClass("dot-active");

	// Delay carouselInterval
	delayCarouselRotation();

	// Remove all instances of active dot
	$(".carousel-item").removeClass("active-carousel-item").eq(dotIndex).addClass("active-carousel-item");

	// Set active dot
	$(".dot[data-index='" + dotIndex + "'").addClass("dot-active");

}
//================================================================









// function adjustHeaderFooterImagesSrc(){
// 	console.log("adjustHeaderFooterImagesSrc()");
	
// 	var $headerLogoImg = $(".header-nav-holder .logo img");
// 	var headerLogoSrc = $headerLogoImg.attr("src");

// 	var $footerLogoImg = $("footer .logo img");
// 	var footerLogoSrc = $footerLogoImg.attr("src");

// 	var $apLogoImg = $(".ap-bug img");
// 	var apLogoSrc = $apLogoImg.attr("src");

// 	$(".award").each(function(){
// 		var $awardLogoImg = $(this).find("img");
// 		var awardLogoSrc = $awardLogoImg.attr("src");

// 		$awardLogoImg.attr("src", "../" + awardLogoSrc);
// 	})
	
// 	$headerLogoImg.attr("src", "../" + headerLogoSrc);
// 	$footerLogoImg.attr("src", "../" + footerLogoSrc);
// 	$apLogoImg.attr("src", "../" + apLogoSrc);
// }



// dinePage ====================================================================
function dinePage(){
	$('.menuBtn-dine').addClass("active");
}

// unwindPage ====================================================================
function unwindPage(){
	$('.menuBtn-unwind').addClass("active");

	$(".plan-now").on("click", handleContactClick);
}

// enlightenPage ====================================================================
function enlightenPage(){
	$('.menuBtn-enlighten').addClass("active");
}


// pressPage ====================================================================
function pressPage(){

}

// galleryPage ====================================================================
function galleryPage(){

	var thumbItem = $('.thumbItem');

	thumbItem.each(function(e){
		var thumb = $(this);
		thumb.find('.img').on('click',function(e){
			var photoID = Number($(this).attr("photoID"));
			setUp($('.gallery'),photoID);
		});
	});

	//
	function setUp(target,photoID){

		var nextBtn = target.find('.controller .nextBtn');
		var preBtn = target.find('.controller .preBtn');
		
		var galleryItem = target.find('.galleryItem');
		var item = galleryItem.find('.item');
		var number = target.find('.number');
		var bg = target.find('.bg');
		var total = item.length;
		var curID = photoID;
		var itemArr = [];
	

		target.slideDown("200");

		item.each(function(id){
			itemArr.push($(this));
			if(id != curID) $(this).addClass("dn");
			else $(this).removeClass("dn");
		});

	
		setNumber();
		nextBtn.off('click');
		preBtn.off('click');

		nextBtn.on('click',nextFn);
		preBtn.on('click',preFn);

		bg.on('click',function(e){
			target.slideUp("200");
		});		

		function nextFn(){
			curID++;
			if(curID >=total) curID =0;
			changeItem();
		}

		function preFn(){
			curID--;
			if(curID < 0) curID =total-1;
			changeItem();
		}

		function changeItem(){
			trace("changeItem" + curID);

			for(var i=0; i<total; i++){
				var ob = itemArr[i];
				if(curID == i){
					ob.removeClass('dn');
				}else{
					ob.addClass('dn');
				}
			}

			//
			setNumber();
		}

		function setNumber(){
			number.text((curID+1) + "/" + total)	
		}
	}	
}

// library ====================================================================

// ui ====================================================================
var ui = {
	init:function(){

		////
		if(window.innerWidth <= 767){
			ui.mobile_menu();
		}

		//change path for img and href if it is subpage in subfolder
		if(isSubPage) ui.changePath($('.header-nav-holder'),"../");
		if(isSubPage) ui.changePath($('footer'),"../");

		//ui.scrollFn(0);
	},//init
	changePath:function(target,path){
		$(target).find("img").each(function(e){
			var src = path + $(this).attr('src');
			$(this).attr('src',src);
		})

		$(target).find("a").each(function(e){
			var href = $(this).attr('href');
			var newHref;
			if(href.indexOf("http") == -1 && href != "#"){
				newHref = path + $(this).attr('href');
				$(this).attr('href',newHref);
			}
			
		})
	},//changePath

	getHastags:function(){
		if(window.location.hash) {
      		return window.location.hash.substring(1); //Puts hash in variable, and removes the # character
      	} else {
      		return null;
  		}
	},//end getHastags

	scrollFn:function(dy,callBack){
		trace("scrollFn =" + dy)
		$("body, html").stop(true).animate({ scrollTop: dy}, "slow",function(){
			if(callBack != undefined) callBack();
		});

	},// end scrollFn

	mobile_menu:function(txt){		

		switch(txt){
			case 'show': showFn(); return;
			case 'hide': hideFn(); return;
		}

		$('header .menuBtn').on('click',function(e){
			if($(this).hasClass('open')) hideFn();
			else showFn();
		})

		$('#main').on('click',function(e){
			hideFn();	
		})

		function showFn(){
			trace("show")
			$('header .menuBtn').addClass('open');
			$('header .desktop').addClass('slideLeft');
			$('#main').addClass('overlay');
			$('footer').addClass('overlay');
		}

		function hideFn(){
			$('header .menuBtn').removeClass('open');
			$('header .desktop').removeClass('slideLeft');
			$('#main').removeClass('overlay');
			$('footer').removeClass('overlay');
		}


	},//mobile_menu

	showPopUp:function(target,dur){
		$('.popUpItem').addClass('dn');
		$('.popUpItem.' + target).removeClass('dn');

		if(dur == undefined) dur = 300;
		var popUp = $('#popUp');
		popUp.fadeIn(dur);

		if(window.innerWidth <= 767) { ui.mobile_menu('hide'); }

		$('#main').addClass('noScroll');

		popUp.click(function() {
			ui.hidePopUp();
		});

		popUp.find('.core').hide();
		popUp.find('.popup-contact').show();

		$('#popUp .core').click(function(event){
		    event.stopPropagation();
		});
	},// end showPopUp

	showExperiencePopUp:function(target,dur){
		$('.popUpItem').addClass('dn');
		$('.popUpItem.' + target).removeClass('dn');

		if(dur == undefined) dur = 300;
		var popUp = $('#popUp');
		popUp.fadeIn(dur);

		if(window.innerWidth <= 767) { ui.mobile_menu('hide'); }

		$('#main').addClass('noScroll');

		popUp.click(function() {
			ui.hidePopUp();
		});

		popUp.find('.popup-contact').hide();
		// popUp.find('.core').hide();
		popUp.find('.popup-offer').show();

		$('#popUp .core').click(function(event){
			event.stopPropagation();
		});
	},
	hidePopUp:function(dur){
		if(dur == undefined) dur = 300;
		// $('body').removeClass('noScroll');		
		$('#popUp').fadeOut(dur);	
		$('#main').removeClass('noScroll');
		$("#popUp").off();
		$('#popUp .core').off();

	},// end hidePopUp		
	detectIEVersion:function(){
		var ua = window.navigator.userAgent;

	  // Test values; Uncomment to check result …

	  // IE 10
	  // ua = 'Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; Trident/6.0)';
	  
	  // IE 11
	  // ua = 'Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko';
	  
	  // Edge 12 (Spartan)
	  // ua = 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36 Edge/12.0';
	  
	  // Edge 13
	  // ua = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.10586';

	  var msie = ua.indexOf('MSIE ');
	  if (msie > 0) {
	    // IE 10 or older => return version number
	    return parseInt(ua.substring(msie + 5, ua.indexOf('.', msie)), 10);
	  }

	  var trident = ua.indexOf('Trident/');
	  if (trident > 0) {
	    // IE 11 => return version number
	    var rv = ua.indexOf('rv:');
	    return parseInt(ua.substring(rv + 3, ua.indexOf('.', rv)), 10);
	  }

	  var edge = ua.indexOf('Edge/');
	  if (edge > 0) {
	    // Edge (IE 12+) => return version number
	    return parseInt(ua.substring(edge + 5, ua.indexOf('.', edge)), 10);
	  }

	  // other browser
	  return false;
	}//detectIEVersion
	
}//end ui

//trace your message
function trace(str){
	console.log(str);
}

//rotation a object
jQuery.fn.rotate = function(degrees) {
	$(this).css({'-webkit-transform' : 'rotate('+ degrees +'deg)',
			 '-moz-transform' : 'rotate('+ degrees +'deg)',
			 '-ms-transform' : 'rotate('+ degrees +'deg)',
			 'transform' : 'rotate('+ degrees +'deg)'});
};

//rotation a object
jQuery.fn.translateY = function(n) {
	$(this).css({
			'webkit-transform': 'translateY('+n+'px)',
			'-ms-transform': 'translateY('+n+'px)',
			'transform': 'translateY('+n+'px)'
		});
};

jQuery.fn.translateX = function(n) {
	$(this).css({
			'webkit-transform': 'translateX('+n+'px)',
			'-ms-transform': 'translateX('+n+'px)',
			'transform': 'translateX('+n+'px)'
		});
};


function htmlAddress_setValue(txt){
  window.location.hash = txt;
}

function htmlAddress_getValue(){
	return window.location.hash.substr(1);
}

function htmlAddress_getPagename(){
	var path = htmlAddress_getValue();
	var index = path.indexOf("?");
	
	if(index == -1) return path;
	else return path.substr(0,index);
}

function htmlAddress_getParameter(txt){
	var path = htmlAddress_getValue();
	var index = path.indexOf("?");
	if(index == -1) return -1;

	var str = path.substr(index + 1);
	var arr = str.split("&");

	for(var i=0; i<arr.length; i++){
		str = arr[i];
		index = str.indexOf(txt + "=");
		if(index != -1){
			return str.substr(index + 1 + txt.length,str.length)
		}
	}
	return -1;
}

