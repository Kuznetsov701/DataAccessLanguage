function createEditor(element, language) {
    if(element !== null)
        return monaco.editor.create(element, { language: language });
}