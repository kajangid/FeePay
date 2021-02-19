'use strict';
// CSRF (XSRF) security
function addAntiForgeryToken(data) {
    //if the object is undefined, create a new one.
    if (!data) {
        data = {};
    }
    //add token
    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }
    return data;
};

const swalWithBootstrapButtons = Swal.mixin({
    customClass: {
        confirmButton: 'btn btn-success',
        cancelButton: 'btn btn-danger mr-2'
    },
    buttonsStyling: false
});
const swalWithOutButtons = Swal.mixin({
    showCancelButton: false,
    showConfirmButton: false,
    buttonsStyling: false
});
function addErrorMsgToValidatonSummery(errorMsg) {
    var validationSummary = $('#form-validation-summary ul');
    if (validationSummary.length == 0) {
        $('#form-validation-summary').after('<ul></ul>');
        validationSummary = $('#form-validation-summary ul');
    }
    if (Array.isArray(errorMsg)) {
        $.each(errorMsg, function (i, v) { validationSummary.append('<li>' + v + '</li>'); });
    } else { validationSummary.append('<li>' + errorMsg + '</li>'); }

}
var modal_animate_custom = {
    init: function (modelId) {
        modelId = modelId || this.myModelId;
        var model = $('#' + modelId);
        model.on('show.bs.modal', function (e) {
            var anim = $(this).attr("data-animodel-in");
            if (typeof (anim) != "undefined" && anim != null && anim != '')
                $('.modal .modal-dialog').attr('class', 'modal-dialog  modal-dialog-centered ' + anim + '  animated');
        })
        model.on('hide.bs.modal', function (e) {
            var anim = $(this).attr('data-animodel-out');
            if (typeof (anim) != "undefined" && anim != null && anim != '')
                $('.modal .modal-dialog').attr('class', 'modal-dialog  modal-dialog-centered ' + anim + '  animated');
        })
    }
};
function openImageModalBox(src) {
    modal_animate_custom.init('image-ModalBox');
    var img = $('#image-ModalBox').find('img.modal-img');
    if (typeof (img) != "undefined" && img != null && img.length > 0) {
        img.attr("src", src);
        $('#image-ModalBox').modal('show');
    }
    $('#image-ModalBox').on('hide.bs.modal', function (e) {
        if (typeof (img) != "undefined" && img != null && img.length > 0)
            setTimeout(function () { img.attr("src", ''); }, 200);            
    });
}
(function ($) {
    'use strict';
    $.fn.reArrangeDatatableSerialNumber = function () {
        if (typeof jQuery().DataTable === 'function') {
            var table = $(this).DataTable();
            table.on('order.dt search.dt', function () {
                table.column(0, {
                    search: 'applied',
                    order: 'applied'
                }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        } else { console.log("DataTable library not loaded...."); }
        return this;
    };
}(jQuery));
(function ($) {
    'use strict';
    // Bootstrap toast notification
    $(".notification").on("click", function () {
        $.notify({
            icon: 'glyphicon glyphicon-star',
            message: "Code Copied to clipboard!  "
        }, {
            type: 'copy',
            newest_on_top: false,
            mouse_over: false,
            showProgressbar: false,
            spacing: 10,
            timer: 1400,
            z_index: 1000,
            allow_dismiss: true,
            delay: 1000,
            placement: {
                from: 'bottom',
                align: 'right'
            },
            animate: {
                enter: 'animated bounce',
                exit: 'animated bounce'
            }
        });
    });
    // Ajax activity indicator bound to ajax start/stop document events
    $(document).ajaxStart(function () { $('#ajaxBusySpin').show(); }).ajaxStop(function () { $('#ajaxBusySpin').hide(); });
    if (typeof (ClipboardJS) === 'function') {
        var clipboard = new ClipboardJS('a[data-clipboard="init"]');
        clipboard.on('success', function (e) { });
        clipboard.on('error', function (e) { });
    } else { console.log("ClipboardJS library not loaded...."); }
    //
})(jQuery);
(function ($) {
    'use strict';
    $('table[data-type="datatable"]').on('click', 'a[data-action="deleterow"]', function (e) {
        e.preventDefault();
        var $this = $(this);
        try {
            swalWithBootstrapButtons.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes, delete it!',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true
            }).then((result) => {
                if (result.value) {
                    dataTableRowDelete($this, swalWithBootstrapButtons);
                } else if (result.dismiss === Swal.DismissReason.cancel) { }
            }).catch(swal.noop);
        } catch (ex) {
            console.log("Ah, Snap!. Catch an error..", ex);
        }
    });

    $('table[data-type="datatable"]').on('click', 'input[data-action="isactiverow"]', function (e) {
        e.preventDefault();
        var $this = $(this);
        try {
            swalWithBootstrapButtons.fire({
                title: 'Are you sure?',
                text: "Do you want to apply this change!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true
            }).then((result) => {
                if (result.value) {
                    dataTableRowActive($this);
                } else if (result.dismiss === Swal.DismissReason.cancel) { }
            }).catch(swal.noop);
        } catch (ex) {
            console.log("Ah, Snap!. Catch an error..", ex);
        }
    });

    $('table[data-type="datatable"]').on('click', 'input[data-action="approverow"]', function (e) {
        e.preventDefault();
        var $this = $(this);
        try {
            swalWithBootstrapButtons.fire({
                title: 'Are you sure?',
                text: "Do you want to apply this change!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No, cancel!',
                reverseButtons: true
            }).then((result) => {
                if (result.value) {
                    dataTableRowApprove($this);
                } else if (result.dismiss === Swal.DismissReason.cancel) { }
            }).catch(swal.noop);
        } catch (ex) {
            console.log("Ah, Snap!. Catch an error..", ex);
        }
    });

    function dataTableRowDelete(buttonClicked) {
        var table = $('table[data-type="datatable"]');
        var url = buttonClicked.attr("href");
        var txtmsg = buttonClicked.attr("data-actionmag");
        var parentRow = buttonClicked.closest('tr');
        $.ajax({
            url: url,
            method: 'delete',
            contentType: 'application/json',
            //data: { name: "John", location: "Boston" },
            //dataType : 'json'
        }).done(function (data) {
            if (data.success) {
                swalWithBootstrapButtons.fire('Deleted!', txtmsg + ' has been deleted.', 'success');
                if (parentRow) {
                    table.DataTable().row(parentRow).remove().draw();
                    table.reArrangeDatatableSerialNumber();
                }
            } else {
                var error = (data != null && data.message != null) ? data.message : 'there is an error deleting ' + txtmsg + '...';
                swalWithBootstrapButtons.fire('Fail!', error, 'warning');
            }
        }).fail(function (jqXHR, textStatus) { console.log("Request failed: " + textStatus); }).always(function () { });
    }
    function dataTableRowActive(buttonClicked) {
        swalWithBootstrapButtons.fire('Not Available', 'This service temporarily not available.', 'info');
    }
    function dataTableRowApprove(buttonClicked) {
        swalWithBootstrapButtons.fire('Not Available', 'This service temporarily not available.', 'info');
    }

    modal_animate_custom.init('showUserNamePass');
    $('[data-onclilck="showPassword"]').on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        $.ajax({
            url: $this.attr('data-ajaxurl'),
            dataType: 'json',
            method: 'get',
        }).done(function (res) {
            if (res && res.success && res.data) {
                $('[data-setjs="UserName"]').html(res.data.userName);
                $('[data-setjs="Password"]').html(res.data.password);
                $('#showUserNamePass').modal({ backdrop: 'static', keyboard: false }).modal('show');
            } else {
                $('[data-setjs="UserName"]').html('');
                $('[data-setjs="Password"]').html('');
            }
        }).fail(function (jrhx, status) { console.log(jrhx, status); });
    });
    $('form[id="resetPassword"]').on('submit', function (e) {
        var $this = $(this);
        if ($this.valid()) {
            e.preventDefault();
            var postData = $this.serialize();
            $.ajax({
                url: $this.attr('action'),
                dataType: 'json',
                method: $this.attr('method'),
                data: postData,
            }).done(function (res) {
                if (res && res.success) {
                    $this.find('input').val('');
                    swalWithBootstrapButtons.fire('Password Change!', 'New Password has been Changed.', 'success');
                } else {
                    addErrorMsgToValidatonSummery(res.message);
                    swalWithBootstrapButtons.fire('Fail!', 'there is an error changing password...', 'warning');
                }
            }).fail(function (jrhx, status) { console.log(jrhx, status); });
        }
    });
    var showHidePass = $('[data-onclick="showhidepass"]');
    showHidePass.show();
    showHidePass.on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        var indicator = $this.find('i');
        var showClass = 'fa fa-eye';
        var hideClass = 'fa fa-eye-slash';
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
    var editReadOnly = $('[data-onclick="editreadonly"]');
    editReadOnly.show();
    editReadOnly.on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        var indicator = $this.find('i');
        var showClass = 'fa fa-edit';
        var hideClass = 'fa fa-shield';
        var input = $this.parent().find('input[data-inputtype="readonly"]');
        var readOnly = input.prop('readOnly');
        if (readOnly) {
            input.prop('readOnly', false);
            indicator.attr('class', showClass);
        } else {
            input.prop('readOnly', true);
            indicator.attr('class', hideClass);
        }
    });
    $('a[data-onclilck="changePasswordAdmin"]').on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        var form = $('form[id="changePass_adminForm"]');
        form.find('input[name="Id"]').val($this.attr('data-ajaxkey'));
        form.find('input[name="CurrentPassword"]').val('admin');
        form.attr("action", $this.attr('data-ajaxurl'));
        $('#changePass_admin').modal({ backdrop: 'static', keyboard: false }).modal('show');
    });
    modal_animate_custom.init('changePass_admin');
    $('form[id="changePass_adminForm"]').on('submit', function (e) {
        var $this = $(this);
        if ($this.valid()) {
            e.preventDefault();
            var postData = $this.serialize();
            $.ajax({
                url: $this.attr('action'),
                dataType: 'json',
                method: $this.attr('method'),
                data: postData,
            }).done(function (res) {
                if (res && res.success) {
                    $this.find('input[type="text"]').val('');
                    $this.attr('action', '');
                    swalWithBootstrapButtons.fire('Password Change!', 'New Password has been set.', 'success');
                } else {
                    addErrorMsgToValidatonSummery(res.message);
                    swalWithBootstrapButtons.fire('Fail!', 'there is an error changing password...', 'warning');
                }
                $('#changePass_admin').modal('hide');
            }).fail(function (jrhx, status) { console.log(jrhx, status); });
        }
    });
})(jQuery);

(function ($) {
    "use strict";
    // search student partial
    var selectClass = $('select[name="ClassId"]');
    if (selectClass != null && selectClass.length > 0) {
        var selectedSection = $('select[name="SectionId"]').attr("data-key-selected");
        bindSectionDropDown(selectClass, selectedSection);
        selectClass.on('change', function (e) {
            e.preventDefault();
            var $this = $(this);
            bindSectionDropDown($this);
        });
    }
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
})(jQuery);

(function ($) {
    "use strict";
    var modal = $('#changeSession');
    modal_animate_custom.init('changeSession');
    $('#changeSessionModal').on('click', function (e) {
        e.preventDefault();
        var $this = $(this);
        $.ajax({
            url: $this.attr('data-ajaxurl'),
            dataType: 'json',
            method: 'get',
        }).done(function (res) {
            if (res && res.success) {
                console.log(res.data);
                var select = modal.find('form select[id="Id"]');
                var html = '';
                if (select != null && select.length > 0) {
                    $.each(res.data, function (index, value) {
                        html += `<option value="${value.id}" ${(value.selected ? 'selected' : '')}>${value.name}</option>`;
                    });
                    select.html(html);
                }
                modal.modal('show');
            } else {
            }
        }).fail(function (jrhx, status) { console.log(jrhx, status); });
    });

    modal.find('form[id="changeSessionForm"]').on('submit', function (e) {
        var $this = $(this);
        event.preventDefault();
        var postData = $this.serialize();
        $.ajax({
            url: $this.attr('action'),
            dataType: 'json',
            method: $this.attr('method'),
            data: postData,
        }).done(function (res) {
            if (res && res.success) {
                swalWithBootstrapButtons.fire('Session Change!', 'New Session has been set.', 'success');
                window.setTimeout(function () { window.location.href = global_base_url + "/School/Dashboard"; }, 200);
            } else {
                addErrorMsgToValidatonSummery(res.message);
                swalWithBootstrapButtons.fire('Fail!', 'there is an error changing session...', 'warning');
            }
            modal.modal('hide');
        }).fail(function (jrhx, status) { console.log(jrhx, status); });
    });
})(jQuery);