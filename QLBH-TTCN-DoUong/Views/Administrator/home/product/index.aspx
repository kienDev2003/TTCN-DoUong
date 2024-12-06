<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Administrator/home/index.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QLBH_TTCN_DoUong.Views.Administrator.home.product.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="container_child">
        <h2>Sản phẩm</h2>
        <div class="header_child">
            <div class="search_child">
                <input type="text" id="txtSearch">
                <input type="button" value="Tìm kiếm" onclick="btnSearch()">
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
    <script>
        // Định nghĩa số sản phẩm trên mỗi trang
        const PageSize = 6;
        let PageIndex = 1;

        // Tìm kiếm sản phẩm theo tên
        function btnSearch() {
            var txtSearch = document.getElementById("txtSearch").value;
            $.ajax({
                type: "POST",
                url: "index.aspx/SearchProductByName",
                data: JSON.stringify({ name: txtSearch }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var products = response.d;
                    renderTable(products, PageIndex); // Chuyển PageIndex vào đây
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }

        // Hàm tải sản phẩm (load sản phẩm)
        function load() {
            $.ajax({
                type: "POST",
                url: "index.aspx/GetProducts",
                data: JSON.stringify({}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var products = response.d;
                    renderTable(products, PageIndex); // Chuyển PageIndex vào đây
                },
                error: function (xhr, status, error) {
                    console.error(xhr.responseText);
                }
            });
        }

        // Xóa sản phẩm
        function btnDelete(productID) {
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
                        url: "index.aspx/DeleteProduct",
                        data: JSON.stringify({ productID: productID }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            if (response.d === true) {
                                var txtSearch = document.getElementById("txtSearch").value;
                                if (txtSearch !== '' && txtSearch !== null) {
                                    btnSearch();
                                } else {
                                    load();  // Để lại PageIndex là 1 khi xóa và tải lại
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

        // Hàm render bảng sản phẩm
        function renderTable(products, PageIndex) {
            const start = (PageIndex - 1) * PageSize;
            const end = start + PageSize;
            const currentProducts = products.slice(start, end); // Lấy dữ liệu trang hiện tại
            console.log(products);
            const tableBody = document.getElementById('product_load');
            tableBody.innerHTML = ""; // Xóa nội dung cũ
            currentProducts.forEach(product => {
                const row = document.createElement('tr');
                row.innerHTML = `
                <td>${product.Product_Name}</td>
                <td>${product.Product_Price}</td>
                <td class="text-overflow">${product.Product_Describe}</td>
                <td><img style="width: 40px;height: 40px;" src="${product.Product_Image_Url}"></td>
                <td>${product.Product_Categori_Name}</td>
                <td>${product.Product_Availability ? 'Bán' : 'Không bán'}</td>
                <td>
                    <a href="editProduct.aspx?productID=${product.Product_Id}">Sửa</a>
                    <input type="button" value="Xóa" onclick="btnDelete(${product.Product_Id})" class="btn">
                </td>
            `;
                tableBody.appendChild(row);
            });
            setupPagination(products);
        }

        // Hàm phân trang
        function setupPagination(products) {
            const totalPages = Math.ceil(products.length / PageSize);
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
                    if (txtSearch !== '' && txtSearch !== null) {
                        btnSearch();
                    } else {
                        load();
                    }
                }
            });
            paginationDiv.appendChild(prevPage);

            // Tạo các liên kết trang số
            const totalPagesToShow = 5; // Hiển thị tối đa 5 trang
            let startPage = Math.max(1, PageIndex - Math.floor(totalPagesToShow / 2));
            let endPage = Math.min(totalPages, PageIndex + Math.floor(totalPagesToShow / 2));

            if (startPage > 1) {
                const firstPage = document.createElement('a');
                firstPage.href = "#";
                firstPage.textContent = "1";
                firstPage.addEventListener('click', function (event) {
                    event.preventDefault();
                    PageIndex = 1;
                    var txtSearch = document.getElementById("txtSearch").value;
                    if (txtSearch !== '' && txtSearch !== null) {
                        btnSearch();
                    } else {
                        load();
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
                    if (txtSearch !== '' && txtSearch !== null) {
                        btnSearch();
                    } else {
                        load();
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
                    if (txtSearch !== '' && txtSearch !== null) {
                        btnSearch();
                    } else {
                        load();
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
                    if (txtSearch !== '' && txtSearch !== null) {
                        btnSearch();
                    } else {
                        load();
                    }
                }
            });
            paginationDiv.appendChild(nextPage);
        }

        // Khởi tạo tải dữ liệu khi trang được tải
        load();
    </script>

</asp:Content>
