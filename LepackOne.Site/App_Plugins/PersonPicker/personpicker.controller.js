(function () {
    'use strict';

    angular.module('umbraco')
        .controller('My.PersonPickerController', function ($scope, personResource) {
            personResource.getAll().then(function (response) {
                $scope.people = response.data;
            });
        });
})();