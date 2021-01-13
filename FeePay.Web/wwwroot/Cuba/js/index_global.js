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
    if (typeof Swal === 'function') {
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-success',
                cancelButton: 'btn btn-danger mr-2'
            },
            buttonsStyling: false
        })
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
                }).then(
                    value => {
                        console.log("Confirmed....");
                        dataTableRowDelete($this, swalWithBootstrapButtons);
                    },
                    dismiss => {
                        console.log("Dismiss....");
                    }
                ).catch(swal.noop);
            } catch (ex) {

                console.log("catch an error..", ex);
            }
        });
        function dataTableRowDelete(buttonClicked) {
            var table = $('table[data-type="datatable"]');
            var url = buttonClicked.attr("href");
            var txtmsg = buttonClicked.attr("data-actionmag");
            var parentRow = buttonClicked.closest('tr');
            console.log("Row Delete ajax....");
            $.ajax({
                url: url,
                method: 'delete',
                contentType: 'application/json',
                //data: { name: "John", location: "Boston" },
                //dataType : 'json'
            })
                .done(function (data) {
                    console.log(data);
                    if (data.success) {
                        swalWithBootstrapButtons.fire('Deleted!', txtmsg + ' has been deleted.', 'success');
                        if (parentRow) {
                            table.DataTable().row(parentRow).remove().draw();
                            table.reArrangeDatatableSerialNumber();                            
                        }
                    } else {
                        swalWithBootstrapButtons.fire('Fail!', 'there is an error deleting role...', 'warning');
                    }
                })
                .fail(function (jqXHR, textStatus) {
                    console.log("Request failed: " + textStatus);
                })
                .always(function () {
                    console.log("complete");
                });
        }
    } else {
        console.log("sweet alert library not loaded....");
    }
})(jQuery);