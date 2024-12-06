var btnlogin = document.getElementById("btnLogin");
btnlogin.addEventListener('click', function () {
    var user = document.getElementById("user").value;
    var password = document.getElementById("password").value;
    data = {
        Username: user,
        Password: password
    }
    console.log(data)
    $.ajax({
        type: "POST",
        url: "http://kienmobile.ddns.net:8888/api/account/login",
        data: JSON.stringify(data),// dữ liệu cần gửui
        contentType: "application/json; charset=utf-8", //kiểu dữ liệu gửi đi 
        crossDomain: true,
        dataType: "json",// kiểu dữ liệu trả về 
        success: function (response) {
            console.log(response.data.UserName)
            if(response.data.UserName!= null)
            {
                console.log(response)
                console.log(data)
                window.location.href='index.html'
            }
            else{
                console.log(" sai thông tin")
                alert("xem lại ")
            }
        },
        error: function (jqXHR, status) {
            // error handler
            console.log(jqXHR);
            console.log('fail' + status.code);
        }
    });
})