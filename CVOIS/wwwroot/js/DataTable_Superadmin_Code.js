$(document).ready(function () {
    // ===== Ministry =====
    const ministryTable = $('#ministryDataTable').DataTable({
        dom: "<'row mb-2' <'col-md-3' <'ministry-title'>> <'col-md-7'B> <'col-md-2 text-end'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row mt-2'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            { extend: 'excelHtml5', text: '<i class="fa fa-file-excel-o"></i> Export to Excel', className: 'btn btn-sm btn-success me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'pdfHtml5', text: '<i class="fa fa-file-pdf-o"></i> Export to PDF', className: 'btn btn-sm btn-danger me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'print', text: '<i class="fa fa-print"></i> Print', className: 'btn btn-sm btn-primary me-2', exportOptions: { columns: ':not(:last-child)' } },
            {
                text: '<i class="fa fa-history"></i> Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/MinistryAuditTrail';
                }
            },
            {
                text: '<i class="fa fa-history"></i> Deleted Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/DeleteMinistryAuditTrail';
                }
            }
        ],
        paging: true, searching: true, ordering: true, responsive: true, lengthChange: false, info: true, autoWidth: false, scrollX: true,
        columnDefs: [{ targets: -1, className: 'text-center' }],
        language: {
            searchPlaceholder: "Search ministries...", search: "",
            zeroRecords: "No matching ministries found", info: "Showing _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "No entries available", paginate: { previous: "<", next: ">" }
        }
    });
    $('.ministry-title').html('<h3 class="m-0 fw-bold">Ministry List</h3>');
    $('#ministryDataTable_wrapper .dt-buttons').prepend(`
        <button type="button" class="btn btn-sm btn-primary addministrybtn me-2" data-toggle="modal" data-target="#ministryModal">
            <i class="fa fa-plus"></i> Add Ministry
        </button>
    `);


    // ===== Department =====
    const departmentTable = $('#departmentDataTable').DataTable({
        dom: "<'row mb-2' <'col-md-3' <'department-title'>> <'col-md-7'B> <'col-md-2 text-end'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row mt-2'<'col-sm-5'i><'col-sm-7'p>>",

        buttons: [
            { extend: 'excelHtml5', text: '<i class="fa fa-file-excel-o"></i> Export to Excel', className: 'btn btn-sm btn-success me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'pdfHtml5', text: '<i class="fa fa-file-pdf-o"></i> Export to PDF', className: 'btn btn-sm btn-danger me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'print', text: '<i class="fa fa-print"></i> Print', className: 'btn btn-sm btn-primary', exportOptions: { columns: ':not(:last-child)' } },
            {
                text: '<i class="fa fa-history"></i> Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/DepartmentAuditTrail';
                }
            },
            {
                text: '<i class="fa fa-history"></i> Deleted Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/DeleteDepartmentAuditTrail';
                }
            }
        ],
        paging: true, searching: true, ordering: true, responsive: true, autoWidth: false, scrollX: true,
        lengthChange: false, info: true,
        columnDefs: [{ targets: -1, className: 'text-center' }],
        language: {
            searchPlaceholder: "Search departments...", search: "",
            zeroRecords: "No matching departments found", info: "Showing _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "No entries available", paginate: { previous: "<", next: ">" }
        }
    });
    $('.department-title').html('<h3 class="m-0 fw-bold">Department List</h3>');
    $('#departmentDataTable_wrapper .dt-buttons').prepend(`
        <button type="button" class="btn btn-sm btn-primary adddepartmentbtn me-2" data-toggle="modal" data-target="#departmentModal">
            <i class="fa fa-plus"></i> Add Department
        </button>
    `);



    // ===== Organization =====
    const organizationTable = $('#organizationDataTable').DataTable({
        dom: "<'row mb-2' <'col-md-3' <'organization-title'>> <'col-md-7'B> <'col-md-2 text-end'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row mt-2'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            { extend: 'excelHtml5', text: '<i class="fa fa-file-excel-o"></i> Export to Excel', className: 'btn btn-sm btn-success me-2', exportOptions: { columns: ':not(:first-child)' } },
            { extend: 'pdfHtml5', text: '<i class="fa fa-file-pdf-o"></i> Export to PDF', className: 'btn btn-sm btn-danger me-2', exportOptions: { columns: ':not(:first-child)' } },
            { extend: 'print', text: '<i class="fa fa-print"></i> Print', className: 'btn btn-sm btn-primary', exportOptions: { columns: ':not(:first-child)' } },
            {
                text: '<i class="fa fa-history"></i> Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/OrganizationAuditTrail';
                }
            },
            {
                text: '<i class="fa fa-history"></i> Deleted Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/DeleteOrganizationAuditTrail';
                }
            }
        ],
        paging: true, searching: true, ordering: true, responsive: true, autoWidth: false, scrollX: true,
        lengthChange: false, info: true,
        columnDefs: [{ targets: -1, className: 'text-center' }],
        language: {
            searchPlaceholder: "Search organization...", search: "",
            zeroRecords: "No matching organizations found", info: "Showing _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "No entries available", paginate: { previous: "<", next: ">" }
        }
    });
    $('.organization-title').html('<h3 class="m-0 fw-bold">Organization List</h3>');
    $('#organizationDataTable_wrapper .dt-buttons').prepend(`
        <button type="button" class="btn btn-sm btn-primary me-2" data-toggle="modal" data-target="#organizationModal">
            <i class="fa fa-plus"></i> Add Organization
        </button>
    `);

    // ===== Org Level =====
    const orgLevelTable = $('#orgLevelDataTable').DataTable({
        dom: "<'row mb-2' <'col-md-3' <'orgLevel-title'>> <'col-md-7'B> <'col-md-2 text-end'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row mt-2'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            { extend: 'excelHtml5', text: '<i class="fa fa-file-excel-o"></i> Export to Excel', className: 'btn btn-sm btn-success me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'pdfHtml5', text: '<i class="fa fa-file-pdf-o"></i> Export to PDF', className: 'btn btn-sm btn-danger me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'print', text: '<i class="fa fa-print"></i> Print', className: 'btn btn-sm btn-primary', exportOptions: { columns: ':not(:last-child)' } },
            {
                text: '<i class="fa fa-history"></i> Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/LevelAuditTrail';
                }
            },
            {
                text: '<i class="fa fa-history"></i> Deleted Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/DeleteLevelAuditTrail';
                }
            }
        ],
        paging: true, searching: true, ordering: true, responsive: true, scrollX: true, autoWidth: false,
        lengthChange: false, info: true,
        columnDefs: [{ targets: -1, className: 'text-center' }],
        language: {
            searchPlaceholder: "Search levels...", search: "",
            zeroRecords: "No matching levels found", info: "Showing _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "No entries available", paginate: { previous: "<", next: ">" }
        }
    });
    $('.orgLevel-title').html('<h3 class="m-0 fw-bold">Org Level List</h3>');
    $('#orgLevelDataTable_wrapper .dt-buttons').prepend(`
        <button type="button" class="btn btn-sm btn-primary me-2" data-toggle="modal" data-target="#levelModal">
            <i class="fa fa-plus"></i> Add Org Level
        </button>
    `);

    // ===== Appointing Authority =====
    const appointingTable = $('#appointingAuthorityDataTable').DataTable({
        dom: "<'row mb-2' <'col-md-3' <'appointingAuthority-title'>> <'col-md-7'B> <'col-md-2 text-end'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row mt-2'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            { extend: 'excelHtml5', text: '<i class="fa fa-file-excel-o"></i> Export to Excel', className: 'btn btn-sm btn-success me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'pdfHtml5', text: '<i class="fa fa-file-pdf-o"></i> Export to PDF', className: 'btn btn-sm btn-danger me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'print', text: '<i class="fa fa-print"></i> Print', className: 'btn btn-sm btn-primary', exportOptions: { columns: ':not(:last-child)' } },
            {
                text: '<i class="fa fa-history"></i> Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/AppointingAuthorityAuditTrail';
                }
            },
            {
                text: '<i class="fa fa-history"></i> Deleted Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/DeleteAppointingAuthorityAuditTrail';
                }
            }
        ],
        paging: true, searching: true, ordering: true, responsive: true, scrollX: true,
        autoWidth: false, lengthChange: false, info: true,
        columnDefs: [{ targets: -1, className: 'text-center' }],
        language: {
            searchPlaceholder: "Search authority...", search: "",
            zeroRecords: "No matching records found", info: "Showing _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "No entries available", paginate: { previous: "<", next: ">" }
        }
    });
    $('.appointingAuthority-title').html('<h3 class="m-0 fw-bold">Appointing Authority List</h3>');
    $('#appointingAuthorityDataTable_wrapper .dt-buttons').prepend(`
        <button type="button" class="btn btn-sm btn-primary me-2" data-toggle="modal" data-target="#appointingauthorityModal">
            <i class="fa fa-plus"></i> Add Appointing Authority
        </button>
    `);


    // === Master CVO Services ===
    const cvoServicesTable = $('#masterCVOServicesDataTable').DataTable({
        dom: "<'row mb-2' <'col-md-3' <'masterCVOServices-title'>> <'col-md-7'B> <'col-md-2 text-end'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row mt-2'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            { extend: 'excelHtml5', text: '<i class="fa fa-file-excel-o"></i> Export to Excel', className: 'btn btn-sm btn-success me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'pdfHtml5', text: '<i class="fa fa-file-pdf-o"></i> Export to PDF', className: 'btn btn-sm btn-danger me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'print', text: '<i class="fa fa-print"></i> Print', className: 'btn btn-sm btn-primary', exportOptions: { columns: ':not(:last-child)' } },
            {
                text: '<i class="fa fa-history"></i> Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/MasterCvoServicesAuditTrail';
                }
            },
            {
                text: '<i class="fa fa-history"></i> Deleted Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/DeleteMasterCvoServicesAuditTrail';
                }
            }
        ],
        paging: true, searching: true, ordering: true, responsive: true, scrollX: true,
        lengthChange: false, info: true,
        autoWidth: false,
        columnDefs: [{ targets: -1, className: 'text-center' }],
        language: {
            searchPlaceholder: "Search services...", search: "",
            zeroRecords: "No matching services found", info: "Showing _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "No entries available", paginate: { previous: "<", next: ">" }
        }
    });
    $('.masterCVOServices-title').html('<h3 class="m-0 fw-bold">Master Services List</h3>');
    $('#masterCVOServicesDataTable_wrapper .dt-buttons').prepend(`
        <button type="button" class="btn btn-sm btn-primary me-2" data-toggle="modal" data-target="#mastercvoservicesModal">
            <i class="fa fa-plus"></i> Add Master Services
        </button>
    `);


    // === State Table ===
    const stateTable = $('#stateDataTable').DataTable({
        dom: "<'row mb-2' <'col-md-3' <'state-title'>> <'col-md-7'B> <'col-md-2 text-end'f>>" +
            "<'row'<'col-sm-12'tr>>" +
            "<'row mt-2'<'col-sm-5'i><'col-sm-7'p>>",
        buttons: [
            { extend: 'excelHtml5', text: '<i class="fa fa-file-excel-o"></i> Export to Excel', className: 'btn btn-sm btn-success me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'pdfHtml5', text: '<i class="fa fa-file-pdf-o"></i> Export to PDF', className: 'btn btn-sm btn-danger me-2', exportOptions: { columns: ':not(:last-child)' } },
            { extend: 'print', text: '<i class="fa fa-print"></i> Print', className: 'btn btn-sm btn-primary', exportOptions: { columns: ':not(:last-child)' } },
            {
                text: '<i class="fa fa-history"></i> Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/StateAuditTrail';
                }
            },
            {
                text: '<i class="fa fa-history"></i> Deleted Audit Trail',
                className: 'btn btn-sm btn-warning me-2',
                action: function () {
                    window.location.href = '/SuperAdmin/DeleteStateAuditTrail';
                }
            }
        ],
        paging: true, searching: true, ordering: true, responsive: true, scrollX: true, autoWidth: false,
        lengthChange: false, info: true, columnDefs: [{ targets: -1, className: 'text-center' }],
        language: {
            searchPlaceholder: "Search State...", search: "",
            zeroRecords: "No matching State found", info: "Showing _START_ to _END_ of _TOTAL_ entries",
            infoEmpty: "No entries available", paginate: { previous: "<", next: ">" }
        }
    });
    $('.state-title').html('<h3 class="m-0 fw-bold">State List</h3>');
    $('#stateDataTable_wrapper .dt-buttons').prepend(`
        <button type="button" class="btn btn-sm btn-primary me-2" data-toggle="modal" data-target="#stateModal">
            <i class="fa fa-plus"></i> Add State
        </button>
    `);
});
