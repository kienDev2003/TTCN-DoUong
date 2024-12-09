<%@ Page Title="" Language="C#" MasterPageFile="~/view/administrator/home/master/index.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.view.administrator.home.inventory.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="styles.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div id="page-stock">
        <h1>Quản Lý Tồn Kho</h1>
        <div class="date-filter">
            <label for="datePicker">Xem tồn kho ngày:</label>
            <input type="date" id="datePicker" />
        </div>
        <table id="stockTable">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nhân viên</th>
                    <th>Ngày cập nhật</th>
                    <th>Chức năng</th>
                </tr>
            </thead>
            <tbody>
                <!-- Nội dung sẽ được thêm từ JavaScript -->
            </tbody>
        </table>
        <div class="pagination" id="pagination"></div>
        <input type="button" id="endShiftButton" value="Kết thúc ca"></input>
    </div>
    <script src="index.js"></script>
</asp:Content>
