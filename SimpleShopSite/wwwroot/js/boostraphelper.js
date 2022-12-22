let tooltipList = null;
let popoverList = null;
export function CheckForTooltips() {
    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    if (tooltipList != null) {
        tooltipList.forEach(function (x) {
            x.dispose();
        });
    }
    tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })
}

export function GetToolTipFromElement(elem) {
    return bootstrap.Tooltip.getInstance(elem);
}

export function CheckForPopOvers() {
    // Initialize tooltips
    var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'))
    if (popoverList != null) {
        popoverList.forEach(function (x) {
            x.dispose();
        });
    }
    popoverList = popoverTriggerList.map(function (tooltipTriggerEl) {
        var attr = tooltipTriggerEl.getAttribute('data-html-name-target');
        if (attr != undefined) {
            var doc = [].slice.call(document.querySelectorAll('[data-html-cs-name="' + attr + '"]'));

            // create html pop
            if (doc[0].getAttribute('data-is-clone') == 'false') {
                var clone = doc[0].cloneNode(true);
                clone.setAttribute("data-is-clone", "true");
                let options = {
                    html: true,
                    content: clone
                };
                return new bootstrap.Popover(tooltipTriggerEl, options);
            }
        }
        return new bootstrap.Popover(tooltipTriggerEl);
    })
}