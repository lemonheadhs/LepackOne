(function () {
    'use strict';

    function leRequestHelper($http, $q) {
        return {
            postDataFile: function (url, data, files) {

            }
        }
    }

    angular.module('umbraco.services').factory("leRequestHelper", leRequestHelper);

})();