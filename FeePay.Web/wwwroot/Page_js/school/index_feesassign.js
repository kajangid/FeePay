(function ($) {
    "use strict";
    var tbl = $('table[id="assignfees_table"]');
    var allCb = $('input[name="assignCkAll"]');
    var assignCb = $('.assign-checkbox');
    allCb.on('click', function (e) {
        var $this = $(this);
        var cboxlist = tbl.find('tbody tr th:first-child').find('input[type="checkbox"]');
        if (typeof (cboxlist) != "undefined" && cboxlist != null && cboxlist.length > 0) {
            if ($this.prop('checked')) { $.each(cboxlist, function (i, v) { $(v).prop('checked', true); }); }
            else { $.each(cboxlist, function (i, v) { $(v).prop('checked', false); }); }
        }
    });
    assignCb.on('click', function (e) {
        var cboxlist = tbl.find('tbody tr th:first-child').find('input[type="checkbox"]');
        if (typeof (cboxlist) != "undefined" && cboxlist != null && cboxlist.length > 0) {
            if ($('.assign-checkbox:checked').length == assignCb.length) {
                allCb.prop('checked', true);
            } else {
                allCb.prop('checked', false);
            }
        }
    });
    if ($('.assign-checkbox:checked').length === assignCb.length) { allCb.prop('checked', true); }
    $('#assignform').on('submit', function (e) {
        e.preventDefault();
        var url = $(this).attr('action');
        var swalWithBootstrapButtons = Swal.mixin(global_swanMixin);
        if ($('.assign-checkbox:checked').length <= 0) {
            swalWithBootstrapButtons.fire('Please select a student first.')
            return false;
        } else {
            $.post(url, $(this).serialize(), function (data) {
                if (data != null && data.success) {
                    swalWithBootstrapButtons.fire({
                        title: 'Assign Successfully',
                        text: "Fees are assign to all selected students.",
                        icon: 'success',
                    });
                }
            }).fail(function (jqXHR, textStatus) { console.log("Request failed: " + textStatus); });
        }
    });
})(jQuery)