var fixed_menu = true;
window.jQuery = window.$ = jQuery;


		jQuery(document).ready(function() {
				$('#countdown_dashboard').countDown({
					targetDate: {
						'day': 		10,
						'month': 	11,
						'year': 	2020,
						'hour': 	11,
						'min': 		0,
						'sec': 		0
					}
				});
			});


/* *** script.js *** */
var demo = true;

jQuery(document).ready(function() {

	if (fixed_menu) {
		jQuery('html').addClass('fixed_menu');
		jQuery('.main_wrapper').css('margin-top', jQuery('header').height());
	}
	
	jQuery('header').find('.sub-menu').find('a').each(function() {
		if (jQuery(this).height() < 30) {
			jQuery(this).css({'padding-top' : '16px', 'padding-bottom' : '17px'});
		}
	});
	

	


	if (jQuery('.content_block').hasClass('no-sidebar')) {
		if (jQuery('html').hasClass('user_bg_layout')) {
			jQuery('.module_line_trigger').each(function(){
				jQuery(this).css('margin-left' , -1*(jQuery('.main_wrapper').width()-jQuery('.container').width())/2+'px').width(jQuery('.main_wrapper').width());
				jQuery(this).wrapInner('<div class="module_line '+jQuery(this).attr('data-option')+' '+jQuery(this).attr('data-top-padding')+' '+jQuery(this).attr('data-bottom-padding')+'" style="background:'+jQuery(this).attr('data-background')+'"><div class="module_line_wrapper container"></div></div>');
			});
			jQuery('.module_gallery_wall').each(function(){
				jQuery(this).css('margin-left' , -1*(jQuery('.main_wrapper').width()-jQuery('.container').width())/2+'px').width(jQuery('.main_wrapper').width());
			});			
		} else {
			jQuery('.module_line_trigger').each(function(){
				jQuery(this).css('margin-left' , -1*(jQuery(window).width()-jQuery('.container').width())/2+'px').width(jQuery(window).width());
				jQuery(this).wrapInner('<div class="module_line '+jQuery(this).attr('data-option')+' '+jQuery(this).attr('data-top-padding')+' '+jQuery(this).attr('data-bottom-padding')+'" style="background:'+jQuery(this).attr('data-background')+'"><div class="module_line_wrapper container"></div></div>');
			});
			jQuery('.module_gallery_wall').each(function(){
				jQuery(this).css('margin-left' , -1*(jQuery(window).width()-jQuery('.container').width())/2+'px').width(jQuery(window).width());
			});			
		}
	}
	
	if ($('.header2top').size() > 0) {
		if ($(window).width() > 760) {
			$('.fullscreen_block').css({'padding-top' : $('header').height()+$('.fullscreen_title').height()+'px', 'min-height' : ($(window).height() - $('header').height()- $('.fullscreen_title').height())+'px'});
			$('.fullscreen_title').css('top',$('header').height()+'px');
		}
		$('html').addClass('header2top');
		$('body').removeClass('fullscreen_layout');
	}

	jQuery('.video_frame').each(function(){
		jQuery(this).height((jQuery(this).width()/16)*9);
	});	
	
	if (jQuery(window).width() > 760) {
		jQuery('.fullscreen_content_wrapper').height(jQuery(window).height()-jQuery('.fullscreen_title').height()).css('top', jQuery('.fullscreen_title').height()+'px');
		if ($('.header2top').size() > 0) {
			jQuery('.fullscreen_content_wrapper').height(jQuery(window).height()-jQuery('.fullscreen_title').height()).css('top', jQuery('.fullscreen_title').height()+$('header').height()+'px');
		}
		jQuery('.fullscreen_content').css({'min-height' : (jQuery(window).height()-jQuery('header').height() - jQuery('.fullscreen_title').height()-100)+'px', 'padding-top' : jQuery('.fullscreen_title').height()+25+'px'});
	} else {
		jQuery('.fs_map').parent('.fullscreen_content_wrapper').addClass('iphone_map');
	}
	
	
});	


jQuery(window).resize(function(){
	if (jQuery(window).width() > 760) {
		jQuery('.fullscreen_content_wrapper').height(jQuery(window).height()-jQuery('.fullscreen_title').height()).css('top', jQuery('.fullscreen_title').height()+'px');
		if ($('.header2top').size() > 0) {
			jQuery('.fullscreen_content_wrapper').height(jQuery(window).height()-jQuery('.fullscreen_title').height()).css('top', jQuery('.fullscreen_title').height()+$('header').height()+'px');
		}
		jQuery('.fullscreen_content').css({'min-height' : (jQuery(window).height()-jQuery('header').height() - jQuery('.fullscreen_title').height()-100)+'px', 'padding-top' : jQuery('.fullscreen_title').height()+25+'px'});
	}
	
	
});