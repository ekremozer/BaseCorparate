$('#petition-fields').multiSelect({ selectableOptgroup: true });

if ($("textarea#body").length > 0) {
    CKEDITOR.replace('body', { customConfig: '/areas/admin/assets/plugins/ckeditor/config.js', filebrowserBrowseUrl: '/admin/elfinder' });
}
document.querySelectorAll(".mvc-grid").forEach(element => new MvcGrid(element));
function UserDelete(userId) {
    var url = "/admin/user/delete/" + userId;
    DeleteConfirm("Silme İşlemi", "Kullanıcıyı silmek istediğinizden emin misiniz?", "Kullanıcı silindi", "Kullanıcı silme işlemi iptal edildi", url, userId);
}

function CategoryDelete(categoryId) {
    var url = "/admin/category/delete/" + categoryId;
    DeleteConfirm("Silme İşlemi", "Kategoriyi silmek istediğinizden emin misiniz?", "Kategori silindi", "Kategori silme işlemi iptal edildi", url, categoryId);
}

function ArticleDelete(articleId) {
    var url = "/admin/article/delete/" + articleId;
    DeleteConfirm("Silme İşlemi", "Makaleyi silmek istediğinizden emin misiniz?", "Makale silindi", "Makale silme işlemi iptal edildi", url, articleId);
}

function TagDelete(tagId) {
    var url = "/admin/tag/delete/" + tagId;
    DeleteConfirm("Silme İşlemi", "Etiketi silmek istediğinizden emin misiniz?", "Etiket silindi", "Etiket silme işlemi iptal edildi", url, tagId);
}

function TopicDelete(topicId) {
    var url = "/admin/topic/delete/" + topicId;
    DeleteConfirm("Silme İşlemi", "Sayfayı silmek istediğinizden emin misiniz?", "Sayfa silindi", "Sayfa silme işlemi iptal edildi", url, topicId);
}

function SettingDelete(settingId) {
    var url = "/admin/setting/delete/" + settingId;
    DeleteConfirm("Silme İşlemi", "Ayarı silmek istediğinizden emin misiniz?", "Ayar silindi", "Ayar silme işlemi iptal edildi", url, settingId);
}

function PageNotFoundLogDeleteAll() {
    var url = "/admin/PageNotFoundLog/DeleteAll/";
    DeleteConfirm("Silme İşlemi", "Tüm 404 Loglarını silmek istediğinizden emin misiniz?", "404 Logları silindi", "404 Logları silme işlemi iptal edildi", url, 0);
}

function ErrorLogDeleteAll() {
    var url = "/admin/ErrorLog/DeleteAll/";
    DeleteConfirm("Silme İşlemi", "Tüm Hata Kayıtlarını silmek istediğinizden emin misiniz?", "Hata Kayıtları silindi", "Hata Kayıtları silme işlemi iptal edildi", url, 0);
}

function RedirectRecordDelete(redirectRecordId) {
    var url = "/admin/redirectRecord/delete/" + redirectRecordId;
    DeleteConfirm("Silme İşlemi", "301 Yönlendirmesini silmek istediğinizden emin misiniz?", "301 Yönlendirmesi silindi", "301 Yönlendirmesi silme işlemi iptal edildi", url, redirectRecordId);
}

function MenuGroupDelete(menuGroupId) {
    var url = "/admin/menugroup/delete/" + menuGroupId;
    DeleteConfirm("Silme İşlemi", "Menü Grubunu silmek istediğinizden emin misiniz?", "Menü Grubu silindi", "Menü Grubu silme işlemi iptal edildi", url, menuGroupId);
}

function MenuItemDelete(menuItemId) {
    var url = "/admin/menuitem/delete/" + menuItemId;
    DeleteConfirm("Silme İşlemi", "Linki silmek istediğinizden emin misiniz?", "Link silindi", "Link silme işlemi iptal edildi", url, menuItemId);
}

function MenuSubItemDelete(menuSubItemId) {
    var url = "/admin/menusubitem/delete/" + menuSubItemId;
    DeleteConfirm("Silme İşlemi", "Linki silmek istediğinizden emin misiniz?", "Link silindi", "Link silme işlemi iptal edildi", url, menuSubItemId);
}

function CustomPetitionDelete(customPetitionId) {
    var url = "/admin/customPetition/delete/" + customPetitionId;
    DeleteConfirm("Silme İşlemi", "Dilekçeyi silmek istediğinizden emin misiniz?", "Dilekçe silindi", "Dilekçe silme işlemi iptal edildi", url, customPetitionId);
}

function PageNotFoundLogDelete(pageNotFoundLogId) {
    var url = "/admin/pageNotFoundLog/delete/" + pageNotFoundLogId;
    DeleteConfirm("Silme İşlemi", "404 Kaydını silmek istediğinizden emin misiniz?", "404 Kaydı silindi", "404 Kaydı silme işlemi iptal edildi", url, pageNotFoundLogId);
}

function ErrorLogDelete(errorLogId) {
    var url = "/admin/errorLog/delete/" + errorLogId;
    DeleteConfirm("Silme İşlemi", "Hata Kaydını silmek istediğinizden emin misiniz?", "Hata Kaydı silindi", "Hata Kaydı silme işlemi iptal edildi", url, errorLogId);
}

function MessageDeleteDelete(messageId) {
    var url = "/admin/message/delete/" + messageId;
    DeleteConfirm("Silme İşlemi", "Mesajı silmek istediğinizden emin misiniz?", "Mesaj silindi", "Mesaj silme işlemi iptal edildi", url, messageId);
}

function PetitionFieldDelete(petitionFieldId) {
    var url = "/admin/petitionField/delete/" + petitionFieldId;
    DeleteConfirm("Silme İşlemi", "Dilekçe alanını silmek istediğinizden emin misiniz?", "Dilekçe alanı silindi", "Dilekçe alanı silme işlemi iptal edildi", url, petitionFieldId);
}

function DeleteConfirm(title, text, confirmInfo, cancelledInfo, url, dataId) {
    swal({
        title: title,
        text: text,
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Sil",
        cancelButtonText: "Vazgeç",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'GET',
                url: url,
                success: function (result) {
                    if (dataId === 0) {
                        $("tr[data-id]").animate({ backgroundColor: "#003" }, "slow").animate({ opacity: "hide" }, "slow");
                    } else {
                        $("[data-id='" + dataId + "']").animate({ backgroundColor: "#003" }, "slow").animate({ opacity: "hide" }, "slow");
                    }
                    swal("İşlem Tamamlandı", confirmInfo, "success");
                },
                failure: function (response) {
                    alert('Error');
                }
            });

        } else {
            swal("İşlem İptal Edildi", cancelledInfo, "error");
        }
    });
}

function OrderPayTrConfirm(orderId) {
    var url = "/admin/order/OrderPayTrConfirm/" + orderId;
    swal({
        title: "Sipariş Onayı",
        text: "Ödemesi onaylanmamış siparişini onaylamak istediğinizden emin misiniz?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Onayla",
        cancelButtonText: "Vazgeç",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'GET',
                url: url,
                success: function (result) {
                    swal("İşlem Tamamlandı", "Sipariş onaylandı", "success");
                },
                failure: function (response) {
                    alert('Error');
                }
            });

        } else {
            swal("İşlem İptal Edildi", "Sipariş onaylama işlemi iptal edildi", "error");
        }
    });
}

function MessageTest(messageId) {
    var url = "/admin/message/Test/" + messageId;
    swal({
        title: "Test mesajı gönderme Onayı",
        text: "Editore test mesajını göndermek istediğinizden emin misiniz?",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Onayla",
        cancelButtonText: "Vazgeç",
        closeOnConfirm: false,
        closeOnCancel: false
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                type: 'GET',
                url: url,
                success: function (result) {
                    swal("İşlem Tamamlandı", "Test mesajı gönderildi", "success");
                },
                failure: function (response) {
                    alert('Error');
                }
            });

        } else {
            swal("İşlem İptal Edildi", "Test mesajı gönderimi iptal edildi", "error");
        }
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

function getFile(articleUid, fileName) {
    $.ajax({
        url: '/admin/article/getFile/?articleUid=' + articleUid,
        method: 'GET',
        xhrFields: {
            responseType: 'blob'
        },
        success: function (data) {
            var a = document.createElement('a');
            var url = window.URL.createObjectURL(data);
            a.href = url;
            a.download = fileName;
            a.click();
            window.URL.revokeObjectURL(url);
        }
    });
};