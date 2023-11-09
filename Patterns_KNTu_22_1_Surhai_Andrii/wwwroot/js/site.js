
$(document).ready(function () {
    $('#instrumentsTable').DataTable({
        "buttons": [
            {
                extend: 'searchBuilder',
                config: {
                    depthLimit: 2
                }
            }
        ],
        
        "scrollY": "592px",
        "scrollCollapse": true,
        "paging": true,
        
    });

    $('#instrumentSelectTable').DataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "searching": false

    });

});