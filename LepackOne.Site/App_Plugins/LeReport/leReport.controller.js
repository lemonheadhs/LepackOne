(function () {
    'use strict';

    function leReportCtrl($scope, leRequestHelper) {
        var ctrl = this;
        ctrl.reportFiles = null;

        ctrl.importReportData = importReportData;

        $scope.$on('filesSelected', function (event, args) {
            ctrl.reportFiles = args.files;

            if (event.stopPropagation) {
                event.stopPropagation();
            }
        });

        function importReportData() {
            leRequestHelper
                .postDataFile('', {}, ctrl.reportFiles || [])
                .succcess(function (data, status, headers, config) {

                })
                .error();
        }

    }
    
    angular.module('umbraco').controller('leReportCtrl', leReportCtrl)

})();