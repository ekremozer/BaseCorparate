$(".aside-menu").click(function () {
    $('.menu-aside').toggleClass('open');
    $('.invisible-area').toggleClass('open');
    $('body').toggleClass('fixed');
});
$(".menu-aside-close-btn").click(function () {
    $('.menu-aside').removeClass('open');
    $('.invisible-area').removeClass('open');
    $('body').removeClass('fixed');
});
$(".dark-mark").click(function () {
    $('.menu-aside').removeClass('open');
    $('.invisible-area').removeClass('open');
    $('body').removeClass('open');
    $('body').removeClass('fixed');
    $('.deals-modal').removeClass('open');
    $('.register-modal').removeClass('open');
    $('.login-modal').removeClass('open');
    $('.forgot-password-modal').removeClass('open');
});
$(".close-all").click(function () {
    $('.deals-modal').removeClass('open');
    $('.register-modal').removeClass('open');
    $('.login-modal').removeClass('open');
    $('.forgot-password-modal').removeClass('open');
    $('.invisible-area').removeClass('open');
    $('body').removeClass('open');
    $('body').removeClass('fixed');
});
$(".mobile-menu-btn").click(function () {
    $(this).toggleClass('open');
    $('.mobile-dropdown-menu').slideToggle();
});
$(".mobile-has-subdropdown").click(function () {
    $(this).toggleClass('open');
    $(this).children('.mobile-subdropdown-menu').slideToggle();
});

$(".profile-btn").hover(function () {
    $('.invisible-area').toggleClass('open');
});

$("#phone").inputmask({ "mask": "(999) 999-9999" });
$("#phone-update").inputmask({ "mask": "(999) 999-9999" });
$("#corporate-phone").inputmask({ "mask": "(999) 999-9999" });
$("#country-code").inputmask({ "mask": "+99" });
$("#corporate-country-code").inputmask({ "mask": "+99" });

$("#home-search-btn").click(function () {
    var q = $("#home-search-q").val();
    if (q !== "") {
        window.location.href = '/ara?q=' + q;
    }
});

$("#header-search-btn").click(function () {
    var q = $("#header-search-q").val();
    if (q !== "") {
        window.location.href = '/ara?q=' + q;
    }
});

$("#header-search-q").keyup(function (e) {
    if (e.keyCode === 13) {
        var q = $("#header-search-q").val();
        if (q !== "") {
            window.location.href = '/ara?q=' + q;
        }
    }
});

$("#category-search-btn").click(function () {
    var q = $("#category-search-q").val();
    if (q !== "") {
        var catId = $("#category-search-q").attr("cat-id");
        window.location.href = '/ara?q=' + q + "&c=" + catId;
    }
});

$("#category-search-q").keyup(function (e) {
    if (e.keyCode === 13) {
        var q = $("#category-search-q").val();
        if (q !== "") {
            var catId = $("#category-search-q").attr("cat-id");
            window.location.href = '/ara?q=' + q + "&c=" + catId;
        }
    }
});


$('.owl-carousel').owlCarousel({
    loop: false,
    margin: 8,
    nav: false,
    responsive: {
        0: { items: 1 },
        575: { items: 2 },
        767: { items: 3 }
    }
});

$(".sorting-view-item").click(function () {
    var x = $(this).attr("data-tab");
    if (!$(this).hasClass("active")) {
        $(this).toggleClass("active");
        $(this).siblings().removeClass("active");
        $('.case-card-list[data-tab="' + x + '"]').toggleClass("active").siblings().removeClass("active");
    }
});
$(".filter-title").click(function () {
    $('.filter-options').slideToggle();
});

$("[name='phone']").inputmask({ "mask": "(999) 999-9999" });

function downloadPetition(uid, isArticle, fileName) {
    $.ajax({
        url: '/dilekce-indir?uid=' + uid + '&isArticle=' + isArticle,
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
}

$(document).ready(function () {
    getCartInfo();
});
function addToCart(id) {
    $.ajax({
        url: '/sepete-ekle',
        method: 'POST',
        data: { "articleId": id },
        success: function (data) {
            setCartInfo(data);
            $(".add-to-cart-btn").attr('onclick', 'removeFromCart(' + id + ')');
            $(".add-to-cart-btn").text("Sepetten Çıkart");
        }
    });
}

function removeFromCart(id) {
    $.ajax({
        url: '/sepetten-cikart',
        method: 'POST',
        data: { "articleId": id },
        success: function (data) {
            setCartInfo(data);
            $(".add-to-cart-btn").attr('onclick', 'addToCart(' + id + ')');
            $(".add-to-cart-btn").text("Sepete Ekle");
            $("#cart-item-" + id + "").animate({ backgroundColor: "#003" }, "slow").animate({ opacity: "hide" }, "slow");
            $("#write-support-" + id + "").animate({ backgroundColor: "#003" }, "slow").animate({ opacity: "hide" }, "slow");
        }
    });
}

function setWriteSupportToCart(id) {
    $.ajax({
        url: '/dilekcemi-avukat-doldursun',
        method: 'POST',
        data: { "articleId": id },
        success: function (data) {
            setCartInfo(data);
        }
    });
}

function setLawyerSupportToCart() {
    $.ajax({
        url: '/avukat-destegi',
        method: 'POST',
        success: function (data) {
            setCartInfo(data);
        }
    });
}

//isConfirmed
function setIsConfirmed() {
    $.ajax({
        url: '/sozlesmeyi-kabul-et',
        method: 'POST',
        success: function (data) {
            setCartInfo(data);
        }
    });
}

function getCartInfo() {
    $.ajax({
        url: '/sepet-bilgisi',
        method: 'POST',
        success: function (data) {
            setCartInfo(data);
        }
    });
}

function setCartInfo(data) {
    $("#cart-item-count-badge").text(data.itemCount);
    $("#cart-item-count").text(data.itemCount + " dilekçe");
    $("#cart-taxExcludedAmount").text(decimalFix(data.taxExcludedAmount) + "TL");
    $("#cart-taxAmount").text(decimalFix(data.taxAmount) + "TL");
    $("#cart-totalAmount").text(decimalFix(data.totalAmount) + "TL");
}

function decimalFix(number) {
    var numberText = number.toFixed(2).replace(".00", "");
    if (numberText.includes(".")) {
        var decimal = numberText.split(".")[1];
        if (decimal.length === 1) {
            numberText = numberText + "0";
            return numberText;
        }
    }
    return numberText;
}

function downloadInvoice(uid, fileName) {
    $.ajax({
        url: '/fatura-indir?uid=' + uid,
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
}

function downloadCustomPetitionField(id,fileName) {
    $.ajax({
        url: '/dilekce-alani-indir?id=' + id,
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
}

document.getElementById("whatswidget-conversation").style.display = "none";
document.getElementById("whatswidget-conversation").style.opacity = "0";
var button = document.getElementById("whatswidget-button");
button.addEventListener("click", openChat);
var conversationMessageOuter = document.getElementById("whatswidget-conversation-message-outer");
conversationMessageOuter.addEventListener("click", openChat);
var chatOpen = !1;

function openChat() {
    0 == chatOpen ? (document.getElementById("whatswidget-conversation").style.display = "block", document.getElementById("whatswidget-conversation").style.opacity = 100, chatOpen = !0, document.getElementById("whatswidget-conversation-message-outer").style.display = "none") : (document.getElementById("whatswidget-conversation").style.opacity = 0, document.getElementById("whatswidget-conversation").style.display = "none", chatOpen = !1)
}