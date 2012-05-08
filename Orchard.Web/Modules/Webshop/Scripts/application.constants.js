var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Constants = (function () {

    var instance;

    function create() {
        return {
            ServiceBaseUrl: 'http://1juhasza/CompanyGroup.ServicesHost/',
            PictureServiceUrl: 'PictureService.svc/GetItem/'
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