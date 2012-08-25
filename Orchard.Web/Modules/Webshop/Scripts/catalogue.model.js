var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Catalogue = (function () {
    var self = this;

    var showPicture = function (productId, dataAreaId, productName) {
        var arr_pics = new Array();
        var data = new Object();
        data.ProductId = productId;
        data.DataAreaId = dataAreaId;
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
                        item.href = CompanyGroupCms.Constants.Instance().getBigPictureUrl(productId, pic.RecId, dataAreaId);
                        item.title = productId;
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
                                    return '<a href="' + CompanyGroupCms.Constants.Instance().getProductDetailsUrl(productId) + '"><span id="fancybox-title-over"> ' + (currentIndex + 1) + ' / ' + currentArray.length + (title.length ? '&nbsp; ' + title + '&nbsp;&nbsp;' + productName + '&nbsp;' : '') + '</span></a>';
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
    var firstPage = function () {
        var currentPageIndex = CompanyGroupCms.CatalogueListRequest.CurrentPageIndex;
        if (currentPageIndex > 1) {
            CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
            loadCatalogue();
        }
    };
    var lastPage = function () {
        var currentPageIndex = CompanyGroupCms.CatalogueListRequest.CurrentPageIndex;
        var lastPageIndex = $("#spanTopLastPageIndex").text();
        if (currentPageIndex < (lastPageIndex)) {
            CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = lastPageIndex;
            loadCatalogue();
        }
    };
    var nextPage = function () {
        var currentPageIndex = CompanyGroupCms.CatalogueListRequest.CurrentPageIndex;
        var lastPageIndex = $("#spanTopLastPageIndex").text();
        if (currentPageIndex < (lastPageIndex)) {
            currentPageIndex = currentPageIndex + 1;
            CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = currentPageIndex;
            loadCatalogue();
        }
    };
    var previousPage = function () {
        var currentPageIndex = CompanyGroupCms.CatalogueListRequest.CurrentPageIndex;
        if (currentPageIndex > 1) {
            currentPageIndex = currentPageIndex - 1;
            CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = currentPageIndex;
            loadCatalogue();
        }
    };
    var visibleItemListChanged = function (value) {
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        if (value === 'top') {
            CompanyGroupCms.CatalogueListRequest.ItemsOnPage = parseInt($("#select_visibleitemlist_top").val(), 0);
        }
        else {
            CompanyGroupCms.CatalogueListRequest.ItemsOnPage = parseInt($("#select_visibleitemlist_bottom").val(), 0);
        }
        loadCatalogue();
    };
    var selectedPageIndexChanged = function (value) {
        if (value === 'top') {
            CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = parseInt($("#select_pageindex_top").val(), 0);
        }
        else {
            CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = parseInt($("#select_pageindex_bottom").val(), 0);
        }
        loadCatalogue();
    };
    var sequenceByPriceUp = function () {
        CompanyGroupCms.CatalogueListRequest.Sequence = 6;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByPriceDown = function () {
        CompanyGroupCms.CatalogueListRequest.Sequence = 7;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByPartNumberUp = function () {
        //console.log(self);
        CompanyGroupCms.CatalogueListRequest.Sequence = 2;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByPartNumberDown = function () {
        //console.log(self);
        CompanyGroupCms.CatalogueListRequest.Sequence = 3;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByNameUp = function () {
        //console.log(this);
        CompanyGroupCms.CatalogueListRequest.Sequence = 4;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByNameDown = function () {
        //console.log(this);
        CompanyGroupCms.CatalogueListRequest.Sequence = 5;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByStockUp = function () {
        CompanyGroupCms.CatalogueListRequest.Sequence = 8;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByStockDown = function () {
        CompanyGroupCms.CatalogueListRequest.Sequence = 9;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByGarantyUp = function () {
        CompanyGroupCms.CatalogueListRequest.Sequence = 10;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByGarantyDown = function () {
        CompanyGroupCms.CatalogueListRequest.Sequence = 11;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var filterByStock = function (value) {
        CompanyGroupCms.CatalogueListRequest.StockFilter = value;
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var filterByAction = function (value) {
        CompanyGroupCms.CatalogueListRequest.ActionFilter = value;
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var filterByBargain = function (value) {
        CompanyGroupCms.CatalogueListRequest.BargainFilter = value;
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var filterByNew = function (value) {
        CompanyGroupCms.CatalogueListRequest.NewFilter = value;
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var filterByHrp = function () {
        CompanyGroupCms.CatalogueListRequest.Clear();
        CompanyGroupCms.CatalogueListRequest.HrpFilter = true;
        CompanyGroupCms.CatalogueListRequest.BscFilter = false;
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var filterByCategoryHrp = function (value) {
        CompanyGroupCms.CatalogueListRequest.Clear();
        CompanyGroupCms.CatalogueListRequest.Category1IdList.push(value);
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var filterByBsc = function () {
        CompanyGroupCms.CatalogueListRequest.Clear();
        CompanyGroupCms.CatalogueListRequest.HrpFilter = false;
        CompanyGroupCms.CatalogueListRequest.BscFilter = true;
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var filterByCategoryBsc = function (value) {
        CompanyGroupCms.CatalogueListRequest.Clear();
        CompanyGroupCms.CatalogueListRequest.Category1IdList.push(value);
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var clearFilters = function () {
        CompanyGroupCms.CatalogueListRequest.Clear();
        loadCatalogue();
        loadStructure(true, true, true, true);
    }
    var loadCatalogue = function () {
        var dataString = $.toJSON(CompanyGroupCms.CatalogueListRequest);
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
                    //console.log(result);
                    $("#div_pager_top").empty();
                    $("#pagerTemplateTop").tmpl(result.Products).appendTo("#div_pager_top");
                    $("#div_pager_bottom").empty();
                    $("#pagerTemplateBottom").tmpl(result.Products).appendTo("#div_pager_bottom");
                    $("#div_catalogue").empty();
                    $("#productTemplate").tmpl(result).appendTo("#div_catalogue");
                    $('.number').spin();
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
    var downloadPriceList = function () {
        console.log('downloadPriceList');
        window.location = CompanyGroupCms.Constants.Instance().getDownloadPriceListServiceUrl() + '?' + $.param(CompanyGroupCms.CatalogueListRequest);
        //        var dataString = $.toJSON(CompanyGroupCms.CatalogueListRequest);
        //        $.ajax({
        //            type: "POST",
        //            url: CompanyGroupCms.Constants.Instance().getDownloadPriceListServiceUrl(),
        //            data: dataString,
        //            contentType: "application/json; charset=utf-8",
        //            timeout: 1000000,
        //            dataType: "json",
        //            processData: false,
        //            success: function (result) {
        //                alert('DownloadPriceList call completed!');
        //            },
        //            error: function (status) {

        //                console.log(status);

        //            }
        //        });
    };
    var loadStructure = function (loadManufacturer, loadCategory1, loadCategory2, loadCategory3) {
        var dataString = $.toJSON(CompanyGroupCms.CatalogueListRequest);
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
    return {
        DownloadPriceList: downloadPriceList,
        LoadStructure: loadStructure,
        LoadCatalogue: loadCatalogue,
        ShowPicture: showPicture,
        FirstPage: firstPage,
        LastPage: lastPage,
        NextPage: nextPage,
        PreviousPage: previousPage,
        VisibleItemListChanged: visibleItemListChanged,
        SelectedPageIndexChanged: selectedPageIndexChanged,
        SequenceByPriceUp: sequenceByPriceUp,
        SequenceByPriceDown: sequenceByPriceDown,
        SequenceByPartNumberUp: sequenceByPartNumberUp,
        SequenceByPartNumberDown: sequenceByPartNumberDown,
        SequenceByNameUp: sequenceByNameUp,
        SequenceByNameDown: sequenceByNameDown,
        SequenceByStockUp: sequenceByStockUp,
        SequenceByStockDown: sequenceByStockDown,
        SequenceByGarantyUp: sequenceByGarantyUp,
        SequenceByGarantyDown: sequenceByGarantyDown,
        FilterByStock: filterByStock,
        FilterByAction: filterByAction,
        FilterByBargain: filterByBargain,
        FilterByNew: filterByNew,
        FilterByHrp: filterByHrp,
        FilterByBsc: filterByBsc,
        FilterByCategoryHrp: filterByCategoryHrp,
        FilterByCategoryBsc: filterByCategoryBsc,
        ClearFilters: clearFilters
    };
})();

//CompanyGroupCms.Catalogue.LoadStructure(true, true, true, true);
//nts.Instance().getPictureUrl(
//CompanyGroupCms.ConCompanyGroupCms.Constastants.Instance().getProductDetailsUrl(



