(function(angular) {
    'use strict';

    var appModule = angular.module('pow.app');

    appModule.controller('mainController', mainController);

    mainController.$inject = ['$scope', 'TaskWorkService'];
    
    function mainController($scope, TaskWorkService) {
        $scope.title = Date.now();

        $scope.getTaskWork = function () {
            TaskWorkService.getList().then(
                function (response) {
                    console.log(response.data);
                },
                function (error) {
                    window.alert(error);
                }
            )
        };
        $scope.getTaskWork();
    }
})(angular);