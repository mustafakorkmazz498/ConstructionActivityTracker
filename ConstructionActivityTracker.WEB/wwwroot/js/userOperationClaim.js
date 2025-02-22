$(function () {
    var userOperationClaimsApiUrl = "https://localhost:44309/api/UserOperationClaims";
    var usersApiUrl = "https://localhost:44309/api/Users";
    var operationClaimsApiUrl = "https://localhost:44309/api/OperationClaims";

    ToolbarHelper.initToolbar({
        containerId: "gridToolbar",
        buttons: [
            {
                id: "btnAdd",
                text: "Ekle",
                icon: "fa fa-plus",
                onClick: function () {
                    $("#userOperationClaimForm")[0].reset();
                    $("#userOperationClaimId").val(0);
                    $("#userId").val("");
                    $("#operationClaimId").val("");
                    $("#userOperationClaimModalLabel").text("Yeni Yetki Ekle");
                    $("#userOperationClaimModal").modal("show");
                }
            },
            {
                id: "btnEdit",
                text: "Güncelle",
                icon: "fa fa-edit",
                onClick: function () {
                    var grid = $("#userOperationClaimsGrid").dxDataGrid("instance");
                    var selectedData = grid.getSelectedRowKeys();
                    if (selectedData.length === 0) {
                        showError("Lütfen güncellemek için bir kayıt seçiniz.");
                        return;
                    }
                    var rowData = grid.getSelectedRowsData()[0];
                    $("#userOperationClaimId").val(rowData.id);
                    $("#userId").val(rowData.userId);
                    $("#operationClaimId").val(rowData.operationClaimId);
                    $("#userOperationClaimModalLabel").text("Yetki Güncelle");
                    $("#userOperationClaimModal").modal("show");
                }
            },
            {
                id: "btnDelete",
                text: "Sil",
                icon: "fa fa-trash",
                onClick: function () {
                    var grid = $("#userOperationClaimsGrid").dxDataGrid("instance");
                    var selectedData = grid.getSelectedRowKeys();
                    if (selectedData.length === 0) {
                        showError("Lütfen silmek için bir kayıt seçiniz.");
                        return;
                    }
                    confirmAction("Seçili yetkiyi silmek istediğinize emin misiniz?", function () {
                        var rowData = grid.getSelectedRowsData()[0];
                        $.ajax({
                            url: userOperationClaimsApiUrl + "/" + rowData.id,
                            type: "DELETE",
                            success: function () {
                                showSuccess("Yetki silindi.");
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
        containerId: "userOperationClaimsGrid",
        apiUrl: userOperationClaimsApiUrl,
        key: "id",
        columns: [
            { dataField: "id", caption: "ID", visible: false },
            { dataField: "userId", visible: false },
            { dataField: "fullName", caption: "Kullanıcı" },
            { dataField: "operationClaimId", visible: false },
            { dataField: "operationClaimName", caption: "Yetki Adı" }
        ],
        pageSize: 10,
        gridOptions: {
            selection: { mode: "single" }
        }
    });

    function loadUsers() {
        $.ajax({
            url: usersApiUrl,
            dataType: "json",
            data: { PageIndex: 0, PageSize: 100 },
            success: function (result) {
                var optionsHtml = '<option value="">Seçiniz</option>';
                $.each(result.items, function (index, item) {
                    var fullName = item.fullName || (item.firstName + " " + item.lastName);
                    optionsHtml += '<option value="' + item.id + '">' + fullName + '</option>';
                });
                $("#userId").html(optionsHtml);
            },
            error: function () {
                showError("Kullanıcılar yüklenirken hata oluştu.");
            }
        });
    }

    function loadOperationClaims() {
        $.ajax({
            url: operationClaimsApiUrl,
            dataType: "json",
            data: { PageIndex: 0, PageSize: 100 },
            success: function (result) {
                var optionsHtml = '<option value="">Seçiniz</option>';
                $.each(result.items, function (index, item) {
                    optionsHtml += '<option value="' + item.id + '">' + item.name + '</option>';
                });
                $("#operationClaimId").html(optionsHtml);
            },
            error: function () {
                showError("Yetkiler yüklenirken hata oluştu.");
            }
        });
    }

    loadUsers();
    loadOperationClaims();

    $("#btnSave").on("click", function () {
        var id = $("#userOperationClaimId").val();
        var userId = $("#userId").val();
        var operationClaimId = $("#operationClaimId").val();

        if (!userId || !operationClaimId) {
            showError("Lütfen gerekli alanları doldurunuz.");
            return;
        }

        var data = {
            UserId: parseInt(userId),
            OperationClaimId: parseInt(operationClaimId)
        };

        if (id == 0 || id === "0") {
            $.ajax({
                url: userOperationClaimsApiUrl,
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function () {
                    $("#userOperationClaimModal").modal("hide");
                    showSuccess("Yetki eklendi.");
                    $("#userOperationClaimsGrid").dxDataGrid("instance").refresh();
                },
                error: function () {
                    showError("Bu işleme yetkiniz yok.");
                }
            });
        } else {
            data.Id = id;
            $.ajax({
                url: userOperationClaimsApiUrl,
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function () {
                    $("#userOperationClaimModal").modal("hide");
                    showSuccess("Yetki güncellendi.");
                    $("#userOperationClaimsGrid").dxDataGrid("instance").refresh();
                },
                error: function () {
                    showError("Bu işleme yetkiniz yok.");
                }
            });
        }
    });
});
