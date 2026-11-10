(function ($) {
    var _adoptionService = abp.services.app.adoption,   // Service for Adoption
        l = abp.localization.getSource('buddy'),        // Localization function
        _$modal = $('#AdoptionEditModal'),              // Modal ID
        _$form = _$modal.find('form');                  // Form inside the modal

    function save() {
        if (!_$form.valid()) {
            return;  // Stop if form is invalid
        }

        var adoption = _$form.serializeFormToObject();  // Serialize form data to object
        abp.ui.setBusy(_$modal);                        // Show loading spinner on modal

        _adoptionService.update(adoption)               // Call adoption update service
            .done(function () {
                _$modal.modal('hide');                  // Close the modal on success
                abp.notify.info(l('SavedSuccessfully')); // Show success notification
                abp.event.trigger('adoption.edited', adoption); // Trigger custom event
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);              // Clear loading spinner
            });
    }

    // Handle save button click
    _$modal.find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    // Handle Enter key submission
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    // Autofocus on the first input when the modal is shown
    _$modal.on('shown.bs.modal', function () {
        _$form.find('input:not([type=hidden]):first').focus();
    });
})(jQuery);
