window.bootstrapInterop = {
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
    }
};