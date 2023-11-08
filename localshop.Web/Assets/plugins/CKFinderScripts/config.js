/*
 Copyright (c) 2007-2019, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.html or https://ckeditor.com/sales/license/ckfinder
 */

//var config = {};

// Set your configuration options below.

// Examples:
// config.language = 'pl';
// config.skin = 'jquery-mobile';

// Include the CKFinder script if you haven't already
// <script src="/js/ckfinder3/ckfinder.js"></script>

// Define the CKFinder configuration
CKFinder.define(function (config) {
    // Set the resourceType, appId, and other configuration options
    config.resourceType = 'Image';
    config.appId = '12345';
    config.chooseFiles = true;

    // Set file browser URLs
    ////config.filebrowserBrowseUrl = '/js/ckfinder3/ckfinder.html?type=Files&appId=' + config.appId;
    ////config.filebrowserUploadUrl = '/js/ckfinder3/connector?command=QuickUpload&type=Files&appId=' + config.appId;
    
    config.filebrowserImageBrowseUrl = '/ckfinder/connector/azstore/ckfinder.html?type=Image&appId=' + config.appId;
    config.filebrowserImageUploadUrl = '/ckfinder/connector/azstore/connector?command=QuickUpload&type=Files&responseType=json?command=QuickUpload&type=Image&appId=' + config.appId;

    // Set the upload URL
    config.uploadUrl = '/ckfinder/connector/azstore/connector?command=QuickUpload&type=Image&responseType=json&appId=' + config.appId;

    // You can set more configuration options here

    // Initialize CKFinder with the defined configuration
    CKFinder.init(config);
});

//CKFinder.define( config );
