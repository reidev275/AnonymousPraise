var ap = (function(ap, $, ko) {

	ap.praise = ap.praise || {};

	var canCache = localStorage != null,
		storageLocation = 'ap.Likes',
		moderator = $.cookie('praisemoderator'),
		aplikes = localStorage[storageLocation],
		likes = [];

	if (aplikes) {
		var parsed = JSON.parse(aplikes);
		if (typeof parsed === 'number') {
			likes.push(parsed);
		} else {
			likes = JSON.parse(aplikes);
		}
	} 

	var addHeader = function(xhr) {
		xhr.setRequestHeader('PraiseModerator', moderator);
	};
	
	function canLike(id) {
		if (canCache) {
			if (likes) {
				return $.inArray(id, likes) < 0;
			} else {
				return true;
			}
		} else {
			return false;
		}
	}

	ap.Praise = function(obj) {
		var self = this;
		self.Id = obj.Id;
		self.Person = obj.Person;
		self.Comment = obj.Comment;
		self.Moderated = obj.Moderated;
		self.Likes = ko.observable(obj.Likes);
		self.Date = obj.Date;
		self.CanLike = ko.observable(canLike(obj.Id));
	};

	ap.praise.get = function (person, callback) {
		$.ajax({
			url: "../api/Praise?name=" + person,
			type: "GET",
			cache: false,
			contentType: "application/json; charset=utf-8"
		}).done(function(data) {
			if (callback) callback(data);
		});
	};

	ap.praise.getRecent = function(callback) {
		$.ajax({
			url: "../api/Praise",
			type: "GET",
			cache: false,
			contentType: "application/json; charset=utf-8"
		}).done(function (data) {
			if (callback) callback(data);
		});
	};

	ap.praise.like = function (id) {
		if (canCache) {
			if (canLike(id)) {
				$.ajax({
					url: "../api/Praise/" + id + "/likes",
					type: "POST",
					cache: false,
					contentType: "application/json; charset=utf-8"
				});

				likes.push(id);
				localStorage[storageLocation] = JSON.stringify(likes);
			}
		}
	};

	ap.praise.approve = function(id) {
		$.ajax({
			url: "../api/Praise/" + id + "?Moderated=true",
			type: "Put",
			cache: false,
			contentType: "application/json; charset=utf-8",
			beforeSend: addHeader
		});
	};

	ap.praise.deny = function(id) {
		$.ajax({
			url: "../api/Praise/" + id,
			type: "DELETE",
			cache: false,
			contentType: "application/json; charset=utf-8",
			beforeSend: addHeader
		});
	};

	ap.praise.unModerated = function (callback) {
		$.ajax({
			url: "../api/Praise?moderated=false",
			type: "GET",
			cache: false,
			contentType: "application/json; charset=utf-8",
			beforeSend: addHeader
		}).done(function (data) {
			if (callback) callback(data);
		});
	};

	ap.praise.submit = function (praise, callback) {
		$.ajax({
			url: "../api/Praise",
			type: "POST",
			data: JSON.stringify(praise),
			cache: false,
			contentType: "application/json; charset=utf-8",
			beforeSend: addHeader
		}).done(function (data) {
			if (callback) callback(data);
		});
	};

	return ap;
})(ap || {}, jQuery, ko)