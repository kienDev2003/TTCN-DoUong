const PageSize = 9;
let PageIndex = 1;

function btnThanhToan(orderID) {
    $.ajax({
        type: "POST",
        url: "index.aspx/Pay",
        data: JSON.stringify({orderID: orderID}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.d === true) {
                Swal.fire({
                    title: 'Thông báo!',
                    text: "Thanh toan thanh cong",
                    icon: 'success',
                    confirmButtonText: 'OK'
                })
            }
            renderTable(thanhToan, PageIndex);
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}
function load() {
    var thanhToan = [];
    $.ajax({
        type: "POST",
        url: "index.aspx/GetOrderNotPay",
        data: JSON.stringify({}),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            thanhToan = response.d;
            renderTable(thanhToan, PageIndex); // Chuyển PageIndex vào đây
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });

    renderTable(thanhToan, PageIndex)
}

function renderTable(thanhToan) {
    const start = (PageIndex - 1) * PageSize;
    const end = start + PageSize;
    const currentEmployees = thanhToan.slice(start, end); // Lấy dữ liệu trang hiện tại

    const tableBody = document.getElementById('table_thanhToan');
    tableBody.innerHTML = ""; // Xóa nội dung cũ
    currentEmployees.forEach(order => {
        const row = document.createElement('tr');
        row.innerHTML = `
                            <td>${order.TableId}</td>
                            <td>${order.OrderDate}</td>
                            <td>
                                <select id="paymentMethod">
                                    <option value="bankTransfer">Chuyển khoản ngân hàng</option>
                                    <option value="cash">Tiền mặt</option>
                                </select>
                            </td>
                            <td>${order.TotalAmount}</td>
                            <td><input class="button" onclick="btnThanhToan(${order.OrderId})" type="button" value="Thanh toán" "></td>
       
                `;
        tableBody.appendChild(row);
    });
    setupPagination(thanhToan, PageIndex);
}

function setupPagination(thanhToan) {
    const totalPages = Math.ceil(thanhToan.length / PageSize);
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
            // var txtSearch = document.getElementById("txtSearch").value;
            // if (txtSearch != '' || txtSearch != null) {
            //     btnSearch();
            // }
            // else {
            load(PageIndex);
            // }
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
            // // var txtSearch = document.getElementById("txtSearch").value;
            // if (txtSearch != '' || txtSearch != null) {
            //     btnSearch();
            // }
            // else {
            load(PageIndex);
            // }
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
            // var txtSearch = document.getElementById("txtSearch").value;
            // if (txtSearch != '' || txtSearch != null) {
            //     btnSearch();
            // }
            // else {
            load(PageIndex);
            // }
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
            // var txtSearch = document.getElementById("txtSearch").value;
            // if (txtSearch != '' || txtSearch != null) {
            //     btnSearch();
            // }
            // else {
            load(PageIndex);
            // }
        }
    });
    paginationDiv.appendChild(nextPage);
}
load();