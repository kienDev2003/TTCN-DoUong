<%@ Page Title="" Language="C#" MasterPageFile="~/view/administrator/home/master/index.Master" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="QLBH_TTCN_DoUong.view.administrator.home.ingredient.edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="./editIngredints.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form id="form1" runat="server">
        <div class="container_child">
            <div class="title">
                <h2>Thêm Nguyên liệu</h2>
            </div>
            <div class="form">
                <div class="mb-2">
                    <label for="Ingredients_Name" class="form-label">Tên Nguyên Liệu</label>
                    <input type="text" class="form-control" id="txtIngredients_Name" runat="server" required>
                </div>
                <div class="mb-2">
                    <label for="Ingredients_Price" class="form-label">Don Gia</label>
                    <input type="number" class="form-control" id="txtIngredients_Price" runat="server" required>
                </div>
                <div class="mb-2">
                    <label for="Ingredients_Quatity" class="form-label">Số Lượng</label>
                    <input type="number" value="0" readonly class="form-control" id="txtIngredients_Quantity" runat="server" required>
                </div>
                <div class="mb-2">
                    <label for="ingredientCboUnit" class="form-label">Loại sản phẩm</label>
                    <asp:DropDownList class="form-select" ID="ingredientCboUnit" runat="server">
                    </asp:DropDownList>
                </div>
                <input type="button" class="btninsert" id="btnCRUD" runat="server" onserverclick="btnCRUD_ServerClick" value="Luu">
                <a style="margin-left: 10px;" href="./" class="btn btn-danger mb-2">Hủy</a>
            </div>
        </div>
    </form>
</asp:Content>
