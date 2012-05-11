var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.ProductList = function () {
    var self = this;
    self.items = ko.observableArray([]);
    self.listCount = ko.observable(0);
    self.pager = ko.observable();
    self.itemCount = ko.computed(function () {
        return self.items.length;
    });

    self.addProduct = function (product) {
        self.items.push(product);
    };

    self.removeProduct = function (product) {
        self.items.remove(product);
        //alert(ko.toJSON(line));
    };

};

CompanyGroupCms.Pager = function () {
    var self = this;
    self.firstEnabled = ko.observable(false);
    self.lastEnabled = ko.observable(false);
    self.previousEnabled = ko.observable(false);
    self.nextEnabled = ko.observable(false);
    self.lastPageIndex = ko.observable(0);
    self.pageItemList = ko.observableArray([]);

    self.addPage = function (page) {
        self.pageItemList.push(page);
    };
}

CompanyGroupCms.Page = function () {
    var self = this;
    self.selected = ko.observable(false);
    self.index = ko.observable(0);
    self.value = ko.observable('');
}

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

    self.pictureUrl = ko.computed(function () {
        return '\'' + CompanyGroupCms.Constants.Instance().ServiceBaseUrl + CompanyGroupCms.Constants.Instance().PictureServiceUrl + self.primaryPicture.recId + '/' + self.productId() + '/hrp/94/69' + '\'';
    });

    self.productDetailsUrl = ko.computed(function () {
        return CompanyGroupCms.Constants.Instance().getWebshopBaseUrl() + self.productId() + '/Details';
    });

    self.description = ko.observable('');
    self.garanty = ko.observable();
    self.productManager = ko.observable();
    self.dataAreaId = ko.observable('');
    self.isInStock = ko.observable(false);
    self.isInCart = ko.observable(false);
    self.sequenceNumber = ko.observable(0);
    self.purchaseInProgress = ko.observable(false);
    self.shippingDate = ko.observable('');
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
    var createManufacturer = function (id, name) {
        var manufacturer = new CompanyGroupCms.Manufacturer();
        manufacturer.id(id);
        manufacturer.name(name);
        return manufacturer;
    };
    var createFirstLevelCategory = function (id, name) {
        var category1 = new CompanyGroupCms.FirstLevelCategory();
        category1.id(id);
        category1.name(name);
        return category1;
    };
    var createSecondLevelCategory = function (id, name) {
        var category2 = new CompanyGroupCms.SecondLevelCategory();
        category2.id(id);
        category2.name(name);
        return category2;
    };
    var createThirdLevelCategory = function (id, name) {
        var category3 = new CompanyGroupCms.ThirdLevelCategory();
        category3.id(id);
        category3.name(name);
        return category3;
    };
    var createPage = function (selected, index, value) {
        var page = new CompanyGroupCms.Page();
        page.selected(selected);
        page.index(index);
        page.value(value);
        return page;
    };
    var createPager = function (firstEnabled, lastEnabled, previousEnabled, nextEnabled, lastPageIndex, pageItemList) {

        var pager = new CompanyGroupCms.Pager();
        pager.firstEnabled(firstEnabled);
        pager.lastEnabled(lastEnabled);
        pager.previousEnabled(previousEnabled);
        pager.nextEnabled(nextEnabled);
        pager.lastPageIndex(lastPageIndex);
        pager.pageItemList([]);
        $.each(pageItemList, function (index, page) {
            //alert(page.Selected + ' ; ' + page.Index + ' ; ' + page.Value);
            var p = createPage(page.Selected, page.Index, page.Value);
            pager.addPage(p);
            //alert(p.selected() + ' ; ' + p.index() + ' ; ' + p.value());
        });
        return pager;
    };
    return {
        CreateFlags: createFlags,
        CreateGaranty: createGaranty,
        CreatePicture: createPicture,
        CreateProductManager: createProductManager,
        CreateStock: createStock,
        CreateManufacturer: createManufacturer,
        CreateFirstLevelCategory: createFirstLevelCategory,
        CreateSecondLevelCategory: createSecondLevelCategory,
        CreateThirdLevelCategory: createThirdLevelCategory,
        CreateProduct: function (currency, dataAreaId, description, category1, category2, category3, flags, garanty, isInCart, isInStock, itemName, itemState, manufacturer, partNumber, pictures, picture, price, productId, productManager, purchaseInProgress, sequenceNumber, shippingDate, stock) {
            var product = new CompanyGroupCms.Product();
            product.currency(currency);
            product.dataAreaId(dataAreaId);
            product.description(description);
            product.firstLevelCategory(category1);
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
        CreateProductList: function (items, listCount, pager) {
            var list = new CompanyGroupCms.ProductList();
            list.items(items);
            list.listCount(listCount);
            list.pager(pager);
//            alert(pager.pageItemList.length);
//            for (var i = 0; i <= pager.pageItemList.length; i++) {
//                console.log(pager.pageItemList[i].selected + ' ; ' + pager.pageItemList[i].index + ' ; ' + pager.pageItemList[i].value);
//            }
            return list;
        },
        CreatePage: createPage,
        CreatePager: createPager
    };
})();

