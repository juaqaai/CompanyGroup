var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.GetCartByKeyRequest = (function () {

    var _cartd = '';

    return {
        Cartd: _cartd
    };

})();

CompanyGroupCms.CatalogueListRequest = (function () {
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
    //    listStatusOpen = false
    //    listItemStatusOpen = false
    //    listItem = ''
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
        Sequence: _sequence,
        CurrentPageIndex: _currentPageIndex,
        ItemsOnPage: _itemsOnPage
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
    return {
        ManufacturerIdList: _manufacturerIdList,
        Category1IdList: _category1IdList,
        Category2IdList: _category2IdList,
        Category3IdList: _category3IdList,
        ActionFilter: _actionFilter,
        BargainFilter: _bargainFilter,
        NewFilter: _newFilter,
        StockFilter: _stockFilter,
        TextFilter: _textFilter
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