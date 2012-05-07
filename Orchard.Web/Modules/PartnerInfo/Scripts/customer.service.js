var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Services = CompanyGroupCms.Services || {};

CompanyGroupCms.Services.CustomerService = (function () {

    var instance;

    var updateLoginInfo = function (visitor) {
        if (visitor) {
            if (visitor.IsValidLogin) {
                $('#span_visitorinfo_inner').show();
                $('#span_visitorinfo_outer').hide();
                $('#loginwin').hide();
                $('#anchor_signout').show();
                $('#span_visitor_name').text(visitor.CompanyName + ' ' + visitor.PersonName);
            } else {
                $('#span_visitorinfo_inner').hide();
                $('#span_visitorinfo_outer').show();
                $('#loginwin').show();
                $('#anchor_signout').hide();
                $('#span_visitor_name').text('');
            }
        }
    }

    function create() {
        return {
            SignIn: function (userName, password, token, serviceUrl) {
                var data = new Object();
                data.UserName = userName;
                data.Password = password;
                data.__RequestVerificationToken = token;
                var dataString = $.toJSON(data);
                $.ajax({
                    type: "POST",
                    url: serviceUrl,
                    data: dataString,
                    contentType: "application/json; charset=utf-8",
                    timeout: 10000,
                    dataType: "json",
                    processData: true,
                    success: function (result) {
                        if (result) {
                            $.fancybox.close();
                            updateLoginInfo(result.Visitor);
                        }
                        else {
                            alert('SignIn call failed!');
                        }
                    },
                    error: function () {
                        alert('SignIn call failed!');
                    }
                });
            },
            SignOut: function (serviceUrl) {
                var data = new Object();
                var dataString = $.toJSON(data);
                $.ajax({
                    type: "POST",
                    url: serviceUrl,
                    data: dataString,
                    contentType: "application/json; charset=utf-8",
                    timeout: 10000,
                    dataType: "json",
                    processData: true,
                    success: function (result) {
                        if (result) {
                            $('#span_visitorinfo_inner').hide();
                            $('#span_visitorinfo_outer').show();
                            $('#loginwin').show();
                            $('#anchor_signout').hide();
                            $('#span_visitor_name').text('');
                        }
                        else {
                            alert('SignOut result failed');
                        }
                    },
                    error: function () {
                        alert('SignOut call failed');
                    }
                });
            },
            VisitorInfo: function (serviceUrl) {
                var data = new Object();
                var dataString = $.toJSON(data);
                $.ajax({
                    type: "POST",
                    url: serviceUrl,
                    data: dataString,
                    contentType: "application/json; charset=utf-8",
                    timeout: 10000,
                    dataType: "json",
                    processData: true,
                    success: function (result) {
                        if (result) {
                            updateLoginInfo(result);
                        }
                        else {
                            alert('VisitorInfo result failed');
                        }
                    },
                    error: function () {
                        alert('VisitorInfo call failed');
                    }
                });
            }
        }
    }
    return {
        Instance: function () {
            if (!instance) {
                instance = create();
            }
            return instance;
        }
    };
} ());

// CompanyGroupCms.Services.CustomerService.Instance().SignIn('', '', '', '');