// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    const dropdownElement = document.getElementById('booksDropdown');

    dropdownElement.addEventListener('mouseenter', function () {
        const dropdownMenu = this.querySelector('.dropdown-menu');
        dropdownMenu.classList.add('show');
    });

    dropdownElement.addEventListener('mouseleave', function () {
        const dropdownMenu = this.querySelector('.dropdown-menu');
        dropdownMenu.classList.remove('show');
    });
});