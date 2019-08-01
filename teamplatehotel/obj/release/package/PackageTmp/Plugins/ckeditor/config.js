/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	 config.language = 'vi';
    // config.uiColor = '#AADC6E';
	 config.height = 400;
    config.filebrowserBrowseUrl = '/ckfinder/ckfinder.html';//[tên miền của bạn]/ckfinder/ckfinder.html’;
    config.filebrowserImageBrowseUrl = '/ckfinder/ckfinder.html?type=Images';//[tên miền của bạn]/ckfinder/ckfinder.html?type=Images’;
    config.filebrowserFlashBrowseUrl = '/ckfinder/ckfinder.html?type=Flash';//[tên miền của bạn]/ckfinder/ckfinder.html?type=Flash’;
    config.enterMode = CKEDITOR.ENTER_BR;
    config.shiftEnterMode = CKEDITOR.ENTER_P;
};