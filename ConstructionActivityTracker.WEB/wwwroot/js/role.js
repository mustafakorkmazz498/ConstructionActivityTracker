$(function () {
    var apiUrl = "https://localhost:44309/api/OperationClaims";

    ToolbarHelper.initToolbar({
        containerId: "gridToolbar",
        buttons: [
            {
                id: "btnAdd",
                text: "Ekle",
                icon: "fa fa-plus",
                onClick: function () {
                    $("#roleForm")[0].reset();
                    $("#roleId").val(0);
                    $("#roleModalLabel").text("Yeni Faaliyet Tipi Ekle");
                    $("#roleModal").modal("show");
                }
            },
            {
                id: "btnEdit",
                text: "Güncelle",
                icon: "fa fa-edit",
                onClick: function () {
                    var grid = $("#roleGrid").dxDataGrid("instance");
                    var selectedData = grid.getSelectedRowKeys();
                    if (selectedData.length === 0) {
                        showError("Lütfen güncellemek için bir kayıt seçiniz.");
                        return;
                    }
                    var rowData = grid.getSelectedRowsData()[0];
                    $("#roleId").val(rowData.id);
                    $("#roleName").val(rowData.name);
                    $("#roleCode").val(rowData.code);
                    $("#roleModalLabel").text("Faaliyet Tipi Güncelle");
                    $("#roleModal").modal("show");
                }
            },
            {
                id: "btnDelete",
                text: "Sil",
                icon: "fa fa-trash",
                onClick: function () {
                    var grid = $("#roleGrid").dxDataGrid("instance");
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
                            error: function () {
                                showError("Bu işleme yetkiniz yok.");
                            }
                        });
                    });
                }
            }
        ]
    });

    GridHelper.initGrid({
        containerId: "roleGrid",
        apiUrl: apiUrl,
        key: "id",
        columns: [
            { dataField: "name", caption: "Rol Adı" },
        ],
        pageSize: 10,
        gridOptions: {
            selection: { mode: "single" }
        }
    });

    $("#btnSave").on("click", function () {
        var id = $("#roleId").val();
        var name = $("#roleName").val();

        if (!name) {
            showError("Lütfen tüm alanları doldurunuz.");
            return;
        }

        var data = {
            Name: name
        };

        if (id == 0 || id === "0") {
            $.ajax({
                url: apiUrl,
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function () {
                    $("#roleModal").modal("hide");
                    showSuccess("Kayıt eklendi.");
                    $("#roleGrid").dxDataGrid("instance").refresh();
                },
                error: function () {
                    showError("Bu işleme yetkiniz yok.");
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
                    $("#roleModal").modal("hide");
                    showSuccess("Kayıt güncellendi.");
                    $("#roleGrid").dxDataGrid("instance").refresh();
                },
                error: function () {
                    showError("Bu işleme yetkiniz yok.");
                }
            });
        }
    });
});
