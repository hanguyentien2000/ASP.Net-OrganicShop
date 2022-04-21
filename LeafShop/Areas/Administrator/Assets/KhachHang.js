//load dữ liệu
function loadData(id) {
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/KhachHang/Index',
        success: function (response) {
            $("#makh1").val(response.MaKhachHang);
            $("#tenKhachHang1").val(response.TenKhachHang);
            $("#ngaySinh1").val(response.NgaySinh);
            $("#diaChiKhachHang1").val(response.DiaChiKhachHang);
            $("#email1").val(response.Email);
            $("#tenDangNhap1").val(response.TenDangNhap);
            $("#matKhau1").val(response.MatKhau);
            $("#dienThoaiKhachHang1").val(response.DienThoaiKhachHang);
            $("#output2").attr("src", response.Avatar);
            response.GioiTinh ? $("#gioiTinhb").prop("checked", true) : $("#gioiTinhg").prop("checked", true);
            response.TrangThai ? $("#trangThaib").prop("checked", true) : $("#trangThaig").prop("checked", true);

            $("#ngaySinh1").val(new Date(parseFloat(response.NgaySinh.substring(response.NgaySinh.indexOf("(") + 1, response.NgaySinh.indexOf(")")))).toString("yyyy-MM-ddThh:mm"))
        },
        error: function (response) {
            //debugger;  
            alert("Error has occurred..");
        }
    });
}

//Thêm mới nhân viên
function themKhachHang() {
    let data = {};
    let formData = $('#add-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/KhachHang/Create',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                $("#add-message").addClass("text-warning");
                setTimeout(function () {
                    window.location.replace("/Administrator/KhachHang/Index");
                }, 1000)
            } else {
                $("#add-message").addClass("text-danger");
            }
            $("#add-message").html(response.message);
        },
        error: function (response) {
            console.log(response);
        }
    });
    return false;
}


//Cập nhật nhân viên
function capNhatKhachHang() {
    let data = {};
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/KhachHang/Update',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            $("#update-message").html(response.message);
            if (response.status == true) {
                $("#update-message").addClass("text-warning");
                setTimeout(function () {
                    window.location.replace("/Administrator/KhachHang/Index");
                }, 1000)
            } else {
                $("#update-message").addClass("text-danger");
            }
        },
        error: function (response) {
            console.log(response);
        }
    });
    return false;
}

//load data lên form xóa
function deleteData(id) {
    $("#delete-makh").val(id);
}

function xoaKhachHang() {
    let id = $("#delete-makh").val();
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/KhachHang/Delete',
        success: function (response) {
            if (response.status == true) {
                $(".cancelPopup").click();
                $("#row-" + id).remove();
                setTimeout(function () {
                    window.location.replace("/Administrator/KhachHang/Index");
                }, 1000)
            }
        },
        error: function (response) {
            alert("Error has occurred..");
        }
    });
}