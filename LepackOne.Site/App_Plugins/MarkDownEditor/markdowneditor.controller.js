(function () {
    'use strict';

    angular.module('umbraco')
        .controller('My.MarkdownEditorController', function ($scope, assetsService, dialogService, $log, mediaHelper) {
            // tell the assetsService to load the markdown.editor libs from plugin folder
            assetsService
                .load([
                    "/App_Plugins/MarkDownEditor/lib/Markdown.Converter.js",
                    "/App_Plugins/MarkDownEditor/lib/Markdown.Sanitizer.js",
                    "/App_Plugins/MarkDownEditor/lib/Markdown.editor.js",
                ])
                .then(function () {
                    // this function will execute all dependencies have loaded
                    var converter2 = new Markdown.Converter();
                    var editor2 = new Markdown.Editor(converter2, '-' + $scope.model.alias);
                    editor2.run();

                    // subscribe to the image dialog clicks
                    editor2.hooks.set('insertImageDialog', function (callback) {
                        // here we can intercept our own dialog handling

                        // the callback is called when the user selects images
                        dialogService.mediaPicker({
                            callback: function (data) {
                                console.log(data);
                                
                                var imagePropVal = mediaHelper.getMediaPropertyValue({ mediaModel: data, imageOnly: true });
                                callback(imagePropVal);
                            }
                        });

                        return true; // tell the editor that we'll take care of getting the image url
                    })

                });

            // load the separate css for the editor to avoid it blocking our js loading
            assetsService.loadCss("/App_Plugins/MarkDownEditor/lib/Markdown.editor.css");

            if ($scope.model.value === null || $scope.model.value === "") {
                $scope.model.value = $scope.model.config.defaultValue;
            }
        });
})();