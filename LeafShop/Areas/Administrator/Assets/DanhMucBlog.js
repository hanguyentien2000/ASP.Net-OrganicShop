//load dữ liệu lên form sửa
function loadData(id) {
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/DanhMucBlogs/Index',
        success: function (response) {
            $("#madmblog1").val(response.MaDanhMucBlog);
            $("#tenDanhMucBlog1").val(response.TenDanhMucBlog);
        },
        error: function (response) {
            //debugger;  
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
}

//gửi ajax thêm danh mục
function themDanhMucBlog() {
    let data = {};
    let formData = $('#add-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/DanhMucBlogs/Create',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            if (response.status == true) {
                $("#add-message").addClass("text-warning");
                setTimeout(function () {
                //    window.location.replace("/Administrator/DanhMucBlogs/Index");
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
function capNhatDanhMucBlog() {
    let data = {};
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/DanhMucBlogs/Update',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            $("#update-message").html(response.message);
            if (response.status == true) {
                $("#update-message").addClass("text-warning");
                setTimeout(function () {
                    //    window.location.replace("/Administrator/DanhMucBlogs/Index");
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
    $("#delete-madmblog").val(id);
}

function xoaDanhMucBlog() {
    let id = $("#delete-madmblog").val();
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/DanhMucBlogs/Delete',
        success: function (response) {
            if (response.status == true) {
                $("#row-" + id).remove();
            //    window.location.replace("/Administrator/DanhMucBlogs/Index");
                location.reload();

            }
        },
        error: function (response) {
            alert("Error has occurred..");
            console.log(response);
        }
    });
}