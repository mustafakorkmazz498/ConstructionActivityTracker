var ToolbarHelper = (function () {
    function initToolbar(options) {
        var container = $("#" + options.containerId);
        container.empty();

        var toolbarContainer = $("<div>", { class: "custom-toolbar-container" });

        options.buttons.forEach(function (btn) {
            var buttonHtml = "";
            if (btn.icon) {
                buttonHtml += '<i class="' + btn.icon + '"></i>';
            }
            if (btn.text) {
                buttonHtml += " " + btn.text;
            }

            var button = $("<button>", {
                id: btn.id,
                type: "button",
                class: "btn custom-toolbar-button",
                html: buttonHtml
            });

            button.on("click", btn.onClick);
            toolbarContainer.append(button);
        });

        container.append(toolbarContainer);
    }

    return {
        initToolbar: initToolbar
    };
})();
