var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Administration/GetAllUsers"
        },
        "columns": [
            { "data": "id" },
            { "data": "username"},
            { "data": "name" },
            { "data": "cnic" },
        ]
    }); 
   

}