$(function () {
    var apiUrl = "https://localhost:44309/api/ActivityTypes";

    ToolbarHelper.initToolbar({
        containerId: "gridToolbar",
        buttons: [
            {
                id: "btnAdd",
                text: "Ekle",
                icon: "fa fa-plus",
                onClick: function () {
                    $("#activityTypeForm")[0].reset();
                    $("#activityTypeId").val(0);
                    $("#activityTypeModalLabel").text("Yeni Faaliyet Tipi Ekle");
                    $("#activityTypeModal").modal("show");
                }
            },
            {
                id: "btnEdit",
                text: "Güncelle",
                icon: "fa fa-edit",
                onClick: function () {
                    var grid = $("#activityTypeGrid").dxDataGrid("instance");
                    var selectedData = grid.getSelectedRowKeys();
                    if (selectedData.length === 0) {
                        showError("Lütfen güncellemek için bir kayıt seçiniz.");
                        return;
                    }
                    var rowData = grid.getSelectedRowsData()[0];
                    $("#activityTypeId").val(rowData.id);
                    $("#activityTypeName").val(rowData.name);
                    $("#activityTypeCode").val(rowData.code);
                    $("#activityTypeModalLabel").text("Faaliyet Tipi Güncelle");
                    $("#activityTypeModal").modal("show");
                }
            },
            {
                id: "btnDelete",
                text: "Sil",
                icon: "fa fa-trash",
                onClick: function () {
                    var grid = $("#activityTypeGrid").dxDataGrid("instance");
                    var selectedData = grid.getSelectedRowKeys();
                    if (selectedData.length === 0) {
                        showError("Lütfen silmek için bir kayıt seçiniz.");
                        return;
                    }
                    confirmAction("Seçili kaydı silmek istediğinize emin misiniz?", function () {
                        var rowData = grid.getSelectedRowsData()[0];
                        $.ajax({
                            url: apiUrl + "/" + rowData.id,
                            type: "DELETE",
                            success: function () {
                                showSuccess("Kayıt silindi.");
                                grid.refresh();
                            },
                            error: function (xhr) {
                                var message = (xhr.responseJSON && xhr.responseJSON.errorMessage) ?
                                    xhr.responseJSON.errorMessage :
                                    "Bu işlemi gerçekleştirmek için yetkiniz yok.";
                                showError(message);
                            }
                        });
                    });
                }
            }
        ]
    });

    GridHelper.initGrid({
        containerId: "activityTypeGrid",
        apiUrl: apiUrl,
        key: "id",
        columns: [
            { dataField: "name", caption: "Faaliyet Tipi" },
            { dataField: "code", caption: "Kod" }
        ],
        pageSize: 10,
        gridOptions: {
            selection: { mode: "single" }
        }
    });

    $("#btnSave").on("click", function () {
        var id = $("#activityTypeId").val();
        var name = $("#activityTypeName").val();
        var code = $("#activityTypeCode").val();

        if (!name || !code) {
            showError("Lütfen tüm alanları doldurunuz.");
            return;
        }

        var data = {
            Name: name,
            Code: code
        };

        if (id == 0 || id === "0") {
            $.ajax({
                url: apiUrl,
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function () {
                    $("#activityTypeModal").modal("hide");
                    showSuccess("Kayıt eklendi.");
                    $("#activityTypeGrid").dxDataGrid("instance").refresh();
                },
                error: function (xhr) {
                    var message = (xhr.responseJSON && xhr.responseJSON.errorMessage) ?
                        xhr.responseJSON.errorMessage :
                        "Bu işlemi gerçekleştirmek için yetkiniz yok.";
                    showError(message);
                }
            });
        } else {
            data.Id = id;
            $.ajax({
                url: apiUrl,
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function () {
                    $("#activityTypeModal").modal("hide");
                    showSuccess("Kayıt güncellendi.");
                    $("#activityTypeGrid").dxDataGrid("instance").refresh();
                },
                error: function (xhr) {
                    var message = (xhr.responseJSON && xhr.responseJSON.errorMessage) ?
                        xhr.responseJSON.errorMessage :
                        "Bu işlemi gerçekleştirmek için yetkiniz yok.";
                    showError(message);
                }
            });
        }
    });
});
