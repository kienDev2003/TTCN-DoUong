<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.Views.Client.cart.index" %>

<!DOCTYPE html>
<html lang="vi">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

    <title>Giỏ Hàng</title>

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
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
                    <p class="total">Tổng: <span id="txtTotalPrice" class="float-end">VND</span></p>
                </div>

                <!-- Checkout Button -->
                <button onclick="checkOut()" class="btn-checkout mt-3">Tiến hành thanh toán</button>
            </div>
        </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <!-- Custom JS -->
    <script src="./assets/js/scripts.js"></script>
</body>

</html>
