window.bootstrapInterop =
{
    showModal: (modalId) => {
        const myModal = new bootstrap.Modal(document.querySelector(modalId));
        myModal.show();
    },

    hideModal: (modalId) => {
        const modalEl = document.querySelector(modalId);
        const modal = bootstrap.Modal.getInstance(modalEl);
        if (modal) {
            modal.hide();
        }
    },

    setTheme: (theme) => {
        document.documentElement.setAttribute("data-bs-theme", theme);
    },

    showToast: (toastId) => {
        const toastBootstrap = bootstrap.Toast.getOrCreateInstance(document.getElementById(toastId));
        toastBootstrap.show()
    },

    login: async (userId, roleIds) => {
        const response = await fetch('/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                userId: userId,
                roleIds: roleIds
            })
        });

        if (response.ok) {
            console.log("Login Successful");
            location.href = "/";
        } else {
            alert("Login failed");
        }
    }
};
