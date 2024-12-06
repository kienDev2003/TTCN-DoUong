<%@ Page Title="" Language="C#" MasterPageFile="~/view/administrator/home/master/index.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="QLBH_TTCN_DoUong.view.administrator.home.user.edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="userEdit.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="container_child">
        <div class="title">
            <h2>Thêm Nhân Viên</h2>
        </div>
        <div class="form">
            <div class="mb-2">
                <label for="FullName" class="form-label">Họ và Tên</label>
                <input type="text" class="form-control" id="txtFullName" required>
            </div>
            <div class="mb-2">
                <label for="UserName" class="form-label">Tài Khoản</label>
                <input type="text" class="form-control" id="txtUserName" required>
            </div>
            <div class="mb-2">
                <label for="Password" class="form-label">Mật Khẩu</label>
                <input type="text" class="form-control" id="txtPassword" required>
            </div>
            <div class="mb-2">
                <label for="email" class="form-label">Email</label>
                <input type="email" class="form-control" id="txtEmail" required>
            </div>
            <div class="mb-2">
                <label for="PhoneNumber" class="form-label">Số Điện Thoại</label>
                <input type="number" class="form-control" id="txtPhoneNumber" required>
            </div>
            <div class="mb-2">
                <label for="txtSelect" class="form-label">Vai Trò</label>
                <select class="form-select" id="cboRole" runat="server">
                </select>
            </div>
            <div class="action" style="margin-top: 20px;">
                <input type="button" id="btnCRUD" class="btninsert" onclick="insert()" value="Thêm">
                <a style="margin-left: 10px;" href="./" class="btn btn-danger mb-2">Hủy</a>
            </div>
        </div>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="userEdit.js"></script>
</asp:Content>
