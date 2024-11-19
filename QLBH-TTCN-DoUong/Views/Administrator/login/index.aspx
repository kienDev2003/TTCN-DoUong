<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.Views.Administrator.Login.login" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Tiên Lữ Quán</title>
    <!-- file reset css -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="asset/css/login.css">
    <link rel="stylesheet" href="asset/responsive/login.css">

</head>

<body>
    <form runat="server">
        <div class="container">
            <div class="main">
                <div class="body">
                    <div class="img_bakgrang">
                        <img src="asset/image/team.jpg" alt="">
                    </div>
                    <div class="form">
                        <h2>ĐĂNG NHẬP HỆ THỐNG</h2>
                        <div class="input_container">
                            <input class="input100" id="txtUserName" runat="server" type="text" placeholder="UserName" required>
                            <span class="symbol-input100">
                                <svg xmlns="http://www.w3.org/2000/svg" width="1.2em" height="1.2em"
                                    viewBox="0 0 24 24">
                                    <path fill="currentColor"
                                        d="M11.5 14c4.14 0 7.5 1.57 7.5 3.5V20H4v-2.5c0-1.93 3.36-3.5 7.5-3.5m6.5 3.5c0-1.38-2.91-2.5-6.5-2.5S5 16.12 5 17.5V19h13zM11.5 5A3.5 3.5 0 0 1 15 8.5a3.5 3.5 0 0 1-3.5 3.5A3.5 3.5 0 0 1 8 8.5A3.5 3.5 0 0 1 11.5 5m0 1A2.5 2.5 0 0 0 9 8.5a2.5 2.5 0 0 0 2.5 2.5A2.5 2.5 0 0 0 14 8.5A2.5 2.5 0 0 0 11.5 6" />
                                </svg>
                            </span>
                        </div>
                        <div class="input_container">
                            <input class="input100" id="txtPassword" runat="server" type="password" placeholder="Mật khẩu" required>
                            <span class="symbol-input100">
                                <svg xmlns="http://www.w3.org/2000/svg" width="1.2em" height="1.2em"
                                    viewBox="0 0 24 24">
                                    <path fill="currentColor"
                                        d="M11.5 18c4 0 7.46-2.22 9.24-5.5C18.96 9.22 15.5 7 11.5 7s-7.46 2.22-9.24 5.5C4.04 15.78 7.5 18 11.5 18m0-12c4.56 0 8.5 2.65 10.36 6.5C20 16.35 16.06 19 11.5 19S3 16.35 1.14 12.5C3 8.65 6.94 6 11.5 6m0 2C14 8 16 10 16 12.5S14 17 11.5 17S7 15 7 12.5S9 8 11.5 8m0 1A3.5 3.5 0 0 0 8 12.5a3.5 3.5 0 0 0 3.5 3.5a3.5 3.5 0 0 0 3.5-3.5A3.5 3.5 0 0 0 11.5 9" />
                                </svg>
                            </span>
                        </div>
                        <div class="container-login100-form-btn">
                            <button runat="server" onserverclick="btnLogin_ServerClick" id="btnLogin">Đăng nhập</button>
                        </div>
                        <!--=====LINK TÌM MẬT KHẨU======-->
                        <div class="forgot_password">
                            <a class="forgot" href="./ResetPassword.aspx">Bạn quên mật khẩu?
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>

</html>
