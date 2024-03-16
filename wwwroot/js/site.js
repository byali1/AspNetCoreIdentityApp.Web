// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    var closeButtons = document.querySelectorAll('.message .close');
    closeButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            var message = this.closest('.message');
            message.style.opacity = '0.5';
            setTimeout(function () {
                message.style.display = 'none';
            }, 200);
        });
    });
});