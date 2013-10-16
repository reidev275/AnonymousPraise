var ap = (function(ap, ko) {
	ap.ViewModel = function() {
		var self = this;

		self.people = ko.observableArray([]);
		
		self.moderatorKey = ko.observable(ap.getModerator());
		self.saveModeratorKey = function () {
			ap.setModerator(self.moderatorKey());
		};

		self.alertMessage = ko.observable("");
		self.alertClass = ko.observable("alert alert-success");
		self.clearMessage = function () {
			self.alertMessage("");
		};

		self.recentPraise = ko.observableArray();
		self.unModeratedPraise = ko.observableArray([]);
		self.currentPraise = ko.observable("");
		self.remainingCharacters = ko.computed(function () {
			var result = 140 - self.currentPraise().length;
			if (result < 0) {
				self.alertClass("alert alert-error");
				self.alertMessage("Your praise must fit in 140 characters");
			} else {
				self.alertMessage("");
			}
			return result;
		});

		self.topPraise = ko.computed(function() {
			var result = [];
			for (var i = 0; (i < 10) && (i < self.recentPraise().length); i++) {
				result.push(self.recentPraise()[i]);
			}
			return result;
		});

		self.selectedPerson = ko.observable("");
		self.viewPerson = ko.observable("");
		self.currentEmployeePraise = ko.observableArray();
		self.currentEmployeeLikes = ko.computed(function () {
			var result = 0;
			for (var i = 0; i < self.currentEmployeePraise().length; i++) {
				result = result + self.currentEmployeePraise()[i].Likes();
			}
			return result;
		});

		self.select = function (person) {
			self.viewPerson(person);
			self.selectedPerson(person);
			ap.praise.get(person, function (data) {
				self.currentEmployeePraise([]);
				for (var i = 0; i < data.length; i++) {
					self.currentEmployeePraise.push(new ap.Praise(data[i]));
				}
			});
		};

		self.submitPraise = function () {
			if (self.currentPraise() == "") {
				self.alertClass("alert alert-error");
				self.alertMessage("No praise entered to submit");
				return;
			}
			
			self.alertClass("alert alert-success");
			self.alertMessage("Praise submitted.  Thank you!");
			var praise = { CreatedDate: Date(), Person: self.selectedPerson(), Comment: self.currentPraise() };
			ap.praise.submit(praise, function (data) {
				if (data) self.unModeratedPraise.push(data);
			});
			self.clearPraise();
		};



		self.like = function(praise) {
			if (praise) {
				ap.praise.like(praise.Id);
				praise.Likes(praise.Likes() + 1);
				praise.CanLike(false);
			}
		};

		self.clearPraise = function() {
			self.currentPraise("");
			self.selectedPerson("");
		};

		var removeUnmoderatedPraise = function (praise) {
			for (var i = 0; i < self.unModeratedPraise().length; i++) {
				if (self.unModeratedPraise()[i].Id == praise.Id) {
					self.unModeratedPraise.splice(i, 1);
				}
			}
		};

		self.refresh = function() {
			ap.praise.getRecent(function (data) {
				self.recentPraise([]);
				for (var i = 0; i < data.length; i++) {
					self.recentPraise.push(new ap.Praise(data[i]));
				}
			});

			ap.people.get(function(data) {
				self.people(data);
			});
			
			ap.praise.unModerated(function (data) {
				self.unModeratedPraise(data);
			});
		};

		self.approve = function(praise) {
			removeUnmoderatedPraise(praise);
			self.recentPraise.splice(0, 0, new ap.Praise(praise));
			ap.praise.approve(praise.Id);
		};
		self.deny = function (praise) {
			removeUnmoderatedPraise(praise);
			ap.praise.deny(praise.Id);
		};
	};
	return ap;
})(ap || {}, ko);