(function ($) {
    "use strict";
    var tbl = $('table[id="feestable"]');
    var allCb = $('input[name="assignCkAll"]');
    var assignCb = $('.select-checkbox');
    if ($('.select-checkbox:checked').length === assignCb.length) { allCb.prop('checked', true); }
    if ($('.select-checkbox:checked').length > 0) calculateSelectedAmount();
    allCb.on('click', function (e) {
        var $this = $(this);
        var cboxlist = tbl.find('tbody tr th:last-child').find('input[type="checkbox"]');
        if (typeof (cboxlist) != "undefined" && cboxlist != null && cboxlist.length > 0) {
            if ($this.prop('checked')) { $.each(cboxlist, function (i, v) { $(v).prop('checked', true); }); }
            else { $.each(cboxlist, function (i, v) { $(v).prop('checked', false); }); }
        }
        calculateSelectedAmount();
    });
    assignCb.on('click', function (e) {
        var cboxlist = tbl.find('tbody tr th:last-child').find('input[type="checkbox"]');
        if (typeof (cboxlist) != "undefined" && cboxlist != null && cboxlist.length > 0) {
            if ($('.select-checkbox:checked').length == assignCb.length) {
                allCb.prop('checked', true);
            } else {
                allCb.prop('checked', false);
            }
        }
        calculateSelectedAmount();
    });
    function calculateSelectedAmount() {
        var tr = $('.select-checkbox:checked').closest('tr');
        var amountPay = 0;
        $.each(tr, function (i, v) {
            var amountTd = $(v).find('td[data-amount]');
            if (amountTd != null && amountTd != '')
                amountPay += parseFloat(amountTd.attr('data-amount'));
        });
        var formatedAmount = new Intl.NumberFormat('en-IN', {
            maximumFractionDigits: 2,
            minimumFractionDigits: 2,
        }).format(amountPay);
        $('[data-paying="amount"]').html(formatedAmount);
    }
    $('#feepay').on('submit', function (e) {
        if ($('.select-checkbox:checked').length <= 0) {
            e.preventDefault();
            swalWithOutButtons.fire('', 'Please select a fee to proceed.')
            return false;
        } else { }
    });
})(jQuery)