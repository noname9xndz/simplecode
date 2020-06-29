function LoadProducts() {
    $.ajax({
        url: "/api/products",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var output = '';
            $.each(result, function (key, item) {
                output += '<tr>';
                output += '<td>' + item.productId + '</td>';
                output += '<td>' + item.name + '</td>';
                output += '<td>' + item.amount + '</td>';
                output += '<td>' + item.brand + '</td>';
                output += '<td>' + item.value + '</td>';
                output += `<td><a href="#" class="btn redgate" onclick="SetUpEditModal(${item.productId})">Edit</a> |
                        <a href="#" class="btn redgate" onclick="DeleteProduct(${item.productId})">Delete</a></td>`;
                output += '</tr>';
            });
            $('.tbody').html(output);
        },
        error: function (message) {
            console.log(message.responseText);
        }
    });
}

function AddProduct() {
    var res = validateForm();
    if (res == false) {
        return false;
    }

    var productObj = {
        name: $('#Name').val(),
        amount: parseInt($('#Amount').val()),
        brand: $('#Brand').val(),
        value: parseFloat($('#Value').val())
    };

    $.ajax({
        url: "/api/products",
        data: JSON.stringify(productObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        success: function () {
            LoadProducts();
            $('#productModal').modal('hide');
        },
        error: function (message) {
            console.log(message.responseText);
        }
    });
}

function SetUpEditModal(id) {
    $('form input').css('border-color', 'grey');
    $('#productModal h4').text('Edit Product');

    $.ajax({
        url: "/api/products/" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#ProductID').val(result.productId);
            $('#Name').val(result.name);
            $('#Amount').val(result.amount);
            $('#Brand').val(result.brand);
            $('#Value').val(result.value);
            $('#productModal').modal('show');
            $('#btnUpdateProduct').show();
            $('#btnAddProduct').hide();
        },
        error: function (message) {
            console.log(message.responseText);
        }
    });
    return false;
}

function UpdateProduct() {
    if (!validateForm()) {
        return false;
    }

    var productObj = {
        ProductID: parseInt($('#ProductID').val()),
        Name: $('#Name').val(),
        Amount: parseInt($('#Amount').val()),
        Brand: $('#Brand').val(),
        Value: parseFloat($('#Value').val()),
    };

    $.ajax({
        url: "/api/products",
        data: JSON.stringify(productObj),
        type: "PUT",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function () {
            LoadProducts();

            $('#productModal').modal('hide');

            clearStuff();
        },
        error: function (message) {
            console.log(message.responseText);
        }
    });
}

function DeleteProduct(id) {
    if (confirm("Are you sure?")) {
        $.ajax({
            url: "/api/products/" + id,
            type: "DELETE",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function () {
                LoadProducts();
            },
            error: function (message) {
                console.log(message.responseText);
            }
        });
    }
}

/** Utility functions **/
function clearStuff() {
    $('form').trigger("reset");
    $('#btnUpdateProduct').hide();
    $('#productModal h4').text('Add Product');
    $('#btnAddProduct').show();
}

function validateForm() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', '#c00');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'grey');
    }
    if ($('#Amount').val().trim() == "") {
        $('#Amount').css('border-color', '#c00');
        isValid = false;
    }
    else {
        $('#Amount').css('border-color', 'grey');
    }
    if ($('#Brand').val().trim() == "") {
        $('#Brand').css('border-color', '#c00');
        isValid = false;
    }
    else {
        $('#Brand').css('border-color', 'grey');
    }
    if ($('#Value').val().trim() == "") {
        $('#Value').css('border-color', '#c00');
        isValid = false;
    }
    else {
        $('#Value').css('border-color', 'grey');
    }

    return isValid;
}

LoadProducts();