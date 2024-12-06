<%@ Page Title="" Language="C#" MasterPageFile="~/view/administrator/home/master/index.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.view.administrator.home.uer.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="user.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="container_child">
        <h2 class="title_child">Tài khoản</h2>
        <div class="header_child">
            <div class="search_child">
                <input type="text" id="txtSearch" placeholder="Nhận Tên Nhân Sự">
                <input type="button" onclick="btnSearch()" value="Tìm Kiếm ">
            </div>
            <a href="./edit.aspx">Thêm Tài Khoản</a>
        </div>
        <div class="tb_load">
            <table>
                <thead>
                    <tr>
                        <th>Họ Và Tên</th>
                        <th>Tài Khoản</th>
                        <th>Email</th>
                        <th>Số Điện Thoại</th>
                        <th>Vai Trò</th>
                        <th>Chức Năng</th>
                    </tr>
                </thead>
                <tbody id="user_load">
                </tbody>
            </table>
        </div>
        <div class="pagination" id="pagination"></div>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="./user.js"></script>
</asp:Content>
