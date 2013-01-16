ko.bindingHandlers.slideVisible = {
	update: function (element, valueAccessor, allBindingsAccessor) {
		// First get the latest data that we're bound to
		var value = valueAccessor(), allBindings = allBindingsAccessor();

		// Next, whether or not the supplied model property is observable, get its current value
		var valueUnwrapped = ko.utils.unwrapObservable(value);

		// Grab some more data from another binding property
		var duration = allBindings.slideDuration || 400; // 400ms is default duration unless otherwise specified

		// Now manipulate the DOM element
		if (valueUnwrapped == true)
			$(element).slideDown(duration); // Make the element visible
		else
			$(element).slideUp(duration);   // Make the element invisible
	}
};