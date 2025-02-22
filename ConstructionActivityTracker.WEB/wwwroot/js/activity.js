$(function () {
    var activitiesApiUrl = "https://localhost:44309/api/Activities";
    var activityTypesApiUrl = "https://localhost:44309/api/ActivityTypes";

    ToolbarHelper.initToolbar({
        containerId: "gridToolbar",
        buttons: [
            {
                id: "btnAdd",
                text: "Ekle",
                icon: "fa fa-plus",
                onClick: function () {
                    $("#activityForm")[0].reset();
                    $("#activityId").val(0);
                    $("#activityTypeId").val("");
                    $("#activityModalLabel").text("Yeni Aktivite Ekle");
                    $("#activityModal").modal("show");
                }
            },
            {
                id: "btnEdit",
                text: "Güncelle",
                icon: "fa fa-edit",
                onClick: function () {
                    var grid = $("#activitiesGrid").dxDataGrid("instance");
                    var selectedData = grid.getSelectedRowKeys();
                    if (selectedData.length === 0) {
                        showError("Lütfen güncellemek için bir kayıt seçiniz.");
                        return;
                    }
                    var rowData = grid.getSelectedRowsData()[0];
                    $("#activityId").val(rowData.id);
                    $("#activityDate").val(formatDateInput(rowData.activityDate));
                    $("#description").val(rowData.description);
                    $("#activityTypeId").val(rowData.activityTypeId);
                    $("#activityModalLabel").text("Aktivite Güncelle");
                    $("#activityModal").modal("show");
                }
            },
            {
                id: "btnDelete",
                text: "Sil",
                icon: "fa fa-trash",
                onClick: function () {
                    var grid = $("#activitiesGrid").dxDataGrid("instance");
                    var selectedData = grid.getSelectedRowKeys();
                    if (selectedData.length === 0) {
                        showError("Lütfen silmek için bir kayıt seçiniz.");
                        return;
                    }
                    confirmAction("Seçili kaydı silmek istediğinize emin misiniz?", function () {
                        var rowData = grid.getSelectedRowsData()[0];
                        $.ajax({
                            url: activitiesApiUrl + "/" + rowData.id,
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
        containerId: "activitiesGrid",
        apiUrl: activitiesApiUrl,
        key: "id",
        columns: [
            { dataField: "id", caption: "ID", visible: false },
            { dataField: "userId", visible: false },
            { dataField: "fullName", caption: "Kullanıcı" },
            { dataField: "activityTypeId", visible: false },
            { dataField: "activityTypeName", caption: "Faaliyet Tipi Adı" },
            { dataField: "activityTypeCode", caption: "Faaliyet Tipi Kodu" },
            { dataField: "activityDate", caption: "Tarih", dataType: "date", format: "dd-MM-yyyy" },
            { dataField: "description", caption: "Açıklama" }
        ],
        pageSize: 10,
        gridOptions: {
            selection: { mode: "single" }
        }
    });

    function loadActivityTypes() {
        $.ajax({
            url: activityTypesApiUrl,
            dataType: "json",
            data: { PageIndex: 0, PageSize: 100 },
            success: function (result) {
                var optionsHtml = '<option value="">Seçiniz</option>';
                $.each(result.items, function (index, item) {
                    optionsHtml += '<option value="' + item.id + '">' + item.code + ' - ' + item.name + '</option>';
                });
                $("#activityTypeId").html(optionsHtml);
            },
            error: function () {
                showError("Faaliyet tipleri yüklenirken hata oluştu.");
            }
        });
    }

    loadActivityTypes();

    function formatDateInput(dateString) {
        if (!dateString) return "";
        var date = new Date(dateString);
        var year = date.getFullYear();
        var month = ("0" + (date.getMonth() + 1)).slice(-2);
        var day = ("0" + date.getDate()).slice(-2);
        return year + "-" + month + "-" + day;
    }

    $("#btnSave").on("click", function () {
        var id = $("#activityId").val();
        var activityTypeId = $("#activityTypeId").val();
        var activityDate = $("#activityDate").val();
        var description = $("#description").val();

        if (!activityTypeId || !activityDate) {
            showError("Lütfen gerekli alanları doldurunuz.");
            return;
        }

        var data = {
            ActivityTypeId: parseInt(activityTypeId),
            ActivityDate: activityDate,
            Description: description
        };

        if (id == 0 || id === "0") {
            $.ajax({
                url: activitiesApiUrl,
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function () {
                    $("#activityModal").modal("hide");
                    showSuccess("Kayıt eklendi.");
                    $("#activitiesGrid").dxDataGrid("instance").refresh();
                },
                error: function () {
                    showError("Bu işleme yetkiniz yok.");
                }
            });
        } else {
            data.Id = id;
            $.ajax({
                url: activitiesApiUrl,
                type: "PUT",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: function () {
                    $("#activityModal").modal("hide");
                    showSuccess("Kayıt güncellendi.");
                    $("#activitiesGrid").dxDataGrid("instance").refresh();
                },
                error: function () {
                    showError("Bu işleme yetkiniz yok.");
                }
            });
        }
    });
});
