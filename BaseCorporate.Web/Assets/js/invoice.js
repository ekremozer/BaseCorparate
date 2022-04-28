$("input[name='IsPerson']").click(function () {
    var x = $(this).parents('.invoice-choice-item').attr("data-tab");
    if (!$(this).parents('.invoice-choice-item').hasClass("active")) {
        $(this).parents('.invoice-choice-item').toggleClass("active");
        $(this).parents('.invoice-choice-item').siblings().removeClass("active");
        $('.invoice-info-form-wrapper[data-tab="' + x + '"]').toggleClass("active").siblings().removeClass("active");
    }
});

(function () {
    validate.extend(validate.validators.datetime, {
        parse: function (value, options) {
            return + moment.utc(value);
        },
        // Input is a unix timestamp
        format: function (value, options) {
            var format = options.dateOnly ? "YYYY-MM-DD" : "YYYY-MM-DD hh:mm:ss";
            return moment.utc(value).format(format);
        }
    });
    var constraints = {
        "Name": {
            presence: {
                allowEmpty: false,
                message: "^İsim boş bırakılamaz"
            },
            length: {
                minimum: 3,
                message: "^İsiminiz çok kısa"
            },
            format: {
                pattern: "[a-zöşüçığİ ]+",
                flags: "i",
                message: "^İsim sadece harflerden oluşabilir"
            }
        },
        "Surname": {
            presence: {
                allowEmpty: false,
                message: "^Soyisim boş bırakılamaz"
            },
            length: {
                minimum: 2,
                message: "^Soyisiminiz çok kısa"
            },
            format: {
                pattern: "[a-zöşüçıİ]+",
                flags: "gi",
                message: "^Soyisim sadece harflerden oluşabilir"
            }
        },
        "Tckno": {
            presence: {
                allowEmpty: false,
                message: "^T.C. Kimlik Numarası boş bırakılamaz"
            }
        },
        "Email": {
            presence: {
                allowEmpty: false,
                message: "^E-Mail boş bırakılamaz"
            }
        }
    };

    var form = document.querySelector(".individual-invoice-info-form");
    form.addEventListener("submit", function (ev) {
        ev.preventDefault();
        handleFormSubmit(form);
    });

    var inputs =
        document.querySelectorAll(".individual-invoice-info-form input,.individual-invoice-info-form textarea,.individual-invoice-info-form select");
    for (var i = 0; i < inputs.length; ++i) {
        inputs.item(i).addEventListener("change", function (ev) {
            var errors = validate(form, constraints) || {};
            showErrorsForInput(this, errors[this.name])
        });
    }

    function handleFormSubmit(form, input) {
        var errors = validate(form, constraints);
        showErrors(form, errors || {});
        if (!errors) {
            form.submit();
        }
    }

    function showErrors(form, errors) {
        _.each(form.querySelectorAll("input[name], textarea[name], select[name]"), function (input) {
            showErrorsForInput(input, errors && errors[input.name]);
        });
    }

    function showErrorsForInput(input, errors) {
        var formGroup = closestParent(input.parentNode, "validate-messages-wrapper")
            , messages = formGroup.querySelector(".messages");
        resetFormGroup(formGroup);
        if (errors) {
            formGroup.classList.add("has-error");
            _.each(errors, function (error) {
                addError(messages, error);
            });
        } else {
            formGroup.classList.add("has-success");
        }
    }

    function closestParent(child, className) {
        if (!child || child === document) {
            return null;
        }
        if (child.classList.contains(className)) {
            return child;
        } else {
            return closestParent(child.parentNode, className);
        }
    }

    function resetFormGroup(formGroup) {
        formGroup.classList.remove("has-error");
        formGroup.classList.remove("has-success");
        _.each(formGroup.querySelectorAll(".help-block.error"), function (el) {
            el.parentNode.removeChild(el);
        });
    }
    function addError(messages, error) {
        var block = document.createElement("p");
        block.classList.add("help-block");
        block.classList.add("error");
        block.innerText = error;
        messages.appendChild(block);
    }
})();

(function () {
    validate.extend(validate.validators.datetime, {
        parse: function (value, options) {
            return +moment.utc(value);
        },
        // Input is a unix timestamp
        format: function (value, options) {
            var format = options.dateOnly ? "YYYY-MM-DD" : "YYYY-MM-DD hh:mm:ss";
            return moment.utc(value).format(format);
        }
    });

    var constraints = {
        "CompanyTitle": {
            presence: {
                allowEmpty: false,
                message: "^Firma Ünvanı boş bırakılamaz"
            },
            length: {
                minimum: 3,
                message: "^Firma Ünvanı çok kısa"
            },
            format: {
                pattern: "[a-zöüçığİ ]+",
                flags: "i",
                message: "^Firma Ünvanı sadece harflerden oluşabilir"
            }
        },
        "TaxOffice": {
            presence: {
                allowEmpty: false,
                message: "^Vergi Dairesi bırakılamaz"
            },
            length: {
                minimum: 3,
                message: "^Vergi Dairesi çok kısa"
            },
            format: {
                pattern: "[a-zöüçığİ ]+",
                flags: "i",
                message: "^Vergi Dairesi sadece harflerden oluşabilir"
            }
        },
        "TaxNumber": {
            presence: {
                allowEmpty: false,
                message: "^Vergi Numarası boş bırakılamaz"
            }

        },
        "CompanyEmail": {
            presence: {
                allowEmpty: false,
                message: "^E-Mail boş bırakılamaz"
            }
        },
        "Address": {
            presence: {
                allowEmpty: false,
                message: "^Adres boş bırakılamaz"
            }
        }
    };

    var form = document.querySelector(".corporate-invoice-info-form");
    form.addEventListener("submit", function (ev) {
        ev.preventDefault();
        handleFormSubmit(form);
    });

    var inputs =
        document.querySelectorAll(".corporate-invoice-info-form input,.corporate-invoice-info-form textarea,.corporate-invoice-info-form select");
    for (var i = 0; i < inputs.length; ++i) {
        inputs.item(i).addEventListener("change", function (ev) {
            var errors = validate(form, constraints) || {};
            showErrorsForInput(this, errors[this.name]);
        });
    }

    function handleFormSubmit(form, input) {
        var errors = validate(form, constraints);
        showErrors(form, errors || {});
        if (!errors) {
            form.submit();
        }
    }

    function showErrors(form, errors) {
        _.each(form.querySelectorAll("input[name], textarea[name], select[name]"), function (input) {
            showErrorsForInput(input, errors && errors[input.name]);
        });
    }

    function showErrorsForInput(input, errors) {
        var formGroup = closestParent(input.parentNode, "validate-messages-wrapper"), messages = formGroup.querySelector(".messages");
        resetFormGroup(formGroup);
        if (errors) {
            formGroup.classList.add("has-error");
            _.each(errors, function (error) {
                addError(messages, error);
            });
        } else {
            formGroup.classList.add("has-success");
        }
    }

    function closestParent(child, className) {
        if (!child || child === document) {
            return null;
        }
        if (child.classList.contains(className)) {
            return child;
        } else {
            return closestParent(child.parentNode, className);
        }
    }

    function resetFormGroup(formGroup) {
        formGroup.classList.remove("has-error");
        formGroup.classList.remove("has-success");
        _.each(formGroup.querySelectorAll(".help-block.error"), function (el) {
            el.parentNode.removeChild(el);
        });
    }
    function addError(messages, error) {
        var block = document.createElement("p");
        block.classList.add("help-block");
        block.classList.add("error");
        block.innerText = error;
        messages.appendChild(block);
    }
})();