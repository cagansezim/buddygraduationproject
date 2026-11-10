(function ($) {
    var _adoptionService = abp.services.app.adoption,   // Adoption service
        l = abp.localization.getSource('buddy'),        // Localization function
        _$modal = $('#AdoptionEditModal'),              // Edit modal
        _$form = _$modal.find('form');                  // Form inside the modal

    function save() {
        if (!_$form.valid()) {
            return;  // Stop if the form is invalid
        }

        var adoption = _$form.serializeFormToObject();  // Serialize form data to an object
        abp.ui.setBusy(_$modal);                        // Show loading spinner on modal

        _adoptionService.update(adoption)               // Call the update method of the adoption service
            .done(function () {
                _$modal.modal('hide');                  // Close the modal on success
                abp.notify.info(l('SavedSuccessfully')); // Show a success notification
                abp.event.trigger('adoption.edited', adoption); // Trigger 'adoption.edited' event
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);              // Clear the loading spinner
            });
    }

    // Handle save button click
    _$modal.find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    // Handle Enter key press to trigger save
    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    // Auto-focus on the first input field when the modal is shown
    _$modal.on('shown.bs.modal', function () {
        _$form.find('input:not([type=hidden]):first').focus();
    });
})(jQuery);
