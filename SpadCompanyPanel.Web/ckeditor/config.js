/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.uiColor = '#ffffff';
    config.contentsLangDirection = 'rtl';
    config.language = 'fa';
    config.filebrowserImageUploadUrl = '/Home/UploadImage';

    config.toolbar = 'MyToolbar';
    config.toolbar_MyToolbar =
        [
            //{ name: 'document', items: ['NewPage', 'Preview'] },
            { name: 'clipboard', items: ['Undo', 'Redo'] },
            { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', '-', 'Scayt'] },
            //{
            //    name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak'
            //                 , 'Iframe']
            //},
            //              '/',
            { name: 'styles', items: ['Format'] },
            { name: 'basicstyles', items: ['Bold', 'Italic', 'Strike', '-', 'RemoveFormat'] },
            { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl',] },
            { name: 'links', items: ['Link', 'Unlink'] },
            { name: 'tools', items: ['Maximize', '-'] }
        ];
};
