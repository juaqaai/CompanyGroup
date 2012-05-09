//var CompanyGroupCms = CompanyGroupCms || {};

//CompanyGroupCms.Models = CompanyGroupCms.Models || {};

//CompanyGroupCms.Models.SignInRequest = (function (userName, password, serviceUrl, token) {
//    var _userName = userName;
//    var _password = password;
//    var _serviceUrl = serviceUrl;
//    var _token = token;

//    return {
//        getUserName: function () {
//            return _userName;
//        },
//        setUserName: function (value) {
//            _userName = value;
//        },
//        getPassword: function () {
//            return _password;
//        },
//        setPassword: function (value) {
//            _password = value;
//        },
//        getServiceUrl: function () {
//            return _serviceUrl;
//        },
//        setServiceUrl: function (value) {
//            _serviceUrl = value;
//        },
//        getToken: function () {
//            return _token;
//        },
//        setToken: function (value) {
//            _token = value;
//        }
//    };
//} ());

//CompanyGroupCms.Models.SignInRequest.getPassword();

var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.VisitorInfo = function () {

    var self = this;

    self.companyName = ko.observable('');

    self.personName = ko.observable('');

    self.isValidLogin = ko.observable(false);

    self.isWebAdministrator = ko.observable(false);

    self.invoiceInfoEnabled = ko.observable(false);

    self.priceListDownloadEnabled = ko.observable(false);

    self.canOrder = ko.observable(false);

    self.recieveGoods = ko.observable(false);

    self.authorizationData = function (serviceUrl) {
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
                    self.isValidLogin(result.IsValidLogin);
                    self.companyName(result.CompanyName);
                    self.personName(result.PersonName);
                    self.isWebAdministrator(result.Permission.IsWebAdministrator);
                    self.invoiceInfoEnabled(result.Permission.InvoiceInfoEnabled);
                    self.priceListDownloadEnabled(result.Permission.PriceListDownloadEnabled);
                    self.canOrder(result.Permission.CanOrder);
                    self.recieveGoods(result.Permission.RecieveGoods);
                }
                else {
                    alert('VisitorInfo result failed');
                }
            },
            error: function () {
                alert('VisitorInfo call failed');
            }
        });
    };
    this.signOut = function (serviceUrl) {
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
                    self.isValidLogin(result.IsValidLogin);
                    self.companyName(result.CompanyName);
                    self.personName(result.PersonName);
                    self.isWebAdministrator(result.Permission.IsWebAdministrator);
                    self.invoiceInfoEnabled(result.Permission.InvoiceInfoEnabled);
                    self.priceListDownloadEnabled(result.Permission.PriceListDownloadEnabled);
                    self.canOrder(result.Permission.CanOrder);
                    self.recieveGoods(result.Permission.RecieveGoods);
                }
                else {
                    alert('SignOut result failed');
                }
            },
            error: function () {
                alert('SignOut call failed');
            }
        });
    };

    this.signIn = function (userName, password, token, serviceUrl) {
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
                    self.isValidLogin(result.IsValidLogin);
                    self.companyName(result.CompanyName);
                    self.personName(result.PersonName);
                    self.isWebAdministrator(result.Permission.IsWebAdministrator);
                    self.invoiceInfoEnabled(result.Permission.InvoiceInfoEnabled);
                    self.priceListDownloadEnabled(result.Permission.PriceListDownloadEnabled);
                    self.canOrder(result.Permission.CanOrder);
                    self.recieveGoods(result.Permission.RecieveGoods);
                }
                else {
                    alert('SignIn call failed!');
                }
            },
            error: function () {
                alert('SignIn call failed!');
            }
        });
    };

    //			    return {
    //			        isValidLogin: ko.computed({
    //			            read: function () {
    //			                return _isValidLogin;
    //			            },
    //			            write: function (value) {
    //			                _isValidLogin(value);
    //			            },
    //			            owner: this
    //			        }),
    //			    };
};    //());


CompanyGroupCms.VisitorInfoFactory = (function () {
    var createVisitorInfo = function (initialData) {
        var visitor = new CompanyGroupCms.VisitorInfo();
        visitor.companyName(initialData.CompanyName);
        visitor.personName(initialData.PersonName);
        visitor.isValidLogin(initialData.IsValidLogin);
        visitor.isWebAdministrator(initialData.IsWebAdministrator);
        visitor.invoiceInfoEnabled(initialData.InvoiceInfoEnabled);
        visitor.priceListDownloadEnabled(initialData.PriceListDownloadEnabled);
        visitor.canOrder(initialData.CanOrder);
        visitor.recieveGoods(initialData.RecieveGoods);
        return visitor;
    }
    return {
        Create: createVisitorInfo
    };
})();

//CompanyGroupCms.VisitorInfoFactory.create(companyName, personName, isValidLogin, isWebAdministrator, invoiceInfoEnabled, priceListDownloadEnabled, canOrder, recieveGoods);