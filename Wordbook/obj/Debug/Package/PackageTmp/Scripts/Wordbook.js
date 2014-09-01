$(document).ready(function () {
    var table = $('#table_id').DataTable(
        { "columns":[
                    { "width": "5%" ,
                      "class": 'details-control',
                      "searchable": false,
                      "orderable": false,
                    },
                    { "width": "25%" },
                    { "width": "60%",
                      "visible": false,
                    },
                    { "width": "10%" }
                ],
            "order": [[3, 'asc']],
            "drawCallback": function (settings) {
                var api = this.api();
                var rows = api.rows({ page: 'current' }).nodes();
                var last = null;
                api.column(3, { page: 'current' }).data().each(function (group, i) {
                    if (last !== group) {
                        $(rows).eq(i).before('<tr class="group"><td colspan="5">' + group + '</td></tr>');
                        last = group;
                    }
                });
            }
        }
    );
     table.on( 'order.dt search.dt', function () {
        table.column(0, {search:'applied', order:'applied'}).nodes().each( function (cell, i) {
            cell.innerHTML = i+1;
        } );
    } ).draw();

    $('#table_id tbody').on( 'click', 'tr', function (evt) {
        if (evt.ctrlKey)
            $(this).toggleClass('selected');//alert('Ctrl down');
        //if (evt.altKey)alert('Alt down');
        
    } );
 

    // Add event listener for opening and closing details
    //$('#table_id tbody').on('click', 'td.details-control', function () {
     $('#table_id tbody').on('click', 'tr', function (evt) {
        if (evt.ctrlKey){

        }
        else{
            var tr = $(this).closest('tr');
            var row = table.row(tr);
            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                tr.removeClass('shown');
            }
            else {
            // Open this row
                row.child(format(row.data())).show();
                tr.addClass('shown');
            }
        }
    });

    $('#AddWord').on( 'click', function (e) {
        e.preventDefault();
        //var column = table.column( $(this).attr('data-column') );
        //var column = table.column( 2);
        //column.visible( ! column.visible() );
    } );
   
   $('#DeleteWord').on( 'click', function (e) {
        
        
        e.preventDefault();
        var count = table.rows('.selected').data().length;
        if (count==0)
        {
            alert('単語が選択されません。');
        }else{
            var data = table.rows('.selected').data();
            var words =[];

            alert(data.length);
            /*var l = Ladda.create(this);
	 	    l.start();

            var transport = new Thrift.TXHRTransport(URL+"/WordbookHandler.ashx");
            var protocol = new Thrift.TJSONProtocol(transport);
            var client = new WordbookThriftServiceClient(protocol);
            client.logout(function (result) {})
                .fail(function (jqXHR, status, error) {
                     
                })
                .done(function () {
                    window.location = URL+"/wordbook/Login";
                })
                .always(function () {  
                    l.stop();
                }); 
                */
        }
    } );

    $('#Logout').on( 'click', function (e) {
        e.preventDefault();
        var l = Ladda.create(this);
	 	l.start();

        var transport = new Thrift.TXHRTransport(URL+"/WordbookHandler.ashx");
        var protocol = new Thrift.TJSONProtocol(transport);
        var client = new WordbookThriftServiceClient(protocol);
        client.logout(function (result) {})
            .fail(function (jqXHR, status, error) {
                     
                })
            .done(function () {
                    window.location = URL+"/wordbook/Login";
                })
            .always(function () {  
                    l.stop();
                }); 
    } );

    $('#LoadData').on( 'click', function (e) {
        e.preventDefault();
            var l = Ladda.create(this);
	 	    l.start();

            var transport = new Thrift.TXHRTransport(URL+"/WordbookHandler.ashx");
            var protocol = new Thrift.TJSONProtocol(transport);
            var client = new WordbookThriftServiceClient(protocol);
            var secretKey = client.getWords('limingliang@kyotsu.com', function (result) {
                
                })
                .fail(function (jqXHR, status, error) {
                     var err = error;
                     var sta = status
                })
                .done(function (result) {
                    if(result)
                    {
                        var trs ="";
                        table.clear().draw();
                        for (i = 0; i < result.count; i++) { 
                            var word =result.words[i];
                            var date = new Date(word.time.year, word.time.month, word.time.day);
                            var dateStr = getFormattedDate(date);
                            table.row.add( [i+1,word.item+'1',word.trans,dateStr]);
                            table.row.add( [i+2,word.item+'2',word.trans,dateStr]);
                            table.row.add( [i+3,word.item+'3',word.trans,dateStr]);
                        }
                        table.draw();
                    }
                })
                .always(function () {  
                    l.stop();
                }); 
    } );
});


function format(d) {
    // `d` is the original data object for the row
    return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
		'<tr>' +
			'<td>意味:</td>' +
			'<td>' + d[2] + '</td>' +
		'</tr>' +
	'</table>';
}

function getFormattedDate(date) {
    var year = date.getFullYear();
    var month = (1 + date.getMonth()).toString();
    month = month.length > 1 ? month : '0' + month;
    var day = date.getDate().toString();
    day = day.length > 1 ? day : '0' + day;
    return year + '/' + month + '/' + day;
}

function getQueryParams() {
    var qs = window.location.search.substring(1);
    qs = qs.split("+").join(" ");
    var params = {}, tokens,re = /[?&]?([^=]+)=([^&]*)/g;
    while (tokens = re.exec(qs)) {
        params[decodeURIComponent(tokens[1])]
            = decodeURIComponent(tokens[2]);
    }
    return params;
}

