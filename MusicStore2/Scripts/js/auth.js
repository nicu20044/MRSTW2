// auth.js - Enhanced authentication form interactions for MusicStore2

document.addEventListener('DOMContentLoaded', function() {
    // Focus effects for input fields
    const formControls = document.querySelectorAll('.form-control');

    formControls.forEach(input => {
        // Add focus effect
        input.addEventListener('focus', function() {
            this.parentElement.classList.add('focused');
            const label = this.parentElement.querySelector('label');
            if (label) {
                label.classList.add('active');
            }
        });

        // Remove focus effect if field is empty
        input.addEventListener('blur', function() {
            if (this.value.trim() === '') {
                this.parentElement.classList.remove('focused');
                const label = this.parentElement.querySelector('label');
                if (label) {
                    label.classList.remove('active');
                }
            }
        });

        // Check if input has value on page load
        if (input.value.trim() !== '') {
            input.parentElement.classList.add('focused');
            const label = input.parentElement.querySelector('label');
            if (label) {
                label.classList.add('active');
            }
        }
    });

    // Password visibility toggle
    const passwordFields = document.querySelectorAll('input[type="password"]');

    passwordFields.forEach(field => {
        // Create toggle button
        const toggleBtn = document.createElement('button');
        toggleBtn.type = 'button';
        toggleBtn.className = 'password-toggle';
        toggleBtn.innerHTML = '<i class="fas fa-eye"></i>';
        toggleBtn.setAttribute('aria-label', 'Toggle password visibility');

        // Append button after password field
        field.parentElement.style.position = 'relative';
        field.parentElement.appendChild(toggleBtn);

        // Toggle password visibility
        toggleBtn.addEventListener('click', function() {
            const type = field.getAttribute('type') === 'password' ? 'text' : 'password';
            field.setAttribute('type', type);

            // Change icon based on visibility
            this.innerHTML = type === 'password' ?
                '<i class="fas fa-eye"></i>' :
                '<i class="fas fa-eye-slash"></i>';
        });
    });

    // Form validation feedback
    const authForms = document.querySelectorAll('.auth-form form');

    authForms.forEach(form => {
        form.addEventListener('submit', function(event) {
            let isValid = true;
            const requiredInputs = form.querySelectorAll('[required]');

            requiredInputs.forEach(input => {
                if (input.value.trim() === '') {
                    isValid = false;
                    input.classList.add('invalid');

                    // Add shake animation for empty required fields
                    input.classList.add('shake');
                    setTimeout(() => {
                        input.classList.remove('shake');
                    }, 500);
                } else {
                    input.classList.remove('invalid');
                }
            });

            // Handle email validation
            const emailInput = form.querySelector('input[type="email"]');
            if (emailInput && emailInput.value.trim() !== '') {
                const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                if (!emailPattern.test(emailInput.value)) {
                    isValid = false;
                    emailInput.classList.add('invalid');
                }
            }

            if (!isValid) {
                event.preventDefault();
            } else {
                // Add loading state to button
                const submitBtn = form.querySelector('button[type="submit"]');
                submitBtn.innerHTML = '<span class="spinner"></span> Processing...';
                submitBtn.disabled = true;
            }
        });
    });

    // Alert message animation and dismiss
    const alertMessages = document.querySelectorAll('.alert');

    alertMessages.forEach(alert => {
        // Add dismiss button
        const dismissBtn = document.createElement('button');
        dismissBtn.type = 'button';
        dismissBtn.className = 'alert-dismiss';
        dismissBtn.innerHTML = '&times;';
        alert.appendChild(dismissBtn);

        // Auto dismiss after 5 seconds
        setTimeout(() => {
            fadeOut(alert);
        }, 5000);

        // Manual dismiss
        dismissBtn.addEventListener('click', function() {
            fadeOut(alert);
        });
    });

    function fadeOut(element) {
        element.style.opacity = '1';

        (function fade() {
            if ((element.style.opacity -= '.1') < 0) {
                element.style.display = 'none';
            } else {
                requestAnimationFrame(fade);
            }
        })();
    }

    // Add nice ripple effect to buttons
    const buttons = document.querySelectorAll('.submit-btn, .btn-dark');

    buttons.forEach(button => {
        button.addEventListener('click', function(e) {
            const ripple = document.createElement('span');
            ripple.classList.add('ripple');
            this.appendChild(ripple);

            const x = e.clientX - this.getBoundingClientRect().left;
            const y = e.clientY - this.getBoundingClientRect().top;

            ripple.style.left = `${x}px`;
            ripple.style.top = `${y}px`;

            setTimeout(() => {
                ripple.remove();
            }, 600);
        });
    });
});