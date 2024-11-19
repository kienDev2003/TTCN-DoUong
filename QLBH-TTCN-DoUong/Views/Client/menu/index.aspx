<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.Views.Client.Menu" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>GrabFood</title>
    <link rel="stylesheet" href="./assets/css/reset.css" />
    <link rel="stylesheet" href="./assets/css/main.css" />
    <link rel="stylesheet" href="./assets/css/responsive.css" />

</head>
<body>
    <form runat="server">
        <div id="header">
            <div class="container">
                <div id="head">
                    <div class="menu_item" onclick="btn_nav()">
                        <i class="fa-solid fa-bars"></i>
                    </div>
                    <div class="logo">
                        <a href="#">
                            <h1>GrabFood</h1>
                        </a>
                    </div>
                    <div class="form-cart">
                        <a id="cart" href="#">
                            <img src="assets/img/icon-cart.svg" alt="">
                        </a>
                        <a id="login" href="../../Administrator/login/">Đăng nhập</a>
                    </div>
                </div>
                <div id="nav" runat="server">
                    <a onclick="nav_item()" href="#combo">Combo ưu đãi</a>
                    <a onclick="nav_item()" href="#new_sp">Sản phẩm mới</a>
                    <a onclick="nav_item()" href="#banh_trang">Bánh Tráng</a>
                    <a onclick="nav_item()" href="#do_uong">Đồ uống</a>
                    <a onclick="nav_item()" href="#my_cay">Mỳ cay</a>
                    <a onclick="nav_item()" href="#hoa_qua">Hoa quả</a>
                </div>
            </div>
        </div>

        <div id="content">
            <div class="container" runat="server" id="content_container">
            </div>
        </div>
        <div id="footer">
            <div class="container"></div>
        </div>
        <script src="https://kit.fontawesome.com/f8e1a90484.js" crossorigin="anonymous"></script>
        <script>
            function btn_nav() {
                var nav = document.getElementById("nav");
                if (nav.style.height === "0px" || nav.style.height === "") {
                    nav.style.height = "200px"; // Điều chỉnh chiều cao tùy thuộc vào số lượng mục menu
                } else {
                    nav.style.height = "0px";
                }
            }
            function nav_item() {
                var nav = document.getElementById("nav");
                if (nav.style.height === "200px") {
                    nav.style.height = "0px";
                }
            }
        </script>
    </form>
</body>
</html>

