(function () {
    'use strict';

    // there is one issue with this service , the content-type of the post request is application/json;
    function leRequestHelper($http, $q) {
        return {
            postDataFile: function (url, data, files) {
                return $http({
                    method: 'POST',
                    url: url,
                    headers: { 'Content-Type': undefined },
                    data: data,
                    transformRequest: function (data) {
                        var formData = new FormData();
                        angular.forEach(data, function (value, key) {
                            formData.append(key,
                                !angular.isString(value) ? angular.toJson(value) : value);
                        });

                        //if (angular.isArray(files)) {
                        //    angular.forEach(files, function (file, idx) {
                        //        formData.append('file_' + idx, file);
                        //    });
                        //} else {
                        //    angular.forEach(files, function (file, key) {
                        //        formData.append(file.name, file);
                        //    });
                        //}
                        formData.append('files', files);

                        return formData;
                    }
                });
            }
        }
    }

    angular.module('umbraco.services').factory("leRequestHelper", leRequestHelper);

})();
