(function ($) {
    var _petService = abp.services.app.pet,
        l = abp.localization.getSource('buddy'),
        _$table = $('#PetsTable'),
        _$petsTable = _$table.DataTable({
            paging: true,
            serverSide: true,
            listAction: {
                ajaxFunction: _petService.getAll,
                inputFilter: function () {
                    return $('#PetsSearchForm').serializeFormToObject(true);
                }
            },
            buttons: [
                {
                    name: 'refresh',
                    text: '<i class="fas fa-redo-alt"></i>',
                    action: function () {
                        _$petsTable.draw(false);
                    }
                }
            ],
            responsive: {
                details: {
                    type: 'column'
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    className: 'control',
                    defaultContent: ''
                },
                {
                    targets: 1,
                    data: 'name',
                    sortable: false
                },
                {
                    targets: 2,
                    data: 'species',
                    sortable: false
                },
                {
                    targets: 3,
                    data: 'age',
                    sortable: false
                },
                {
                    targets: 4,
                    data: 'breed',
                    sortable: false
                },
                {
                    targets: 5,
                    data: 'ownerId',
                    sortable: false
                },
                {
                    targets: 6,
                    data: null,
                    sortable: false,
                    autoWidth: false,
                    defaultContent: '',
                    render: function (data, type, row, meta) {
                        return [
                            `<button type="button" class="btn btn-sm bg-secondary edit-pet" data-pet-id="${row.id}" data-toggle="modal" data-target="#PetEditModal">
                                <i class="fas fa-pencil-alt"></i> ${l('Edit')}
                             </button>`,
                            `<button type="button" class="btn btn-sm bg-danger delete-pet" data-pet-id="${row.id}">
                                <i class="fas fa-trash"></i> ${l('Delete')}
                             </button>`
                        ].join('');
                    }
                }
            ]
        });

    // Delete pet
    $(document).on('click', '.delete-pet', function () {
        var petId = $(this).data('pet-id');
        abp.message.confirm(
            abp.utils.formatString(l('AreYouSureWantToDelete'), petId),
            null,
            function (isConfirmed) {
                if (isConfirmed) {
                    _petService.delete(petId).then(function () {
                        abp.notify.success(l('SuccessfullyDeleted'));
                        _$petsTable.ajax.reload();
                    });
                }
            }
        );
    });

    // Load edit modal via AJAX
    $(document).on('click', '.edit-pet', function () {
        var petId = $(this).data('pet-id');
        abp.ajax({
            url: abp.appPath + 'Pets/EditModal?petId=' + petId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#PetEditModal .modal-content').html(content);
                $('#PetEditModal').modal('show');
            }
        });
    });

    // Refresh table on pet edit event
    abp.event.on('pet.edited', function () {
        _$petsTable.ajax.reload();
    });

})(jQuery);
