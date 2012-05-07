var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Models = CompanyGroupCms.Models || {};

//CompanyGroup.Webshop.CatalogueFilter
function CatalogueFilter() {

    var _manufacturerIdList = '';
    var _category1IdList = '';
    var _category2IdList = '';
    var _category3IdList = '';
    var _actionFilter = false;
    var _bargainFilter = false;
    var _newFilter = false;
    var _stockFilter = false;
    var _textFilter = '';
    var _sequence = 0;
    var _currentPageIndex = 0;
    var _itemsOnPage = 30;
    var _listStatusOpen = false;
    var _listItemStatusOpen = false;
    var _listItem = '';

    this.getManufacturerIdList = function () {
        return _manufacturerIdList;
    }

    this.setManufacturerIdList = function (manufacturerIdList) {
        _manufacturerIdList = manufacturerIdList;
    }

    this.getCategory1IdList = function () {
        return _category1IdList;
    }

    this.setCategory1IdList = function (category1IdList) {
        _category1IdList = category1IdList;
    }

    this.getCategory2IdList = function () {
        return _category2IdList;
    }

    this.setCategory2IdList = function (category2IdList) {
        _category2IdList = category2IdList;
    }

    this.getCategory3IdList = function () {
        return _category3IdList;
    }

    this.setCategory3IdList = function (category3IdList) {
        _category3IdList = category3IdList;
    }

    this.getActionFilter = function () {
        return _actionFilter;
    }

    this.setActionFilter = function (actionFilter) {
        _actionFilter = actionFilter;
    }

    this.getBargainFilter = function () {
        return _bargainFilter;
    }

    this.setBargainFilter = function (bargainFilter) {
        _bargainFilter = bargainFilter;
    }

    this.getNewFilter = function () {
        return _newFilter;
    }

    this.setNewFilter = function (newFilter) {
        _newFilter = newFilter;
    }

    this.getStockFilter = function () {
        return _stockFilter;
    }

    this.setStockFilter = function (stockFilter) {
        _stockFilter = stockFilter;
    }

    this.getTextFilter = function () {
        return _textFilter;
    }

    this.setTextFilter = function (textFilter) {
        _textFilter = textFilter;
    }

    this.getSequence = function () {
        return _sequence;
    }

    this.setSequence = function (sequence) {
        _sequence = sequence;
    }

    this.getCurrentPageIndex = function () {
        return _currentPageIndex;
    }

    this.setCurrentPageIndex = function (currentPageIndex) {
        _currentPageIndex = currentPageIndex;
    }

    this.getItemsOnPage = function () {
        return _itemsOnPage;
    }

    this.setItemsOnPage = function (itemsOnPage) {
        _itemsOnPage = itemsOnPage;
    }

    this.getListStatusOpen = function () {
        return _listStatusOpen;
    }

    this.setListStatusOpen = function (listStatusOpen) {
        _listStatusOpen = listStatusOpen;
    }

    this.getListItemStatusOpen = function () {
        return _listItemStatusOpen;
    }

    this.setListItemStatusOpen = function (listItemStatusOpen) {
        _listItemStatusOpen = listItemStatusOpen;
    }

    this.getListItem = function () {
        return _listItem;
    }

    this.setListItem = function (listItem) {
        _listItem = listItem;
    }
}

function ShoppingCart() {

    var _id = '';
    var _sumTotal = 0;
    var _items = [];

    this.getId = function () {
        return _id;
    }
    this.setId = function (id) {
        _id = id;
    }

    this.getSumTotal = function () {
        return _sumTotal;
    }
    this.setSumTotal = function (value) {
        _sumTotal = value;
    }

    this.getItems = function () {
        return _items;
    }
    this.setItems = function (value) {
        _items = value;
    }
}

function ShoppingCartItem() {

    var _id = '';
    var _productId = '';
    var _partNumber = '';
    var _customerPrice = 0;
    var _currency = 0;
    var _pictures = [];
    var _primaryPicture = null;
    var _flags = new Flags();
    var _stock = new Stock();
    var _itemState = 0;
    var _shippingDate = '';
    var _language = '';
    var _dataAreaId = '';
    var _garanty = new Garanty();
    var _productManager = new ProductManager();
    var _quantity = 0;
    var _itemTotal = 0;
    var _isInStock = false;
    var _purchaseInProgress = false;

    this.getId = function () {
        return _id;
    }
    this.setId = function (value) {
        _id = value;
    }

    this.getProductId = function () {
        return _productId;
    }
    this.setProductId = function (value) {
        _productId = value;
    }

    this.getPartNumber = function () {
        return _partNumber;
    }
    this.setPartNumber = function (value) {
        _partNumber = value;
    }

    this.getCustomerPrice = function () {
        return _customerPrice;
    }
    this.setCustomerPrice = function (value) {
        _customerPrice = value;
    }

    this.getCurrency = function () {
        return _currency;
    }
    this.setCurrency = function (value) {
        _currency = value;
    }

    this.getPictures = function () {
        return _pictures;
    }
    this.setPictures = function (value) {
        _pictures = value;
    }

    this.getPrimaryPicture = function () {
        return _primaryPicture;
    }
    this.setPrimaryPicture = function (value) {
        _primaryPicture = value;
    }

    this.getFlags = function () {
        return _flags;
    }
    this.setFlags = function (value) {
        _flags = value;
    }

    this.getStock = function () {
        return _stock;
    }
    this.setStock = function (value) {
        _stock = value;
    }

    this.getItemState = function () {
        return _itemState;
    }
    this.setItemState = function (value) {
        _itemState = value;
    }

    this.getShippingDate = function () {
        return _shippingDate;
    }
    this.setShippingDate = function (value) {
        _shippingDate = value;
    }

    this.getLanguage = function () {
        return _language;
    }
    this.setLanguage = function (value) {
        _language = value;
    }

    this.getDataAreaId = function () {
        return _dataAreaId;
    }
    this.setDataAreaId = function (value) {
        _dataAreaId = value;
    }

    this.getGaranty = function () {
        return _garanty;
    }
    this.setGaranty = function (value) {
        _garanty = value;
    }
    this.getProductManager = function () {
        return _productManager;
    }
    this.setProductManager = function (value) {
        _productManager = value;
    }

    this.getQuantity = function () {
        return _quantity;
    }
    this.setQuantity = function (value) {
        _quantity = value;
    }

    this.getItemTotal = function () {
        return _itemTotal;
    }
    this.setItemTotal = function (value) {
        _itemTotal = value;
    }

    this.getIsInStock = function () {
        return _isInStock;
    }
    this.setIsInStock = function (value) {
        _isInStock = value;
    }

    this.getPurchaseInProgress = function () {
        return _purchaseInProgress;
    }
    this.setPurchaseInProgress = function (value) {
        _purchaseInProgress = value;
    }
}

function Flags() {
    var _bargain = false;
    var _discount = false;
    var _inStock = false;
    var _new = false;
    var _special = false;

    this.getBargain = function () {
        return _bargain;
    }
    this.setBargain = function (value) {
        _bargain = value;
    }

    this.getDiscount = function () {
        return _discount;
    }
    this.setDiscount = function (value) {
        _discount = value;
    }
    this.getInStock = function () {
        return _inStock;
    }
    this.setInStock = function (value) {
        _inStock = value;
    }
    this.getNew = function () {
        return _new;
    }
    this.setNew = function (value) {
        _new = value;
    }
    this.getSpecial = function () {
        return _special;
    }
    this.setSpecial = function (value) {
        _special = value;
    }
}

function Stock() { 
    var _inner = 0;
    var _outer = 0;
    var _secondHand = 0;
    var _serbian = 0;

    this.getInner = function () {
        return _inner;
    }
    this.setInner = function (value) {
        _inner = value;
    }
    this.getOuter = function () {
        return _outer;
    }
    this.setOuter = function (value) {
        _outer = value;
    }
    this.getSecondHand = function () {
        return _secondHand;
    }
    this.setSecondHand = function (value) {
        _secondHand = value;
    }
    this.getSerbian= function () {
        return _serbian;
    }
    this.setSerbian = function (value) {
        _serbian = value;
    }
}

function Garanty() {
    var _mode = '';
    var _time = '';

    this.getMode = function () {
        return _mode;
    }
    this.setMode = function (value) {
        _mode = value;
    }
    this.getTime = function () {
        return _time;
    }
    this.setTime = function (value) {
        _time = value;
    }
}

function ProductManager() {
    var _email = '';
    var _extension = '';
    var _mobile = '';
    var _name = '';

    this.setEmail = function (value) {
        _email = value;
    }
    this.getEmail = function () {
        return _email;
    }
    this.setExtension = function (value) {
        _extension = value;
    }
    this.getExtension = function () {
        return _extension;
    }
    this.setMobile = function (value) {
        _mobile = value;
    }
    this.getMobile = function () {
        return _mobile;
    }
    this.setName = function (value) {
        _name = value;
    }
    this.getName = function () {
        return _name;
    }
}

var newShoppingCartRequest = (function(language, name, serviceUrl, visitorId) {
    var _language = language;
    var _name = name;
    var _serviceUrl = serviceUrl;
    var _visitorId = visitorId;

    return {
        getLanguage : function () {
            return _language;
        },
        setLanguage : function (value) {
            _language = value;
        },
        getName : function () {
            return _name;
        },
        setName : function (value) {
            _name = value;
        },
        getServiceUrl : function () {
            return _serviceUrl;
        },
        setServiceUrl : function (value) {
            _serviceUrl = value;
        },
        getVisitorId : function () {
            return _visitorId;
        },
        setVisitorId : function (value) {
            _visitorId = value;
        }
    };
}());

var deleteShoppingCartRequest = (function(cartId, language, serviceUrl, visitorId) {
    var _language = language;
    var _cartId = cartId;
    var _serviceUrl = serviceUrl;
    var _visitorId = visitorId;
    return {
        getCartId : function () {
            return _cartId;
        },
        setCartId : function (value) {
            _cartId = value;
        },
        getLanguage : function () {
            return _language;
        },
        setLanguage : function (value) {
            _language = value;
        },
        getServiceUrl : function () {
            return _serviceUrl;
        },
        setServiceUrl : function (value) {
            _serviceUrl = value;
        },
        getVisitorId : function () {
            return _visitorId;
        },
        setVisitorId : function (value) {
            _visitorId = value;
        }
    };
}());

var addShoppingCartItemRequest = (function (productId, cartId, quantity, serviceUrl) {
    var _productId = productId;
    var _cartId = cartId;
    var _quantity = quantity;
    var _serviceUrl = serviceUrl;
    return {
        getProductId: function () {
            return _productId;
        },
        setProductId : function (value) {
            _productId = value;
        },
        getQuantity: function () {
            return _quantity;
        },
        setQuantity: function (value) {
            _quantity = value;
        },
        getCartId: function () {
            return _cartId;
        },
        setCartId: function (value) {
            _cartId = value;
        },
        getServiceUrl: function () {
            return _serviceUrl;
        },
        setServiceUrl: function (value) {
            _serviceUrl = value;
        }
    };
} ());
