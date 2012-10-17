var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Utils = (function () {
    //számformátum ellenőrzés
    var validateNumber = function (value) {
        return (/^[0-9]+$/.test(value));
    };
    //szám formázása
    var formatNumber = function (str) {
        if (!validateNumber(str)) {
            return str;
        }
        str += '';
        x = str.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1))
            x1 = x1.replace(rgx, '$1' + ' ' + '$2');
        return x1 + x2;
    };
    return {
        ValidateNumber: validateNumber,
        FormatNumber: formatNumber
    }

})();
//CompanyGroupCms.Utils.FormatNumber('3434343434');

CompanyGroupCms.ShoppingCartSummary = (function () {
    var _isValidLogin = false;
    var _sumTotal = 0;

    var instance;

    function create() {
        return {
            SumTotal: function () {
                return _sumTotal;
            },
            IsValidLogin: function () {
                return _isValidLogin;
            },
            Init: function (isValidLogin, sumTotal) {
                _isValidLogin = isValidLogin;
                _sumTotal = sumTotal
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

CompanyGroupCms.ShoppingCartInfo = (function () {

    var cartId = '';
    var isShoppingCartOpen = false;

    var instance;

    function create() {
        return {
            CartId: function () {
                return cartId;
            },
            SetCartId: function (value) {
                cartId = value;
            }, 
            IsShoppingCartOpen: function () {
                return isShoppingCartOpen;
            },
            SetShoppingCartOpen: function (value) {
                isShoppingCartOpen = value;
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

CompanyGroupCms.ShoppingCart = (function () {
    //aktiv kosar lekerdezes
    var getCartByKey = function () {
        //console.log('GetCartByKey');
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('GetCartByKey'),
            data: {},
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {

                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('GetCartByKey call failed');
            }
        });
    };
    var getActiveCart = function () {
        //console.log('GetActiveCart');
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('GetActiveCart'),
            data: {},
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {

                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('GetActiveCart call failed');
            }
        });
    };
    //sor hozzáadás
    var addLine = function (productId, quantity) {
        if (!CompanyGroupCms.Utils.ValidateNumber(quantity)) {
            $.fancybox(
                '<p>A mennyiség megadása nem megfelelő!</p>',
                {
                    'autoDimensions': true,
                    'padding': 0,
                    'transitionIn': 'elastic',
                    'transitionOut': 'elastic',
                    'changeFade': 0,
                    'speedIn': 300,
                    'speedOut': 300,
                    'width': '150%',
                    'height': '150%',
                    'autoScale': true
                });
            return false;
        }
        var data = new Object();
        //data.CartId = $("#hidden_cartId").val();
        data.ProductId = productId;
        data.Quantity = quantity;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('AddLine'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button");

                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                    $('.cartnumber').spin();

                    $("#leasingOptionsContainer").empty();
                    $("#leasingOptionsTemplate").tmpl(result.ActiveCart.LeasingOptions).appendTo("#leasingOptionsContainer");
                    if (result.ActiveCart.LeasingOptions.Items.length == 0) {
                        $("#form_financeoffer").hide();
                    }
                    else {
                        $("#form_financeoffer").show();
                    }
                    $.floatingMessage('<span style="font-family: verdana; font-size: 13px; color:#fff;"> A kiválasztott termék:<br /><strong>' + productId + '</strong><br />bekerült a kosárba.</span>', {
                        time: 5000,
                        align: 'right',
                        verticalAlign: 'bottom',
                        show: 'blind',
                        hide: 'puff',
                        stuffEaseTime: 100,
                        stuffEasing: 'easeInExpo',
                        moveEaseTime: 200,
                        moveEasing: 'easeOutBounce'
                    });
                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('addLine call failed');
            }
        });
    };
    //sor módosítás
    var updateLine = function (productId, quantity) {
        if (!CompanyGroupCms.Utils.ValidateNumber(quantity)) {
            $.fancybox(
                '<p>A mennyiség megadása nem megfelelő!</p>',
                {
                    'autoDimensions': true,
                    'padding': 0,
                    'transitionIn': 'elastic',
                    'transitionOut': 'elastic',
                    'changeFade': 0,
                    'speedIn': 300,
                    'speedOut': 300,
                    'width': '150%',
                    'height': '150%',
                    'autoScale': true
                });
            return false;
        }
        var data = new Object();
        //data.CartId = $("#hidden_cartId").val();
        data.ProductId = productId;
        data.Quantity = quantity;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('UpdateLineQuantity'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button");

                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                    $('.cartnumber').spin();

                    $("#leasingOptionsContainer").empty();
                    $("#leasingOptionsTemplate").tmpl(result.ActiveCart.LeasingOptions).appendTo("#leasingOptionsContainer");
                    if (result.ActiveCart.LeasingOptions.Items.length == 0) {
                        $("#form_financeoffer").hide();
                    }
                    else {
                        $("#form_financeoffer").show();
                    }
                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('updateLine call failed');
            }
        });
    };
    //sor törlés
    var removeLine = function (productId) {
        var data = new Object();
        data.ProductId = productId;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('RemoveLine'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button");

                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                    $('.cartnumber').spin();

                    $("#leasingOptionsContainer").empty();
                    $("#leasingOptionsTemplate").tmpl(result.ActiveCart.LeasingOptions).appendTo("#leasingOptionsContainer");
                    if (result.ActiveCart.LeasingOptions.Items.length == 0) {
                        $("#form_financeoffer").hide();
                    }
                    else {
                        $("#form_financeoffer").show();
                    }
                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('removeCart call failed');
            }
        });

    };
    var showSaveCartPanel = function () {
        $.fancybox({
            href: '#save_basket_win',
            autoDimensions: true,
            autoScale: false,
            transitionIn: 'fade',
            transitionOut: 'fade'
        });
    };
    //aktív kosár mentése
    var saveCart = function (cartName) {
        console.log($('#saved_shoppingcart_list').val());
        var data = new Object();
        //data.CartId = $('#saved_shoppingcart_list').val();
        data.Name = cartName;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('SaveCart'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    $.fancybox.close();
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button"); // CompanyGroupCms.ShoppingCartSummary.Instance() 
                    $("#cus_basket_menu").empty();
                    $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");
                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                    //$("#deliveryAddressTemplate").tmpl(result.DeliveryAddresses).appendTo("#site_select");

                    $("#leasingOptionsContainer").empty();
                    $("#leasingOptionsTemplate").tmpl(result.LeasingOptions).appendTo("#leasingOptionsContainer");
                    if (result.LeasingOptions.Items.length == 0) {
                        $("#form_financeoffer").hide();
                    }
                    else {
                        $("#form_financeoffer").show();
                    }
                    $('.cartnumber').spin();
                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('saveCart call failed');
            }
        });
    };
    //kosár hozzáadás
    var addCart = function () {
        var data = new Object();
        data.Name = '';
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('AddCart'),
            data: {},
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.ActiveCart.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button"); // CompanyGroupCms.ShoppingCartSummary.Instance() 
                    $("#cus_basket_menu").empty();
                    $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");
                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");

                    $("#leasingOptionsContainer").empty();
                    $("#leasingOptionsTemplate").tmpl(result.LeasingOptions).appendTo("#leasingOptionsContainer");
                    if (result.LeasingOptions.Items.length == 0) {
                        $("#form_financeoffer").hide();
                    }
                    else {
                        $("#form_financeoffer").show();
                    }
                    $('.cartnumber').spin();
                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('addCart call failed');
            }
        });
    };
    //kosár törlés
    var removeCart = function () {
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('RemoveCart'),
            data: {},
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.ActiveCart.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button"); // CompanyGroupCms.ShoppingCartSummary.Instance() 
                    $("#cus_basket_menu").empty();
                    $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");
                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");

                    $("#leasingOptionsContainer").empty();
                    $("#leasingOptionsTemplate").tmpl(result.LeasingOptions).appendTo("#leasingOptionsContainer");

                    if (result.LeasingOptions.Items.length == 0) {
                        $("#form_financeoffer").hide();
                    }
                    else {
                        $("#form_financeoffer").show();
                    }

                    $('.cartnumber').spin();
                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('removeCart call failed');
            }
        });
    };
    //kosár aktiválás
    var activateCart = function (cartId) {
        var data = new Object();
        data.CartId = cartId;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('ActivateCart'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button"); // CompanyGroupCms.ShoppingCartSummary.Instance() 
                    $("#cus_basket_menu").empty();
                    $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");
                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");

                    $("#leasingOptionsContainer").empty();
                    $("#leasingOptionsTemplate").tmpl(result.LeasingOptions).appendTo("#leasingOptionsContainer");

                    if (result.LeasingOptions.Items.length == 0) {
                        $("#form_financeoffer").hide();
                    }
                    else {
                        $("#form_financeoffer").show();
                    }

                    $('.cartnumber').spin();
                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('removeCart call failed');
            }
        });
    };
    var saveShoppingCartOpenStatus = function () {
        var isOpen = !CompanyGroupCms.ShoppingCartInfo.Instance().IsShoppingCartOpen();
        CompanyGroupCms.ShoppingCartInfo.Instance().SetShoppingCartOpen(isOpen);
        var data = new Object();
        data.IsOpen = isOpen;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('SaveShoppingCartOpenStatus'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                $("#basket_panel").slideToggle("fast");
                $("#active_basket").toggleClass("active");
                $("#shoppingCartSummaryCaption").text(CompanyGroupCms.ShoppingCartInfo.Instance().IsShoppingCartOpen() ? '[ - Bezárás ]' : '[ + Megnyitás ]');
            },
            error: function () {
                alert('saveShoppingCartOpenStatus call failed');
            }
        });
    };
    var saveCatalogueOpenStatus = function () {
        var data = new Object();
        data.IsOpen = CompanyGroupCms.ShoppingCartInfo.Instance().IsShoppingCartOpen();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('SaveCatalogueOpenStatus'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {

            },
            error: function () {
                alert('saveCatalogueOpenStatus call failed');
            }
        });
    };
    //finanszírozási ajánlat elküldése
    var createFinanceOffer = function () {
        var data = new Object();
        data.PersonName = $("#txt_offername").val();
        data.Address = $("#txt_offeraddress").val();
        data.Phone = $("#txt_offerphone").val();
        data.StatNumber = $("#txt_offerstatnumber").val();
        data.NumOfMonth = $("input[name=radio_selectNumOfMonth]").val();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('CreateFinanceOffer'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button");
                    $("#cus_basket_menu").empty();
                    $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");
                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                    $("#leasingOptionsContainer").empty();
                    $("#leasingOptionsTemplate").tmpl(result.LeasingOptions).appendTo("#leasingOptionsContainer");
                    $('.cartnumber').spin();
                    if (result.LeasingOptions.Items.length == 0) {
                        $("#form_financeoffer").hide();
                    }
                    else {
                        $("#form_financeoffer").show();
                    }
                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('createFinanceOffer call failed');
            }
        });
    };
    //rendelés feladás
    var createOrder = function () {
        var data = new Object();
        data.CustomerOrderNote = $("#user_comment").val();
        data.CustomerOrderId = $("#custom_number").val();
        data.DeliveryRequest = $("input[name=radio_szallitasimod]:checked").val() === '2';  //szállítást kért-e
        data.DeliveryDate = $("#naptar").val();                                             //szállítás időpontja
        data.PaymentTerm = $("input[name=radio_fizetesimod]:checked").val();                //1: átut, 2: KP, 3: előreut, 4: utánvét
        data.DeliveryTerm = $("input[name=radio_szallitasimod]:checked").val();             //1: raktár, 2: kiszállítás
        data.DeliveryAddressRecId = $("#site_select").val();                                //szállítási cím azonosító
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('CreateOrder'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    /*
                Visitor = visitor,
                ActiveCart = response.ActiveCart,
                OpenedItems = response.OpenedItems,
                StoredItems = response.StoredItems,
                ShoppingCartOpenStatus = shoppingCartOpenStatus,
                CatalogueOpenStatus = catalogueOpenStatus,
                LeasingOptions = response.LeasingOptions,
                Created = response.Created,
                WaitForAutoPost = response.WaitForAutoPost,
                Message = response.Message                    
                    */
                    $.fancybox('<p>A rendelés feladása sikeresen megtörtént</p>',
                    {
                        'autoDimensions': true,
                        'padding': 0,
                        'transitionIn': 'elastic',
                        'transitionOut': 'elastic',
                        'changeFade': 0,
                        'speedIn': 300,
                        'speedOut': 300,
                        'width': '150%',
                        'height': '150%',
                        'autoScale': true
                    });
                    console.log(result);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(result).appendTo("#basket_open_button");
                    $("#cus_basket_menu").empty();
                    $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");
                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                    $("#leasingOptionsContainer").empty();
                    $("#leasingOptionsTemplate").tmpl(result.LeasingOptions).appendTo("#leasingOptionsContainer");
                    $('.cartnumber').spin();
                    if (result.LeasingOptions.Items.length == 0) {
                        $("#form_financeoffer").hide();
                    }
                    else {
                        $("#form_financeoffer").show();
                    }
                    //$("#form_createorder").hide();
                }
                else {
                    alert('Nincs eleme a listának.');
                }
            },
            error: function () {
                alert('createOrder call failed');
            }
        });
    };
    return {
        AddLine: addLine,
        UpdateLine: updateLine,
        RemoveLine: removeLine,
        SaveCart: saveCart,
        AddCart: addCart,
        RemoveCart: removeCart,
        ActivateCart: activateCart,
        SaveShoppingCartOpenStatus: saveShoppingCartOpenStatus,
        SaveCatalogueOpenStatus: saveCatalogueOpenStatus,
        ShowSaveCartPanel: showSaveCartPanel,
        CreateFinanceOffer: createFinanceOffer,
        CreateOrder: createOrder
    };
})();
//CompanyGroupCms.ShoppingCart.ActivateCart(cartId);
