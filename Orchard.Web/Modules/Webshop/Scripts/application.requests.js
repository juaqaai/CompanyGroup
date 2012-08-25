var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.GetCartByKeyRequest = (function () {

    var _cartd = '';

    return {
        Cartd: _cartd
    };

})();

CompanyGroupCms.CatalogueListRequest = (function () {
    //    var _manufacturerIdList = [];
    //    var _category1IdList = [];
    //    var _category2IdList = [];
    //    var _category3IdList = [];
    //    var _actionFilter = false;
    //    var _bargainFilter = false;
    //    var _newFilter = false;
    //    var _stockFilter = false;
    //    var _textFilter = '';
    //    var _hrpFilter = true;
    //    var _bscFilter = true;
    //    var _sequence = 0;
    //    var _currentPageIndex = 0;
    //    var _itemsOnPage = 30;
    //    listStatusOpen = false
    //    listItemStatusOpen = false
    //    listItem = ''
    //var self = this;
    var clear = function () {
        this.ManufacturerIdList = [];
        this.Category1IdList = [];
        this.Category2IdList = [];
        this.Category3IdList = [];
        this.ActionFilter = false;
        this.BargainFilter = false;
        this.NewFilter = false;
        this.StockFilter = false;
        this.TextFilter = '';
        this.HrpFilter = true;
        this.BscFilter = true;
        this.PriceFilter = '0';
        this.PriceFilterRelation = '0';
        this.NameOrPartNumberFilter = '';
        this.Sequence = 0;
        this.CurrentPageIndex = 1;
        this.ItemsOnPage = 30;
    };
    return {
        ManufacturerIdList: [],
        Category1IdList: [],
        Category2IdList: [],
        Category3IdList: [],
        ActionFilter: false,
        BargainFilter: false,
        NewFilter: false,
        StockFilter: false,
        TextFilter: '',
        HrpFilter: true,
        BscFilter: true,
        PriceFilter: '0',
        PriceFilterRelation: '0',
        NameOrPartNumberFilter: '',
        Sequence: 0,
        CurrentPageIndex: 1,
        ItemsOnPage: 30,
        Clear: clear
    };
})();

CompanyGroupCms.StructureListRequest = (function () {
    var _manufacturerIdList = '';
    var _category1IdList = '';
    var _category2IdList = '';
    var _category3IdList = '';
    var _actionFilter = false;
    var _bargainFilter = false;
    var _newFilter = false;
    var _stockFilter = false;
    var _textFilter = '';
    var _hrpFilter = '';
    var _bscFilter = '';
    return {
        ManufacturerIdList: _manufacturerIdList,
        Category1IdList: _category1IdList,
        Category2IdList: _category2IdList,
        Category3IdList: _category3IdList,
        ActionFilter: _actionFilter,
        BargainFilter: _bargainFilter,
        NewFilter: _newFilter,
        StockFilter: _stockFilter,
        TextFilter: _textFilter, 
        HrpFilter: _hrpFilter,
        BscFilter: _bscFilter
    }
})();

CompanyGroupCms.ShoppingCartLineRequest = (function () {

    var _cartId = '';
    var _productId = '';
    var _quantity = 0;
    return {
        CartId: _cartId,
        ProductId: _productId,
        Quantity: _quantity        
    }
})();

CompanyGroupCms.SaveShoppingCartRequest = (function () {

    var _cartId = '';
    var _name = '';
    return {
        CartId: _cartId,
        Name: _name
    }
})();

CompanyGroupCms.OpenStatusRequest = (function () {

    var _isOpen = false;
    return {
        IsOpen: _isOpen
    }
})();