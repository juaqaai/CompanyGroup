var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Constants = (function () {

    var _webshopBaseUrl = '/cms/Webshop/Catalogue/';

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

//CompanyGroupCms.Constants.Instance().ServiceBaseUrl;