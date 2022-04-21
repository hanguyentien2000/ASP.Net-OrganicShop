//load dữ liệu lên form sửa
function loadData(id) {
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/DanhMuc/Index',
        success: function (response) {
            $("#madm1").val(response.MaDanhMuc);
            $("#tendanhmuc1").val(response.TenDanhMuc);
            $("#parentId1").val(response.ParentId);
        },
        error: function (response) {
            //debugger;  
            alert("Error has occurred..");
        }
    });
}

//gửi ajax thêm danh mục
function themDanhMuc() {
    let data = {};
    let formData = $('#add-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/DanhMuc/Create',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                $("#add-message").addClass("text-warning");
                setTimeout(function () {
                    location.reload();
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


//gửi ajax sửa danh mục
function capNhatDanhMuc() {
    let data = {};
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/DanhMuc/Update',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
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
            console.log(response);
        }
    });
    return false;
}

//load data lên form xóa
function deleteData(id) {
    $("#delete-madm").val(id);
}

function xoaDanhMuc() {
    let id = $("#delete-madm").val();
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/DanhMuc/Delete',
        success: function (response) {
            if (response.status == true) {
                $("#row-" + id).remove();
                location.reload();
            }
        },
        error: function (response) {
            alert("Error has occurred..");
        }
    });
}