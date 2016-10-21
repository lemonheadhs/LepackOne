(function () {
    'use strict';

    angular.module('umbraco.resources')
        .factory('personResource', function ($q, $http) {
            return {
                getAll: function () {
                    return $http.get('backoffice/My/PersonApi/GetAll');
                }
            }
        });
})();