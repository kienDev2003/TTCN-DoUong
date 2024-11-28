<!DOCTYPE html>
<html lang="vi">

<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

  <title>Giỏ Hàng</title>

  <!-- Bootstrap -->
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">

  <!-- Custom CSS -->
  <link rel="stylesheet" href="./assets/css/styles.css">
</head>

<body>
  <div class="container my-5">
    <div class="row">
      <!-- Cart Items -->
      <div class="col-md-8">
        <h4 class="cart-header">Giỏ hàng</h4>
        <table class="table">
          <thead>
            <tr>
              <th>Sản phẩm</th>
              <th>Giá</th>
              <th>Số lượng</th>
              <th>Tạm tính</th>
              <th></th>
            </tr>
          </thead>
          <tbody id="table_content">
          </tbody>
        </table>
      </div>

      <!-- Cart Summary -->
      <div class="col-md-4">
        <h4 class="cart-header">Cộng giỏ hàng</h4>
        <div class="summary">
          <p class="total">Tổng: <span id="txtTotalPrice" class="float-end">272.000 VND</span></p>
        </div>

        <!-- Payment Method -->
        <div class="payment-method mt-4">
          <label for="payment-method-select" class="form-label">Phương thức thanh toán</label>
          <select id="payment-method-select" class="form-select">
            <option selected>Chuyển khoản ngân hàng</option>
            <option>Thanh toán tiền mặt</option>
          </select>
        </div>

        <!-- Checkout Button -->
        <button class="btn-checkout mt-3">Tiến hành thanh toán</button>
      </div>
    </div>
  </div>

  <!-- Custom JS -->
  <script src="./assets/js/scripts.js"></script>
</body>

</html>
