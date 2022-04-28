if ($("textarea#body").length > 0) {
    CKEDITOR.replace('body', { customConfig: '/areas/admin/assets/plugins/ckeditor/config.js' });
}

$("#UserAddOrUpdate").ajaxForm({
    beforeSubmit: UserShowRequest,
    success: UserSubmitSuccessful,
    error: UserAjaxError
});

function UserShowRequest(a, b, c) {

}

function UserAjaxError() {

}

function UserSubmitSuccessful(a, b, c) {
    var avatarUrl = "/areas/admin/assets/avatar/" + a.avatar;
    parent.$("#avatar-" + a.id).attr("src", avatarUrl);
    $("#avatar-" + a.id).attr("src", avatarUrl);

    swal({
        title: "İşlem Tamamlandı",
        text: "Kullanıcı Bilgileri Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#CategoryAddOrUpdate").ajaxForm({
    beforeSubmit: CategoryShowRequest,
    success: CategorySubmitSuccessful,
    error: CategoryAjaxError
});

function CategoryShowRequest(a, b, c) {
}

function CategoryAjaxError() {

}

function CategorySubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Kategori Bilgileri Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#CustomerAddOrUpdate").ajaxForm({
    beforeSubmit: CustomerShowRequest,
    success: CustomerSubmitSuccessful,
    error: CustomerAjaxError
});

function CustomerShowRequest(a, b, c) {
    debugger;
    var id = $("#id").val();
    var password = $("#password").val();
    if (id === "0" && password === "") {
        swal("Şifreyi giriniz");
        return false;
    }
}

function CustomerAjaxError() {

}

function CustomerSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Müşteri Bilgileri Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#SettingAddOrUpdate").ajaxForm({
    beforeSubmit: SettingShowRequest,
    success: SettingSubmitSuccessful,
    error: SettingAjaxError
});

function SettingShowRequest(a, b, c) {
}

function SettingAjaxError() {

}

function SettingSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Ayar Bilgileri Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#RedirectRecordAddOrUpdate").ajaxForm({
    beforeSubmit: RedirectRecordShowRequest,
    success: RedirectRecordSubmitSuccessful,
    error: RedirectRecordAjaxError
});

function RedirectRecordShowRequest(a, b, c) {
}

function RedirectRecordAjaxError() {

}

function RedirectRecordSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "301 Yönlendirme Bilgileri Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}


$("#MenuGroupAddOrUpdate").ajaxForm({
    beforeSubmit: MenuGroupShowRequest,
    success: MenuGroupSubmitSuccessful,
    error: MenuGroupAjaxError
});

function MenuGroupShowRequest(a, b, c) {
}

function MenuGroupAjaxError() {

}

function MenuGroupSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Menü Bilgileri Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#MenuItemAddOrUpdate").ajaxForm({
    beforeSubmit: MenuItemShowRequest,
    success: MenuItemSubmitSuccessful,
    error: MenuItemAjaxError
});

function MenuItemShowRequest(a, b, c) {

}

function MenuItemAjaxError() {

}

function MenuItemSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Menü Linki Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#CustomPetitionAddOrUpdate").ajaxForm({
    beforeSubmit: CustomPetitionShowRequest,
    success: CustomPetitionSubmitSuccessful,
    error: CustomPetitionAjaxError
});

function CustomPetitionShowRequest(a, b, c) {
}

function CustomPetitionAjaxError() {

}

function CustomPetitionSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Özel Dilekçe Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#InvoiceAddOrUpdate").ajaxForm({
    beforeSubmit: InvoiceAddOrUpdateShowRequest,
    success: InvoiceAddOrUpdateSubmitSuccessful,
    error: InvoiceAddOrUpdateAjaxError
});

function InvoiceAddOrUpdateShowRequest(a, b, c) {
}

function InvoiceAddOrUpdateAjaxError() {

}

function InvoiceAddOrUpdateSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Fatura Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#UpdateInvoiceInfo").ajaxForm({
    beforeSubmit: UpdateInvoiceInfoShowRequest,
    success: UpdateInvoiceInfoSubmitSuccessful,
    error: UpdateInvoiceInfoAjaxError
});

function UpdateInvoiceInfoShowRequest(a, b, c) {
}

function UpdateInvoiceInfoAjaxError() {

}

function UpdateInvoiceInfoSubmitSuccessful(a, b, c) {
    if (a.status === true) {
        swal({
            title: "İşlem Tamamlandı",
            text: "Fatura Güncellendi",
            confirmButtonText: 'Tamam',
            type: "success"
        }, function () {
            parent.jQuery.fancybox.close();
        });
    } else {
        swal({
            title: "Hata",
            text: a.message,
            confirmButtonText: 'Tamam',
            type: "error"
        }, function () {

        });
    }
}

$("#MessageAddOrUpdate").ajaxForm({
    beforeSubmit: MessageShowRequest,
    success: MessageSubmitSuccessful,
    error: MessageAjaxError
});

function MessageShowRequest(a, b, c) {
    a[3].value = $("iframe").contents().find("body").html();
    console.log(a);
    console.log(b);
    console.log(c);
}

function MessageAjaxError() {

}

function MessageSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Mesaj Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#PetitionFieldAddOrUpdate").ajaxForm({
    beforeSubmit: PetitionFieldShowRequest,
    success: PetitionFieldSubmitSuccessful,
    error: PetitionFieldAjaxError
});

function PetitionFieldShowRequest(a, b, c) {
}

function PetitionFieldAjaxError() {

}

function PetitionFieldSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Dilekçe Alanı Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("#UrlRecordUpdate").ajaxForm({
    beforeSubmit: UrlRecordShowRequest,
    success: UrlRecordSubmitSuccessful,
    error: UrlRecordAjaxError
});

function UrlRecordShowRequest(a, b, c) {
}

function UrlRecordAjaxError() {

}

function UrlRecordSubmitSuccessful(a, b, c) {
    swal({
        title: "İşlem Tamamlandı",
        text: "Url Kaydı Güncellendi",
        confirmButtonText: 'Tamam',
        type: "success"
    }, function () {
        parent.jQuery.fancybox.close();
    });
}

$("[type='checkbox']").each(function () {
    if ($(this).is(':checked')) {
        $(this).attr('value', 'true');
    } else {
        $(this).attr('value', 'false');
    }
});

$("[type='checkbox']").on('change', function () {
    if ($(this).is(':checked')) {
        $(this).attr('value', 'true');
    } else {
        $(this).attr('value', 'false');
    }
});