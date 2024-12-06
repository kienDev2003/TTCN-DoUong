const urlParams = new URLSearchParams(window.location.search);
const userID = urlParams.get('userID'); 

if (userID) {
    document.getElementById('btnCRUD').setAttribute('onclick', 'update()');

    document.getElementById('btnCRUD').value = "Cập nhật";

    document.getElementById('txtPassword').style.display = "none";

    getUser(userID);
}
function getUser(userID) {
    $.ajax({
        type: "POST",
        url: "edit.aspx/Get",
        data: JSON.stringify({ userID: userID }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (respone) {
            var user = respone.d;

            document.getElementById("txtFullName").value = user.fullName;
            document.getElementById("txtUserName").value = user.userName;
            document.getElementById("txtPassword").value = user.password;
            document.getElementById("txtEmail").value = user.email;
            document.getElementById("txtPhoneNumber").value = user.phone.trim();

            var selectElement = document.getElementById('content_cboRole');
            for (var i = 0; i < selectElement.options.length; i++) {
                if (selectElement.options[i].value === String(user.roleId)) {
                    selectElement.selectedIndex = i;
                    break;
                }
            }

        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}

function checkUser(fullName, userName, password, email, phone, roleID) {
    if (fullName === "" || userName === "" || password === "" || email === "" || phone === "") {
        Swal.fire({
            title: 'Thông báo!',
            text: "Vui lòng điền đầy đủ các trường",
            icon: 'error',
            confirmButtonText: 'OK'
        })
        return false;
    }

    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    if (!emailPattern.test(email)) {
        Swal.fire({
            title: 'Thông báo!',
            text: "Email không hợp lệ!",
            icon: 'error',
            confirmButtonText: 'OK'
        })
        return false;
    }

    var phonePattern = /^[0-9]{10}$/;
    if (!phonePattern.test(phone)) {
        Swal.fire({
            title: 'Thông báo!',
            text: "Số điện thoại không hợp lệ!",
            icon: 'error',
            confirmButtonText: 'OK'
        })
        return false;
    }

    return true;
}
function insert() {
    var fullName = document.getElementById("txtFullName").value;
    var userName = document.getElementById("txtUserName").value;
    var password = document.getElementById("txtPassword").value;
    var email = document.getElementById("txtEmail").value;
    var phone = document.getElementById("txtPhoneNumber").value;

    var selectElement = document.getElementById('content_cboRole');
    var selectedOption = selectElement.options[selectElement.selectedIndex];

    var roleID = selectedOption.value;

    var validate = checkUser(fullName, userName, password, phone, roleID);
    if (!validate) return;

    var user = {
        fullName: fullName,
        userName: userName,
        password: password,
        email: email,
        phone: phone,
        roleId: roleID
    }

    $.ajax({
        type: "POST",
        url: "edit.aspx/Insert",
        data: JSON.stringify({ user: user }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (respone) {
            if (respone.d === true) {
                Swal.fire({
                    title: 'Thông báo!',
                    text: "Thêm tài khoản thành công",
                    icon: 'success',
                    confirmButtonText: 'OK'
                }).then(function () {
                    window.location.href = "./";
                });
            }
            else {
                Swal.fire({
                    title: 'Thông báo!',
                    text: "Tài khoản bị trùng. Vui lòng nhập tài khoản khác",
                    icon: 'error',
                    confirmButtonText: 'OK'
                })
            }
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}

function update() {
    var fullName = document.getElementById("txtFullName").value;
    var userName = document.getElementById("txtUserName").value;
    var password = document.getElementById("txtPassword").value;
    var email = document.getElementById("txtEmail").value;
    var phone = document.getElementById("txtPhoneNumber").value;

    var selectElement = document.getElementById('content_cboRole');
    var selectedOption = selectElement.options[selectElement.selectedIndex];

    var roleID = selectedOption.value;

    var validate = checkUser(fullName, userName, password, phone, roleID);
    if (!validate) return;

    var user = {
        Id: userID,
        fullName: fullName,
        userName: userName,
        password: password,
        email: email,
        phone: phone,
        roleId: roleID
    }

    $.ajax({
        type: "POST",
        url: "edit.aspx/Update",
        data: JSON.stringify({ user: user }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (respone) {
            if (respone.d === true) {
                Swal.fire({
                    title: 'Thông báo!',
                    text: "Thay đổi thông tin tài khoản thành công",
                    icon: 'success',
                    confirmButtonText: 'OK'
                }).then(function () {
                    window.location.href = "./";
                });
            }
            else {
                Swal.fire({
                    title: 'Thông báo!',
                    text: "Lỗi",
                    icon: 'error',
                    confirmButtonText: 'OK'
                })
            }
        },
        error: function (xhr, status, error) {
            console.error(xhr.responseText);
        }
    });
}