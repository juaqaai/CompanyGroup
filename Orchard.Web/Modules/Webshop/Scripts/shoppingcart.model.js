var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.ShoppingCart = function () {

    var self = this;
    self.id = ko.observable('');
    self.lines = ko.observableArray([]);    //new CompanyGroupCms.ShoppingCartLine()

    self.openedCarts = ko.observableArray([]);
    self.storedCarts = ko.observableArray([]);

    self.isShoppingCartOpen = ko.observable(false);
    self.isCatalogueOpen = ko.observable(false);

    self.sumTotal = ko.computed(function () {
        var total = 0;
        $.each(self.lines(), function () { total += this.itemTotal() })
        return total;
    });
    //aktiv kosar lekerdezes
    self.getCartByKey = function () {
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
    self.getCartCollectionByVisitor = function () {
        console.log('GetCartCollectionByVisitor');
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
    self.initLines = function (lines) {
        /*
        {"Items":[{"Id":"4fba031e6ee01218d879110f","ProductId":"AMVS238H","PartNumber":"90LME1101T00041C-","CustomerPrice":0,"Currency":null,
        "Pictures":{"Items":[]},"PrimaryPicture":{"FileName":"","Primary":false,"RecId":0},
        "Flags":{"Discount":false,"Bargain":false,"New":false,"Special":false,"InStock":false},
        "Stock":{"Inner":3,"Outer":0,"SecondHand":0,"Serbian":0},
        "ItemState":0,"ShippingDate":"\/Date(-2208992400000)\/",
        "Language":"hu","DataAreaId":"hrp","Garanty":{"Time":"3 év","Mode":"Szervizben"},"ProductManager":{"Name":"Fekete Károly","Email":"kfekete@hrp.hu","Extension":"","Mobile":""},
        "Quantity":2,"ItemTotal":0,"IsInStock":true,"PurchaseInProgress":false}],"SumTotal":0,"Id":"4fba031e6ee01218d879110f"}
        */
        $.each(lines, function (index, item) {
            //item.
            var stock = CompanyGroupCms.CatalogueFactory.CreateStock(item.InnerStock, item.OuterStock, 0, 0);
            var garanty = CompanyGroupCms.CatalogueFactory.CreateGaranty(item.GarantyMode, item.GarantyTime);
            var flags = CompanyGroupCms.CatalogueFactory.CreateFlags(item.Bargain, item.Discount, item.IsInStock, item.New, item.Special);
            var productManager = CompanyGroupCms.CatalogueFactory.CreateProductManager(item.ProductManager.Email, item.ProductManager.Extension, item.ProductManager.Mobile, item.ProductManager.Name);
            var primaryPicture = CompanyGroupCms.CatalogueFactory.CreatePicture(item.PrimaryPicture.FileName, item.PrimaryPicture.Primary, item.PrimaryPicture.RecId);
            var line = CompanyGroupCms.ShoppingCartFactory.CreateLine(item.Currency, item.CustomerPrice, item.DataAreaId, flags, garanty, item.Id, item.IsInStock, item.ItemState, item.Language, item.PartNumber, [], primaryPicture, item.ProductId, productManager, item.PurchaseInProgress, 1, item.ShippingDate, stock);
            self.lines.push(line);
        });
    };
    self.initStoredCarts = function (storedCarts) {
        //console.log(storedCarts);
        $.each(storedCarts, function (index, item) {
            var storedCart = CompanyGroupCms.ShoppingCartFactory.CreateStoredCart(item.Id, item.Name, item.Active);
            self.storedCarts.push(storedCart);
        });
    };
    self.initOpenedCarts = function (openedCarts) {
        //console.log(openedCarts);
        $.each(openedCarts, function (index, item) {
            var openedCart = CompanyGroupCms.ShoppingCartFactory.CreateOpenedCart(item.Id, item.Name, item.Active);
            self.openedCarts.push(openedCart);
        });
    };
    //sor hozzáadás
    self.addLine = function (product) {
        console.log(product);
        /*currency, customerPrice, dataAreaId, flags, garanty, id, isInStock, itemState, language, partNumber, pictures, picture, productId, productManager, purchaseInProgress, quantity, shippingDate, stock
        self.manufacturer = CompanyGroupCms.CatalogueFactory.CreateManufacturer('', '');
        self.firstLevelCategory = CompanyGroupCms.CatalogueFactory.CreateFirstLevelCategory('', '');
        self.secondLevelCategory = CompanyGroupCms.CatalogueFactory.CreateSecondLevelCategory('', '');
        self.thirdLevelCategory = CompanyGroupCms.CatalogueFactory.CreateThirdLevelCategory('','');
        self.productId = ko.observable('');
        self.partNumber = ko.observable('');
        self.itemName = ko.observable('');
        self.stock = CompanyGroupCms.CatalogueFactory.CreateStock(0,0,0,0);
        self.price = ko.observable(0);
        self.currency = ko.observable('');
        self.shippingDate = ko.observable('');
        self.itemState = ko.observable(0);
        self.flags = CompanyGroupCms.CatalogueFactory.CreateFlags(false, false, false, false, false);
        self.primaryPictureRecId = ko.observable(0);
        self.description = ko.observable('');
        self.garanty = CompanyGroupCms.CatalogueFactory.CreateGaranty('', '');
        self.productManager = CompanyGroupCms.CatalogueFactory.CreateProductManager('', '', '', '');
        self.dataAreaId = ko.observable('');
        self.isInStock = ko.observable(false);
        self.isInCart = ko.observable(false);
        self.sequenceNumber = ko.observable(0);
        self.purchaseInProgress = ko.observable(false);
        self.shippingDate = ko.observable('');        
        */
        var line = CompanyGroupCms.ShoppingCartFactory.CreateLine(product.currency, product.price, product.dataAreaId, product.flags, product.garanty, '', product.isInStock, product.itemState, 'hu', product.partNumber, [], {}, product.productId, product.productManager, product.purchaseInProgress, 1, product.shippingDate, product.stock);
        CompanyGroupCms.ShoppingCartLineRequest.CartId = $('#saved_shoppingcart_list').val(); //self.id();
        CompanyGroupCms.ShoppingCartLineRequest.ProductId = product.productId();
        CompanyGroupCms.ShoppingCartLineRequest.Quantity = product.quantity();

        var dataString = ko.toJSON(CompanyGroupCms.ShoppingCartLineRequest);
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
                    //self.lines.push(line);
                    self.lines.removeAll();
                    self.id(result.Id);
                    self.initLines(result.Items);

                    $.floatingMessage('<span style="font-family: verdana; font-size: 13px; color:#fff;"> A kiválasztott termék:<br /><strong>' + product.productId() + '</strong><br />bekerült a kosárba.</span>', {
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
    self.updateLine = function (line) {
        //alert(ko.toJSON(line));
        console.log(line);
        CompanyGroupCms.ShoppingCartLineRequest.CartId = $('#saved_shoppingcart_list').val();
        CompanyGroupCms.ShoppingCartLineRequest.ProductId = line.productId();
        CompanyGroupCms.ShoppingCartLineRequest.Quantity = line.quantity();
        var dataString = ko.toJSON(CompanyGroupCms.ShoppingCartLineRequest);
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
                    self.lines.removeAll();
                    self.id(result.Id);
                    self.initLines(result.Items);
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
    self.removeLine = function (line) {
        //self.lines.remove(line)
        //alert(ko.toJSON(line));
        CompanyGroupCms.ShoppingCartLineRequest.CartId = $('#saved_shoppingcart_list').val(); //line.id();
        CompanyGroupCms.ShoppingCartLineRequest.ProductId = line.productId();
        CompanyGroupCms.ShoppingCartLineRequest.Quantity = line.quantity();
        var dataString = ko.toJSON(CompanyGroupCms.ShoppingCartLineRequest);
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
                    self.lines.removeAll();
                    self.id(result.Id);
                    self.initLines(result.Items);
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
    self.saveCart = function (cartName) {
        if (self.storedCarts().length == 0 && self.openedCarts().length == 0) {
            alert('Nincs menthető kosár!');
        }
        CompanyGroupCms.SaveShoppingCartRequest.CartId = $('#saved_shoppingcart_list').val();
        CompanyGroupCms.SaveShoppingCartRequest.Name = cartName;
        var dataString = ko.toJSON(CompanyGroupCms.SaveShoppingCartRequest);
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
                    self.openedCarts.removeAll();
                    self.initOpenedCarts(result.OpenedItems);
                    self.storedCarts.removeAll();
                    self.initStoredCarts(result.StoredItems);
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
    self.addCart = function () {
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
                    self.lines.removeAll();
                    self.id(result.Id);
                    self.initLines([]);

                    self.openedCarts.removeAll();
                    self.initOpenedCarts(result.OpenedItems);
                    self.storedCarts.removeAll();
                    self.initStoredCarts(result.StoredItems);

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
    self.removeCart = function () {
        if (self.storedCarts().length == 0 && self.openedCarts().length == 0) {
            alert('Nincs törölhető kosár!');
        }
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
                    self.lines.removeAll();
                    self.id(result.ShoppingCart.Id);
                    self.initLines(result.ShoppingCart.Items);

                    self.openedCarts.removeAll();
                    self.initOpenedCarts(result.OpenedItems);
                    self.storedCarts.removeAll();
                    self.initStoredCarts(result.StoredItems);
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

    // ActivateCart([Bind(Prefix = "")] Cms.CommonCore.Models.Request.ActivateCart request
    self.activateCart = function (cartId) {
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
                    self.lines.removeAll();
                    self.id(result.Id);
                    self.initLines(result.Items);
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

    self.saveShoppingCartOpenStatus = function () {
        var isOpen = !self.isShoppingCartOpen();
        CompanyGroupCms.OpenStatusRequest.IsOpen = isOpen;
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('SaveShoppingCartOpenStatus'),
            data: ko.toJSON(CompanyGroupCms.OpenStatusRequest),
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                $("#basket_panel").slideToggle("fast");
                $("#active_basket").toggleClass("active");
            },
            error: function () {
                alert('saveShoppingCartOpenStatus call failed');
            }
        });
    };
    self.saveCatalogueOpenStatus = function () {
        CompanyGroupCms.OpenStatusRequest.IsOpen = self.isCatalogueOpen();
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getShoppingCartServiceUrl('SaveCatalogueOpenStatus'),
            data: ko.toJSON(CompanyGroupCms.OpenStatusRequest),
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
};
//kosár listaelem entitás
CompanyGroupCms.ShoppingCartLine = function () {
    var self = this;
    self.id = ko.observable('');
    self.productId = ko.observable('');
    self.partNumber = ko.observable('');
    self.customerPrice = ko.observable(0);
    self.currency = ko.observable('');
    self.pictures = ko.observableArray([]);     //new CompanyGroupCms.Picture()
    self.primaryPicture = ko.observable();      //new CompanyGroupCms.Picture()
    self.flags = ko.observable();               //new CompanyGroupCms.Flags();
    self.stock = ko.observable();               //new CompanyGroupCms.Stock();
    self.itemState = ko.observable(0);
    self.shippingDate = ko.observable('');
    self.language = ko.observable('');
    self.dataAreaId = ko.observable('');
    self.garanty = ko.observable();            //new CompanyGroupCms.Garanty();
    self.productManager = ko.observable();     //new CompanyGroupCms.ProductManager();
    self.isInStock = ko.observable(false);
    self.purchaseInProgress = ko.observable(false);
    self.quantity = ko.observable(0);
    self.itemTotal = ko.computed(function () {
        return self.customerPrice() > 0 ? self.customerPrice() * parseInt("0" + self.quantity(), 10) : 0;
    });
};
//mentett kosár entitás
CompanyGroupCms.StoredShoppingCart = function (id, name, active) {
    var self = this;
    self.id = ko.observable(id);
    self.name = ko.observable(name);
    self.active = ko.observable(active);
};
//nyitott kosár entitás
CompanyGroupCms.OpenedShoppingCart = function (id, name, active) {
    var self = this;
    self.id = ko.observable(id);
    self.name = ko.observable(name);
    self.active = ko.observable(active);
};
//gyárak
CompanyGroupCms.ShoppingCartFactory = (function () {
    var createLine = function (currency, customerPrice, dataAreaId, flags, garanty, id, isInStock, itemState, language, partNumber, pictures, picture, productId, productManager, purchaseInProgress, quantity, shippingDate, stock) {
        var line = new CompanyGroupCms.ShoppingCartLine();
        line.currency(currency);
        line.customerPrice(customerPrice);
        line.dataAreaId(dataAreaId);
        line.flags(flags);
        line.garanty(garanty);
        line.id(id);
        line.isInStock(isInStock);
        line.itemState(itemState);
        line.language(language);
        line.partNumber(partNumber);
        line.pictures(pictures);
        line.primaryPicture(picture);
        line.productId(productId);
        line.productManager(productManager);
        line.purchaseInProgress(purchaseInProgress);
        line.quantity(quantity);
        line.shippingDate(shippingDate);
        line.stock(stock);
        return line;
    };
    var createCart = function (id, lines, storedCarts, openedCarts, isCatalogueOpen, isShoppingCartOpen) {
        var cart = new CompanyGroupCms.ShoppingCart();
        cart.id(id);
        cart.lines([]);
        //cart.addLine(line);
        cart.initLines(lines);
        cart.initStoredCarts(storedCarts);
        cart.initOpenedCarts(openedCarts);
        cart.isCatalogueOpen(isCatalogueOpen);
        cart.isShoppingCartOpen(isShoppingCartOpen);
        if (isShoppingCartOpen) {
            $("#basket_panel").slideToggle("fast");
            $("#active_basket").toggleClass("active");
        }
        return cart;
    };
    var createStoredCart = function (id, name, active) {
        return new CompanyGroupCms.StoredShoppingCart(id, name, active);
    };
    var createOpenedCart = function (id, name, active) {
        return new CompanyGroupCms.OpenedShoppingCart(id, name, active);
    };
    return {
        CreateLine: createLine,
        CreateCart: createCart,
        CreateStoredCart: createStoredCart,
        CreateOpenedCart: createOpenedCart
    };
})();

/*
var flags = CompanyGroupCms.ShoppingCartFactory.CreateFlags(false, false, false, false, false);

var garanty = CompanyGroupCms.ShoppingCartFactory.CreateGaranty('pick_up_and_return', '1901.01.01');

var pictures = [];

var picture = CompanyGroupCms.ShoppingCartFactory.CreatePicture('thumbnail.jpg', true, 1000649);

var productManager = CompanyGroupCms.ShoppingCartFactory.CreateProductManager('juaqaai@inf.elte.hu', '457', '30 20 36 448', 'Attila Juhasz');

var stock = CompanyGroupCms.ShoppingCartFactory.CreateStock(0, 200, 0, 2);

var line = CompanyGroupCms.ShoppingCartFactory.CreateLine('HUF', 10638, 'bsc', flags, garanty, '452435435645646465fdgdgeg356535', false, 0, 'hu', 'ecs148', pictures, picture, 'p623456', productManager, false, 3, '1900.01.01', stock);

var vm = CompanyGroupCms.ShoppingCartFactory.CreateCart('wert12334', line);

//alert(ko.toJSON(vm));

ko.applyBindings(vm);
*/