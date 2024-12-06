let orderDetails = [];

//Sự kiện nút cộng, trừ
function updateQuantity(element, increment, i) {
    const input = element.parentElement.querySelector('input');
    let currentValue = parseInt(input.value);
    if (!isNaN(currentValue)) {
        currentValue += increment;
        if (currentValue > 0) {
            input.value = currentValue;
        }
    }
    changeQuantity(i);
}

//Lấy cookie
function getCookie(name) {
    let cookies = document.cookie.split('; ');
    for (let i = 0; i < cookies.length; i++) {
        let [key, value] = cookies[i].split('=');
        if (key === name) return decodeURIComponent(value);
    }
    return [];
}

//Lưu cookie
function saveOrderDetailsToCookie(nameCookie, timeoutCookie, orderDetails) {
    document.cookie = `${nameCookie}=${encodeURIComponent(JSON.stringify(orderDetails))}; path=/; max-age=${timeoutCookie};`;
}

//Hàm render tableContent
function renderTable() {
    var tableContent = document.getElementById('table_content');
    tableContent.innerHTML = ``;
    var html = ``;

    orderDetails = JSON.parse(getCookie("OrderDetails"));
    for (let i = 0; i < orderDetails.length; i++) {

        let productName = orderDetails[i].name;
        let productQuantity = orderDetails[i].quantity;
        let productPrice = orderDetails[i].price;
        let productId = orderDetails[i].productId;

        let temp = `
			<tr>
				  <td class='d-flex align-items-center'>
					<div>
					  <p class='mb-1'>${productName}</p>
					</div>
				  </td>
				  <td class='price'>${productPrice} VND</td>
				  <td>
					<div class='quantity-control'>
					  <button onclick='updateQuantity(this, -1,${i})'>-</button>
					  <input data-itemPrice='${productPrice}' id="txtQuantity${i}" type='number' onchange='changeQuantity(${i})' value='${productQuantity}' min='1'>
					  <button onclick='updateQuantity(this, 1,${i})'>+</button>
					</div>
				  </td>
				  <td class='total' id='total${i}'>${productPrice * productQuantity}</td>
				  <td>
					<button onclick='deleteProduct(${i})' class='btn btn-sm btn-danger'>X</button>
				  </td>
				</tr>
		`;
        html += temp;
    }
    console.log(html);
    tableContent.innerHTML = html;
    changeTotalPrice();
}

//hàm thay đổi số lượng sản phẩm
function changeQuantity(input) {
    let quantity = document.getElementById(`txtQuantity${input}`).value;

    orderDetails[input].quantity = quantity;

    document.getElementById(`total${input}`).innerText = orderDetails[input].price * quantity;

    changeTotalPrice();

    saveOrderDetailsToCookie("OrderDetails", 360, orderDetails);
}

//hàm thay đổi tổng tiền của tất cả sản phẩm
function changeTotalPrice() {
    var totalPriceAll = 0;
    for (let i = 0; i < orderDetails.length; i++) {
        let productName = orderDetails[i].name;
        let productQuantity = orderDetails[i].quantity;
        let productPrice = orderDetails[i].price;
        let productId = orderDetails[i].productId;

        let totalPrice = productQuantity * productPrice;
        totalPriceAll += totalPrice;
    }

    txtTotalPrice.innerText = totalPriceAll;
}

//hàm xóa sản phẩm khỏi orderDetails
function deleteProduct(input) {

    orderDetails.splice(input, 1);
    saveOrderDetailsToCookie("OrderDetails", 360, orderDetails);
    renderTable();
    changeTotalPrice();
}

//hàm render Order
function checkOut() {
    var selectElement = document.getElementById('payment');
    var selectedOption = selectElement.options[selectElement.selectedIndex];

    var paymentMethod_ID = selectedOption.value;
    var totalPrice = txtTotalPrice.innerText;

    var order = {
        paymentMethod_ID: paymentMethod_ID,
        totalPrice: totalPrice,
        orderDetails: orderDetails
    };

    $.ajax({
        type: "POST",
        url: "index.aspx/checkOrder",
        data: JSON.stringify({ orderRequest: order }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (respone) {
            if (respone.d.key === -1) {
                Swal.fire({ title: 'Thông báo!', text: respone.d.content, icon: 'warning', confirmButtonText: 'OK' });
            }
            else window.location.href = respone.d.content;

        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}

document.addEventListener('DOMContentLoaded', function () {
    renderTable();
});