$("#register-page-phone").inputmask({ "mask": "(999) 999-9999" });
(function () {
    // Before using it we must add the parse and format functions
    // Here is a sample implementation using moment.js
    validate.extend(validate.validators.datetime, {
        // The value is guaranteed not to be null or undefined but otherwise it
        // could be anything.
        parse: function (value, options) {
            return +moment.utc(value);
        },
        // Input is a unix timestamp
        format: function (value, options) {
            var format = options.dateOnly ? "YYYY-MM-DD" : "YYYY-MM-DD hh:mm:ss";
            return moment.utc(value).format(format);
        }
    });

    // These are the constraints used to validate the form
    var constraints = {
        "IsConfirmed": {
            presence: {
                message: "^Sözleşmeyi kabul etmeniz gerekli"
            },
            inclusion: {
                within: [true],
                message: "^Sözleşmeyi kabul etmeniz gerekli"
            }
        },
        "CityId": {
            presence: {
                allowEmpty: false,
                message: "^Lütfen bir şehir seçiniz"
            }
        },
        "Name": {
            presence: {
                allowEmpty: false,
                message: "^İsim boş bırakılamaz"
            },
            length: {
                minimum: 2,
                message: "^İsiminiz çok kısa"
            },
            format: {
                pattern: "[a-zğüşıöçĞÜŞİÖÇ ]+",
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
                pattern: "[a-zğüşıöçĞÜŞİÖÇ]+",
                flags: "gi",
                message: "^Soyisim sadece harflerden oluşabilir"
            }
        },
        "Email": {
            presence: {
                allowEmpty: false,
                message: "^Email boş bırakılamaz"
            },
            email: {
                message: "^Lütfen gerçeli bir Email giriniz."
            }

        },
        "Phone": {
            presence: {
                allowEmpty: false,
                message: "^Telefon numarası boş bırakılamaz"
            },
            format: {
                pattern: /^\(\d{3}\) \d{3}-\d{4}$/,
                message: "^Telefon numarası geçerli değildir"
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
    // Hook up the form so we can prevent it from being posted
    var form = document.querySelector(".register-page-form");
    if (form != null) {
        form.addEventListener("submit", function (ev) {
            ev.preventDefault();
            handleFormSubmit(form);
        });
    }


    // Hook up the inputs to validate on the fly
    var inputs = document.querySelectorAll(".register-page-form input,.register-page-form textarea,.register-page-form select");
    for (var i = 0; i < inputs.length; ++i) {
        inputs.item(i).addEventListener("change", function (ev) {
            var errors = validate(form, constraints) || {};
            showErrorsForInput(this, errors[this.name])
        });
    }

    function handleFormSubmit(form, input) {
        // validate the form against the constraints
        var errors = validate(form, constraints);
        // then we update the form to reflect the results
        showErrors(form, errors || {});
        if (!errors) {
            form.submit();
        }
    }

    // Updates the inputs with the validation errors
    function showErrors(form, errors) {
        // We loop through all the inputs and show the errors for that input
        _.each(form.querySelectorAll("input[name], textarea[name], select[name]"), function (input) {
            // Since the errors can be null if no errors were found we need to handle
            // that
            showErrorsForInput(input, errors && errors[input.name]);
        });
    }

    // Shows the errors for a specific input
    function showErrorsForInput(input, errors) {
        // This is the root of the input
        var formGroup = closestParent(input.parentNode, "form-list")
            // Find where the error messages will be insert into
            , messages = formGroup.querySelector(".messages");
        // First we remove any old messages and resets the classes
        resetFormGroup(formGroup);
        // If we have errors
        if (errors) {
            // we first mark the group has having errors
            formGroup.classList.add("has-error");
            // then we append all the errors
            _.each(errors, function (error) {
                addError(messages, error);
            });
        } else {
            // otherwise we simply mark it as success
            formGroup.classList.add("has-success");
        }
    }

    // Recusively finds the closest parent that has the specified class
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
        // Remove the success and error classes
        formGroup.classList.remove("has-error");
        formGroup.classList.remove("has-success");
        // and remove any old messages
        _.each(formGroup.querySelectorAll(".help-block.error"), function (el) {
            el.parentNode.removeChild(el);
        });
    }

    // Adds the specified error with the following markup
    // <p class="help-block error">[message]</p>
    function addError(messages, error) {
        var block = document.createElement("p");
        block.classList.add("help-block");
        block.classList.add("error");
        block.innerText = error;
        messages.appendChild(block);
    }
})();