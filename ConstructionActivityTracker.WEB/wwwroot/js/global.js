var GridHelper = (function () {
    function initGrid(options) {
        var defaultOptions = {
            pageSize: 10,
            remoteOperations: true,
            pager: {
                showPageSizeSelector: true,
                allowedPageSizes: [5, 10, 25],
                showInfo: true
            },
            sorting: {
                mode: "multiple"
            },
            filterRow: { visible: true },
            gridOptions: {
                columnAutoWidth: true,
                allowColumnResizing: true,
                allowColumnReordering: true,
                headerFilter: { visible: true },
                grouping: { autoExpandAll: false }
            }
        };

        var settings = $.extend(true, {}, defaultOptions, options);

        var dataStore = new DevExpress.data.CustomStore({
            key: settings.key || "id",
            load: function (loadOptions) {
                var deferred = $.Deferred();
                var params = {};

                var skip = loadOptions.skip || 0;
                var take = loadOptions.take || settings.pageSize;
                var pageIndex = take ? Math.floor(skip / take) : 0;

                params.PageIndex = pageIndex;
                params.PageSize = take;

                if (loadOptions.sort) {
                    params.sort = JSON.stringify(loadOptions.sort);
                }
                if (loadOptions.filter) {
                    params.filter = JSON.stringify(loadOptions.filter);
                }

                $.ajax({
                    url: settings.apiUrl,
                    dataType: "json",
                    data: params,
                    crossDomain: true
                }).done(function (result) {
                    deferred.resolve(result.items, { totalCount: result.count });
                }).fail(function () {
                    deferred.reject("Veri yükleme hatası!");
                });
                return deferred.promise();
            }
        });

        $("#" + settings.containerId).dxDataGrid($.extend({
            dataSource: dataStore,
            remoteOperations: settings.remoteOperations,
            columns: settings.columns,
            paging: {
                enabled: true,
                pageSize: settings.pageSize
            },
            pager: settings.pager,
            sorting: settings.sorting,
            filterRow: settings.filterRow,
            columnChooser: settings.columnChooser
        }, settings.gridOptions));
    }

    return {
        initGrid: initGrid
    };
})();


$.ajaxSetup({
    beforeSend: function (xhr) {
        var token = localStorage.getItem("accessToken");
        if (token) {
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        }
    },
    xhrFields: { withCredentials: true }
});

(function () {
    window.showError = function (message) {
        Swal.fire({
            icon: 'error',
            title: 'Hata',
            text: message
        });
    };

    window.showSuccess = function (message) {
        Swal.fire({
            icon: 'success',
            title: 'Başarılı',
            text: message
        });
    };

    window.confirmAction = function (message, onConfirm) {
        Swal.fire({
            title: 'Emin misiniz?',
            text: message,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Evet',
            cancelButtonText: 'İptal'
        }).then((result) => {
            if (result.isConfirmed) {
                onConfirm();
            }
        });
    };
})();

