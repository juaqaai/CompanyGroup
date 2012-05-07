var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.ShoppingCart = function () {

    var self = this;
    self.id = ko.observable('');
    self.lines = ko.observableArray([]);    //new CompanyGroupCms.ShoppingCartLine()

    self.sumTotal = ko.computed(function () {
        var total = 0;
        $.each(self.lines(), function () { total += this.itemTotal() })
        return total;
    });

    self.addLine = function (line, serviceUrl) {

        self.lines.push(line);

//        var dataString = ko.toJSON(line);
//        $.ajax({
//            type: "POST",
//            url: serviceUrl,
//            data: dataString,
//            contentType: "application/json; charset=utf-8",
//            timeout: 10000,
//            dataType: "json",
//            processData: true,
//            success: function (result) {
//                if (result) {
//                    if (result) {

//                        self.lines.push(line);
//                    }
//                    else {
//                        alert('Nincs eleme a listának.');
//                    }
//                }
//                else {
//                    alert('addLine result failed');
//                }
//            },
//            error: function () {
//                alert('addLine call failed');
//            }
//        });
    };

    self.updateLine = function (line) {
        //line.quantity(8);
        //alert(ko.toJSON(line));
    };

    self.removeLine = function (line) {
        //self.lines.remove(line)
        alert(ko.toJSON(line));
    };

    self.addCart = function (cartName, serviceUrl) {
        var data = new Object();
        data.Name = cartName;
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
                    if (result) {

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
    };

    self.removeCart = function (cartId, serviceUrl) {   
        var data = new Object();
        data.CartId = cartId;
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
                    if (result) {

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
    };
};

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
    var createCart = function (id, line) {
        var cart = new CompanyGroupCms.ShoppingCart();
        cart.id(id);
        cart.lines([]);
        cart.addLine(line);
        return cart;
    };
    return {
        CreateLine: createLine,
        CreateCart: createCart
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