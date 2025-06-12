// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function checkLocationChanged(form) {
    const input = form.querySelector('.location-input');
    const button = form.querySelector('.btn');
    const original = input.dataset.original;
    button.disabled = input.value === original;

    input.addEventListener('input', () => {
        button.disabled = input.value === original;
    });
}

// Initialize on page load (to correctly set button state)
window.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.location-form').forEach(form => {
        checkLocationChanged(form);
    });
});

function showInputModal() {
    var modal = new bootstrap.Modal(document.getElementById('inputModal'));
    modal.show();
}

function openSaleModal() {
    var modal = new bootstrap.Modal(document.getElementById('saleModal'));
    modal.show();
}

document.getElementById('resetFiltersBtn').addEventListener('click', function () {
    const form = document.getElementById('searchForm');
    const all = document.querySelectorAll('.search-input');
    console.log(all);
    all.forEach(input => input.value = '');
    form.submit();
});
