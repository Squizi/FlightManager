// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#ResultsOnPage').on('change', function () {
        this.form.submit();
    });

    $('#AddPerson').on('click', function () {
        $.ajax({
            async: true,
            data: $(this.form).serialize(),
            type: 'POST',
            url: '/Reservations/AddReservationPersonItem',
            success: function (partialView) {
                $('form').removeData("validator")    // Added by jQuery Validate
                    .removeData("unobtrusiveValidation");   // Added by jQuery Unobtrusive Validation
                $('#ReservationPersons').html(partialView);
                $.validator.unobtrusive.parse('form');
            }
        });
    });
    $('#RemoveLastPerson').on('click', function () {
        $.ajax({
            async: true,
            data: $(this.form).serialize(),
            type: 'POST',
            url: '/Reservations/RemoveReservationPersonItem/',
            success: function (partialView) {
                $('form').removeData("validator")    // Added by jQuery Validate
                    .removeData("unobtrusiveValidation");   // Added by jQuery Unobtrusive Validation
                $('#ReservationPersons').html(partialView);
                $("input[type='hidden'][name^='ReservationPersons[']").remove();
                $.validator.unobtrusive.parse('form');
            }
        });
    });
});
