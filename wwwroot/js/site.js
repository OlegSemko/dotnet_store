// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function checkLocationChanged(form) {
    console.log('form', form);
    const input = form.querySelector('.location-input');
    const button = form.querySelector('.btn');
    console.log('input', input);
    const original = input.dataset.original;
    console.log('original', original);
    button.disabled = input.value === original;

    input.addEventListener('input', () => {
        button.disabled = input.value === original;
        console.log('button disabled', button.disabled);
    });
}

// Initialize on page load (to correctly set button state)
window.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.location-form').forEach(form => {
        checkLocationChanged(form);
    });
});
