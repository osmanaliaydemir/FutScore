// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function() {
    console.log('Application initialized');

    // Initialize SimpleBar
    document.querySelectorAll('[data-simplebar]').forEach(element => {
        new SimpleBar(element);
    });

    // Sidebar Toggle
    const sidebarToggle = document.getElementById('sidebarToggleBtn');
    const sidebar = document.querySelector('.sidebar');
    const main = document.querySelector('.main');

    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', () => {
            sidebar.classList.toggle('collapsed');
            main.classList.toggle('expanded');
        });

        // Close sidebar when clicking outside on mobile
        document.addEventListener('click', (e) => {
            if (window.innerWidth < 992) {
                if (!sidebar.contains(e.target) && !sidebarToggle.contains(e.target)) {
                    sidebar.classList.remove('collapsed');
                }
            }
        });
    }

    // AJAX Setup
    if (typeof $ !== 'undefined') {
        $.ajaxSetup({
            headers: {
                'X-CSRF-TOKEN': document.querySelector('meta[name="csrf-token"]')?.getAttribute('content')
            },
            beforeSend: function() {
                showLoading();
            },
            complete: function() {
                hideLoading();
            },
            error: function(xhr, status, error) {
                hideLoading();
                showError('Bir hata oluştu: ' + error);
            }
        });
    }
});

// Theme Management
(() => {
    'use strict';

    const getStoredTheme = () => localStorage.getItem('theme');
    const setStoredTheme = theme => localStorage.setItem('theme', theme);

    const getPreferredTheme = () => {
        const storedTheme = getStoredTheme();
        if (storedTheme) {
            return storedTheme;
        }
        return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    };

    const setTheme = theme => {
        document.documentElement.setAttribute('data-bs-theme', theme);
    };

    setTheme(getPreferredTheme());

    const showActiveTheme = (theme) => {
        const activeThemeIcon = document.querySelector('.theme-icon-active use');
        const btnToActive = document.querySelector(`[data-bs-theme-value="${theme}"]`);
        const svgOfActiveBtn = btnToActive?.querySelector('i')?.getAttribute('class');

        document.querySelectorAll('[data-bs-theme-value]').forEach(element => {
            element.classList.remove('active');
        });

        btnToActive?.classList.add('active');
        activeThemeIcon?.setAttribute('href', svgOfActiveBtn);
    };

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', () => {
        const storedTheme = getStoredTheme();
        if (!storedTheme) {
            setTheme(getPreferredTheme());
        }
    });

    document.querySelectorAll('[data-bs-theme-value]')
        .forEach(toggle => {
            toggle.addEventListener('click', () => {
                const theme = toggle.getAttribute('data-bs-theme-value');
                setStoredTheme(theme);
                setTheme(theme);
                showActiveTheme(theme);
            });
        });
})();

// Loading Spinner
function showLoading() {
    document.getElementById('loadingSpinner').style.display = 'flex';
}

function hideLoading() {
    document.getElementById('loadingSpinner').style.display = 'none';
}

// Toast Notifications
function showSuccess(message) {
    Swal.fire({
        icon: 'success',
        title: 'Başarılı!',
        text: message,
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true
    });
}

function showError(message) {
    Swal.fire({
        icon: 'error',
        title: 'Hata!',
        text: message,
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true
    });
}

// Confirmation Dialog
function confirmDelete(message) {
    return Swal.fire({
        title: 'Emin misiniz?',
        text: message || 'Bu öğeyi silmek istediğinize emin misiniz?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Evet, sil!',
        cancelButtonText: 'İptal'
    });
}

// Form Validation
function validateForm(formElement) {
    const requiredFields = formElement.querySelectorAll('[required]');
    let isValid = true;

    requiredFields.forEach(field => {
        if (!field.value.trim()) {
            isValid = false;
            field.classList.add('is-invalid');
            
            // Add validation message if not exists
            let feedback = field.nextElementSibling;
            if (!feedback || !feedback.classList.contains('invalid-feedback')) {
                feedback = document.createElement('div');
                feedback.className = 'invalid-feedback';
                feedback.textContent = 'Bu alan zorunludur.';
                field.parentNode.insertBefore(feedback, field.nextSibling);
            }
        } else {
            field.classList.remove('is-invalid');
            
            // Remove validation message
            const feedback = field.nextElementSibling;
            if (feedback && feedback.classList.contains('invalid-feedback')) {
                feedback.remove();
            }
        }
    });

    return isValid;
}

// Date Formatting
const formatDate = (date) => {
    if (!date) return '';
    return moment(date).format('DD/MM/YYYY');
};

const formatDateTime = (date) => {
    if (!date) return '';
    return moment(date).format('DD/MM/YYYY HH:mm');
};

// DataTables Default Configuration
if (typeof $.fn.DataTable !== 'undefined') {
    $.extend(true, $.fn.dataTable.defaults, {
        language: {
            url: '//cdn.datatables.net/plug-ins/1.13.7/i18n/tr.json'
        },
        responsive: true,
        processing: true,
        pageLength: 10,
        dom: '<"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6"f>>' +
             '<"row"<"col-sm-12"tr>>' +
             '<"row"<"col-sm-12 col-md-5"i><"col-sm-12 col-md-7"p>>',
        buttons: [
            {
                extend: 'excel',
                className: 'btn btn-success',
                text: '<i class="fas fa-file-excel"></i> Excel'
            },
            {
                extend: 'pdf',
                className: 'btn btn-danger',
                text: '<i class="fas fa-file-pdf"></i> PDF'
            },
            {
                extend: 'print',
                className: 'btn btn-info',
                text: '<i class="fas fa-print"></i> Yazdır'
            }
        ]
    });
}

// Initialize Components
$(document).ready(() => {
    // Initialize all tooltips
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(tooltipTriggerEl => new bootstrap.Tooltip(tooltipTriggerEl));

    // Initialize all popovers
    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    popoverTriggerList.map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl));

    // Initialize DataTables
    $('.datatable').each(function() {
        const table = $(this).DataTable();
        
        if (table.buttons().container()) {
            table.buttons().container().appendTo(`#${$(this).attr('id')}_wrapper .col-md-6:eq(0)`);
        }
    });
});
