var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Catalogue = (function () {
    var self = this;
    //cikkhez tartozó képlista elkérése
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
    /*
    /// 0: átlagos életkor csökkenő, akciós csökkenő, gyártó növekvő, termékazonosító szerint növekvőleg,
    /// 1: átlagos életkor növekvő, akciós csökkenő, gyártó növekvő, termékazonosító szerint növekvőleg, 
    /// 2: azonosito növekvő, (cikkszám)
    /// 3: azonosito csökkenő, (cikkszám)
    /// 4 nev növekvő,
    /// 5: nev csökkenő,
    /// 6: ar növekvő,
    /// 7: ar csökkenő, 
    /// 8: belső készlet növekvően, 
    /// 9: belső készlet csökkenően
    /// 10: külső készlet növekvően
    /// 11: külső készlet csökkenően
    /// 12: garancia növekvően
    /// 13: garancia csökkenő    
    */
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
        CompanyGroupCms.CatalogueListRequest.Sequence = 12;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
    };
    var sequenceByGarantyDown = function () {
        CompanyGroupCms.CatalogueListRequest.Sequence = 13;
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
        $('#category1List').val(value);
        $("#category1List").trigger("liszt:updated");
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
        $('#category1List').val(value);
        $("#category1List").trigger("liszt:updated");
    };
    var clearFilters = function () {
        CompanyGroupCms.CatalogueListRequest.Clear();
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var showSecondHandList = function (productId, dataAreaId) {
        alert('Ide jön a ' + productId + '-hoz kapcsolt használtcikk lista,');
    };
    var searchByTextFilter = function (value) {
        CompanyGroupCms.CatalogueListRequest.TextFilter = value;
        CompanyGroupCms.CatalogueListRequest.CurrentPageIndex = 1;
        loadCatalogue();
        loadStructure(true, true, true, true);
    };
    var initAutoCompletionBaseProduct = function () {
        $("#txt_filterbynameorpartnumber").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "GET",
                    url: CompanyGroupCms.Constants.Instance().getCompletionListBaseProductServiceUrl(),
                    data: { Prefix: request.term },
                    contentType: "application/json; charset=utf-8",
                    timeout: 10000,
                    dataType: "json",
                    processData: true,
                    success: function (result) {
                        if (result) {
                            var resultObject = result.Items;
                            var suggestions = [];
                            $.each(resultObject, function (i, val) {
                                suggestions.push(val);
                            });
                            response(suggestions);
                        }
                        else {
                            console.log(result);
                        }
                    },
                    error: function () {
                        console.log('CompletionListServiceUrl failed');
                    }
                });
            },
            minLength: 2
        }).data("autocomplete")._renderItem = function (ul, item) {
            console.log(item);
            var inner_html = '<div class="list_item_container"><div class="image"><img src="' + CompanyGroupCms.Constants.Instance().getThumbnailPictureUrl(item.ProductId, item.RecId, item.DataAreaId) + ' alt=\"\" /></div><div class="label">' + item.ProductId + '</div><div class="description">' + item.ProductName + '</div></div>';
            return $("<li></li>")
            .data("item.autocomplete", item)
            .append(inner_html)
            .appendTo(ul);
        };
    };
    var initAutoCompletionAllProduct = function () {
        $("#txt_globalsearch").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "GET",
                    url: CompanyGroupCms.Constants.Instance().getCompletionListAllProductServiceUrl(),
                    data: { Prefix: request.term },
                    contentType: "application/json; charset=utf-8",
                    timeout: 10000,
                    dataType: "json",
                    processData: true,
                    success: function (result) {
                        if (result) {
                            var resultObject = result.Items;
                            var suggestions = [];
                            $.each(resultObject, function (i, val) {
                                suggestions.push(val);
                            });
                            response(suggestions);
                        }
                        else {
                            console.log(result);
                        }
                    },
                    error: function () {
                        console.log('CompletionListServiceUrl failed');
                    }
                });
            },
            minLength: 2
            //            //define select handler
            //            select: function (e, ui) {
            //                //create formatted friend
            //                var friend = ui.item.value,
            //							span = $("<span>").text(friend),
            //							a = $("<a>").addClass("remove").attr({
            //							    href: "javascript:",
            //							    title: "Remove " + friend
            //							}).text("x").appendTo(span);
            //                //add friend to friend div
            //							span.insertBefore("#txtSearch");
            //            },
            //            //define select handler
            //            change: function () {
            //                //prevent 'to' field being updated and correct position
            //                $("#txtSearch").val("").css("top", 2);
            //            }
            /*
            .data("autocomplete")._renderItem = function (ul, item) {
            var inner_html = '<a><div class="list_item_container"><div class="image"><img src="' + item.image + '"></div><div class="label">' + item.label + '</div><div class="description">' + item.description + '</div></div></a>';
            return $("<li></li>")
            .data("item.autocomplete", item)
            .append(inner_html)
            .appendTo(ul);
            };            
            */
        })
        .data("autocomplete")._renderItem = function (ul, item) {
            console.log(item);
            var inner_html = '<div class="list_item_container"><div class="image"><img src="' + CompanyGroupCms.Constants.Instance().getThumbnailPictureUrl(item.ProductId, item.RecId, item.DataAreaId) + ' alt=\"\" /></div><div class="label">' + item.ProductId + '</div><div class="description">' + item.ProductName + '</div></div>';
            return $("<li></li>")
            .data("item.autocomplete", item)
            .append(inner_html)
            .appendTo(ul);
        };

    };

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
        ClearFilters: clearFilters,
        ShowSecondHandList: showSecondHandList,
        SearchByTextFilter: searchByTextFilter,
        InitAutoCompletionAllProduct: initAutoCompletionAllProduct,
        InitAutoCompletionBaseProduct: initAutoCompletionBaseProduct
    };
})();

//CompanyGroupCms.Catalogue.LoadStructure(true, true, true, true);
//CompanyGroupCms.ConCompanyGroupCms.Constastants.Instance().getProductDetailsUrl(



