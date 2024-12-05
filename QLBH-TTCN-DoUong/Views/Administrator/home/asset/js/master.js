document.querySelector('.menu-icon').addEventListener('click', function () {
    const sidebar = document.querySelector('.sidebar');
    const headerName = document.querySelector(".admin-title");
    const spanName = document.getElementById("span_name");

    sidebar.classList.toggle('collapsed');
    sidebar.classList.toggle('expanded');

    // Đổi nội dung hiển thị tên admin
    if (headerName.style.display === 'none' || headerName.style.display === '') {
        headerName.style.display = 'block';
        spanName.style.display = 'none';
    } else {
        headerName.style.display = 'none';
        spanName.style.display = 'block';
    }
});
document.addEventListener("DOMContentLoaded", function() {
    // Lấy tất cả các mục trong sidebar
    const links = document.querySelectorAll('.sidebar ul li a');

    links.forEach(link => {
        // Gắn sự kiện click cho mỗi mục
        link.addEventListener('click', function() {
            // Xóa lớp 'active' khỏi tất cả các mục
            links.forEach(item => item.classList.remove('active'));
            // Thêm lớp 'active' vào mục được nhấp
            this.classList.add('active');
        });
    });
});             