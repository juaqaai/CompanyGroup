//application namespace
window.CompanyGroup = Ember.Application.create({
    ready: function () {
        this._super();
        alert('You did it!');
    }
});

//definig model 
CompanyGroup.VisitorInfo = Ember.Object.extend({
    companyName: null,
    personName: null,
    isValidLogin: false,
    isWebAdministrator: false,
    invoiceInfoEnabled: false,
    priceListDownloadEnabled: false,
    canOrder: false,
    recieveGoods: false
//    create: function (item) {
//        title = item.title;
//        author = item.author;
//        genre = item.genre;
//    }
});

CompanyGroup.authorizationController = Ember.ArrayController.create({
    content: [],
    visitorInfo: function () {
        var self = this;
//        $.getJSON('books.json', function (data) {
//            data.forEach(function (item) {
//                self.pushObject(App.Book.create(item));
//            });
//        });

        var data = new Object();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getPartnerInfoServiceUrl() + 'VisitorInfo',
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    self.set('isValidLogin', result.IsValidLogin);
                    self.set('companyName', result.CompanyName);
                    self.set('personName', result.PersonName);
                    self.set('isWebAdministrator', result.Permission.IsWebAdministrator);
                    self.set('invoiceInfoEnabled', result.Permission.InvoiceInfoEnabled);
                    self.set('priceListDownloadEnabled', result.Permission.PriceListDownloadEnabled);
                    self.set('canOrder', result.Permission.CanOrder);
                    self.set('recieveGoods', result.Permission.RecieveGoods);
                }
                else {
                    alert('authorizationData result failed');
                }
            },
            complete: function(xhr) {
	            if(xhr.status == 400 || xhr.status == 420) {
	                me.set('limit', true);
	            }
	        },
            error: function () {
                alert('authorizationData call failed');
            }
        });
    }
});

CompanyGroup.VisitorInfoView = Ember.View.extend({
    mouseDown: function () {
        window.alert("hello world!");
    }
});
