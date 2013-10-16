var ap = (function($, ap) {
	
	ap.route = function (pageToShow) {
		$('section.page').hide();
		$('#' + pageToShow).fadeIn('slow');
	};
	
	return ap;
})(jQuery, ap);