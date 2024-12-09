<%@ Page Title="" Language="C#" MasterPageFile="~/view/administrator/home/master/index.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.view.administrator.home.pay.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="./bctk.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="content_body">
        <h1>Thanh toán</h1>
        <table>
            <thead>
                <tr>
                    <th>Bàn</th>
                    <th>Thời gian đặt</th>
                    <th>Kiểu thanh toán</th>
                    <th>Tổng tiền</th>
                    <th>Chức năng</th>
                </tr>
            </thead>
            <tbody id="table_thanhToan">
            </tbody>
        </table>
        <div class="pagination" id="pagination" style="position: absolute;"></div>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="./thanhToan.js"></script>
</asp:Content>
