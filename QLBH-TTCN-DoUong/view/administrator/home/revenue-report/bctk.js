
const orders = [
    { table: '1', date: '05/12/2003', totalMoney: '200000', paymentMethod: 'Chuyển khoản' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },
    { table: '2', date: '05/12/2003', totalMoney: '300000', paymentMethod: 'Tiền mặt' },

];

const itemsPerPage = 6;
let currentPage = 1;

function displayTable(page) {
    const start = (page - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const currentOrders = orders.slice(start, end);

    const tableBody = document.querySelector('#table_container tbody');
    tableBody.innerHTML = ""; // Xóa nội dung cũ

    currentOrders.forEach(order => {
        const row = document.createElement('tr');
        row.innerHTML = `
                    <td>${order.table}</td>
                    <td>${order.date}</td>
                    <td>${order.paymentMethod}</td>
                    <td>${order.table}</td>
                    <td>${order.totalMoney}</td>
                    <td><a class="button" style="text-decoration: none; color:#fff;" href="order_detail.html">Xem chi tiết</a></td>

                `;
        tableBody.appendChild(row);
    });
}

function setupPagination() {
    const totalPages = Math.ceil(orders.length / itemsPerPage);
    const paginationDiv = document.getElementById('pagination');
    paginationDiv.innerHTML = ""; // Xóa nội dung cũ

    const prevPage = document.createElement('a');
    prevPage.href = "#";
    prevPage.textContent = "Trước";
    if (currentPage === 1) {
        prevPage.classList.add('disabled');
    }
    prevPage.addEventListener('click', function (event) {
        event.preventDefault();
        if (currentPage > 1) {
            currentPage--;
            displayTable(currentPage);
            setupPagination();
        }
    });
    paginationDiv.appendChild(prevPage);

    const totalPagesToShow = 2; // Hiển thị tối đa 5 trang
    let startPage = currentPage - Math.floor(totalPagesToShow / 2);
    let endPage = currentPage + Math.floor(totalPagesToShow / 2);

    if (startPage <= 0) {
        startPage = 1;
        endPage = totalPagesToShow;
    }

    if (endPage > totalPages) {
        endPage = totalPages;
        startPage = totalPages - totalPagesToShow + 1;
    }

    if (startPage > 1) {
        const firstPage = document.createElement('a');
        firstPage.href = "#";
        firstPage.textContent = "1";
        firstPage.addEventListener('click', function (event) {
            event.preventDefault();
            currentPage = 1;
            displayTable(currentPage);
            setupPagination();
        });
        paginationDiv.appendChild(firstPage);

        paginationDiv.appendChild(document.createTextNode("..."));
    }

    for (let i = startPage; i <= endPage; i++) {
        const pageLink = document.createElement('a');
        pageLink.href = "#";
        pageLink.textContent = i;
        if (i === currentPage) {
            pageLink.classList.add('active');
        }

        pageLink.addEventListener('click', function (event) {
            event.preventDefault();
            currentPage = i;
            displayTable(currentPage);
            setupPagination();
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
            currentPage = totalPages;
            displayTable(currentPage);
            setupPagination();
        });
        paginationDiv.appendChild(lastPage);
    }

    const nextPage = document.createElement('a');
    nextPage.href = "#";
    nextPage.textContent = "Tiếp";
    if (currentPage === totalPages) {
        nextPage.classList.add('disabled');
    }
    nextPage.addEventListener('click', function (event) {
        event.preventDefault();
        if (currentPage < totalPages) {
            currentPage++;
            displayTable(currentPage);
            setupPagination();
        }
    });
    paginationDiv.appendChild(nextPage);
}

// Hiển thị bảng và phân trang ban đầu
displayTable(currentPage);
setupPagination();