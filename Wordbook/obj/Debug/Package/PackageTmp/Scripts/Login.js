$(document).ready(function () {
    $('#login').click(
        function (e) {
            e.preventDefault();
            var l = Ladda.create(this);
            l.start();

            var transport = new Thrift.TXHRTransport(URL+"/WordbookHandler.ashx");
            var protocol = new Thrift.TJSONProtocol(transport);
            var client = new WordbookThriftServiceClient(protocol);
            var email = $('#email').val();
            var password = $('#password').val();
            var secretKey = client.login(email, password, function (result) {

            })
                .fail(function (jqXHR, status, error) {
                    alert(error.message);
                })
                .done(function (result) {
                    if (result.success) {
                        window.location = URL+"/Wordbook/index?abcd=1";
                    } else {
                        alert(result.msg);
                    }
                })
                .always(function () {
                    l.stop();
                }); ;
        }
    );
});

  