$("#login-page-phone").inputmask({ "mask": "(999) 999-9999" });
(function () {
    validate.extend(validate.validators.datetime, {
        parse: function (value, options) {
            return +moment.utc(value);
        },
        format: function (value, options) {
            var format = options.dateOnly ? "YYYY-MM-DD" : "YYYY-MM-DD hh:mm:ss";
            return moment.utc(value).format(format);
        }
    });
    var constraints = {
        "Phone": {
            presence: {
                allowEmpty: false,
                message: "^Telefon numarası boş bırakılamaz"
            },
            format: {
                pattern: /^\(\d{3}\) \d{3}-\d{4}$/,
                message: "^Girdiğiniz telefon numarası istenilen formatla uyuşmuyor",
            }
        },
        "Password": {
            presence: {
                allowEmpty: false,
                message: "^Şifre Boş bırakılamaz"
            },
            length: {
                minimum: 6,
                message: "^Şifre en az 6 karakter olmalıdır"
            },
            /*format:{
                pattern:"",
                message: "Şifre istenilen format ile uyuşmuyor"
            }*/
        },
    };

    var form = document.querySelector(".login-page-form");
    form.addEventListener("submit", function (ev) {
        ev.preventDefault();
        handleFormSubmit(form);
    });

    var inputs = document.querySelectorAll(".login-page-form input,.login-page-form textarea,.login-page-form select");
    for (var i = 0; i < inputs.length; ++i) {
        inputs.item(i).addEventListener("change", function (ev) {
            var errors = validate(form, constraints) || {};
            showErrorsForInput(this, errors[this.name]);
        });
    }

    function handleFormSubmit(form, input) {
        var errors = validate(form, constraints);
        showErrors(form, errors || {});

        if (errors == undefined || errors == null) {
            form.submit();
        }
        if (errors != null) {
            return;
        }
        form.submit();
    }

    function showErrors(form, errors) {
        _.each(form.querySelectorAll("input[name], textarea[name], select[name]"), function (input) {
            showErrorsForInput(input, errors && errors[input.name]);
        });
    }

    function showErrorsForInput(input, errors) {

        var formGroup = closestParent(input.parentNode, "form-list");
        if (formGroup != null) {
            var messages = formGroup.querySelector(".messages");
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
    }

    function closestParent(child, className) {
        if (!child || child == document) {
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