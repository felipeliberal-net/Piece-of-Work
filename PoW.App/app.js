(function(angular) {
    'use strict';

    var appModule = angular.module('pow.app');
    
    appModule.config([
        'growlProvider', function(growlProvider) {
            growlProvider.globalTimeToLive(5000);
            growlProvider.globalPosition('top-center');
    }]);

    appModule.controller('mainController', mainController);
    appModule.controller('taskModalController', taskModalController);
    
    mainController.$inject = ['$scope', 'TaskWorkService', '$uibModal', 'growl'];
    
    function mainController($scope, TaskWorkService, $uibModal, growl) {
        $scope.getTaskWork = function () {
            TaskWorkService.getList().then(
                function (response) {
                    $scope.todoTasks = response.data.taskworks.filter(function(item){return item.status == 1;});
                    $scope.doingTasks = response.data.taskworks.filter(function(item){return item.status == 2;});
                    $scope.doneTasks = response.data.taskworks.filter(function(item){return item.status == 3;});                    
                },
                function (error) {
                    growl.error('danger', 'O servidor de tarefas está offline.');
                }
            )
        };
        $scope.getTaskWork();

        $scope.openTaskModal = function (task) {
            var modalInstance = $uibModal.open({
                templateUrl: './modal/task-modal.html',
                controller: 'taskModalController',
                windowClass: 'show',
                backdrop: 'static',
                backdropClass: 'modal-backdrop h-full show',
                resolve: {
                    params: function() {
                        return {
                            task: task
                        };
                    }
                }
            });

            modalInstance.result.then(
                function () {
                    $scope.getTaskWork();
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

    taskModalController.$inject = ['$scope', 'TaskWorkService', '$uibModalInstance', 'params', 'growl'];
    function taskModalController($scope, TaskWorkService, $uibModalInstance, params, growl) {
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
                TaskWorkService.insert($scope.task).then(
                    function (response) {
                        growl.success("Tarefa salva com sucesso.");
                        $uibModalInstance.close();
                    }, 
                    function (error) {
                        growl.error("Ocorreu um erro ao excluir a tarefa.");
                    }
                );
            } else if ($scope.task.id > 0) {
                TaskWorkService.update($scope.task.id, $scope.task).then(
                    function (response) {
                        growl.success("Tarefa salva com sucesso.");
                        $uibModalInstance.close();
                    }, 
                    function (error) {
                        growl.error("Ocorreu um erro ao excluir a tarefa.");
                    }
                );
            }
        };

        $scope.deleteClick = function() {
            if ($scope.task.id == 0)
                return;

            TaskWorkService.delete($scope.task.id).then(
                function (response) {
                    growl.success("Tarefa removida com sucesso.");
                    $uibModalInstance.close();
                },
                function (error) {
                    growl.error("Ocorreu um erro ao excluir a tarefa.");
                }
            );
        };
    }
})(angular);