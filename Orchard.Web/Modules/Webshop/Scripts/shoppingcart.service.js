var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Services = CompanyGroupCms.Services || {};

CompanyGroupCms.Services.ShoppingCartService = (function () {

    var instance;

    function create() {
        return {
            addCart: function (newShoppingCartRequest) {
                var data = new Object();
                data.Language = newShoppingCartRequest.getLanguage();
                data.Name = newShoppingCartRequest.getName();
                data.VisitorId = newShoppingCartRequest.getVisitorId();
                var dataString = $.toJSON(data);
                $.ajax({
                    type: "POST",
                    url: newShoppingCartRequest.getServiceUrl(),
                    data: dataString,
                    contentType: "application/json; charset=utf-8",
                    timeout: 10000,
                    dataType: "json",
                    processData: true,
                    success: function (result) {
                        if (result) {
                            if (result) {
                                $("#cus_basket").empty();
                                $("#shoppingCartTemplate").tmpl(result).appendTo("#cus_basket");
                            }
                            else {
                                alert('Nincs eleme a listának.');
                            }
                        }
                        else {
                            alert('addCart result failed');
                        }
                    },
                    error: function () {
                        alert('addCart call failed');
                    }
                });
            }, //Property1 : 0
            removeCart: function (deleteShoppingCartRequest) {   //void RemoveCart(CompanyGroup.Dto.ServiceRequest.DeleteShoppingCart request)
                var data = new Object();
                data.CartId = deleteShoppingCartRequest.getCartId();
                data.Language = deleteShoppingCartRequest.getLanguage();
                data.VisitorId = deleteShoppingCartRequest.getVisitorId();
                var dataString = $.toJSON(data);
                $.ajax({
                    type: "POST",
                    url: deleteShoppingCartRequest.getServiceUrl(),
                    data: dataString,
                    contentType: "application/json; charset=utf-8",
                    timeout: 10000,
                    dataType: "json",
                    processData: true,
                    success: function (result) {
                        if (result) {
                            if (result) {
                                $("#cus_basket").empty();
                                $("#shoppingCartTemplate").tmpl(result).appendTo("#cus_basket");
                            }
                            else {
                                alert('Nincs eleme a listának.');
                            }
                        }
                        else {
                            alert('removeCart result failed');
                        }
                    },
                    error: function () {
                        alert('removeCart call failed');
                    }
                });                
            },
            addItem: function (addShoppingCartItemRequest) {   //void RemoveCart(CompanyGroup.Dto.ServiceRequest.DeleteShoppingCart request)
                var data = new Object();
                data.CartId = addShoppingCartItemRequest.getCartId();
                data.ProductId = addShoppingCartItemRequest.getProductId();
                data.Quantity = addShoppingCartItemRequest.getQuantity();
                var dataString = $.toJSON(data);
                $.ajax({
                    type: "POST",
                    url: addShoppingCartItemRequest.getServiceUrl(),
                    data: dataString,
                    contentType: "application/json; charset=utf-8",
                    timeout: 10000,
                    dataType: "json",
                    processData: true,
                    success: function (result) {
                        if (result) {
                            if (result) {
                                $("#cus_basket").empty();
                                $("#shoppingCartTemplate").tmpl(result).appendTo("#cus_basket");
                            }
                            else {
                                alert('Nincs eleme a listának.');
                            }
                        }
                        else {
                            alert('removeCart result failed');
                        }
                    },
                    error: function () {
                        alert('removeCart call failed');
                    }
                });
            }
        }
    }

    return {
        getInstance: function () {
            if (!instance) {
                instance = create();
            }
            return instance;
        }
    }

})()

//CompanyGroupCms.Services.ShoppingCartService.getInstance().addCart(newShoppingCartRequest);

