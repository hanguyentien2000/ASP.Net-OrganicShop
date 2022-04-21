//load dữ liệu
function loadData(id) {
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: 'SanPham/Index',
        success: function (response) {
            $("#masp1").val(response.MaSanPham);
            $("#tenSanPham1").val(response.TenSanPham);
            $("#madm1").val(response.MaDanhMuc);
            $("#math1").val(response.MaThuongHieu);
            $("#donViTinh1").val(response.DonViTinh);
            $("#soLuong1").val(response.SoLuong);
            $("#soLuongBan1").val(response.SoLuongBan);
            $("#donGia1").val(response.DonGia);
            $("#moTa1").val(response.MoTa);
            $("#ngayKhoiTao1").val(response.NgayKhoiTao);
            $("#ngayCapNhat1").val(response.NgayCapNhat);
            $("#output2").attr("src", response.HinhMinhHoa);
            response.GioiTinh ? $("#gioiTinhb").prop("checked", true) : $("#gioiTinhg").prop("checked", true);
            //$("#ngayKhoiTao1").val(new Date(parseFloat(response.NgayKhoiTao.substring(response.NgaySinh.indexOf("(") + 1, response.NgaySinh.indexOf(")")))).toString("yyyy-MM-ddThh:mm"))
            //$("#ngayCapNhat1").val(new Date(parseFloat(response.NgayCapNhat.substring(response.NgaySinh.indexOf("(") + 1, response.NgaySinh.indexOf(")")))).toString("yyyy-MM-ddThh:mm"))
        },
        error: function (response) {
            //debugger;  
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
}

//Thêm mới nhân viên
function themSanPham() {
    let sanpham = {};
    let form = new FormData();
    let formData = $('#add-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        sanpham["" + value.name + ""] = value.value;
    });
    let image = $("#uploadhinh")[0].files[0];
    form.append("sanpham", JSON.stringify(sanpham));
    form.append("uploadhinh", image);
    $.ajax({
        url: 'SanPham/Create',
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
function capNhatSanPham() {
    let sanpham = {};
    let form = new FormData();
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        sanpham["" + value.name + ""] = value.value;
    });
    let image = $("#uploadhinh1")[0].files[0];
    console.log(image);
    form.append("sanpham", JSON.stringify(sanpham));
    form.append("uploadhinh", image);
    $.ajax({
        url: 'SanPham/Update',
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
function deleteData(id) {
    $("#delete-masp").val(id);
}

function xoaSanPham() {
    let id = $("#delete-masp").val();
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: 'SanPham/Delete',
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