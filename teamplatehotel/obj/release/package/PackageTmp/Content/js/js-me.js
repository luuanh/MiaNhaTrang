$('#sl0').owlCarousel({
    loop: true,
    margin: 0,
    nav: true,
    navText: [
      "<i class='fa fa-caret-left'></i>",
      "<i class='fa fa-caret-right'></i>"
    ],
    autoplay: true,
    autoplayHoverPause: true,
    responsive: {
      0: {
        items: 1
      },
      600: {
        items: 1
      },
      1000: {
        items: 1
      }
    }
  });
  
$('#sl2').owlCarousel({
  loop: true,
  nav: true,
  margin: 10,
  navText: [
      "<i class='fas fa-chevron-circle-left'></i>",
      "<i class='fas fa-chevron-circle-right'></i>"
  ],
  autoplay: true,
  autoplayHoverPause: true,
  responsive: {
      0: {
          items: 1
      },
      600: {
          items: 1
      },
      1000: {
          items: 1
      }
  }
});
$('#sl3').owlCarousel({
    loop: true,
    margin: 0,
    nav: true,
    navText: [
      "<i class='fas fa-chevron-left'></i>",
      "<i class='fas fa-chevron-right'></i>"
    ],
    autoplay: true,
    autoplayHoverPause: true,
    responsive: {
      0: {
        items: 1
      },
      600: {
        items: 1
      },
      1000: {
        items: 1
      }
    }
  });

$('.owl-carouselDinning').owlCarousel({
    loop: true,
    margin: 0,
    nav: true,
    navText: [
      "<i class='fas fa-long-arrow-alt-left'></i>",
      "<i class='fas fa-long-arrow-alt-right'></i>"
    ],
    autoplay: true,
    autoplayHoverPause: true,
    responsive: {
      0: {
        items: 1
      },
      600: {
        items: 1
      },
      1000: {
        items: 1
      }
    }
  });

  $('#sl5').owlCarousel({
    loop: true,
    margin: 0,
    nav: true,
    navText: [
      "<i class='fas fa-long-arrow-alt-left'></i>",
      "<i class='fas fa-long-arrow-alt-right'></i>"
    ],
    autoplay: true,
    autoplayHoverPause: true,
    responsive: {
      0: {
        items: 1
      },
      600: {
        items: 1
      },
      1000: {
        items: 1
      }
    }
  });
  $('#sl6').owlCarousel({
    loop: true,
    margin: 0,
    nav: true,
    navText: [
      "<i class='fas fa-long-arrow-alt-left'></i>",
      "<i class='fas fa-long-arrow-alt-right'></i>"
    ],
    autoplay: true,
    autoplayHoverPause: true,
    responsive: {
      0: {
        items: 1
      },
      600: {
        items: 1
      },
      1000: {
        items: 1
      }
    }
  });

  $('#sl7').owlCarousel({
    loop: true,
    margin: 0,
    nav: true,
    navText: [
      " <i class='far fa-arrow-alt-circle-left'></i>",
      "<i class='far fa-arrow-alt-circle-right'></i>"
    ],
    autoplay: true,
    autoplayHoverPause: true,
    responsive: {
      0: {
        items: 1
      },
      600: {
        items: 1
      },
      1000: {
        items: 1
      }
    }
  });
 

$(function () {
    $('input[id="daterange"]').daterangepicker({
        //timePicker: true,
        opens: 'left',
        locale: {
            format: 'DD/M/YYYY'
        }
    }, function (start, end, label) {
        $("#CheckOut").val(end.format('YYYY-MM-DD'));
        $("#CheckIn").val(start.format('YYYY-MM-DD'));
        console.log("A new date selection was made: " + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD'));
    });
});


// if ($('.wow').hasClass('animated')) {
//     $(this).removeClass('animated');
//     $(this).removeAttr('style');
//     new WOW().init();
// }


  $(document).ready(function(){
    
    $("#showPanel").click(function(){
        $("#panel").show();
    });
    
    $("#hidePanel").click(function(){
        $("#panel").hide();
    });
  
});

$(window).scroll(function() {
    if ($(this).scrollTop() >= 50) {        // If page is scrolled more than 50px
        $('#return-to-top').fadeIn(200);    // Fade in the arrow
    } else {
        $('#return-to-top').fadeOut(200);   // Else fade out the arrow
    }
});
$('#return-to-top').click(function() {      // When arrow is clicked
    $('body,html').animate({
        scrollTop : 0                       // Scroll to top of body
    }, 500);
});

$(document).ready(function(){
	
	$('ul.tabtour li').click(function(){
		var tab_id = $(this).attr('data-tab');

		$('ul.tabtour li').removeClass('current');
		$('.tab-content').removeClass('current');

		$(this).addClass('current');
		$("#"+tab_id).addClass('current');
	})

})

$(document).ready(function(){
	
	$('ul.tabs li').click(function(){
		var tab_id = $(this).attr('data-tab');

		$('ul.tabs li').removeClass('current');
		$('.tab-content').removeClass('current');

		$(this).addClass('current');
		$("#"+tab_id).addClass('current');
	})

});


