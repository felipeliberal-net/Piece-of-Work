(function(angular) {
    'use strict';

    angular.module('pow.app.taskworkservice').service('TaskWorkService', TaskWorkService);

    TaskWorkService.$inject = ['$http'];

    function TaskWorkService($http) {
        return {
            get: function(id) {
                return $http({
                    method: 'GET',
                    url: 'http://localhost:9000/api/taskwork/get/' + id
                });
            },

            getList: function() {
                return $http({
                    method: 'GET',
                    url: 'http://localhost:9000/api/taskwork/get'
                });
            },

            delete: function (id) {
                return $http({
                    method: "DELETE",
                    url: "http://localhost:9000/api/taskwork/delete/" + id
                });
            },

            insert: function (data) {
                return $http({
                    method: "POST",
                    data: data,
                    url: "http://localhost:9000/api/taskwork/post"
                });
            },

            update: function(id, data) {
                return $http({
                    method: 'PUT',
                    data: data,
                    url: 'http://localhost:9000/api/taskwork/put/' + id
                });
            }
        };
    }
})(angular);