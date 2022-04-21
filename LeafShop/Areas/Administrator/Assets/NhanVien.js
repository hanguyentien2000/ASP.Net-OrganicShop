//load dữ liệu
function loadData(id) {
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/NhanVien/Index',
        success: function (response) {
            $("#manv1").val(response.MaNhanVien);
            $("#tenNhanVien1").val(response.TenNhanVien);
            $("#ngaySinh1").val(response.NgaySinh);
            $("#diaChi1").val(response.DiaChi);
            $("#email1").val(response.Email);
            $("#dienThoai1").val(response.DienThoai);
            $("#output2").attr("src", response.Avatar);
            response.GioiTinh ? $("#gioiTinhb").prop("checked", true) : $("#gioiTinhg").prop("checked", true);
            $("#ngaySinh1").val(new Date(parseFloat(response.NgaySinh.substring(response.NgaySinh.indexOf("(") + 1, response.NgaySinh.indexOf(")")))).toString("yyyy-MM-ddThh:mm"))
        },
        error: function (response) {
            //debugger;  
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
}

//Thêm mới nhân viên
function themNhanVien() {
    let nhanvien = {};
    let form = new FormData();
    let formData = $('#add-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        nhanvien["" + value.name + ""] = value.value;
    });
    let image = $("#uploadhinh")[0].files[0];
    form.append("nhanvien", JSON.stringify(nhanvien));
    form.append("uploadhinh", image);
    $.ajax({
        url: '/NhanVien/Create',
        type: 'POST',
        cache: false,
        processData: false,
        contentType: false,
        enctype: "multipart/form-data",
        data: form,
        async: true,
        success: function (response) {
            $("#add-message").html(response.message);
            if (response.status == true) {
                $("#add-message").addClass("text-warning");
                setTimeout(function () {
                    //    window.location.replace("/Administrator/NhanVien/Index");
                    location.reload();
                }, 1000)
            } else {
                $("#add-message").addClass("text-danger");
            }
        },
        error: function (response) {
            alert("Error has occurred..");
        }
    });
    return false;
}


//Cập nhật nhân viên
function capNhatNhanVien() {
    let nhanvien = {};
    let form = new FormData();
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        nhanvien["" + value.name + ""] = value.value;
    });
    let image = $("#uploadhinh1")[0].files[0];
    form.append("nhanvien", JSON.stringify(nhanvien));
    form.append("uploadhinh", image);
    $.ajax({
        url: '/NhanVien/Update',
        type: 'POST',
        cache: false,
        processData: false,
        contentType: false,
        enctype: "multipart/form-data",
        data: form,
        async: true,
        success: function (response) {
            $("#update-message").html(response.message);
            if (response.status == true) {
                $("#update-message").addClass("text-warning");
                setTimeout(function () {
                    location.reload();
                }, 1000)
            } else {
                $("#update-message").addClass("text-danger");
            }
        },
        error: function (response) {
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
    return false;
}

//load data lên form xóa
function deleteData(id) {
    $("#delete-manv").val(id);
}

function xoaNhanVien() {
    let id = $("#delete-manv").val();
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/NhanVien/Delete',
        success: function (response) {
            if (response.status == true) {
                $(".cancelPopup").click();
                $("#row-" + id).remove();
                setTimeout(function () {
                //    window.location.replace("/Administrator/NhanVien/Index");
                    location.reload();
                }, 1000)
            }
        },
        error: function (response) {
            alert("Error has occurred..");
        }
    });
}