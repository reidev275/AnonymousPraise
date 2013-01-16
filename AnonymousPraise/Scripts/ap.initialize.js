var ap = (function (ap, $, ko) {
	ap.initialize = function() {

		ap.route('recentPraise');
		
		var viewModel = new ap.ViewModel();
		viewModel.refresh();
		ko.applyBindings(viewModel);
		
		$('.carousel').carousel();
		$('.nav li a').click(function () {
			$('.nav li').removeClass('active');
			$(this).parent().addClass('active');
		});

		$("#pageContainer").on('click', 'a.page', function () {
			$('.nav li').removeClass('active');
			var page = $(this).attr('data-page');
			ap.route(page);
		});

		$('a.page').click(function () {
			var page = $(this).attr('data-page');
			ap.route(page);
		});
	};

	return ap;
})(ap || {}, jQuery, ko);
