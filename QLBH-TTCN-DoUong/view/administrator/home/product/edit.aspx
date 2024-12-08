<%@ Page Title="" Language="C#" MasterPageFile="~/view/administrator/home/master/index.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="QLBH_TTCN_DoUong.view.administrator.home.product.edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="./editProduct.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="container_child">
        <div class="title">
            <h2>Thêm thêm sản phẩm</h2>
        </div>
        <form class="form" id="form1" runat="server">
            <div class="mb-2">
                <label for="Product_Categori" class="form-label">Loại sản phẩm</label>
                <asp:DropDownList class="form-select" ID="cboCategori" runat="server">
                </asp:DropDownList>
            </div>
            <div class="mb-2">
                <label for="Product_Name" class="form-label">Tên Sản Phẩm</label>
                <input type="text" class="form-control" id="txtProduct_Name" runat="server" required>
            </div>
            <div class="mb-2">
                <label for="Product_Describe" class="form-label">Mô Tả</label>
                <input type="text" class="form-control" id="txtProduct_Describe" runat="server" required>
            </div>
            <div class="mb-2">
                <label for="Product_Price" class="form-label">Giá Sản Phẩm</label>
                <input type="number" class="form-control" id="txtProduct_Price" runat="server" required>
            </div>
            <div class="mb-2">
                <label for="ProDuct_Image_Url" class="form-label"></label>
                <asp:FileUpload runat="server" ID="fileImageProduct" accept="image/*" />
                <input type="text" id="txtUrlImage" hidden />
            </div>
            <div class="mb-2">
                <label for="cboProductAvailability" class="form-label">Trạng thái</label>
                <asp:DropDownList class="form-select" ID="cboProductAvailability" runat="server">
                    <asp:ListItem Value="1">Bán</asp:ListItem>
                    <asp:ListItem Value="0">Không bán</asp:ListItem>
                </asp:DropDownList>
            </div>
            <input type="button" class="btninsert" id="btnCRUD" runat="server" onserverclick="btnCRUD_ServerClick" value="Lưu">
            <a style="margin-left: 10px;" href="./" class="btn btn-danger mb-2">Hủy</a>
        </form>
    </div>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="./editProduct.js"></script>
</asp:Content>
