   function CatalogueService() {

       this.loadStructure = function (loadManufacturer, loadCategory1, loadCategory2, loadCategory3, serviceUrl) {
           var data = new Object();
           data.ManufacturerIdList = catalogueFilter.getManufacturerIdList();
           data.Category1IdList = catalogueFilter.getCategory1IdList();
           data.Category2IdList = catalogueFilter.getCategory2IdList();
           data.Category3IdList = catalogueFilter.getCategory3IdList();
           data.ActionFilter = catalogueFilter.getActionFilter();
           data.BargainFilter = catalogueFilter.getBargainFilter();
           data.NewFilter = catalogueFilter.getNewFilter();
           data.StockFilter = catalogueFilter.getStockFilter();
           data.TextFilter = catalogueFilter.getTextFilter();
           var dataString = $.toJSON(data);
           $.ajax({
               type: "POST",
               url: serviceUrl,
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
                           //var options = '';
                           //$.each(result.FirstLevelCategories, function (key, value) {
                           //    options += '<option value="' + value.Id + '">' + value.Name + '</option>';
                           //});
                           //options = '<option></option>' + options;
                           //$('#category1List').find('option').remove().end().append(options);

                           //$('<br /><select id="category1List" data-placeholder="Jelleg - 1..." class="chosen" multiple style="width:270px;">' + options + '</select>').appendTo($('#cus_filter_2'));
                           //var selects = $('#cus_filter_2').find('select');
                           //selects.chosen();
                           //options.appendTo($('#category1List'));
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
                       //alert('manufacturerId: ' + requestParams.getManufacturerId() + '\nCategory1Id: ' + requestParams.getCategory1Id() + '\nCategory2Id: ' + requestParams.getCategory2Id() + '\nCategory3Id: ' + requestParams.getCategory3Id());
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

    this.loadCatalogueList = function (serviceUrl) {
        var data = new Object();
        data.ManufacturerIdList = catalogueFilter.getManufacturerIdList();
        data.Category1IdList = catalogueFilter.getCategory1IdList();
        data.Category2IdList = catalogueFilter.getCategory2IdList();
        data.Category3IdList = catalogueFilter.getCategory3IdList();
        data.ActionFilter = catalogueFilter.getActionFilter();
        data.BargainFilter = catalogueFilter.getBargainFilter();
        data.NewFilter = catalogueFilter.getNewFilter();
        data.StockFilter = catalogueFilter.getStockFilter();
        data.TextFilter = catalogueFilter.getTextFilter();
        data.Sequence = catalogueFilter.getSequence();
        data.CurrentPageIndex = catalogueFilter.getCurrentPageIndex();
        data.ItemsOnPage = catalogueFilter.getItemsOnPage();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: serviceUrl,
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    if (result.Items.length > 0) {
                        $("#div_catalogue").empty();
                        //$("#productTemplate").tmpl(result).appendTo("#div_catalogue");
                        $("#div_pager_top").empty();
                        $("#div_pager_bottom").empty();
                        $("#pagerTemplate").tmpl(result).appendTo("#div_pager_top");
                        $("#pagerTemplate").tmpl(result).appendTo("#div_pager_bottom");
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
};

