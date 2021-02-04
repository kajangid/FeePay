(function ($) {
    "use strict";
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


    // New Hide And Show
    var showHidePass = $('[data-onclick="showhidepass"]');
    showHidePass.show();
    showHidePass.on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        var indicator = $this.find('i');
        var hideClass = 'fa fa-eye';
        var showClass = 'fa fa-eye-slash';
        var passInput = $this.parent().find('input[data-inputtype="password"]');
        var inputType = passInput.attr('type');
        if (inputType === 'text') {
            passInput.attr('type', 'password');
            indicator.attr('class', showClass);
        } else if (inputType === 'password') {
            passInput.attr('type', 'text');
            indicator.attr('class', hideClass);
        } else {
            passInput.attr('type', 'password');
            indicator.attr('class', hideClass);
        }
    });
    $('form').on('submit', function () {
        var $this = $(this);
        var showhides = $this.find('[data-onclick="showhidepass"]');
        if (showhides && showhides.length > 0) {
            $.each(showhides, function (i, v) {
                var $ele = $(v);
                var indicator = $ele.find('i');
                var passInput = $ele.parent().find('input[data-inputtype="password"]');
                passInput.attr('type', 'password');
                indicator.attr('class', 'fa fa-eye-slash');
            });
        }
    });
    // PalceHolder Animation
    $('input[data-palceholder="label"]').on('focusin', function (e) {
        var label = $(this).closest('.form-group').find('label');
        label.css({
            "-webkit-transform": "translateY(-20px)",
            "transform": "translateY(-20px)",
            "font-size": "12px",
            "color": "#000",
            "z-index": "2"
        });
    });
    $('input[data-palceholder="label"]').on('focusout', function (e) {
        if (!$(this).val()) {
            $(this).closest('.form-group').find('label').attr("style", "");
        }
    });
})(jQuery);