var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Visitor = (function () {

    /*
    this.Id = String.Empty;
    this.Permission = new Permission();
    this.Roles = new List<string>();
    this.History = new List<string>();
    */
    var companyId = '';
    var companyName = '';
    var currency = '';
    var inventLocation = '';
    var languageId = '';
    var paymTermId = '';
    var personId = '';
    var personName = '';
    var isValidLogin = false;
    var isWebAdministrator = false;
    var invoiceInfoEnabled = false;
    var priceListDownloadEnabled = false;
    var canOrder = false;
    var recieveGoods = false;
    var representativeId = '';
    var representativeName = '';
    var representativeEmail = '';
    var representativePhone = '';
    var representativeMobile = '';
    var bscAuthorized = false;
    var hrpAuthorized = false;
    var loaded = false;

    var instance;

    function create() {
        return {
            CompanyId: function () {
                return companyId;
            },
            CompanyName: function () {
                return companyName;
            },
            Currency: function () {
                return currency;
            },
            setCurrency: function (value) {
                currency = value;
            }, 
            InventLocation: function () {
                return inventLocation;
            },
            LanguageId: function () {
                return languageId;
            },
            setLanguageId: function (value) {
                languageId = value;
            },
            InverseLanguageId: function () {
                return (languageId === 'HU') ? 'EN' : 'HU';
            },
            PaymTermId: function () {
                return paymTermId;
            },
            PersonId: function () {
                return personId;
            },
            PersonName: function () {
                return personName;
            },
            IsValidLogin: function () {
                return isValidLogin;
            },
            IsWebAdministrator: function () {
                return isWebAdministrator;
            },
            InvoiceInfoEnabled: function () {
                return invoiceInfoEnabled;
            },
            PriceListDownloadEnabled: function () {
                return priceListDownloadEnabled;
            },
            CanOrder: function () {
                return canOrder;
            },
            RecieveGoods: function () {
                return recieveGoods;
            },
            IsLoaded: function () {
                return loaded;
            },
            SeLoaded: function (loaded) {
                this.loaded = loaded;
            },
            RepresentativeId: function () {
                return representativeId;
            },
            RepresentativeName: function () {
                return representativeName;
            },
            RepresentativeEmail: function () {
                return representativeEmail;
            },
            RepresentativePhone: function () {
                return representativePhone;
            },
            RepresentativeMobile: function () {
                return representativeMobile;
            },
            BscAuthorized: function () {
                return bscAuthorized;
            },
            HrpAuthorized: function () {
                return hrpAuthorized;
            },
            Clear: function () {
                companyId = '';
                companyName = '';
                currency = '';
                inventLocation = '';
                languageId = '';
                paymTermId = '';
                personId = '';
                personName = '';
                isValidLogin = false;
                isWebAdministrator = false;
                invoiceInfoEnabled = false;
                priceListDownloadEnabled = false;
                canOrder = false;
                recieveGoods = false;
                loaded = false;
                representativeId = '';
                representativeName = '';
                representativeEmail = '';
                representativePhone = '';
                representativeMobile = '';
                bscAuthorized = false;
                hrpAuthorized = false;
            },
            Set: function (data) {
                companyId = data.CompanyId;
                companyName = data.CompanyName;
                currency = data.Currency;
                inventLocation = data.InventLocation;
                languageId = data.LanguageId;
                paymTermId = data.PaymTermId;
                personId = data.PersonId;
                personName = data.PersonName;
                isValidLogin = data.IsValidLogin;
                isWebAdministrator = data.IsWebAdministrator;
                invoiceInfoEnabled = data.InvoiceInfoEnabled;
                priceListDownloadEnabled = data.PriceListDownloadEnabled;
                canOrder = data.CanOrder;
                recieveGoods = data.RecieveGoods;
                loaded = true;
                representativeId = data.Representative.Id;
                representativeName = data.Representative.Name;
                representativeEmail = data.Representative.Email;
                representativePhone = data.Representative.Phone;
                representativeMobile = data.Representative.Mobile;
                bscAuthorized = data.BscAuthorized;
                hrpAuthorized = data.HrpAuthorized;
            }
        }
    };
    return {
        Instance: function () {
            if (!instance) {
                instance = create();
            }
            return instance;
        }
    }
})();

CompanyGroupCms.VisitorInfo = (function () {
    var changeLanguage = function () {
        var language = CompanyGroupCms.Visitor.Instance().LanguageId();
        var data = new Object();
        data.Language = (language === '' || language === 'HU') ? 'EN' : 'HU';
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getPartnerInfoServiceUrl('ChangeLanguage'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                console.log('ChangeLanguage');
                CompanyGroupCms.Visitor.Instance().setLanguageId(data.Language);
                $("#inverse_language_id").html(CompanyGroupCms.Visitor.Instance().InverseLanguageId());
            },
            error: function () {
                alert('ChangeLanguage call failed');
            }
        });
    };
    var changeCurrency = function (value) {
        var data = new Object();
        data.Currency = value;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getPartnerInfoServiceUrl('ChangeCurrency'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                console.log('ChangeCurrency');
                CompanyGroupCms.Visitor.Instance().setCurrency(data.Currency);
                if (data.Currency === 'HUF') {
                    $("#currency_huf").css('background-color', '#900');
                    $("#currency_eur").css('background-color', '#666');
                    $("#currency_usd").css('background-color', '#666');
                    CompanyGroupCms.Catalogue.LoadCatalogue();
                }
                else if (data.Currency === 'EUR') {
                    $("#currency_huf").css('background-color', '#666');
                    $("#currency_eur").css('background-color', '#900');
                    $("#currency_usd").css('background-color', '#666');
                    CompanyGroupCms.Catalogue.LoadCatalogue();
                }
                else {
                    $("#currency_huf").css('background-color', '#666');
                    $("#currency_eur").css('background-color', '#666');
                    $("#currency_usd").css('background-color', '#900');
                    CompanyGroupCms.Catalogue.LoadCatalogue();
                }
            },
            error: function () {
                alert('ChangeCurrency call failed');
            }
        });
    };
    var initView = function () {
        if (CompanyGroupCms.Visitor.Instance().Currency === 'HUF') {
            $("#currency_huf").css('background-color', '#900');
            $("#currency_eur").css('background-color', '#666');
            $("#currency_usd").css('background-color', '#666');
            //CompanyGroupCms.Catalogue.LoadCatalogue();
        }
        else if (CompanyGroupCms.Visitor.Instance().Currency === 'EUR') {
            $("#currency_huf").css('background-color', '#666');
            $("#currency_eur").css('background-color', '#900');
            $("#currency_usd").css('background-color', '#666');
            //CompanyGroupCms.Catalogue.LoadCatalogue();
        }
        else {
            $("#currency_huf").css('background-color', '#666');
            $("#currency_eur").css('background-color', '#666');
            $("#currency_usd").css('background-color', '#900');
            //CompanyGroupCms.Catalogue.LoadCatalogue();
        }
    };
    var authorize = function () {
        var data = new Object();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getPartnerInfoServiceUrl('VisitorInfo'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    CompanyGroupCms.Visitor.Instance().Set(result);
                    $("#visitorInfoTemplate").tmpl(CompanyGroupCms.Visitor.Instance()).appendTo("#cus_header1");
                    $("#usermenuTemplate").tmpl(CompanyGroupCms.Visitor.Instance()).appendTo("#usermenuContainer");
                    $("#quickmenuTemplate").tmpl(CompanyGroupCms.Visitor.Instance()).appendTo("#quickmenuContainer");
                }
                else {
                    alert('authorize result failed');
                }
            },
            error: function () {
                alert('authorize call failed');
            }
        });
    };
    var signOut = function () {
        var data = new Object();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getSignOutServiceUrl(),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (!result.Visitor.IsValidLogin) {
                    CompanyGroupCms.Visitor.Instance().Clear();
                    $("#cus_header1").empty();
                    $("#visitorInfoTemplate").tmpl(result.Visitor).appendTo("#cus_header1");
                    $("#quickmenuContainer").empty();
                    $("#quickmenuTemplate").tmpl(result.Visitor).appendTo("#quickmenuContainer");
                    $("#usermenuContainer").empty();
                    $("#usermenuTemplate").tmpl(result.Visitor).appendTo("#usermenuContainer");
                    //webshop oldal
                    if (CompanyGroupCms.Constants.Instance().isEqualModule('webshop')) {
                        $("#div_pager_top").empty();
                        $("#pagerTemplateTop").tmpl(result.Products).appendTo("#div_pager_top");
                        $("#div_pager_bottom").empty();
                        $("#pagerTemplateBottom").tmpl(result.Products).appendTo("#div_pager_bottom");
                        $("#div_catalogue").empty();
                        $("#productTemplate").tmpl(result).appendTo("#div_catalogue");
                        $("#catalogueSequenceContainer").empty();
                        $("#catalogueSequenceTemplate").tmpl(result.Visitor).appendTo("#catalogueSequenceContainer");
                        $("#catalogueDownloadContainer").empty();
                        $("#catalogueDownloadTemplate").tmpl(result.Visitor).appendTo("#catalogueDownloadContainer");
                        //$("#cus_filter_price").empty();
                        //$("#priceFilterTemplate").tmpl(result.Visitor).appendTo("#cus_filter_price");
                        $("#cus_filter_price").hide();

                        $("#hidden_cartId").val('');
                        //CompanyGroupCms.ShoppingCartInfo.Instance().SetCartId('');
                        //CompanyGroupCms.ShoppingCartSummary.Instance().Init(result.Visitor.IsValidLogin, 0);
                        $("#basket_open_button").empty();
                        $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button");
                        $("#cus_basket_menu").empty();
                        $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");
                        $("#cus_basket").empty();
                        $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                        $("#deliveryAddressTemplate").tmpl(result.DeliveryAddresses).appendTo("#deliveryAddressContainer");
                    }
                    //partnerinfo home oldal
                    if (CompanyGroupCms.Constants.Instance().isEqualModule('partnerinfo')) {
                        $("#dashbordContainer").empty();
                        $("#dashbordTemplate").tmpl(result.Visitor).appendTo("#dashbordContainer");
                    }
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
    var showSignInPanel = function () {
        $.fancybox({
            href: '#div_login',
            autoDimensions: true,
            autoScale: false,
            transitionIn: 'fade',
            transitionOut: 'fade'
        });
    };
    var signIn = function (userName, password, token) {
        var data = new Object();
        data.UserName = userName;
        data.Password = password;
        data.__RequestVerificationToken = token;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getSignInServiceUrl(),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result.Visitor.IsValidLogin) {
                    $.fancybox.close();
                    CompanyGroupCms.Visitor.Instance().Set(result.Visitor);
                    $("#cus_header1").empty();
                    $("#visitorInfoTemplate").tmpl(result.Visitor).appendTo("#cus_header1");
                    $("#quickmenuContainer").empty();
                    $("#quickmenuTemplate").tmpl(result.Visitor).appendTo("#quickmenuContainer");
                    $("#usermenuContainer").empty();
                    $("#usermenuTemplate").tmpl(result.Visitor).appendTo("#usermenuContainer");
                    //webshop oldal
                    if (CompanyGroupCms.Constants.Instance().isEqualModule('webshop')) {
                        $("#div_pager_top").empty();
                        $("#pagerTemplateTop").tmpl(result.Products).appendTo("#div_pager_top");
                        $("#div_pager_bottom").empty();
                        $("#pagerTemplateBottom").tmpl(result.Products).appendTo("#div_pager_bottom");
                        $("#div_catalogue").empty();
                        $("#productTemplate").tmpl(result).appendTo("#div_catalogue");
                        $("#catalogueSequenceContainer").empty();
                        $("#catalogueSequenceTemplate").tmpl(result.Visitor).appendTo("#catalogueSequenceContainer");
                        $("#catalogueDownloadContainer").empty();
                        $("#catalogueDownloadTemplate").tmpl(result.Visitor).appendTo("#catalogueDownloadContainer");

                        //$("#cus_filter_price").empty();
                        //$("#priceFilterTemplate").tmpl(result.Visitor).appendTo("#cus_filter_price");
                        $("#cus_filter_price").show();

                        $("#hidden_cartId").val(result.ActiveCart.Id);
                        //CompanyGroupCms.ShoppingCartInfo.Instance().SetCartId(result.ActiveCart.Id);
                        //CompanyGroupCms.ShoppingCartSummary.Instance().Init(result.Visitor.IsValidLogin, result.ActiveCart.SumTotal);
                        $("#basket_open_button").empty();
                        $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button"); // CompanyGroupCms.ShoppingCartSummary.Instance() 
                        $("#cus_basket_menu").empty();
                        $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");
                        $("#cus_basket").empty();
                        $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                        $("#deliveryAddressTemplate").tmpl(result.DeliveryAddresses).appendTo("#deliveryAddressContainer");
                    }
                    //partnerinfo home oldal
                    if (CompanyGroupCms.Constants.Instance().isEqualModule('partnerinfo')) {
                        $("#dashbordContainer").empty();
                        $("#dashbordTemplate").tmpl(result.Visitor).appendTo("#dashbordContainer");
                    }
                }
                else {
                    $("#login_errors").html(result.Visitor.ErrorMessage);
                    $("#login_errors").show();
                }
            },
            error: function () {
                alert('SignIn call failed!');
            }
        });
    };
    var invoiceInfo = function () {
        var data = new Object();
        data.PaymentType = $("input[name='radio_paymenttype']:radio:checked").val();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getPartnerInfoServiceUrl('FilteredInvoice'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    $("#invoiceInfoListContainer").empty();
                    $("#invoiceInfoListTemplate").tmpl(result).appendTo("#invoiceInfoListContainer");
                    $("#itemCount").html(result.ItemCount);
                    console.log(result);
                }
                else {
                    console.log('invoiceInfo result failed');
                }
            },
            error: function () {
                console.log('invoiceInfo call failed');
            }
        });
    };
    var changePassword = function () {
        alert('');
        var data = new Object();
        data.OldPassword = $('#txt_oldpassword').val();
        data.NewPassword = $('#txt_newpassword').val();
        data.UserName = $('#txt_username').val();
        data.RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getPartnerInfoServiceUrl('ChangePwd'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    console.log(result);
                    if (result.OperationSucceeded) {
                        $('#changePasswordSucceededResult').show();
                        $('#changepassword_succeededmessage').html(result.Message);
                        $('#changePasswordFailedResult').hide();
                    }
                    else {
                        $('#changePasswordFailedResult').show();
                        $('#changepassword_failedmessage').html(result.Message);
                        $('#changePasswordSucceededResult').hide();
                    }
                }
                else {
                    console.log('changePassword result failed');
                }
            },
            error: function () {
                console.log('changePassword call failed');
            }
        });
    };
    return {
        Authorize: authorize,
        SignOut: signOut,
        SignIn: signIn,
        ShowSignInPanel: showSignInPanel,
        ChangeLanguage: changeLanguage,
        ChangeCurrency: changeCurrency,
        InitView: initView,
        InvoiceInfo: invoiceInfo,
        ChangePassword: changePassword
    };
})();

//CompanyGroupCms.VisitorInfo.ChangeCurrency('HU');
//CompanyGroupCms.VisitorInfoFactory = (function () {
//    var createVisitorInfo = function (initialData) {
//        var visitor = new CompanyGroupCms.VisitorInfo();
//        visitor.companyName(initialData.CompanyName);
//        visitor.personName(initialData.PersonName);
//        visitor.isValidLogin(initialData.IsValidLogin);
//        visitor.isWebAdministrator(initialData.IsWebAdministrator);
//        visitor.invoiceInfoEnabled(initialData.InvoiceInfoEnabled);
//        visitor.priceListDownloadEnabled(initialData.PriceListDownloadEnabled);
//        visitor.canOrder(initialData.CanOrder);
//        visitor.recieveGoods(initialData.RecieveGoods);
//        return visitor;
//    }
//    return {
//        Create: createVisitorInfo
//    };
//})();

//CompanyGroupCms.VisitorInfoFactory.create(companyName, personName, isValidLogin, isWebAdministrator, invoiceInfoEnabled, priceListDownloadEnabled, canOrder, recieveGoods);