$(function () {
    var apiUrl = "https://localhost:44309/api/Users";
    GridHelper.initGrid({
        containerId: "userGrid",
        apiUrl: apiUrl,
        key: "id",
        columns: [
            { dataField: "firstName", caption: "Ad" },
            { dataField: "lastName", caption: "Soyad" },
            { dataField: "email", caption: "Email" },
        ],
        pageSize: 10,
        gridOptions: {
            selection: { mode: "single" }
        }
    });
});
