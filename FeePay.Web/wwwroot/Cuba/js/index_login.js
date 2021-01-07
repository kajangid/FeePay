(function ($) {
    "use strict";

    jQuery.validator.setDefaults({
        onfocusout: function (e) { this.element(e); },
        onkeyup: function (e) { this.element(e); },

        highlight: function (element) {
            jQuery(element).closest('.form-control').addClass('is-invalid');
        },
        unhighlight: function (element) {
            jQuery(element).closest('.form-control').removeClass('is-invalid');
            jQuery(element).closest('.form-control').addClass('is-valid');
        },

        errorElement: 'div',
        errorClass: 'invalid-feedback',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group-prepend').length) $(element).siblings(".invalid-feedback").append(error); //error.insertAfter(element.parent());
            else error.insertAfter(element);
        },
    });
    $('.show-hide').show();
    $('.show-hide span').addClass('show');
    $('.show-hide span').click(function () {
        if ($(this).hasClass('show')) {
            $('input[name="Password"]').attr('type', 'text');
            $(this).removeClass('show');
        } else {
            $('input[name="Password"]').attr('type', 'password');
            $(this).addClass('show');
        }
    });
    $('form button[type="submit"]').on('click', function () {
        $('.show-hide span').addClass('show');
        $('.show-hide').parent().find('input[name="Password"]').attr('type', 'password');
    });

    $(".bg-center").parent().addClass('b-center');
    $(".bg-img-cover").parent().addClass('bg-size');
    $('.bg-img-cover').each(function () {
        var el = $(this),
            src = el.attr('src'),
            parent = el.parent();
        parent.css({
            'background-image': 'url(' + src + ')',
            'background-size': 'cover',
            'background-position': 'center',
            'display': 'block'
        });
        el.hide();
    });
})(jQuery);