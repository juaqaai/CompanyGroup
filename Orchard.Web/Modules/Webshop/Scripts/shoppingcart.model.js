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
            data: ko.toJSON(CompanyGroupCms.GetCartByKeyRequest),
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
    var getCartCollectionByVisitor = function () {
        //console.log('GetCartCollectionByVisitor');
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('GetCartCollectionByVisitor'),
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
                alert('GetCartCollectionByVisitorId call failed');
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
        data.CartId = CompanyGroupCms.ShoppingCartInfo.Instance().CartId();
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
                    CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(CompanyGroupCms.ShoppingCartSummary.Instance()).appendTo("#basket_open_button");

                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                    $('.cartnumber').spin();

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
        data.CartId = CompanyGroupCms.ShoppingCartInfo.Instance().CartId();
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
                    CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(CompanyGroupCms.ShoppingCartSummary.Instance()).appendTo("#basket_open_button");

                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
                    $('.cartnumber').spin();
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
                    CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(CompanyGroupCms.ShoppingCartSummary.Instance()).appendTo("#basket_open_button");

                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
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
    //aktív kosár mentése
    var saveCart = function (cartName) {
        var data = new Object();
        data.CartId = CompanyGroupCms.ShoppingCartInfo.Instance().CartId();
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
                    $("#cus_basket_menu").empty();
                    $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");
                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
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
    //kosár hozzáadás
    var addCart = function () {
        var data = new Object();
        data.Name = '';
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('AddCart'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.ActiveCart.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(CompanyGroupCms.ShoppingCartSummary.Instance()).appendTo("#basket_open_button");

                    $("#cus_basket_menu").empty();
                    $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");

                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
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
                    CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.ActiveCart.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(CompanyGroupCms.ShoppingCartSummary.Instance()).appendTo("#basket_open_button");

                    $("#cus_basket_menu").empty();
                    $("#shoppingCartHeaderTemplate").tmpl(result).appendTo("#cus_basket_menu");

                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
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
                    CompanyGroupCms.ShoppingCartSummary.Instance().Init(true, result.SumTotal);
                    $("#basket_open_button").empty();
                    $("#shoppingCartSummaryTemplate").tmpl(CompanyGroupCms.ShoppingCartSummary.Instance()).appendTo("#basket_open_button");
                    $("#cus_basket").empty();
                    $("#shoppingCartLineTemplate").tmpl(result).appendTo("#cus_basket");
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
    return {
        AddLine: addLine,
        UpdateLine: updateLine,
        RemoveLine: removeLine,
        SaveCart: saveCart,
        AddCart: addCart,
        RemoveCart: removeCart,
        ActivateCart: activateCart,
        SaveShoppingCartOpenStatus: saveShoppingCartOpenStatus,
        SaveCatalogueOpenStatus: saveCatalogueOpenStatus
    };
})();
//CompanyGroupCms.ShoppingCart.ActivateCart(cartId);
