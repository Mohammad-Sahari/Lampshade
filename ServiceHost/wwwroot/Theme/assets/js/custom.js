﻿const cookieName = "cart-items";
function addToCart(id, name, price, picture) {
    let products = $.cookie(cookieName);
    if (products === undefined) {
        products = [];
    } else {
        products = JSON.parse(products);
    }

    const count = $("#productCount").val();
    const curentProduct = products.find(x => x.id === id);
    if (curentProduct !== undefined) {
        products.find(x => x.id === id).count = parseInt(currentProduct.count) + parseInt(count);
    } else {
        const product = {
            id,
            name,
            price,
            picture,
            count
        }
        products.push(product);
    }
    $.cookie(cookieName, JSON.stringify(products), { expires: 1, path: "/" });
    updateCart();
}

function updateCart() {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    $("#cart_items_count").text(products.length);
    const cartItemsWrapper = $("#cart_items_wrapper");
    cartItemsWrapper.html('');
    products.forEach(x => {
        const product = `<div class="single-cart-item">
    <a href="javascript:void(0)" class="remove-icon" onclick="removeFromCart('${x.id}')">
        <i class="ion-android-close"></i>
    </a>
    <div class="image">
        <a href="single-product.html">
            <img src="/ProductPictures/${x.picture}"
                 class="img-fluid" alt="">
        </a>
    </div>
    <div class="content">
        <p class="product-title">
            <a href="single-product.html">محصول : ${x.name}</a>
        </p>
        <p class="count">تعداد : ${x.count}</p>
        <p class="count">قیمت واحد : ${x.price}</p>
    </div>
</div>`;

        cartItemsWrapper.append(product);
    });
}

function removeFromCart(id) {
    let products = $.cookie(cookieName);
    products = JSON.parse(products);
    const itemToRemove = products.findIndex(x => x.id === id);
    products.splice(itemToRemove, 1);
    $.cookie(cookieName, JSON.stringify(products), { expires: 1, path: "/" });
    updateCart();
}
//FIX: updated total price doesn't show properly after changing quanitity.
function updateCartItemCount(id, totalId, count) {
    debugger;
    var products = $.cookie(cookieName);
    products = JSON.parse(products);
    const productIndex = products.findIndex(x => x.id === id);
    products[productIndex].count = count;
    const product = products[productIndex];
    const newPrice = parseInt(product.price) * parseInt(count);
    $(`#${totalId}`).text(newPrice);
    $.cookie(cookieName, JSON.stringify(products), { expires: 1, path: "/" });
    updateCart();
}


