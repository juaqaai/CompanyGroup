var CompanyGroupCms = CompanyGroupCms || {};

CompanyGroupCms.Registration = (function () {

    var addNew = function () {
        var data = new Object();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('AddNew'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    $("#bankAccountContainer").empty();
                    $("#bankAccountTemplate").tmpl(result.BankAccounts).appendTo("#bankAccountContainer");
                    $("#contactPersonContainer").empty();
                    $("#contactPersonTemplate").tmpl(result.ContactPersons).appendTo("#contactPersonContainer");
                    $("#deliveryAddressContainer").empty();
                    $("#deliveryAddressTemplate").tmpl(result.DeliveryAddresses).appendTo("#deliveryAddressContainer");

                }
                else {
                    console.log('addNew result failed');
                }
            },
            error: function () {
                console.log('addNew call failed');
            }
        });
    };
    var post = function () {
        var data = new Object();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('Post'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {   //Message, Successed
                    if (!result.Successed) {
                        alert(result.Message);
                    }
                }
                else {
                    console.log('post result failed');
                }
            },
            error: function () {
                console.log('post call failed');
            }
        });
    };
    var updateDataRecording = function () {
        var data = new Object();
        data.Email = $("#txtDataRecordingEmail").val();
        data.Name = $("#txtDataRecordingName").val();
        data.Phone = $("#txtDataRecordingPhone").val();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('UpdateDataRecording'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    if (!result.Successed) {
                        alert(result.Message);
                    }
                }
                else {
                    console.log('updateDataRecording result failed');
                }
            },
            error: function () {
                console.log('updateDataRecording call failed');
            }
        });
    };
    var updateRegistrationData = function () {
        var data = new Object();
        var companyData = new Object();
        companyData.RegistrationNumber = $("#txtRegistrationNumber").val();
        companyData.NewsletterToMainEmail = $('#chkNewsletterToMainEmail').is(':checked'); //bool
        companyData.SignatureEntityFile = $("#signatureEntityFile_Name").html();
        companyData.CustomerId = $("#hfCustomerId").val();
        companyData.CustomerName = $("#txtCustomerName").val();
        companyData.VatNumber = $("#txtVatNumber").val();
        companyData.EUVatNumber = $("#txtEUVatNumber").val();
        companyData.MainEmail = $("#txtMainEmail").val();
        companyData.CountryRegionId = $("#selectCountry").val();
        data.CompanyData = companyData;
        var invoiceAddress = new Object();
        invoiceAddress.CountryRegionId = $("#selectInvoiceCountry").val();
        invoiceAddress.City = $("#txtInvoiceCity").val();
        invoiceAddress.Street = $("#txtInvoiceStreet").val();
        invoiceAddress.ZipCode = $("#txtInvoiceZipCode").val();
        invoiceAddress.Phone = $("#txtInvoicePhone").val();
        data.InvoiceAddress = invoiceAddress;
        var mailAddress = new Object();
        mailAddress.CountryRegionId = $("#selectMailCountry").val();
        mailAddress.City = $("#txtMailAddressCity").val();
        mailAddress.Street = $("#txtMailAddressStreet").val();
        mailAddress.ZipCode = $("#txtMailAddressZipCode").val();
        data.MailAddress = mailAddress;

        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('UpdateRegistrationData'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    if (!result.Successed) {
                        alert(result.Message);
                    }
                }
                else {
                    console.log('updateRegistrationData result failed');
                }
            },
            error: function () {
                console.log('updateRegistrationData call failed');
            }
        });
    };
    var updateWebAdministrator = function () {
        var data = new Object();
        data.AllowOrder = $("#chkWebAdminAllowOrder").val();
        data.AllowReceiptOfGoods = $("#chkWebAdminAllowReceiptOfGoods").val();
        data.ContactPersonId = $("#hiddenWebAdminContactPersonId").val();
        data.Email = $("#txtWebAdminEmail").val();
        data.EmailArriveOfGoods = $("#chkWebAdminEmailArriveOfGoods").val();
        data.EmailOfDelivery = $("#chkWebAdminEmailOfDelivery").val();
        data.EmailOfOrderConfirm = $("#chkWebAdminEmailOfOrderConfirm").val();
        data.FirstName = $("#txtWebAdminFirstName").val();
        data.InvoiceInfo = $("#chkWebAdminInvoiceInfo").val();
        data.LastName = $("#txtWebAdminLastName").val();
        data.LeftCompany = false;
        data.Newsletter = $("#chkWebAdminNewsletter").val();
        data.Password = $("#txtWebAdminPassword").val();
        data.PriceListDownload = $("#chkWebAdminPriceListDownload").val();
        data.RecId = $("#hiddenWebAdminRecId").val();
        data.RefRecId = $("#hiddenWebAdminRefRecId").val();
        data.SmsArriveOfGoods = $("#chkWebAdminSmsArriveOfGoods").val();
        data.SmsOfDelivery = $("#chkWebAdminSmsOfDelivery").val();
        data.SmsOrderConfirm = $("#chkWebAdminSmsOrderConfirm").val();
        data.Telephone = $("#txtWebAdminPhone").val();
        data.UserName = $("#txtWebAdminUserName").val();
        data.RegistrationNumber = $("#txtRegistrationNumber").val();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('UpdateWebAdministrator'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    if (!result.Successed) {
                        alert(result.Message);
                    }
                }
                else {
                    console.log('updateWebAdministrator result failed');
                }
            },
            error: function () {
                console.log('updateWebAdministrator call failed');
            }
        });
    };
    var addDeliveryAddress = function () {
        var data = new Object();
        data.RecId = 0;
        data.City = $('#txtDeliveryAddressCity').val();
        data.Street = $('#txtDeliveryAddressStreet').val();
        data.ZipCode = $('#txtDeliveryAddressZipCode').val();
        data.CountryRegionId = $('#selectDeliveryAddressCountry').val();
        data.Id = '';
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('AddDeliveryAddress'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //result.Items RecId, City, Street, ZipCode, CountryRegionId, Id
                    $("#deliveryAddressContainer").empty();
                    $("#deliveryAddressTemplate").tmpl(result).appendTo("#deliveryAddressContainer");
                    $('#txtDeliveryAddressCity').val('');
                    $('#txtDeliveryAddressStreet').val('');
                    $('#txtDeliveryAddressZipCode').val('');
                }
                else {
                    console.log('addDeliveryAddress result failed');
                }
            },
            error: function () {
                console.log('addDeliveryAddress call failed');
            }
        });
    };
    var selectForUpdateDeliveryAddress = function (selectedId) {
        var data = new Object();
        data.SelectedId = selectedId;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('SelectForUpdateDeliveryAddress'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //result.Items RecId, City, Street, ZipCode, CountryRegionId, Id
                    $("#deliveryAddressContainer").empty();
                    $("#deliveryAddressTemplate").tmpl(result).appendTo("#deliveryAddressContainer");
                }
                else {
                    console.log('selectForUpdateDeliveryAddress result failed');
                }
            },
            error: function () {
                console.log('selectForUpdateDeliveryAddress call failed');
            }
        });
    };
    var updateDeliveryAddress = function (id, recId) {
        var city = '#txtDeliveryAddressCity_' + id;
        var street = '#txtDeliveryAddressStreet_' + id;
        var zipCode = '#txtDeliveryAddressZipCode_' + id;
        var country = '#selectDeliveryAddressCountry_' + id;
        var data = new Object();
        data.RecId = recId;
        data.City = $(city).val();
        data.Street = $(street).val();
        data.ZipCode = $(zipCode).val();
        data.CountryRegionId = $(country).val();
        data.Id = id;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('UpdateDeliveryAddress'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //result.Items RecId, City, Street, ZipCode, CountryRegionId, Id
                    $("#deliveryAddressContainer").empty();
                    $("#deliveryAddressTemplate").tmpl(result).appendTo("#deliveryAddressContainer");
                }
                else {
                    console.log('addDeliveryAddress result failed');
                }
            },
            error: function () {
                console.log('addDeliveryAddress call failed');
            }
        });
    };
    var removeDeliveryAddress = function (deliveryAddressId) {
        var data = new Object();
        data.Id = deliveryAddressId;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('RemoveDeliveryAddress'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //result.Items  RecId, City, Street, ZipCode, CountryRegionId, Id
                    $("#deliveryAddressContainer").empty();
                    $("#deliveryAddressTemplate").tmpl(result).appendTo("#deliveryAddressContainer");
                }
                else {
                    console.log('removeDeliveryAddress result failed');
                }
            },
            error: function () {
                console.log('removeDeliveryAddress call failed');
            }
        });
    };
    var addBankAccount = function () {
        var data = new Object();
        data.Part1 = $('#txtBankAccountPart1').val();
        data.Part2 = $('#txtBankAccountPart2').val();
        data.Part3 = $('#txtBankAccountPart3').val();
        data.RecId = 0;
        data.Id = '';
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('AddBankAccount'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //result.Items Part1 Part2 Part3 RecId Id
                    $("#bankAccountContainer").empty();
                    $("#bankAccountTemplate").tmpl(result).appendTo("#bankAccountContainer");
                    $('#txtBankAccountPart1').val('');
                    $('#txtBankAccountPart2').val('');
                    $('#txtBankAccountPart3').val('');
                }
                else {
                    console.log('removeDeliveryAddress result failed');
                }
            },
            error: function () {
                console.log('removeDeliveryAddress call failed');
            }
        });
    };
    var selectForUpdateBankAccount = function (selectedId) {
        var data = new Object();
        data.SelectedId = selectedId;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('SelectForUpdateBankAccount'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //result.Items Part1 Part2 Part3 RecId Id
                    $("#bankAccountContainer").empty();
                    $("#bankAccountTemplate").tmpl(result).appendTo("#bankAccountContainer");
                }
                else {
                    console.log('selectForUpdateBankAccount result failed');
                }
            },
            error: function () {
                console.log('selectForUpdateBankAccount call failed');
            }
        });
    };
    var updateBankAccount = function (id, recId) {
        //console.log(id);
        var part1 = '#txtBankAccountPart1_' + id;
        var part2 = '#txtBankAccountPart2_' + id;
        var part3 = '#txtBankAccountPart3_' + id;
        var data = new Object();
        data.Part1 = $(part1).val();
        data.Part2 = $(part2).val();
        data.Part3 = $(part3).val();
        data.RecId = recId;
        data.Id = id;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('UpdateBankAccount'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //result.Items Part1 Part2 Part3 RecId Id
                    $("#bankAccountContainer").empty();
                    $("#bankAccountTemplate").tmpl(result).appendTo("#bankAccountContainer");
                }
                else {
                    console.log('removeDeliveryAddress result failed');
                }
            },
            error: function () {
                console.log('removeDeliveryAddress call failed');
            }
        });
    };
    var removeBankAccount = function (bankAccountId) {
        var data = new Object();
        data.Id = bankAccountId;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('RemoveBankAccount'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //result.Items Part1 Part2 Part3 RecId Id   
                    $("#bankAccountContainer").empty();
                    $("#bankAccountTemplate").tmpl(result).appendTo("#bankAccountContainer");
                }
                else {
                    console.log('removeDeliveryAddress result failed');
                }
            },
            error: function () {
                console.log('removeDeliveryAddress call failed');
            }
        });
    };
    var addContactPerson = function () {
        var data = new Object();
        data.Id = '';
        data.AllowOrder = $("#chkContactPersonAllowOrder").val();
        data.AllowReceiptOfGoods = $("#chkContactPersonAllowReceiptOfGoods").val();
        data.ContactPersonId = '';
        data.Email = $("#txtContactPersonEmail").val();
        data.EmailArriveOfGoods = $("#chkContactPersonEmailArriveOfGoods").val();
        data.EmailOfDelivery = $("#chkContactPersonEmailOfDelivery").val();
        data.EmailOfOrderConfirm = $("#chkContactPersonEmailOfOrderConfirm").val();
        data.FirstName = $("#txtContactPersonFirstName").val();
        data.InvoiceInfo = $("#chkContactPersonInvoiceInfo").val();
        data.LastName = $("#txtContactPersonLastName").val();
        data.LeftCompany = false;
        data.Newsletter = $("#chkContactPersonNewsletter").val();
        data.Password = $("#txtContactPersonPassword").val();
        data.PriceListDownload = $("#chkContactPersonPriceListDownload").val();
        data.RecId = 0;
        data.RefRecId = 0;
        data.SmsArriveOfGoods = $("#chkContactPersonSmsArriveOfGoods").val();
        data.SmsOfDelivery = $("#chkContactPersonSmsOfDelivery").val();
        data.SmsOrderConfirm = $("#chkContactPersonSmsOrderConfirm").val();
        data.Telephone = $("#txtContactPersonPhone").val();
        data.UserName = $("#txtContactPersonUserName").val();
        //data.RegistrationNumber = $("#txtRegistrationNumber").val();
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('AddContactPerson'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {//ContactPersonId,FirstName,LastName,AllowOrder ,AllowReceiptOfGoods ,SmsArriveOfGoods,SmsOrderConfirm,SmsOfDelivery ,EmailArriveOfGoods ,EmailOfOrderConfirm ,EmailOfDelivery ,WebAdmin ,PriceListDownload 
                    ///InvoiceInfo,UserName,Password ,Newsletter,Telephone,Email ,LeftCompany,Id
                    $("#contactPersonContainer").empty();
                    $("#contactPersonTemplate").tmpl(result).appendTo("#contactPersonContainer");
                }
                else {
                    console.log('addContactPerson result failed');
                }
            },
            error: function () {
                console.log('addContactPerson call failed');
            }
        });
    };
    var selectForUpdateContactPerson = function (selectedId) {
        var data = new Object();
        data.selectedId = selectedId;
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('SelectForUpdateContactPerson'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {
                    //result.Items Part1 Part2 Part3 RecId Id
                    $("#contactPersonContainer").empty();
                    $("#contactPersonTemplate").tmpl(result).appendTo("#contactPersonContainer");
                }
                else {
                    console.log('selectForUpdateContactPerson result failed');
                }
            },
            error: function () {
                console.log('selectForUpdateContactPerson call failed');
            }
        });
    };
    var updateContactPerson = function () {
        var data = new Object();
        data.ContactPersonId = '';
        data.FirstName = '';
        data.LastName = '';
        data.AllowOrder = '';
        data.AllowReceiptOfGoods = '';
        data.SmsArriveOfGoods = '';
        data.SmsOrderConfirm = '';
        data.SmsOfDelivery = '';
        data.EmailArriveOfGoods = '';
        data.EmailOfOrderConfirm = '';
        data.EmailOfDelivery = '';
        data.WebAdmin = '';
        data.PriceListDownload = '';
        data.InvoiceInfo = '';
        data.UserName = '';
        data.Password = '';
        data.Newsletter = '';
        data.Telephone = '';
        data.Email = '';
        data.LeftCompany = '';
        data.Id = '';
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('UpdateContactPerson'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {//ContactPersonId,FirstName,LastName,AllowOrder ,AllowReceiptOfGoods ,SmsArriveOfGoods,SmsOrderConfirm,SmsOfDelivery ,EmailArriveOfGoods ,EmailOfOrderConfirm ,EmailOfDelivery ,WebAdmin ,PriceListDownload 
                    ///InvoiceInfo,UserName,Password ,Newsletter,Telephone,Email ,LeftCompany,Id
                    $("#contactPersonContainer").empty();
                    $("#contactPersonTemplate").tmpl(result).appendTo("#contactPersonContainer");
                }
                else {
                    console.log('removeDeliveryAddress result failed');
                }
            },
            error: function () {
                console.log('removeDeliveryAddress call failed');
            }
        });
    };
    var removeContactPerson = function () {
        var data = new Object();
        data.Id = '';
        var dataString = $.toJSON(data);
        $.ajax({
            type: "POST",
            url: CompanyGroupCms.Constants.Instance().getRegistrationServiceUrl('RemoveContactPerson'),
            data: dataString,
            contentType: "application/json; charset=utf-8",
            timeout: 10000,
            dataType: "json",
            processData: true,
            success: function (result) {
                if (result) {//ContactPersonId,FirstName,LastName,AllowOrder ,AllowReceiptOfGoods ,SmsArriveOfGoods,SmsOrderConfirm,SmsOfDelivery ,EmailArriveOfGoods ,EmailOfOrderConfirm ,EmailOfDelivery ,WebAdmin ,PriceListDownload 
                    ///InvoiceInfo,UserName,Password ,Newsletter,Telephone,Email ,LeftCompany,Id
                    $("#contactPersonContainer").empty();
                    $("#contactPersonTemplate").tmpl(result).appendTo("#contactPersonContainer");
                }
                else {
                    console.log('removeContactPerson result failed');
                }
            },
            error: function () {
                console.log('removeContactPerson call failed');
            }
        });
    };
    return {
        AddNew: addNew,
        Post: post,
        UpdateDataRecording: updateDataRecording,
        UpdateRegistrationData: updateRegistrationData,
        UpdateWebAdministrator: updateWebAdministrator,
        AddDeliveryAddress: addDeliveryAddress,
        SelectForUpdateDeliveryAddress: selectForUpdateDeliveryAddress,
        UpdateDeliveryAddress: updateDeliveryAddress,
        RemoveDeliveryAddress: removeDeliveryAddress,
        AddBankAccount: addBankAccount,
        SelectForUpdateBankAccount: selectForUpdateBankAccount,
        UpdateBankAccount: updateBankAccount,
        RemoveBankAccount: removeBankAccount,
        AddContactPerson: addContactPerson,
        SelectForUpdateContactPerson: selectForUpdateContactPerson,
        UpdateContactPerson: updateContactPerson,
        RemoveContactPerson: removeContactPerson
    };
})();