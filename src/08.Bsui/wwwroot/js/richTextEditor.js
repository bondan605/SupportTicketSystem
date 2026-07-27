window.richTextEditor = {
    /// <summary>Wires up input/blur listeners so edits are reported back to .NET.</summary>
    init: function (element, dotNetRef) {
        if (!element) return;

        element.addEventListener('input', () => window.richTextEditor.notifyChange(element, dotNetRef));
        element.addEventListener('blur', () => window.richTextEditor.notifyChange(element, dotNetRef));
    },

    notifyChange: function (element, dotNetRef) {
        if (!element || !dotNetRef) return;
        const html = element.innerHTML;
        const length = window.richTextEditor.getPlainTextLength(element);
        dotNetRef.invokeMethodAsync('OnContentChanged', html, length);
    },

    /// <summary>Runs a formatting command (bold/italic/underline/lists/alignment/formatBlock) on the current selection.</summary>
    execCommand: function (element, command, value) {
        if (!element) return;
        element.focus();
        document.execCommand(command, false, value ?? null);
    },

    /// <summary>Wraps the current selection in a link, prompting the user for a URL.</summary>
    insertLink: function (element, url) {
        if (!element || !url) return;
        element.focus();
        document.execCommand('createLink', false, url);
    },

    /// <summary>Inserts an image at the caret, prompting the user for a URL.</summary>
    insertImage: function (element, url) {
        if (!element || !url) return;
        element.focus();
        document.execCommand('insertImage', false, url);
    },

    setContent: function (element, html) {
        if (element) element.innerHTML = html || '';
    },

    getHtml: function (element) {
        return element ? element.innerHTML : '';
    },

    getPlainTextLength: function (element) {
        if (!element) return 0;
        return (element.innerText || element.textContent || '').length;
    },

    focusElement: function (element) {
        if (element) element.focus();
    }
};
