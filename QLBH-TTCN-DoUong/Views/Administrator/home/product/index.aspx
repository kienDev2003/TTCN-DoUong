<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Administrator/home/index.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.Views.Administrator.home.product.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="container_child">
        <h2>Quản Lý Sản Phẩm</h2>
        <div class="header_child">
            <div class="search_child">
                <input type="text" id="txtSearch" placeholder="Nhập tên sản phẩm">
                <button onclick="btnSearch()">Tìm Kiếm</button>
            </div>
            <a href="editProduct.html">Thêm Sản Phẩm</a>
        </div>
        <div class="tb_load">
            <table>
                <thead>
                    <tr>
                        <th>Tên Sản Phẩm</th>
                        <th>Giá Sản Phẩm</th>
                        <th>Mô Tả</th>
                        <th>Hình Ảnh</th>
                        <th>Loại Sản Phảm</th>
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
    <script>

        function btnSearch() {
            var txtSearch = document.getElementById("txtSearch").value;
            $.ajax({
                type: "POST",
                url: "index.aspx/SearchUserByName",
                data: JSON.stringify({ name: txtSearch }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    users = response.d;
                    renderTable(users, PageIndex);
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }

        const PageSize = 9;
        let PageIndex = 1;

        function load() {
            var products = [];

            $.ajax({
                type: "POST",
                url: "index.aspx/Gets",
                data: JSON.stringify({}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    products = response.d;
                    renderTable(products, PageIndex);
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }

        function btnDelete(userID) {
            // Hiển thị hộp thoại xác nhận trước khi thực hiện AJAX xóa
            Swal.fire({
                title: 'Thông báo!',
                text: 'Bạn có chắc chắn muốn xóa?',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'OK',
                cancelButtonText: 'Hủy'
            }).then((result) => {

                if (result.isConfirmed) {

                    $.ajax({
                        type: "POST",
                        url: "index.aspx/DeleteUser",
                        data: JSON.stringify({ userID: userID }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d === true) {
                                var txtSearch = document.getElementById("txtSearch").value;
                                if (txtSearch != '' && txtSearch != null) {
                                    btnSearch();
                                } else {
                                    load(PageIndex);
                                }
                            }
                        },
                        error: function (xhr, status, error) {
                            console.error(xhr.responseText);
                        }
                    });
                }
            });

        }

        function renderTable(products) {
            const start = (PageIndex - 1) * PageSize;
            const end = start + PageSize;
            const currentEmployees = products.slice(start, end); // Lấy dữ liệu trang hiện tại

            const tableBody = document.getElementById('product_load');
            tableBody.innerHTML = ""; // Xóa nội dung cũ
            currentEmployees.forEach(product => {
                const row = document.createElement('tr');
                row.innerHTML = `
                <td>${product.Product_Name}</td>
                <td>${product.Product_Describe}</td>
                <td>${product.Product_Price}</td>
                <td>${product.Product_Categori}</td>
                <td>${product.Product_Availability}</td>
                <td>
                    <a href="edit.aspx?userID=${user.Id}">Sửa</a>
                    <input type="button" onclick="btnDelete(${user.Id})" class="btn" value="Xoa">
                </td>
             `;
                tableBody.appendChild(row);
            });
            setupPagination(users, PageIndex);
        }

        function setupPagination(users) {
            const totalPages = Math.ceil(users.length / PageSize);
            const paginationDiv = document.getElementById('pagination');
            paginationDiv.innerHTML = ""; // Xóa nội dung cũ

            // Tạo nút "Trước"
            const prevPage = document.createElement('a');
            prevPage.href = "#";
            prevPage.textContent = "Trước";
            if (PageIndex === 1) {
                prevPage.classList.add('disabled');
            }
            prevPage.addEventListener('click', function (event) {
                event.preventDefault();
                if (PageIndex > 1) {
                    PageIndex--;
                    var txtSearch = document.getElementById("txtSearch").value;
                    if (txtSearch != '' || txtSearch != null) {
                        btnSearch();
                    }
                    else {
                        load(PageIndex);
                    }
                }
            });
            paginationDiv.appendChild(prevPage);

            // Tạo các liên kết trang số
            const totalPagesToShow = 5; // Hiển thị tối đa 5 trang
            let startPage = PageIndex - Math.floor(totalPagesToShow / 2);
            let endPage = PageIndex + Math.floor(totalPagesToShow / 2);

            if (startPage < 1) {
                startPage = 1;
                endPage = totalPagesToShow;
            }
            if (endPage > totalPages) {
                endPage = totalPages;
                startPage = totalPages - totalPagesToShow + 1;
            }
            if (startPage < 1) {
                startPage = 1;
            }
            if (startPage > 1) {
                const firstPage = document.createElement('a');
                firstPage.href = "#";
                firstPage.textContent = "1";
                firstPage.addEventListener('click', function (event) {
                    event.preventDefault();
                    PageIndex = 1;
                    var txtSearch = document.getElementById("txtSearch").value;
                    if (txtSearch != '' || txtSearch != null) {
                        btnSearch();
                    }
                    else {
                        load(PageIndex);
                    }
                });
                paginationDiv.appendChild(firstPage);

                paginationDiv.appendChild(document.createTextNode("..."));
            }

            for (let i = startPage; i <= endPage; i++) {
                const pageLink = document.createElement('a');
                pageLink.href = "#";
                pageLink.textContent = i;
                if (i === PageIndex) {
                    pageLink.classList.add('active');
                }

                pageLink.addEventListener('click', function (event) {
                    event.preventDefault();
                    PageIndex = i;
                    var txtSearch = document.getElementById("txtSearch").value;
                    if (txtSearch != '' || txtSearch != null) {
                        btnSearch();
                    }
                    else {
                        load(PageIndex);
                    }
                });

                paginationDiv.appendChild(pageLink);
            }

            if (endPage < totalPages) {
                paginationDiv.appendChild(document.createTextNode("..."));

                const lastPage = document.createElement('a');
                lastPage.href = "#";
                lastPage.textContent = totalPages;
                lastPage.addEventListener('click', function (event) {
                    event.preventDefault();
                    PageIndex = totalPages;
                    var txtSearch = document.getElementById("txtSearch").value;
                    if (txtSearch != '' || txtSearch != null) {
                        btnSearch();
                    }
                    else {
                        load(PageIndex);
                    }
                });
                paginationDiv.appendChild(lastPage);
            }

            // Tạo nút "Tiếp"
            const nextPage = document.createElement('a');
            nextPage.href = "#";
            nextPage.textContent = "Tiếp";
            if (PageIndex === totalPages) {
                nextPage.classList.add('disabled');
            }
            nextPage.addEventListener('click', function (event) {
                event.preventDefault();
                if (PageIndex < totalPages) {
                    PageIndex++;
                    var txtSearch = document.getElementById("txtSearch").value;
                    if (txtSearch != '' || txtSearch != null) {
                        btnSearch();
                    }
                    else {
                        load(PageIndex);
                    }
                }
            });
            paginationDiv.appendChild(nextPage);
        }

        load();

    </script>
</asp:Content>
