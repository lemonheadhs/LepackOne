(function () {
    'use strict';

    function leReportCtrl($scope, leRequestHelper, umbRequestHelper, Upload) {
        var ctrl = this;
        ctrl.reportFiles = null;
        ctrl.rebuild = new Date();

        ctrl.importReportData = importReportData;

        $scope.$on('filesSelected', function (event, args) {
            ctrl.reportFiles = args.files;

            if (event.stopPropagation) {
                event.stopPropagation();
            }
        });

        function importReportData() {
            var baseUrl = Umbraco.Sys.ServerVariables.lepackOneUrls.leReportApiBaseUrl;
            var importActionUrl = baseUrl + "ImportAttainment";//"ImportReport";

            //Upload.upload({
            //    url: importActionUrl,
            //    fields: {
            //        'report': { name: 'test' }
            //    },
            //    file: ctrl.reportFiles 
            //})
            //.success(function () {
            //    console.log('lemon success!');
            //    ctrl.rebuild = new Date();
            //})
            //.error(function () {
            //    console.log('failed!');
            //    console.log(arguments);
            //});

            leRequestHelper
                .postDataFile(importActionUrl, { 'report': {} }, ctrl.reportFiles || [])
                .success(function (data, status, headers, config) {
                    console.log('lemon success!');

                    ctrl.rebuild = new Date();
                })
                .error(function (data, status, headers, config) {
                    console.log('failed!');
                    console.log(arguments);

                });
        }

    }
    
    angular.module('umbraco').controller('leReportCtrl', leReportCtrl)

})();