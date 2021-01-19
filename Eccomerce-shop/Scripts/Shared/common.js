
function increase(id) {
    event.preventDefault();
    var isFound = false;
    let cart = JSON.parse(window.localStorage.getItem("cart"));
    if (cart != undefined) {
        let index = cart.items.findIndex(item => item.id == id);
        if (index >= 0) {
            cart.items[index].qty += 1;
            isFound = true;
        }
        if (!isFound) {
            cart.items.push({
                id: id,
                qty: 1
            });
        }
        index = cart.items.findIndex(item => item.id == id);
        setAmount(cart.items[index].qty, id);
        window.localStorage.setItem("cart", JSON.stringify(cart));
        $.post(
            "/Home/EncreaseQty",
            { productId: id}
        )
    }
}

function decrease(id) {
    let cart = JSON.parse(window.localStorage.getItem("cart"));
    if (cart != undefined) {
        if (cart.items.length) {
            let index = cart.items.findIndex(item => item.id == id);
            if (index >= 0) {
                cart.items[index].qty -= 1;
                setAmount(cart.items[index].qty, id);
                if (!cart.items[index].qty) {
                    cart.items.splice(index, 1);
                }
            }
        }
        window.localStorage.setItem("cart", JSON.stringify(cart));
    }
    $.post(
        "/Home/DecreaseQty",
        { productId: id }
    )
}

function setAmount(qty, id) {
    let productHtml = $(`#${id}`);
    let span = productHtml.find("span");
    span.text(qty);
}


function submitOrder() {
    let userDetails = {
        FirstName: $("#firstName").val(),
        LastName: $("#lastName").val(),
        Phone: $("#phone").val(),
        Email: $("#email").val(),
        FullAddress: $("#fullAddress").val()
    }

    $.post(
        "/Checkout/SubmitOrder",
        { userDetails: userDetails }
    ).then(response => {
        if (response == " Success") {
            alert("Congragulation Order Completed Successfuly a mail will send with your order details");
            window.localStorage.removeItem("cart");
        }
        else {
            alert("Somtiong went wrong please try again");
        }
    })
}