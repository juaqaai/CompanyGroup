var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Constants = (function () {

    var _webshopBaseUrl = '/cms/Webshop/Catalogue/';
    var _structureServiceUrl = '';
    var _productListServiceUrl = '';
    var _pictureListServiceUrl = '';
    var _customerServiceUrl = '';
    var _partnerInfoServiceUrl = '';  
    var _shoppingCartServiceBaseUrl = '';

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
                _partnerInfoServiceUrl = partnerInfoServiceUrl;
            },
            getShoppingCartServiceBaseUrl: function () {
                return _shoppingCartServiceUrl;
            },
            setShoppingCartServiceBaseUrl: function (shoppingCartServiceUrl) {
                _shoppingCartServiceBaseUrl = shoppingCartServiceUrl;
            },
            getShoppingCartServiceUrl: function (url) {
                return _shoppingCartServiceBaseUrl + url;
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