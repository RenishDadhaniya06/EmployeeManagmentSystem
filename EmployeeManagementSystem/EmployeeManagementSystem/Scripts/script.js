

function toastify(type, msg) {

    //debugger
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
        switch (type) {
        case 'success': toastr.success(msg);
        break;
    case 'info': toastr.info(msg);
        break;
    case 'warning': toastr.warning(msg);
        break;
    case 'error': toastr.error(msg);
        break;
}
//toastr.clear();
}