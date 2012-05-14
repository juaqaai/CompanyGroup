var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.ProductList = function (items) {
    var self = this;

    self.items = ko.observableArray(ko.utils.arrayMap(items, function (item) {
        var stock = CompanyGroupCms.CatalogueFactory.CreateStock(item.InnerStock, item.OuterStock, 0, 0);
        var garanty = CompanyGroupCms.CatalogueFactory.CreateGaranty(item.GarantyMode, item.GarantyTime);
        var flags = CompanyGroupCms.CatalogueFactory.CreateFlags(item.Bargain, item.Discount, item.IsInStock, item.New, item.Special);
        var category1 = CompanyGroupCms.CatalogueFactory.CreateFirstLevelCategory(item.FirstLevelCategory.Id, item.FirstLevelCategory.Name);
        var category2 = CompanyGroupCms.CatalogueFactory.CreateSecondLevelCategory(item.SecondLevelCategory.Id, item.SecondLevelCategory.Name);
        var category3 = CompanyGroupCms.CatalogueFactory.CreateThirdLevelCategory(item.ThirdLevelCategory.Id, item.ThirdLevelCategory.Name);
        var manufacturer = CompanyGroupCms.CatalogueFactory.CreateManufacturer(item.Manufacturer.Id, item.Manufacturer.Name);
        var productManager = CompanyGroupCms.CatalogueFactory.CreateProductManager(item.ProductManager.Email, item.ProductManager.Extension, item.ProductManager.Mobile, item.ProductManager.Name);
        return CompanyGroupCms.CatalogueFactory.CreateProduct(item.Currency, item.DataAreaId, item.Description, category1, category2, category3, flags, garanty, item.IsInCart, item.IsInStock,
                                                            item.ItemName, item.ItemState, manufacturer, item.PartNumber, item.PrimaryPicture.RecId, item.Price, item.ProductId, productManager, item.PurchaseInProgress, item.SequenceNumber, item.ShippingDate, stock);
    }));

    self.removeAllItems = function () {
        self.items.removeAll();
    };

    //self.items = ko.observableArray([]);
    self.listCount = ko.observable(0);
    self.itemCount = ko.computed(function () {
        return self.items.length;
    });

    self.addProduct = function (product) {
        self.items.push(product);
    };

    self.firstPageEnabled = ko.observable(false);
    self.lastPageEnabled = ko.observable(false);
    self.previousPageEnabled = ko.observable(false);
    self.nextPageEnabled = ko.observable(false);
    self.lastPageIndex = ko.observable(0);
    self.pageItemList = ko.observableArray([]);
    //    self.pageItemList = ko.observableArray(ko.utils.arrayMap(pageItems, function(page) {
    //        return { selected: page.Selected, index: page.Index, value: page.Value };
    //    }));

    self.addPage = function (page) {
        self.pageItemList.push(page);
        //console.log(newPage);
    };

    self.removeAllPages = function () {
        self.pageItemList.removeAll();
    };

    self.showPicture = function (product) {
        var arr_pics = new Array();
        var data = new Object();
        data.ProductId = product.productId();
        data.DataAreaId = product.dataAreaId();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getPictureListServiceUrl(),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 15000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result.Items.length > 0) {
                    $.each(result.Items, function (i, pic) {
                        var item = new Object();
                        item.href = CompanyGroupCms.Constants.Instance().getWebshopBaseUrl() + data.ProductId + '/' + pic.RecId + '/' + data.DataAreaId + '/500/500/Picture';
                        item.title = data.ProductId;
                        arr_pics.push(item);
                        $.fancybox(
                            arr_pics,
                            {
                                'padding': 0,
                                'transitionIn': 'elastic',
                                'transitionOut': 'elastic',
                                'type': 'image',
                                'changeFade': 0,
                                'speedIn': 300,
                                'speedOut': 300,
                                'width': '150%',
                                'height': '150%',
                                'autoScale': true,
                                'titlePosition': 'inside',
                                'titleFormat': function (title, currentArray, currentIndex, currentOpts) {
                                    return '<a href="' + product.productDetailsUrl() + '"><span id="fancybox-title-over"> ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? '&nbsp; ' + title + '&nbsp;&nbsp;' + product.itemName() + '&nbsp;' : '') + '</span></a>';
                                }
                            });
                    });
                }

            },
            error: function () {
                alert('Service call failed: GetListByProduct');
            }
        });
    };
    self.firstPage = function () {
        var currentPageIndex = catalogueListRequest.CurrentPageIndex();
        if (currentPageIndex != 0) {
            catalogueListRequest.CurrentPageIndex(0);
            self.loadCatalogueList();
        }
    };
    self.lastPage = function () {
        var currentPageIndex = catalogueListRequest.CurrentPageIndex();
        var lastPageIndex = $("#spanTopLastPageIndex").text();
        if (currentPageIndex < (lastPageIndex - 1)) {
            catalogueListRequest.CurrentPageIndex(lastPageIndex - 1);
            self.loadCatalogueList();
        }
    };
    self.nextPage = function () {
        var currentPageIndex = catalogueListRequest.CurrentPageIndex();
        var lastPageIndex = $("#spanTopLastPageIndex").text();
        if (currentPageIndex < (lastPageIndex - 1)) {
            currentPageIndex = currentPageIndex + 1;
            catalogueListRequest.CurrentPageIndex(currentPageIndex);
            self.loadCatalogueList();
        }
    };
    self.previousPage = function () {
        var currentPageIndex = catalogueListRequest.CurrentPageIndex();
        if (currentPageIndex > 0) {
            currentPageIndex = currentPageIndex - 1;
            catalogueListRequest.CurrentPageIndex(currentPageIndex);
            self.loadCatalogueList();
        }
    };
    self.loadCatalogueList = function () {
        var dataString = ko.toJSON(catalogueListRequest);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getProductListServiceUrl(),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    if (result.Items.length > 0) {
                        self.removeAllItems();
                        $.each(result.Items, function (index, item) {
                            var stock = CompanyGroupCms.CatalogueFactory.CreateStock(item.InnerStock, item.OuterStock, 0, 0);
                            var garanty = CompanyGroupCms.CatalogueFactory.CreateGaranty(item.GarantyMode, item.GarantyTime);
                            var flags = CompanyGroupCms.CatalogueFactory.CreateFlags(item.Bargain, item.Discount, item.IsInStock, item.New, item.Special);
                            var category1 = CompanyGroupCms.CatalogueFactory.CreateFirstLevelCategory(item.FirstLevelCategory.Id, item.FirstLevelCategory.Name);
                            var category2 = CompanyGroupCms.CatalogueFactory.CreateSecondLevelCategory(item.SecondLevelCategory.Id, item.SecondLevelCategory.Name);
                            var category3 = CompanyGroupCms.CatalogueFactory.CreateThirdLevelCategory(item.ThirdLevelCategory.Id, item.ThirdLevelCategory.Name);
                            var manufacturer = CompanyGroupCms.CatalogueFactory.CreateManufacturer(item.Manufacturer.Id, item.Manufacturer.Name);
                            var productManager = CompanyGroupCms.CatalogueFactory.CreateProductManager(item.ProductManager.Email, item.ProductManager.Extension, item.ProductManager.Mobile, item.ProductManager.Name);
                            var product = CompanyGroupCms.CatalogueFactory.CreateProduct(item.Currency, item.DataAreaId, item.Description, category1, category2, category3, flags, garanty, item.IsInCart, item.IsInStock,
                                                                                       item.ItemName, item.ItemState, manufacturer, item.PartNumber, item.PrimaryPicture.RecId, item.Price, item.ProductId, productManager,
                                                                                       item.PurchaseInProgress, item.SequenceNumber, item.ShippingDate, stock);
                            self.addProduct(product);
                        });
                        self.listCount(result.ListCount);

                        self.removeAllPages();
                        $.each(result.Pager.PageItemList, function (index, page) {
                            var p = CompanyGroupCms.CatalogueFactory.CreatePage(page.Selected, page.Index, page.Value);
                            self.addPage(p);
                        });
                        self.firstPageEnabled(result.Pager.FirstEnabled);
                        self.lastPageEnabled(result.Pager.LastEnabled);
                        self.previousPageEnabled(result.Pager.PreviousEnabled);
                        self.nextPageEnabled(result.Pager.NextEnabled);
                        self.lastPageIndex(result.Pager.LastPageIndex);
                    }
                    else {
                        alert('Nincs eleme a listának.');
                    }
                }
                else {
                    alert('loadCatalogueList result failed');
                }
            },
            error: function () {
                alert('loadCatalogueList call failed');
            }
        });
    };

    this.loadStructure = function (loadManufacturer, loadCategory1, loadCategory2, loadCategory3) {
        var dataString = ko.toJSON(catalogueListRequest);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getStructureServiceUrl(),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    if (loadManufacturer) {
                        var manufacturer = $('#manufacturerList').val()
                        $("#manufacturerList").empty();
                        $.each(result.Manufacturers, function (key, value) {
                            var option = $('<option>').text(value.Name).val(value.Id);
                            $("#manufacturerList").append(option);
                        });
                        if (manufacturer != '') {
                            $("#manufacturerList").val(manufacturer);
                        }
                        $("#manufacturerList").trigger("liszt:updated");
                    }
                    if (loadCategory1) {
                        var selectList = $("#category1List");
                        var categories = selectList.val();
                        selectList.empty();
                        $.each(result.FirstLevelCategories, function (key, value) {
                            var option = $('<option>').text(value.Name).val(value.Id);
                            selectList.append(option);
                        });
                        if (categories != '') {
                            selectList.val(categories);
                        }
                        $("#category1List").trigger("liszt:updated");
                    }
                    if (loadCategory2) {
                        var selectList = $("#category2List");
                        var categories = selectList.val();
                        selectList.empty();
                        $.each(result.SecondLevelCategories, function (key, value) {
                            var option = $('<option>').text(value.Name).val(value.Id);
                            selectList.append(option);
                        });
                        if (categories != '') {
                            selectList.val(categories);
                        }
                        $("#category2List").trigger("liszt:updated");
                    }
                    if (loadCategory3) {
                        var selectList = $("#category3List");
                        var categories = selectList.val();
                        selectList.empty();
                        $.each(result.ThirdLevelCategories, function (key, value) {
                            var option = $('<option>').text(value.Name).val(value.Id);
                            selectList.append(option);
                        });
                        if (categories != '') {
                            selectList.val(categories);
                        }
                        $("#category3List").trigger("liszt:updated");
                    }
                }
                else {
                    alert('LoadStructure call failed!');
                }
            },
            error: function () {
                alert('LoadStructure call failed!');
            }
        });
    };
};

CompanyGroupCms.Page = function () {
    var self = this;
    self.selected = ko.observable(false);
    self.index = ko.observable(0);
    self.value = ko.observable('');
};

CompanyGroupCms.Product = function () {
    var self = this;
    self.manufacturer = CompanyGroupCms.CatalogueFactory.CreateManufacturer('', '');
    self.firstLevelCategory = CompanyGroupCms.CatalogueFactory.CreateFirstLevelCategory('', '');
    self.secondLevelCategory = CompanyGroupCms.CatalogueFactory.CreateSecondLevelCategory('', '');
    self.thirdLevelCategory = CompanyGroupCms.CatalogueFactory.CreateThirdLevelCategory('','');
    self.productId = ko.observable('');
    self.partNumber = ko.observable('');
    self.itemName = ko.observable('');
    self.stock = CompanyGroupCms.CatalogueFactory.CreateStock(0,0,0,0);
    self.price = ko.observable(0);
    self.currency = ko.observable('');
    self.shippingDate = ko.observable('');
    self.itemState = ko.observable(0);
    self.flags = CompanyGroupCms.CatalogueFactory.CreateFlags(false, false, false, false, false);
    self.primaryPictureRecId = ko.observable(0);
    self.description = ko.observable('');
    self.garanty = CompanyGroupCms.CatalogueFactory.CreateGaranty('', '');
    self.productManager = CompanyGroupCms.CatalogueFactory.CreateProductManager('', '', '', '');
    self.dataAreaId = ko.observable('');
    self.isInStock = ko.observable(false);
    self.isInCart = ko.observable(false);
    self.sequenceNumber = ko.observable(0);
    self.purchaseInProgress = ko.observable(false);
    self.shippingDate = ko.observable('');

    self.pictureExist = ko.computed(function () {
        return self.primaryPictureRecId != 0;
    });
    self.pictureUrl = ko.computed(function () {
        return CompanyGroupCms.Constants.Instance().getWebshopBaseUrl() + self.productId() + '/' + self.primaryPictureRecId() + '/' + self.dataAreaId() + '/94/69/Picture';
    });
    self.productDetailsUrl = ko.computed(function () {
        return CompanyGroupCms.Constants.Instance().getWebshopBaseUrl() + self.productId() + '/Details';
    });

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

CompanyGroupCms.CatalogueListRequest = function () {
    var self = this;
    self.ManufacturerIdList = ko.observable('');
    self.Category1IdList = ko.observable('');
    self.Category2IdList = ko.observable('');
    self.Category3IdList = ko.observable('');
    self.ActionFilter = ko.observable(false);
    self.BargainFilter = ko.observable(false);
    self.NewFilter = ko.observable(false);
    self.StockFilter = ko.observable(false);
    self.TextFilter = ko.observable('');
    self.Sequence = ko.observable(0);
    self.CurrentPageIndex = ko.observable(0);
    self.ItemsOnPage = ko.observable(30);
    //    self.listStatusOpen = ko.observable(false);
    //    self.listItemStatusOpen = ko.observable(false);
    //    self.listItem = ko.observable('');
};

CompanyGroupCms.StructureListRequest = function () {
    var self = this;
    self.ManufacturerIdList = ko.observable('');
    self.Category1IdList = ko.observable('');
    self.Category2IdList = ko.observable('');
    self.Category3IdList = ko.observable('');
    self.ActionFilter = ko.observable(false);
    self.BargainFilter = ko.observable(false);
    self.NewFilter = ko.observable(false);
    self.StockFilter = ko.observable(false);
    self.TextFilter = ko.observable('');
};

CompanyGroupCms.CatalogueFactory = (function () {
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
        CreateProduct: function (currency, dataAreaId, description, category1, category2, category3, flags, garanty, isInCart, isInStock, itemName, itemState, manufacturer, partNumber, primaryPictureRecId, price, productId, productManager, purchaseInProgress, sequenceNumber, shippingDate, stock) {
            var product = new CompanyGroupCms.Product();
            product.currency(currency);
            product.dataAreaId(dataAreaId);
            product.description(description);
            product.firstLevelCategory.id(category1.id);
            product.firstLevelCategory.name(category1.name);
            product.flags.bargain(flags.bargain);
            product.flags.discount(flags.discount);
            product.flags.inStock(flags.inStock);
            product.flags.newItem(flags.newItem);
            product.flags.special(flags.special);
            product.garanty.mode(garanty.mode);
            product.garanty.time(garanty.time);
            product.isInCart(isInCart);
            product.isInStock(isInStock);
            product.itemName(itemName);
            product.itemState(itemState);
            product.manufacturer.id(manufacturer.id);
            product.manufacturer.name(manufacturer.name);
            product.partNumber(partNumber);
            product.primaryPictureRecId(primaryPictureRecId);
            product.price(price);
            product.productId(productId);
            product.productManager.email(productManager.email);
            product.productManager.extension(productManager.extension);
            product.productManager.mobile(productManager.mobile);
            product.productManager.name(productManager.name);
            product.purchaseInProgress(purchaseInProgress);
            product.secondLevelCategory.id(category2.id);
            product.secondLevelCategory.name(category2.name);
            product.sequenceNumber(sequenceNumber);
            product.shippingDate(shippingDate);
            product.stock.inner(stock.inner);
            product.stock.outer(stock.outer);
            product.stock.secondHand(stock.secondHand);
            product.stock.serbian(stock.serbian);
            product.thirdLevelCategory.id(category3.id);
            product.thirdLevelCategory.name(category3.name);
            return product;
        },
        CreateProductList: function (items, listCount, firstPageEnabled, lastPageEnabled, previousPageEnabled, nextPageEnabled, lastPageIndex) {
            var list = new CompanyGroupCms.ProductList(items);
            //list.items([]);
            list.listCount(listCount);
            list.firstPageEnabled(firstPageEnabled);
            list.lastPageEnabled(lastPageEnabled);
            list.previousPageEnabled(previousPageEnabled);
            list.nextPageEnabled(nextPageEnabled);
            list.lastPageIndex(lastPageIndex);
            list.pageItemList([]);
            //            alert(pager.pageItemList.length);
            //            for (var i = 0; i <= pager.pageItemList.length; i++) {
            //                console.log(pager.pageItemList[i].selected + ' ; ' + pager.pageItemList[i].index + ' ; ' + pager.pageItemList[i].value);
            //            }
            return list;
        },
        CreatePage: createPage,
        CreateCatalogueListRequest: function () {
            return new CompanyGroupCms.CatalogueListRequest();
        }
    };
})();

