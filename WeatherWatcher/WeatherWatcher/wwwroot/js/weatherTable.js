sessionStorage.setItem("currentOffset", "0");
sessionStorage.setItem("currentSize", "50");
getPaginated();
let currentOffset = JSON.parse(sessionStorage.getItem("currentOffset"));
let currentSize = JSON.parse(sessionStorage.getItem("currentSize"));

function getPaginated() {
    let timeFilter = null;
    let dateFilter = null;
    if ($('#timeOnlyFilter') )
    {
        timeFilter = $('#timeOnlyFilter').val();
    }
    if ($('#dateOnlyFilter') ) {
        dateFilter = $('#dateOnlyFilter').val();
    }
    sessionStorage.setItem("timeFilter", timeFilter );
    sessionStorage.setItem("dateFilter", dateFilter );
   
    let rangeDates = null;
    if (dateFilter) {
        let split = dateFilter.split(" - ");
        rangeDates = {};
        rangeDates.From = split[0];
        rangeDates.To = split[1];
    };
    let currentOffset = JSON.parse(sessionStorage.getItem("currentOffset"));
    let currentSize = JSON.parse(sessionStorage.getItem("currentSize"));
    let filterReq = {
        offset: currentOffset,
        size: currentSize,
        dateOnlyFilters: rangeDates ? JSON.stringify([rangeDates.From, rangeDates.To]) : null,
        timeOnlyFilters: timeFilter ? JSON.stringify(timeFilter) : null
    };
    $('#tableLoading').show();
    $('#tableWithPaginator').hide();
    $.ajax({
        type: "POST",
        url: window.location + "/GetPaginatedTable",
        data: filterReq,
        error: function (data) {
            console.log(data.error);
        },
        success: function (html) {
            $('#tableWithPaginator').show();
            $("#tableWithPaginator").html(html);
            $('#tableLoading').hide();
            $('#timeOnlyFilter').timepicker({ 'timeFormat': 'H:i:s' });

            let time = sessionStorage.getItem("timeFilter");
            if (time && time != 'undefined') {
                $('#timeOnlyFilter').val(time);
            }
            $('#dateOnlyFilter').val("");
            let date = sessionStorage.getItem("dateFilter");
            if (date && date != 'undefined') {
                let split = date.split(" - ");
                let from = split[0];
                let to = split[1];
                $('#dateOnlyFilter').daterangepicker({
                    "showDropdowns": true,
                    "singleDatePicker": false,
                    "startDate": from,
                    "endDate": to,
                    locale: {
                        cancelLabel: 'Clear'
                    },
                    "linkedCalendars": false,
                    "alwaysShowCalendars": true,
                    "opens": "center"
                });
            } else {
                $('#dateOnlyFilter').daterangepicker({
                    "showDropdowns": true,
                    "singleDatePicker": false,
                    locale: {
                        cancelLabel: 'Clear'
                    },
                    "linkedCalendars": false,
                    "alwaysShowCalendars": true,
                    "opens": "center"
                });
            }

           
            $('#timeOnlyFilter').change((event) => {
                $('#tableLoading').show();
                getPaginated();
            });
            $('#dateOnlyFilter').on('apply.daterangepicker', (ev, picker) => {
                $('#tableLoading').show();
                getPaginated();
            });
            $('#dateOnlyFilter').on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
                sessionStorage.removeItem("dateFilter");
                getPaginated();
            });
        }
    });
   

}

