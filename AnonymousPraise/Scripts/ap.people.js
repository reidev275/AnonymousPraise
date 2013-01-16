var ap = (function (ap, $) {
	var cache = localStorage != null,
	    storageLocation = 'ap.People';
	
	ap.people = ap.people || {};
	ap.people.get = function (callback) {
		
		if (cache && localStorage[storageLocation]) {
			var obj = JSON.parse(localStorage[storageLocation]);
			var ts = new Date().getTime();
			if (obj.expdate && obj.people) {
				if (obj.expdate > ts) {
					callback(obj.people);
					return;
				}
			}
		}

		$.ajax({
			url: "../api/People",
			type: "GET",
			cache: false,
			contentType: "application/json; charset=utf-8"
		}).done(function(data) {
			if (cache) {
				var date = new Date();
				localStorage[storageLocation] = JSON.stringify({
					expdate: date.setDate(date.getDate() + 1),
					people: data
				});
			}
			callback(data);
		});
		
	};

	return ap;
})(ap || {}, jQuery);