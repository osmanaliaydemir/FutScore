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
