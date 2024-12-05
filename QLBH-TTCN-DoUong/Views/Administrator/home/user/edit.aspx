<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Administrator/home/index.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="QLBH_TTCN_DoUong.Views.Administrator.home.user.edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Thêm Bootstrap cho trang con -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div style="margin: 10px; padding: 10px; border: 1px solid black;">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="addModalLabel">Thêm tài khoản</h5>
            </div>
            <div class="modal-body">
                <form id="addRecordForm">
                    <div class="mb-2">
                        <label for="FullName" class="form-label">Họ và Tên</label>
                        <input type="text" class="form-control" id="txtFullName" autocomplete="off" required>
                    </div>
                    <div class="mb-2">
                        <label for="UserName" class="form-label">Tài Khoản</label>
                        <input type="text" class="form-control" id="txtUserName" autocomplete="off" required>
                    </div>
                    <div class="mb-2">
                        <label for="Password" class="form-label">Mật Khẩu</label>
                        <input type="text" class="form-control" id="txtPassword" autocomplete="off" required>
                    </div>
                    <div class="mb-2">
                        <label for="email" class="form-label">Email</label>
                        <input type="email" class="form-control" id="txtEmail" autocomplete="off" required>
                    </div>
                    <div class="mb-2">
                        <label for="PhoneNumber" class="form-label">Số Điện Thoại</label>
                        <input type="number" class="form-control" id="txtPhoneNumber" autocomplete="off" required>
                    </div>
                    <div class="mb-2">
                        <label for="cboRole" class="form-label">Vai Trò</label>
                        <select class="form-select" id="cboRole" runat="server">
                        </select>
                    </div>
                    <input id="btnCRUD" type="button" value="Lưu" onclick="insert()" class="btn btn-primary mb-2">
                    <a style="margin-left: 10px;" href="./" class="btn btn-danger mb-2">Hủy</a>
                </form>
            </div>
        </div>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script>

        const urlParams = new URLSearchParams(window.location.search);
        const userID = urlParams.get('userID');  // Lấy giá trị của tham số userID

        // Nếu có userID trong URL, thay đổi sự kiện onclick từ insert() thành update()
        if (userID) {
            // Thay đổi onclick của nút
            document.getElementById('btnCRUD').setAttribute('onclick', 'update()');

            // Cập nhật văn bản nút từ 'Lưu' thành 'Cập nhật'
            document.getElementById('btnCRUD').value = "Cập nhật";

            document.getElementById('txtPassword').style.display = "none";

            getUser(userID);
        }

        function getUser(userID) {
            $.ajax({
                type: "POST",
                url: "edit.aspx/Get",
                data: JSON.stringify({ userID: userID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (respone) {
                    var user = respone.d;

                    document.getElementById("txtFullName").value = user.fullName;
                    document.getElementById("txtUserName").value = user.userName;
                    document.getElementById("txtPassword").value = user.password;
                    document.getElementById("txtEmail").value = user.email;
                    document.getElementById("txtPhoneNumber").value = user.phone.trim();

                    var selectElement = document.getElementById('content_cboRole');
                    for (var i = 0; i < selectElement.options.length; i++) {
                        if (selectElement.options[i].value === String(user.roleId)) {
                            selectElement.selectedIndex = i;
                            break;
                        }
                    }

                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }


        function insert() {
            var fullName = document.getElementById("txtFullName").value;
            var userName = document.getElementById("txtUserName").value;
            var password = document.getElementById("txtPassword").value;
            var email = document.getElementById("txtEmail").value;
            var phone = document.getElementById("txtPhoneNumber").value;

            var selectElement = document.getElementById('content_cboRole');
            var selectedOption = selectElement.options[selectElement.selectedIndex];

            var roleID = selectedOption.value;

            var user = {
                fullName: fullName,
                userName: userName,
                password: password,
                email: email,
                phone: phone,
                roleId: roleID
            }

            $.ajax({
                type: "POST",
                url: "edit.aspx/Insert",
                data: JSON.stringify({ user: user }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (respone) {
                    if (respone.d === true) {
                        Swal.fire({
                            title: 'Thông báo!',
                            text: "Thêm tài khoản thành công",
                            icon: 'success',
                            confirmButtonText: 'OK'
                        }).then(function () {
                            window.location.href = "./";
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Thông báo!',
                            text: "Tài khoản bị trùng. Vui lòng nhập tài khoản khác",
                            icon: 'error',
                            confirmButtonText: 'OK'
                        })
                    }
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }

        function update() {
            var fullName = document.getElementById("txtFullName").value;
            var userName = document.getElementById("txtUserName").value;
            var password = document.getElementById("txtPassword").value;
            var email = document.getElementById("txtEmail").value;
            var phone = document.getElementById("txtPhoneNumber").value;

            var selectElement = document.getElementById('content_cboRole');
            var selectedOption = selectElement.options[selectElement.selectedIndex];

            var roleID = selectedOption.value;

            var user = {
                Id: userID,
                fullName: fullName,
                userName: userName,
                password: password,
                email: email,
                phone: phone,
                roleId: roleID
            }

            console.log(user);

            $.ajax({
                type: "POST",
                url: "edit.aspx/Update",
                data: JSON.stringify({ user: user }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (respone) {
                    if (respone.d === true) {
                        Swal.fire({
                            title: 'Thông báo!',
                            text: "Thay đổi thông tin tài khoản thành công",
                            icon: 'success',
                            confirmButtonText: 'OK'
                        }).then(function () {
                            window.location.href = "./";
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Thông báo!',
                            text: "Lỗi",
                            icon: 'error',
                            confirmButtonText: 'OK'
                        })
                    }
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }
    </script>
</asp:Content>
