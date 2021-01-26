(function ($) {
    "use strict";
    var selectClass = $('select[name="ClassId"]');
    var selectedSection = $('select[name="SectionId"]').attr("data-key-selected");
    bindSectionDropDown(selectClass, selectedSection);
    selectClass.on('change', function (e) {
        e.preventDefault();
        var $this = $(this);
        bindSectionDropDown($this);
    });
    function bindSectionDropDown(select, selected) {
        selected = selected || '0';
        var selectSectionEle = $('select[name="SectionId"]');
        var html = '<option value="">Select</option>';
        var classId = select.val();
        var url = selectSectionEle.attr("data-ajaxurl");
        if (classId === "") { selectSectionEle.html(html); return true; }
        $.ajax({
            url: `${url}?id=${classId}`,
            method: 'get',
            dataType: 'json'
        }).done(function (res) {
            if (res.success) {
                if (res.data != null && res.data.length > 0) {
                    html += res.data.map(function (i) {
                        return `<option value="${i.value}" ${(i.value === selected
                            && typeof (selected) != "undefined"
                            && selected != null && selected != '')
                            ? 'Selected' : ''} >${i.text}</option>`;
                    }).join('');
                    selectSectionEle.html(html);
                }
            } else { }
        }).fail(function (jqXHR, textStatus) {
            console.log("Request failed: " + textStatus);
        }).always(function () {
        });
    }
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