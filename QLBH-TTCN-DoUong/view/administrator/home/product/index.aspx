<%@ Page Title="" Language="C#" MasterPageFile="~/view/administrator/home/master/index.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.view.administrator.home.product.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="./product.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="container_child">
        <h2 class="title_child">Sản Phẩm</h2>
        <div class="header_child">
            <div class="search_child">
                <input type="text" placeholder="Nhận tên sản phẩm" id="txtSearch">
                <input type="button" onclick="btnSearch()" value="Tìm Kiếm ">
            </div>
            <a href="edit.aspx">Thêm sản phẩm</a>
        </div>
        <div class="tb_load">
            <table>
                <thead>
                    <tr>
                        <th>Tên Sản Phẩm</th>
                        <th>Giá Sản Phẩm</th>
                        <th>Hình Ảnh</th>
                        <th>Loại Sản Phảm</th>
                        <th>Trạng thái</th>
                        <th>Chức Năng</th>
                    </tr>
                </thead>
                <tbody id="product_load">
                </tbody>
            </table>
        </div>
        <div class="pagination" id="pagination"></div>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="./product.js"></script>
</asp:Content>
