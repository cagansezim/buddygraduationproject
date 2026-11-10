(function ($) {
    var _petService = abp.services.app.pet,
        l = abp.localization.getSource('buddy'),
        _$modal = $('#PetEditModal'),
        _$form = _$modal.find('form');

    function save() {
        if (!_$form.valid()) {
            return;
        }
        var pet = _$form.serializeFormToObject();
        abp.ui.setBusy(_$modal);
        _petService.update(pet)
            .done(function () {
                _$modal.modal('hide');
                abp.notify.info(l('SavedSuccessfully'));
                abp.event.trigger('pet.edited', pet);
            })
            .always(function () {
                abp.ui.clearBusy(_$modal);
            });
    }

    _$modal.find(".save-button").click(function (e) {
        e.preventDefault();
        save();
    });

    _$form.find('input').on('keypress', function (e) {
        if (e.which === 13) {
            e.preventDefault();
            save();
        }
    });

    _$modal.on('shown.bs.modal', function () {
        _$form.find('input:not([type=hidden]):first').focus();
    });
})(jQuery);
