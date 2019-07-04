
$(document).ready(function () {
    //start of the document ready function
    //delcaring global variable to hold primary key value.
    var pryEmpId;
    $('.delete-prompt').click(function () {
        debugger
        pryEmpId = $(this).attr('id');
        $('#myModal').modal('show');
    });

    $('.delete-confirm').click(function () {
        debugger

        if (pryEmpId != '') {
            $.ajax({
                url: deleteUrl +'/'+ pryEmpId,
                //data: { 'id': pryEmpId },
                type: 'get',
                success: function (data) {
                    if (data) {
                        toastify("error", 'Record deleted successfully');
                        document.location.reload()
                    }
                    $('#myModal').modal('hide');
                }, error: function (err) {
                    toastify("error", err.statusText);
                }
            });
        }
    });
  
});
$('#demoGrid').dataTable({
    "language": {
        "lengthMenu": "Display _MENU_ records per page",
        "zeroRecords": "Nothing found - sorry",
        "info": "Showing page _PAGE_ of _PAGES_",
        "infoEmpty": "No records available",
        "infoFiltered": "(filtered from _MAX_ total records)"
    }
    //end of the docuement ready function
});