

//To close alert messages
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


//Show - hide password for password inputs
document.querySelectorAll('.btn-password-eye').forEach(button => {
    button.addEventListener('click', function () {
        const passwordInput = button.previousElementSibling;

        if (passwordInput.type === 'password') {
            passwordInput.type = 'text';
            button.innerHTML = '<i class="bi bi-eye-fill fs-6"></i>'; // show
        } else {
            passwordInput.type = 'password';
            button.innerHTML = '<i class="bi bi-eye-slash fs-6"></i>'; // hide
        }
    });
});


