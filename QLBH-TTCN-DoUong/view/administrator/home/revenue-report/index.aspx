<%@ Page Title="" Language="C#" MasterPageFile="~/view/administrator/home/master/index.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.view.administrator.home.report.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="./bctk.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form runat="server" id="form1">
        <h1>Thống kê doanh thu</h1>
        <div style="display: flex; justify-content: space-between; width: 100%; margin: 0 auto;">
            <div class="date">
                <div>
                    <span>Từ ngày</span><input style="margin-right: 100px;" type="date" id="dtpStart" runat="server">
                </div>
                <div>
                    <span>Đến ngày</span><input type="date" id="dtpEnd" runat="server">
                </div>
            </div>
            <input type="button" value="Lọc">
        </div>
        
        <table id="table_container">
            <thead>
                <tr>
                    <th>Bàn</th>
                    <th>Thời gian</th>
                    <th>Kiểu thanh toán</th>
                    <th>Tổng tiền</th>
                    <th>Chức năng</th>
                </tr>
            </thead>
            <tbody id="tbody_content" runat="server">
            </tbody>
        </table>

        <div class="pagination" id="pagination"></div>
    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</asp:Content>
