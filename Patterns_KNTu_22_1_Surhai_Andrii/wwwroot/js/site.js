
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

    $('#categoriesTable').DataTable({
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

    $('#categorySelectTable').DataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "searching": false

    });


    $('#brandsTable').DataTable({
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

    $('#brandSelectTable').DataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "searching": false

    });


    $('#countriesTable').DataTable({
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

    $('#countrySelectTable').DataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "searching": false

    });


    $('#usersTable').DataTable({
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

    $('#userSelectTable').DataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "searching": false

    });


    $('#ordersTable').DataTable({
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

    $('#orderSelectTable').DataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "searching": false

    });


    $('#ordersStatusesTable').DataTable({
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

    $('#orderStatusSelectTable').DataTable({
        "bPaginate": false,
        "bLengthChange": false,
        "bFilter": true,
        "bInfo": false,
        "searching": false

    });

});