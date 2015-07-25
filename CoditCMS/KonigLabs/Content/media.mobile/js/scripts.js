$(document).ready(function () {

/*****************************************************************************
	CONTACT FORM
*****************************************************************************/
   	$("#ajax-contact-form").submit(function() {
				var str = $(this).serialize();		
				$.ajax({
					type: "POST",
					url: "contact_form/contact_process.php",
					data: str,
					success: function(msg) {

						if(msg == 'OK') {
							result = '<div class="notification_ok">Ваше сообщение отправлено. Спасибо!</div>';
							$("#fields").hide();
						} else {
							result = msg;
						}
						$('#note').html(result);
					}
				});
				return false;
			});

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
	HEADER
******************************************************************************/	

   $(window).scroll(function () {
	var scaleBg = -$(window).scrollTop() / 4;
		if ($(window).scrollTop() > 1) {
            $('#header').addClass('show-header');
        } else {
            $('#header').removeClass('show-header');
        }
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
	MILESTONE COUNTER
******************************************************************************/

	jQuery('#counter-1').appear(function() {
		$('#counter-1').countTo({
			from: 0,
			to: 120,
			speed: 4000,
			refreshInterval: 50,
			onComplete: function(value) { 
			//console.debug(this); 
			}
			});
		});
	});