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
    },
    enableResizableColumns: (tableId, dotNetHelper) => {
        const table = document.getElementById(tableId);
        const headers = table.querySelectorAll("th");

        headers.forEach((th, index) => {
            const resizer = document.createElement("div");
            resizer.style.position = "absolute";
            resizer.style.right = 0;
            resizer.style.top = 0;
            resizer.style.width = "5px";
            resizer.style.height = "100%";
            resizer.style.cursor = "col-resize";
            resizer.style.zIndex = 1;

            th.style.position = "relative";
            th.appendChild(resizer);

            let startX, startWidth;

            const mouseMove = (e) => {
                const dx = e.clientX - startX;
                th.style.width = `${startWidth + dx}px`;
            };

            const mouseUp = () => {
                document.removeEventListener("mousemove", mouseMove);
                document.removeEventListener("mouseup", mouseUp);
                dotNetHelper.invokeMethodAsync("OnColumnResize", index, th.offsetWidth);
            };

            resizer.addEventListener("mousedown", (e) => {
                startX = e.clientX;
                startWidth = th.offsetWidth;
                document.addEventListener("mousemove", mouseMove);
                document.addEventListener("mouseup", mouseUp);
            });
        });
    }
};
