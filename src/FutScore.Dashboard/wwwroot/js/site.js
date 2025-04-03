// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Sidebar Toggle
$(document).ready(function () {
    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        $('#content').toggleClass('active');
    });

    // Initialize DataTables
    $('.datatable').DataTable({
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
        responsive: true,
        dom: '<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6"f>>' +
             '<"row"<"col-sm-12"tr>>' +
             '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        pageLength: 10,
        order: [[0, 'desc']]
    });

    // Initialize Select2
    $('.select2').select2({
        theme: 'bootstrap-5',
        width: '100%'
    });

    // Toastr Configuration
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    // Logout Function
    $('#logoutBtn').on('click', function (e) {
        e.preventDefault();
        Swal.fire({
            title: 'Çıkış yapmak istediğinize emin misiniz?',
            text: "Oturumunuz sonlandırılacak.",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, çıkış yap',
            cancelButtonText: 'İptal'
        }).then((result) => {
            if (result.isConfirmed) {
                // Logout işlemi
                window.location.href = '/Account/Logout';
            }
        });
    });

    // AJAX Form Submit
    $('form[data-ajax="true"]').on('submit', function (e) {
        e.preventDefault();
        var form = $(this);
        var url = form.attr('action');
        var method = form.attr('method');
        var data = form.serialize();

        $.ajax({
            url: url,
            method: method,
            data: data,
            success: function (response) {
                if (response.success) {
                    toastr.success(response.message);
                    if (response.redirectUrl) {
                        setTimeout(function () {
                            window.location.href = response.redirectUrl;
                        }, 1500);
                    }
                } else {
                    toastr.error(response.message);
                }
            },
            error: function (xhr) {
                toastr.error('Bir hata oluştu. Lütfen tekrar deneyin.');
            }
        });
    });

    // Modal Form Submit
    $('.modal-form').on('submit', function (e) {
        e.preventDefault();
        var form = $(this);
        var modal = form.closest('.modal');
        var url = form.attr('action');
        var method = form.attr('method');
        var data = form.serialize();

        $.ajax({
            url: url,
            method: method,
            data: data,
            success: function (response) {
                if (response.success) {
                    toastr.success(response.message);
                    modal.modal('hide');
                    if (response.redirectUrl) {
                        setTimeout(function () {
                            window.location.href = response.redirectUrl;
                        }, 1500);
                    }
                } else {
                    toastr.error(response.message);
                }
            },
            error: function (xhr) {
                toastr.error('Bir hata oluştu. Lütfen tekrar deneyin.');
            }
        });
    });

    // Delete Confirmation
    $('.delete-btn').on('click', function (e) {
        e.preventDefault();
        var url = $(this).attr('href');
        var itemName = $(this).data('name');

        Swal.fire({
            title: 'Emin misiniz?',
            text: `${itemName} silinecek. Bu işlem geri alınamaz!`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Evet, sil!',
            cancelButtonText: 'İptal'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: url,
                    method: 'POST',
                    success: function (response) {
                        if (response.success) {
                            toastr.success(response.message);
                            setTimeout(function () {
                                window.location.reload();
                            }, 1500);
                        } else {
                            toastr.error(response.message);
                        }
                    },
                    error: function (xhr) {
                        toastr.error('Bir hata oluştu. Lütfen tekrar deneyin.');
                    }
                });
            }
        });
    });

    // File Upload Preview
    $('.custom-file-input').on('change', function () {
        var fileName = $(this).val().split('\\').pop();
        $(this).next('.custom-file-label').html(fileName);
    });

    // Dynamic Form Loading
    $('.dynamic-form').on('change', function () {
        var form = $(this);
        var url = form.data('url');
        var target = form.data('target');
        var data = form.serialize();

        $.ajax({
            url: url,
            method: 'GET',
            data: data,
            success: function (response) {
                $(target).html(response);
            },
            error: function (xhr) {
                toastr.error('Form yüklenirken bir hata oluştu.');
            }
        });
    });
});

// Global AJAX setup
$.ajaxSetup({
    headers: {
        'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
    }
});

// Loading spinner
const showLoading = () => {
    if (!document.querySelector('.loading-spinner')) {
        const spinner = document.createElement('div');
        spinner.className = 'loading-spinner';
        document.body.appendChild(spinner);
    }
};

const hideLoading = () => {
    const spinner = document.querySelector('.loading-spinner');
    if (spinner) {
        spinner.remove();
    }
};

// Global AJAX events
$(document).ajaxStart(showLoading);
$(document).ajaxStop(hideLoading);
$(document).ajaxError((event, jqXHR, settings, error) => {
    hideLoading();
    showErrorToast('Error', 'An error occurred while processing your request.');
});

// Toast notifications
const showSuccessToast = (title, message) => {
    Swal.fire({
        title: title,
        text: message,
        icon: 'success',
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true
    });
};

const showErrorToast = (title, message) => {
    Swal.fire({
        title: title,
        text: message,
        icon: 'error',
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 5000,
        timerProgressBar: true
    });
};

// Confirmation dialog
const confirmDelete = (title, text) => {
    return Swal.fire({
        title: title,
        text: text,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    });
};

// Form validation
const validateForm = (formElement) => {
    let isValid = true;
    const requiredFields = formElement.querySelectorAll('[required]');
    
    requiredFields.forEach(field => {
        if (!field.value.trim()) {
            isValid = false;
            field.classList.add('is-invalid');
            
            if (!field.nextElementSibling || !field.nextElementSibling.classList.contains('invalid-feedback')) {
                const feedback = document.createElement('div');
                feedback.className = 'invalid-feedback';
                feedback.textContent = 'This field is required.';
                field.parentNode.insertBefore(feedback, field.nextSibling);
            }
        } else {
            field.classList.remove('is-invalid');
            const feedback = field.nextElementSibling;
            if (feedback && feedback.classList.contains('invalid-feedback')) {
                feedback.remove();
            }
        }
    });
    
    return isValid;
};

// Date formatting
const formatDate = (date) => {
    if (!date) return '';
    return moment(date).format('DD/MM/YYYY');
};

const formatDateTime = (date) => {
    if (!date) return '';
    return moment(date).format('DD/MM/YYYY HH:mm');
};

// DataTable defaults
$.extend(true, $.fn.dataTable.defaults, {
    language: {
        search: "_INPUT_",
        searchPlaceholder: "Search...",
        lengthMenu: "_MENU_ records per page",
        info: "Showing _START_ to _END_ of _TOTAL_ records",
        infoEmpty: "Showing 0 to 0 of 0 records",
        infoFiltered: "(filtered from _MAX_ total records)",
        zeroRecords: "No matching records found",
        paginate: {
            first: '<i class="fas fa-angle-double-left"></i>',
            previous: '<i class="fas fa-angle-left"></i>',
            next: '<i class="fas fa-angle-right"></i>',
            last: '<i class="fas fa-angle-double-right"></i>'
        }
    },
    pageLength: 10,
    ordering: true,
    responsive: true,
    processing: true,
    stateSave: true,
    dom: "<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6'f>>" +
         "<'row'<'col-sm-12'tr>>" +
         "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
    buttons: [
        {
            extend: 'copy',
            text: '<i class="fas fa-copy"></i> Copy',
            className: 'btn btn-secondary btn-sm',
            exportOptions: {
                columns: ':not(.no-export)'
            }
        },
        {
            extend: 'excel',
            text: '<i class="fas fa-file-excel"></i> Excel',
            className: 'btn btn-success btn-sm',
            exportOptions: {
                columns: ':not(.no-export)'
            }
        },
        {
            extend: 'pdf',
            text: '<i class="fas fa-file-pdf"></i> PDF',
            className: 'btn btn-danger btn-sm',
            exportOptions: {
                columns: ':not(.no-export)'
            }
        },
        {
            extend: 'print',
            text: '<i class="fas fa-print"></i> Print',
            className: 'btn btn-info btn-sm',
            exportOptions: {
                columns: ':not(.no-export)'
            }
        }
    ],
    drawCallback: function(settings) {
        $('.dataTables_paginate > .pagination').addClass('pagination-sm');
    }
});
