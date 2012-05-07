var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.ProductList = function () {
    var self = this;
    self.products = ko.observableArray([]);
    self.listCount = ko.computed(function () {
        return self.products.length;
    });

    self.addProduct = function (product, serviceUrl) {
        self.products.push(product);
    };

    self.removeProduct = function (product) {
        self.products.remove(product);
        //alert(ko.toJSON(line));
    };

};
CompanyGroupCms.Product = function () {
    var self = this;
    self.manufacturer = ko.observable();
    self.firstLevelCategory = ko.observable();
    self.secondLevelCategory = ko.observable();
    self.thirdLevelCategory = ko.observable();
    self.productId = ko.observable('');
    self.partNumber = ko.observable('');
    self.itemName = ko.observable('');
    self.stock = ko.observable();
    self.price = ko.observable(0);
    self.currency = ko.observable('');
    self.shippingDate = ko.observable('');
    self.itemState = ko.observable(0);
    self.flags = ko.observable();
    self.pictures = ko.observableArray([]);     
    self.primaryPicture = ko.observable();
    self.description = ko.observable('');
    self.garanty = ko.observable();            
    self.productManager = ko.observable();     
    self.dataAreaId = ko.observable('');
    self.isInStock = ko.observable(false);
    self.isInCart = ko.observable(false);
    self.sequenceNumber = ko.observable(0);
    self.purchaseInProgress = ko.observable(false);
};

CompanyGroupCms.Picture = function () {
    var self = this;
    self.fileName = ko.observable('');
    self.primary = ko.observable(false);
    self.recId = ko.observable(0);
};

CompanyGroupCms.ProductManager = function () {
    var self = this;
    self.email = ko.observable('');
    self.extension = ko.observable('');
    self.mobile = ko.observable('');
    self.name = ko.observable('');
};

CompanyGroupCms.Flags = function () {

    var self = this;
    self.bargain = ko.observable(false);
    self.discount = ko.observable(false);
    self.inStock = ko.observable(false);
    self.newItem = ko.observable(false);
    self.special = ko.observable(false);
};

CompanyGroupCms.Stock = function () {
    var self = this;
    self.inner = ko.observable(0);
    self.outer = ko.observable(0);
    self.secondHand = ko.observable(0);
    self.serbian = ko.observable(0);
};

CompanyGroupCms.Garanty = function () {
    var self = this;
    self.mode = ko.observable('');
    self.time = ko.observable('');
};

CompanyGroupCms.Manufacturer = function () {
    var self = this;
    self.id = ko.observable('');
    self.name = ko.observable('');
};

CompanyGroupCms.FirstLevelCategory = function () {
    var self = this;
    self.id = ko.observable('');
    self.name = ko.observable('');
};

CompanyGroupCms.SecondLevelCategory = function () {
    var self = this;
    self.id = ko.observable('');
    self.name = ko.observable('');
};

CompanyGroupCms.ThirdLevelCategory = function () {
    var self = this;
    self.id = ko.observable('');
    self.name = ko.observable('');
};

CompanyGroupCms.ProductFactory = (function () {
    var createFlags = function (bargain, discount, inStock, newItem, special) {
        var flags = new CompanyGroupCms.Flags();
        flags.bargain(bargain);
        flags.discount(discount);
        flags.inStock(inStock);
        flags.newItem(newItem);
        flags.special(special);
        return flags;
    };
    var createGaranty = function (mode, time) {
        var garanty = new CompanyGroupCms.Garanty();
        garanty.mode(mode);
        garanty.time(time);
        return garanty;
    };
    var createPicture = function (fileName, primary, recId) {
        var picture = new CompanyGroupCms.Picture();
        picture.fileName(fileName);
        picture.primary(primary);
        picture.recId(recId);
        return picture;
    };
    var createProductManager = function (email, extension, mobile, name) {
        var productManager = new CompanyGroupCms.ProductManager();
        productManager.email(email);
        productManager.extension(extension);
        productManager.mobile(mobile);
        productManager.name(name);
        return productManager;
    };
    var createStock = function (inner, outer, secondHand, serbian) {
        var stock = new CompanyGroupCms.Stock();
        stock.inner(inner);
        stock.outer(outer);
        stock.secondHand(secondHand);
        stock.serbian(serbian);
        return stock;
    };
    return {
        CreateFlags: createFlags,
        CreateGaranty: createGaranty,
        CreatePicture: createPicture,
        CreateProductManager: createProductManager,
        CreateStock: createStock,
        CreateProduct: function (currency, dataAreaId, description, category1, category2, category3, flags, garanty, isInCart, isInStock, itemName, itemState, manufacturer, partNumber, pictures, picture, price, productId, productManager, purchaseInProgress, sequenceNumber, shippingDate, stock) {
            var product = new CompanyGroupCms.Product();
            product.currency(currency);
            product.dataAreaId(dataAreaId);
            product.description(description);
            product.firstLevelCategory = category1;
            product.flags(flags);
            product.garanty(garanty);
            product.isInCart(isInCart);
            product.isInStock(isInStock);
            product.itemName(itemName);
            product.itemState(itemState);
            product.manufacturer(manufacturer);
            product.partNumber(partNumber);
            product.pictures(pictures);
            product.primaryPicture(picture);
            product.price(price);
            product.productId(productId);
            product.productManager(productManager);
            product.purchaseInProgress(purchaseInProgress);
            product.secondLevelCategory(category2);
            product.sequenceNumber(sequenceNumber);
            product.shippingDate(shippingDate);
            product.stock(stock);
            product.thirdLevelCategory(category3);
            return product;
        },
        CreateProductList: function (id, line) {
            var list = new CompanyGroupCms.ProductList();
            list.products([]);
            return list;
        }
    };
})();
