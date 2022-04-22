//load dữ liệu
function loadData(id) {
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: 'Index',
        success: function (response) {
            $("#mabv1").val(response.MaBaiViet);
            $("#manv1").val(response.MaNhanVien);
            $("#madmblog1").val(response.MaDanhMucBlog);
            $("#tieude1").val(response.TieuDe);
            $("#tomtat1").val(response.Tomtat);
            //$("#noidung1").val(response.Noidung);
            CKEDITOR.instances.noidung1.setData(response.Noidung);
            $("#output2").attr("src", response.Anh);
        },
        error: function (response) {
            alert("Error has occurred..");
        }
    });
}

//Thêm mới blog
function themBlog() {
    let blog = {};
    let form = new FormData();
    let formData = $('#add-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        blog["" + value.name + ""] = value.value;
    });
    blog.Noidung = CKEDITOR.instances.noidung.getData();
    let image = $("#uploadhinh")[0].files[0];
    form.append("blog", JSON.stringify(blog));
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
function capNhatBlog() {
    let blog = {};
    let form = new FormData();
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        blog["" + value.name + ""] = value.value;
    });
    blog.Noidung = CKEDITOR.instances.noidung1.getData();
    let image = $("#uploadhinh1")[0].files[0];
    form.append("blog", JSON.stringify(blog));
    form.append("uploadhinh1", image);
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
function deleteData(id) {
    $("#delete-mablog").val(id);
}

function xoaBlog() {
    let id = $("#delete-mablog").val();
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: 'Delete',
        success: function (response) {
            if (response.status == true) {
                $(".cancelPopup").click();
                $("#row-" + id).remove();
                setTimeout(function () {
                    //    window.location.replace("/Administrator/Blogs/Index");
                    location.reload();
                }, 1000)
            }
        },
        error: function (response) {
            alert("Error has occurred..");
        }
    });
}