(function () {
    'use strict';

    function leReportCtrl($scope, leRequestHelper, umbRequestHelper) {
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
            var importActionUrl = baseUrl + "ImportReport";

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