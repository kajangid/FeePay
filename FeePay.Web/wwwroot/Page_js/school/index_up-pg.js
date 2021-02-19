(function () {
    'use strict';
    var currentTab = 0;
    showTab(currentTab);
    function showTab(n) {
        var x = $(".tab");
        x.eq(n).css({ "display": "block" });
        if (n == 0) {
            $("#prevBtn").css({ "display": "none" });
        } else {
            $("#prevBtn").css({ "display": "inline" });
        }
        if (n == (x.length - 1)) {
            $("#nextBtn").html('Submit');
        } else {
            $("#nextBtn").html('Next');
        }
        fixStepIndicator(n)
    }
    function nextPrev(n) {
        var x = $(".tab");
        var form = $("#regForm");
        if (n == 1) form.validate().settings.ignore = ":disabled,:hidden";
        else if (currentTab >= x.length) form.validate().settings.ignore = ":disabled";
        if (n == 1 && !validateForm()) return false;
        x.eq(currentTab).css({ "display": "none" });
        currentTab = currentTab + n;
        if (currentTab >= x.length) {
            form.submit();
            return false;
        }
        showTab(currentTab);
    }
    function validateForm() {
        if ($("#regForm").valid())
            return true;
        return false;
    }
    function fixStepIndicator(n) {
        $(".step").each(function (index, value) {
            $(value).removeClass('active');
        });
        $(".step").eq(n).addClass('active');
    }
    $('button[data-onclick="nextPrev"]').on('click', function (e) {
        e.preventDefault();
        var n = $(this).attr('data-step');
        nextPrev(parseInt(n));
    });


    var select_state = $('select[name="AddressStateId"]');
    var select_city = $('select[name="AddressCityId"]');

    bindCityDropDown(select_state, select_city);

    select_state.on('change', function (e) {
        e.preventDefault();
        var $this = $(this);
        bindCityDropDown($this, select_city);
    });
    function bindCityDropDown(select_State, select_City) {
        var html = '<option value="">Select</option>';
        var stateId = select_State.val();

        if (stateId === "") { select_City.html(html); return true; }

        var url = `${select_City.attr("data-ajaxurl")}?id=${stateId}`;

        var selected = select_city.attr("data-ajaxkey");

        $.ajax({
            url: url,
            method: 'get',
            dataType: 'json'
        }).done(function (res) {
            if (res.success) {
                if (res.data != null && res.data.length > 0) {
                    html += res.data.map(function (i) {
                        return `<option value="${i.value}" ${checkSelectedCity(i.value)} >${i.text}</option>`;
                    }).join('');
                    select_City.html(html);
                }
            } else { }
        }).fail(function (jqXHR, textStatus) {
            console.log("Request failed: " + textStatus);
        }).always(function () { });
    }
    function checkSelectedCity(v) {
        var selected = select_city.attr("data-ajaxkey");
        if (typeof (selected) != "undefined" && selected != null && selected != '')
            if (v === selected)
                return 'selected';
        return '';
    }
    var docHolder = $('div[class="docs-preview-placeholder"]');
    docHolder.each(function (index, value) {
        var savedFile = $(value).attr('data-original-doc');
        if (typeof (savedFile) != "undefined" && savedFile != null && savedFile != '') {
            var anchor = $('<a/>', { href: 'javascript:void(0)', onclick: `openImageModalBox('${savedFile}')` });
            var img = $('<img />', {
                src: savedFile,
                class: "img-thumbnail rounded",
                alt: savedFile
            });
            anchor.append(img);
            $(value).append(anchor);
        }
    });
    $('input[type="file"]').each(function (ietm, value) {
        var selected_file = $(value).attr('value')
        if (typeof (selected_file) != "undefined" && selected_file != null && selected_file != '')
            console.log('removeAttr = required');
        else $(value).attr("required", '');
    });
    $('input[type="file"]').on('change', function () {
        var $this = $(this);
        var image_wrp = $this.closest('.tab').find('div[class="docs-preview-placeholder"]');
        var fileExtension = ['jpeg', 'jpg', 'png'];//, 'gif', 'bmp'
        if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1)
            alert("Only formats are allowed : " + fileExtension.join(', '));
        else {
            if (typeof (FileReader) != "undefined") {
                image_wrp.empty();

                var reader = new FileReader();
                reader.onload = function (e) {
                    $("<img />", {
                        "src": e.target.result,
                        "class": "img-thumbnail rounded"
                    }).appendTo(image_wrp);

                }
                image_wrp.show();
                reader.readAsDataURL($(this)[0].files[0]);
            } else {
                alert("This browser does not support FileReader.");
            }
        }
    });
})(jQuery);