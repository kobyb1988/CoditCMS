$(document).ready(function () {

/*****************************************************************************
	CONTACT FORM
*****************************************************************************/
    var bindForm = function(){
        $("#ajax-contact-form").submit(function() {
            var str = $(this).serialize();
            var url;
            if($.inArray("en", window.location.pathname.split('/'))) {
                url = "/en/home/contact/";
            }
            else
                url = "/home/contact/";
            $.ajax({
                type: "POST",
                url: url,
                data: str,
                success: function(msg, some, response) {
                    var fields = $('#fields').html(msg);
                    if(response.status == 201){
                        fields.find(':input').each(function(i, el){                            
                            if(el.type.toLowerCase()!='submit'){
                                $(el).val('');
                            }                            
                        })
                        toastr.info("Спасибо за ваше сообщение, мы обязательно свяжемся с вами!")
                    }
                    bindForm()
                }
            });
            return false;
        });
    }
    bindForm();

//tooltip 
$("[rel=tooltip]").tooltip();
$("[data-rel=tooltip]").tooltip();
$('#bottom').tooltip();

/***************************************************
	CENTER CONTENT
***************************************************/
$(function() {
    $(window).on('resize', function resize()  {
        $(window).off('resize', resize);
        setTimeout(function () {
            var content = $('#content');
            var top = (window.innerHeight - content.height()) / 2;
            content.css('top', Math.max(0, top) + 'px');
            $(window).on('resize', resize);
        }, 50);
    }).resize();
});
	
/*****************************************************************************
	HEADER & SCROLL
******************************************************************************/	

   $(window).scroll(function () {
       var scaleBg = -$(window).scrollTop() / 4;
       if(window.location.href.indexOf("Blog") <= -1)
       {
           if ($(window).scrollTop() > 1) {
               $('#header').addClass('show-header');
           } else {
               $('#header').removeClass('show-header');
           }
       }
	});

    $('.scroll').smoothScroll({
        offset:-78,
        speed: 800
    });
 
// Mobile Menu Toggle
	$('#nav-toggle').click(function () {
		 if ($('#header').hasClass('responsive-menu')) {
            $('#header').removeClass('responsive-menu');
        } else {
            $('#header').addClass('responsive-menu');
        }
	 });
	$('#menu li a').click(function () {
        if ($('#header').hasClass('responsive-menu')) {
            $('#header').removeClass('responsive-menu');
        }
    });

/*****************************************************************************
PARALLAX
*****************************************************************************/	

( function ( $ ) {
'use strict';
$(document).ready(function(){
$(window).bind('load', function () {
		parallaxInit();						  
	});
	function parallaxInit() {
		testMobile = isMobile.any();
		if (testMobile == null)
		{
			$('#charts .well').parallax("50%", 0.3);
			$('#milestones .well').parallax("50%", 0.3);
			$('#work .well').parallax("50%", 0.3);
			$('#clients_parallax .well').parallax("50%", 0.3);
		}
	}	
	parallaxInit();	 
});	
//Mobile Detect
var testMobile;
var isMobile = {
    Android: function() {
        return navigator.userAgent.match(/Android/i);
    },
    BlackBerry: function() {
        return navigator.userAgent.match(/BlackBerry/i);
    },
    iOS: function() {
        return navigator.userAgent.match(/iPhone|iPad|iPod/i);
    },
    Opera: function() {
        return navigator.userAgent.match(/Opera Mini/i);
    },
    Windows: function() {
        return navigator.userAgent.match(/IEMobile/i);
    },
    any: function() {
        return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
    }
};
}( jQuery ));

/*****************************************************************************
COUNTERS
******************************************************************************/

	jQuery('#counter-1').appear(function() {
		$('#counter-1').countTo({
			from: 0,
			to: 74,
			speed: 4000,
			refreshInterval: 50,
			onComplete: function(value) { 
			//console.debug(this); 
			}
			});
		});
	jQuery('#counter-2').appear(function() {
		$('#counter-2').countTo({
			from: 0,
			to: 232,
			speed: 4000,
			refreshInterval: 50,
			onComplete: function(value) { 
			//console.debug(this); 
			}
			});
		});
	jQuery('#counter-3').appear(function() {
		 $('#counter-3').countTo({
			from: 0,
			to: 22,
			speed: 4000,
			refreshInterval: 50,
			onComplete: function(value) { 
			//console.debug(this); 
			}
			});
		});	
	});