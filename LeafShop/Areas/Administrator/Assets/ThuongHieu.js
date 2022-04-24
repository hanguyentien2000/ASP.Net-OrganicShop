//load dữ liệu
function loadItem(id) {
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: 'Index',
        success: function (response) {
            $("#math1").val(response.MaThuongHieu);
            $("#tenThuongHieu1").val(response.TenThuongHieu);
            $("#diaChiThuongHieu1").val(response.DiaChiThuongHieu);
            $("#dienThoaiThuongHieu1").val(response.DienThoaiThuongHieu);
            //$("#moTaThuongHieu1").val(response.MoTaThuongHieu);
            CKEDITOR.instances.moTaThuongHieu1.setData(response.MoTaThuongHieu);
            $("#output2").attr("src", response.AnhThuongHieu);
        },
        error: function (response) {
            //debugger;  
            alert("Error has occurred..");
        }
    });
}

//Thêm mới nhân viên
function themThuongHieu() {
    let thuonghieu = {};
    let form = new FormData();
    let formData = $('#add-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        thuonghieu["" + value.name + ""] = value.value;
    });
    thuonghieu.MoTaThuongHieu = CKEDITOR.instances.moTaThuongHieu.getData();
    let image = $("#uploadhinh")[0].files[0];
    form.append("thuonghieu", JSON.stringify(thuonghieu));
    form.append("uploadhinh", image);
    $.ajax({
        url: 'Create',
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


//Cập nhật sản phẩm
function capNhatThuongHieu() {
    let thuonghieu = {};
    let form = new FormData();
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        thuonghieu["" + value.name + ""] = value.value;
    });
    thuonghieu.MoTaThuongHieu = CKEDITOR.instances.moTaThuongHieu1.getData();
    let image = $("#uploadhinh1")[0].files[0];
    form.append("thuonghieu", JSON.stringify(thuonghieu));
    form.append("uploadhinh", image);
    $.ajax({
        url: 'Update',
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
            alert("Error has occurred..");
        }
    });
    return false;
}

//load data lên form xóa
function deleteItem(id) {
    $("#delete-math").val(id);
}

function xoaThuongHieu() {
    let id = $("#delete-math").val();
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: 'Delete',
        success: function (response) {
            if (response.status == true) {
                $(".cancelPopup").click();
                $("#row-" + id).remove();
                setTimeout(function () {
                    location.reload();
                }, 1000)
            }
        },
        error: function (response) {
            alert("Error has occurred..");
        }
    });
}