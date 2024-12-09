document.addEventListener("DOMContentLoaded", () => {
    const stockData = [
        { Inventory_ID: 1, User_ID: "U001", Inventory_Date: "2024-12-05" },
        { Inventory_ID: 2, User_ID: "U002", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 1, User_ID: "U001", Inventory_Date: "2024-12-05" },
        { Inventory_ID: 2, User_ID: "U002", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 1, User_ID: "U001", Inventory_Date: "2024-12-05" },
        { Inventory_ID: 2, User_ID: "U002", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 1, User_ID: "U001", Inventory_Date: "2024-12-05" },
        { Inventory_ID: 1, User_ID: "U001", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 1, User_ID: "U001", Inventory_Date: "2024-12-05" },
        { Inventory_ID: 2, User_ID: "U001", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 1, User_ID: "U00", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 2, User_ID: "U002", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 2, User_ID: "U002", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 2, User_ID: "U002", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 2, User_ID: "U002", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 2, User_ID: "U002", Inventory_Date: "2024-12-06" },
        { Inventory_ID: 2, User_ID: "U002", Inventory_Date: "2024-12-06" },
        
    ];

    const itemsPerPage = 10; // Số mục mỗi trang
    let currentPage = 1;
    let filteredData = stockData; // Dữ liệu đã lọc

    // Lọc dữ liệu theo ngày
    function filterDataByDate(selectedDate) {
        return stockData.filter(item => item.Inventory_Date === selectedDate);
    }

    // Hiển thị bảng với dữ liệu
    function displayTable(page) {
        const start = (page - 1) * itemsPerPage;
        const end = start + itemsPerPage;
        const currentOrders = filteredData.slice(start, end);

        const tableBody = document.querySelector('#stockTable tbody');
        tableBody.innerHTML = ""; // Xóa nội dung cũ

        currentOrders.forEach(order => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${order.Inventory_ID}</td>
                <td>${order.User_ID}</td>
                <td>${order.Inventory_Date}</td>
                <td><a class="button" style="text-decoration: none; color:##007BFF;" href="view.html">Xem chi tiết</a></td>
            `;
            tableBody.appendChild(row);
        });

        // Gắn lại sự kiện cho các nút "Xem"
        document.querySelectorAll(".viewButton").forEach((button) => {
            button.addEventListener("click", (event) => {
                const userId = event.target.dataset.userId;
                localStorage.setItem('selectedUserId', userId);
                window.location.href = "view.html";
            });
        });
    }

    // Thiết lập phân trang
    function setupPagination() {
        const totalPages = Math.ceil(filteredData.length / itemsPerPage);
        const paginationDiv = document.getElementById('pagination');
        paginationDiv.innerHTML = ""; // Xóa nội dung cũ

        // Nút "Trước"
        const prevPage = document.createElement('a');
        prevPage.href = "#";
        prevPage.textContent = "Trước";
        if (currentPage === 1) {
            prevPage.classList.add('disabled'); // Vô hiệu hóa nếu đang ở trang đầu
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

        // Nút các trang
        for (let i = 1; i <= totalPages; i++) {
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

        // Nút "Tiếp"
        const nextPage = document.createElement('a');
        nextPage.href = "#";
        nextPage.textContent = "Tiếp";
        if (currentPage === totalPages) {
            nextPage.classList.add('disabled'); // Vô hiệu hóa nếu đang ở trang cuối
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

    // Lấy ngày hiện tại
    const today = new Date().toISOString().split('T')[0];
    const datePicker = document.getElementById("datePicker");
    datePicker.value = today;

    // Lọc dữ liệu theo ngày và hiển thị bảng ban đầu
    filteredData = filterDataByDate(today);
    displayTable(currentPage);
    setupPagination();

    // Xử lý thay đổi ngày
    datePicker.addEventListener("change", (e) => {
        const selectedDate = e.target.value;
        filteredData = selectedDate ? filterDataByDate(selectedDate) : stockData;
        currentPage = 1; // Đặt lại về trang 1 khi thay đổi ngày
        displayTable(currentPage);
        setupPagination();
    });

    // Nút "Kết Ca"
    document.getElementById("endShiftButton").addEventListener("click", () => {
        window.location.href = "endshift.html";
    });
});
