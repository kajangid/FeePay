(function () {
    "use strict";
    //Old
    if (typeof Swal === 'function') {
        const sweetToast = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            //timer: 3000
        });
        let timerInterval
        const sweetModelAutoClose = Swal.mixin({
            //timer: 3000,
            timerProgressBar: false,
            onBeforeOpen: () => {
                Swal.showLoading()
                timerInterval = setInterval(() => {
                    const content = Swal.getContent()
                    if (content) {
                        const b = content.querySelector('b')
                        if (b) b.textContent = Swal.getTimerLeft()
                    }
                }, 100)
            },
            onClose: () => { clearInterval(timerInterval) }
        });
    }
})(jQuery);