(function(angular) {
    'use strict';

    var appModule = angular.module('pow.app');

    appModule.controller('mainController', mainController);
    appModule.controller('taskModalController', taskModalController);

    mainController.$inject = ['$scope', 'TaskWorkService', '$uibModal'];
    
    function mainController($scope, TaskWorkService, $uibModal) {
        $scope.alerts = [];

        $scope.getTaskWork = function () {
            TaskWorkService.getList().then(
                function (response) {
                    $scope.todoTasks = response.data.taskworks.filter(function(item){return item.status == 1;});
                    $scope.doingTasks = response.data.taskworks.filter(function(item){return item.status == 2;});
                    $scope.doneTasks = response.data.taskworks.filter(function(item){return item.status == 3;});                    
                },
                function (error) {
                    window.alert(error);
                }
            )
        };
        $scope.getTaskWork();

        $scope.openTaskModal = function (task) {
            var modalInstance = $uibModal.open({
                templateUrl: './modal/task-modal.html',
                controller: 'taskModalController',
                backdrop: 'static',
                backdropClass: 'modal-backdrop h-full',
                resolve: {
                    params: function() {
                        return {
                            task: task
                        };
                    }
                }
            });

            modalInstance.result.then(
                function (response) {
                    $scope.addAlert('success', 'Tarefa salva com sucesso.');
                    $scope.getTaskWork();
                },
                function (error) {
                    $scope.addAlert('danger', 'Erro ao salvar a tarefa. ' + error.message);
                }
            )
        };

        $scope.addAlert = function(type, message) {
            $scope.alerts.push({type: type, msg: message});
        };

        $scope.closeAlert = function(index) {
            $scope.alerts.splice(index, 1);
          };
    }

    taskModalController.$inject = ['$scope', 'TaskWorkService', '$uibModalInstance', 'params'];
    function taskModalController($scope, TaskWorkService, $uibModalInstance, params) {
        $scope.statuslist = [
            {
                value: 1,
                name: "Pendente"
            },
            {
                value: 2,
                name: "Em andamento"
            },
            {
                value: 3,
                name: "Finalizado"
            },
        ]
        
        if (!params || !params.task) {
            $scope.task = {
                id: 0,
                title: "",
                description: "",
                status: 1
            };
        } else {
            $scope.task = params.task;
        }

        $scope.cancelClick = function() {
            $uibModalInstance.dismiss('cancel');
        };

        $scope.saveClick = function() {
            if (!$scope.task.title || !$scope.task.description || !$scope.task.status) {
                
                return;
            }
            
            if ($scope.task.id == 0) {
                TaskWorkService.insert($scope.task).then(saveSuccess, saveError);
            } else if ($scope.task.id > 0)
                TaskWorkService.update($scope.task.id, $scope.task).then(saveSuccess, saveError);            
        };

        function saveSuccess(response) {
            $uibModalInstance.close();
        }

        function saveError(response) {
            return;
        }
    }

})(angular);