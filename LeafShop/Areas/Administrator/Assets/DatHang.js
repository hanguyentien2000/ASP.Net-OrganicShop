//load dữ liệu lên form sửa
function loadData(id) {
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/DatHang/Index',
        success: function (response) {
            $("#madh1").val(response.MaDanhMucBlog);
            $("#makh1").val(response.TenDanhMucBlog);
            $("#manv1").val(response.TenDanhMucBlog);
            $("#tongtien1").val(response.TenDanhMucBlog);
            $("#ghichu1").val(response.TenDanhMucBlog);
            $("#diachi1").val(response.TenDanhMucBlog);
            response.TrangThai ? $("#trangthai0").prop("checked", true) : $("#trangthai1").prop("checked", true);
        },
        error: function (response) {
            //debugger;  
            alert("Error has occurred..");
        }
    });
}


//gửi ajax sửa đơn đặt hàng
function capNhatDatHang() {
    let data = {};
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/DatHang/Update',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            $("#update-message").html(response.message);
            if (response.status == true) {
                $("#update-message").addClass("text-warning");
                setTimeout(function () {
                    //    window.location.replace("/Administrator/DatHang/Index");
                    location.reload();
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
function deleteItem(id) {
    $("#delete-madh").val(id);
}

function xoaDatHang() {
    let id = $("#delete-madh").val();
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/DatHang/Delete',
        success: function (response) {
            if (response.status == true) {
                $("#row-" + id).remove();
            //    window.location.replace("/Administrator/DatHang/Index");
                location.reload();
            }
        },
        error: function (response) {
            alert("Error has occurred..");
            console.log(response);
        }
    });
}