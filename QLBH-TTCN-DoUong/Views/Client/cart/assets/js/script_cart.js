// Hàm lấy cookie theo tên
function getCookie(name) {
	let cookies = document.cookie.split('; ');
	for (let i = 0; i < cookies.length; i++) {
		let [key, value] = cookies[i].split('=');
		if (key === name) {
			return decodeURIComponent(value);
		}
	}
	return null;
}

// Hàm render dữ liệu từ cookie lên bảng
function renderCartTable() {
	const tableContent = document.getElementById("table_Content"); // Phần nội dung bảng
	const txtTotalPrice = document.getElementById("txtTotalPrice"); // Phần hiển thị tổng giá

	// Đọc cookie
	let orderDetailsCookie = getCookie('OrderDetails');
	if (!orderDetailsCookie) {
		tableContent.innerHTML = `<tr><td colspan="5" class="text-center">Cart is empty.</td></tr>`;
		txtTotalPrice.textContent = '0'; // Không có sản phẩm
		return;
	}

	// Chuyển cookie thành mảng đối tượng
	let orderDetails = JSON.parse(orderDetailsCookie);

	// Kiểm tra nếu giỏ hàng trống
	if (orderDetails.length === 0) {
		tableContent.innerHTML = `<tr><td colspan="5" class="text-center">Cart is empty.</td></tr>`;
		txtTotalPrice.textContent = '0'; // Không có sản phẩm
		return;
	}

	// Xóa nội dung bảng cũ
	tableContent.innerHTML = '';

	let totalPrice = 0;

	// Tạo các dòng bảng
	orderDetails.forEach((item, index) => {
	const { productId, name, price, quantity } = item;

	// Tính tổng giá sản phẩm
	const total = price * quantity;
	totalPrice += total; // Cộng vào tổng giá toàn bộ

	// Thêm dòng vào bảng
	tableContent.innerHTML += `
		<tr data-index="${index}">
			<td class="text-center py-3 px-4">${name}</td>
			<td class="text-right py-3 px-4">${price.toFixed(2)}</td>
			<td class="text-center py-3 px-4">
				<input 
					type="number" 
					class="form-control text-center quantity-input" 
					value="${quantity}" 
					min="1" 
					data-productid="${productId}" 
					onchange="updateQuantity(this)"
				/>
			</td>
			<td class="text-right py-3 px-4 total-price-cell">${total.toFixed(2)}</td>
			<td class="text-center align-middle py-3 px-0">
				<p class="text-danger" data-productid="${productId}" onclick="removeProduct(this)">
					<i class="fas fa-trash-alt"></i>
				</p>
			</td>
		</tr>
	`;
});

	// Hiển thị tổng giá
	txtTotalPrice.textContent = `${totalPrice.toFixed(2)}`;
}

function renderTotalPriceFromCells() {
	const txtTotalPrice = document.getElementById("txtTotalPrice"); // Phần hiển thị tổng giá
	let totalPrice = 0;

	// Lấy tất cả các ô tổng tiền
	const totalCells = document.querySelectorAll('.total-price-cell');
	totalCells.forEach(cell => {
		const total = parseFloat(cell.textContent.replace('$', '')); // Loại bỏ ký tự `$` và chuyển thành số
		if (!isNaN(total)) {
			totalPrice += total; // Cộng tổng giá trị
		}
	});

	// Hiển thị tổng giá trị
	txtTotalPrice.textContent = `${totalPrice.toFixed(2)}`;
}


function updateQuantity(input_quantity) {	
	const productid = parseInt(input_quantity.dataset.productid, 10);
	let newQuantity = parseInt(input_quantity.value, 10);

	if (isNaN(newQuantity) || newQuantity < 1) {
		input_quantity.value = 1;
		newQuantity = 1;
	}

	let orderDetailsCookie = getCookie('OrderDetails');
	if (!orderDetailsCookie) return;

	let orderDetails = JSON.parse(orderDetailsCookie);

	// Tìm sản phẩm và cập nhật số lượng
	let product = orderDetails.find(item => String(item.productId) === String(productid));
	if (product) product.quantity = newQuantity;
		
	saveOrderDetailsToCookie('OrderDetails',120,orderDetails);
	
	// Tính lại tổng giá trị sản phẩm trong bảng
	const price = product.price;
	const totalCell = input_quantity.closest('tr').querySelector('.total-price-cell');
	const total = price * newQuantity;
	totalCell.textContent = `${total.toFixed(2)}`;

	renderTotalPriceFromCells();
}
	
function saveOrderDetailsToCookie(nameCookie, timeoutCookie, orderDetails) 
{
	document.cookie = `${nameCookie}=${encodeURIComponent(JSON.stringify(orderDetails))}; path=/; max-age=${timeoutCookie};`;
}

function removeProduct(input_removeProduct) {
	
	console.log(input_removeProduct);
	let orderDetailsCookie = getCookie('OrderDetails');
	let orderDetails = JSON.parse(orderDetailsCookie);
	
	const productid = parseInt(input_removeProduct.dataset.productid, 10);
	
	orderDetails = orderDetails.filter(item => String(item.productId) !== String(productid));
	
	saveOrderDetailsToCookie('OrderDetails',120, orderDetails);

	renderCartTable();
}

document.addEventListener('DOMContentLoaded', renderCartTable());