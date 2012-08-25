var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Constants = (function () {

    var _webshopBaseUrl = '/cms/Webshop/Catalogue/';
    var _structureServiceUrl = '';
    var _productListServiceUrl = '';
    var _pictureListServiceUrl = '';
    var _customerServiceUrl = '';
    var _partnerInfoServiceUrl = '';
    var _shoppingCartServiceBaseUrl = '';
    var _registrationServiceUrl = '';
    var _signInServiceUrl = '';
    var _signOutServiceUrl = '';
    var _downloadPriceListServiceUrl = '';
    var _module = '';

    var instance;

    function create() {
        return {
            ServiceBaseUrl: 'http://1juhasza/CompanyGroup.ServicesHost/',
            PictureServiceUrl: 'PictureService.svc/GetItem/',
            getWebshopBaseUrl: function () {
                return _webshopBaseUrl;
            },
            setWebshopBaseUrl: function (webshopBaseUrl) {
                _webshopBaseUrl = webshopBaseUrl;
            },
            getStructureServiceUrl: function () {
                return _structureServiceUrl;
            },
            setStructureServiceUrl: function (structureServiceUrl) {
                _structureServiceUrl = structureServiceUrl;
            },
            getProductListServiceUrl: function () {
                return _productListServiceUrl;
            },
            setProductListServiceUrl: function (productListServiceUrl) {
                _productListServiceUrl = productListServiceUrl;
            },
            getPictureListServiceUrl: function () {
                return _pictureListServiceUrl;
            },
            setPictureListServiceUrl: function (pictureListServiceUrl) {
                _pictureListServiceUrl = pictureListServiceUrl;
            },
            getCustomerServiceUrl: function () {
                return _customerServiceUrl;
            },
            setCustomerServiceUrl: function (customerServiceUrl) {
                _customerServiceUrl = customerServiceUrl;
            },
            getPartnerInfoServiceUrl: function () {
                return _partnerInfoServiceUrl;
            },
            setPartnerInfoServiceUrl: function (partnerInfoServiceUrl) {
                if (_partnerInfoServiceUrl === '') {
                    _partnerInfoServiceUrl = partnerInfoServiceUrl;
                }
            },
            getShoppingCartServiceBaseUrl: function () {
                return _shoppingCartServiceUrl;
            },
            setShoppingCartServiceBaseUrl: function (shoppingCartServiceUrl) {
                _shoppingCartServiceBaseUrl = shoppingCartServiceUrl;
            },
            getShoppingCartServiceUrl: function (url) {
                return _shoppingCartServiceBaseUrl + url;
            },
            getProductDetailsUrl: function (productId) {
                return _webshopBaseUrl + productId + '/Details';
            },
            getPictureUrl: function (productId, recId, dataAreaId) {
                return _webshopBaseUrl + productId + '/' + recId + '/' + dataAreaId + '/94/69/Picture';
            },
            getBigPictureUrl: function (productId, recId, dataAreaId) {
                return _webshopBaseUrl + '/PictureItem/?ProductId=' + productId + '&RecId=' + recId + '&DataAreaId=' + dataAreaId + '&MaxWidth=500&MaxHeight=500';
            },
            getRegistrationServiceUrl: function () {
                return _registrationServiceUrl;
            },
            setRegistrationServiceUrl: function (registrationServiceUrl) {
                _registrationServiceUrl = registrationServiceUrl;
            },
            getSignInServiceUrl: function () {
                return _signInServiceUrl;
            },
            setSignInServiceUrl: function (signInServiceUrl) {
                if (_signInServiceUrl === '') {
                    _signInServiceUrl = signInServiceUrl;
                }
            },
            getSignOutServiceUrl: function () {
                return _signOutServiceUrl;
            },
            setSignOutServiceUrl: function (signOutServiceUrl) {
                if (_signOutServiceUrl === '') {
                    _signOutServiceUrl = signOutServiceUrl;
                }
            },
            getDownloadPriceListServiceUrl: function () {
                return _downloadPriceListServiceUrl;
            },
            setDownloadPriceListServiceUrl: function (downloadPriceListServiceUrl) {
                _downloadPriceListServiceUrl = downloadPriceListServiceUrl;
            },
            getModule: function () {
                return _module;
            },
            setModule: function (module) {
                if (_module === '') {
                    _module = module;
                }
            },
            isEqualModule: function (module) {
                return (_module === module);
            }
        }
    }
    return {
        Instance: function () {
            if (!instance) {
                instance = create();
            }
            return instance;
        }
    }
})();

// CompanyGroupCms.Constants.Instance().setWebshopBaseUrl;