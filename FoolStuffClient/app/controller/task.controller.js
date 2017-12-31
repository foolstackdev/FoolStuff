"use strict";
angular
.module('FoolStackApp')
.controller('TaskController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", "ApplicationService",
    function ($scope, RestService, CostantUrl, $state, toastr, ApplicationService) {

        var vm = this;
        vm.task;
        vm.tasklist = [];
        vm.taskClosedList = [];

        vm.riepilogoTask = _riepilogoTask;
        vm.insertTask = _insertTask;
        vm.takeTask = _takeTask;
        vm.userIsAlreadyAssigned = _userIsAlreadyAssigned;
        vm.giveUpTask = _giveUpTask;
        vm.closeTask = _closeTask;
        vm.riepilogoTaskChiusi = _riepilogoTaskChiusi;

        init();
        function init() {
            console.log("Inside Task controller");
        }

        function _riepilogoTask() {
            RestService.GetData(CostantUrl.urlTask, "getalltask").then(function (response) {
                vm.tasklist = response.data;

                var avatars = ApplicationService.getAllAvatars();
                if (avatars != undefined && avatars != null) {
                    for (var i = 0; i < vm.tasklist.length; i++) {
                        for (var k = 0; k < vm.tasklist[i].users.length; k++) {
                            vm.tasklist[i].users[k].avatar = null;
                            for (var j = 0; j < avatars.length; j++) {
                                if (vm.tasklist[i].users[k].id == avatars[j].userId) {
                                    vm.tasklist[i].users[k].avatar = avatars[j].dataHtml;
                                    break;
                                }
                            }
                            if (vm.tasklist[i].users[k].avatar == null) {
                                vm.tasklist[i].users[k].avatar = ApplicationService.getDefaultImageSrc();
                            }
                        }
                    }
                }

                console.log(response);
            });
        }

        function _insertTask() {
            RestService.PostData(CostantUrl.urlTask, "insertnewtask", vm.task).then(function (response) {
                //vm.tasklist = response.data;
                toastr.success('Task inserted', 'Confirmed');
                $state.reload();

            }, function (err) {
                console.log(err)
                toastr.error('Problems during task insert', 'Something went wrong [' + err.data.exceptionMessage + ']');
            });
        }
        function _takeTask(taskIndex) {
            console.log(taskIndex);
            RestService.PostData(CostantUrl.urlTask, "addusertotask/" + sessionStorage.getItem('userId') + "/", vm.tasklist[taskIndex].id).then(function (response) {
                toastr.success('Cool, task assigned to you', 'Confirmed');
                console.log(response);
                $state.reload();
            }, function (err) {
                console.log(err)
                toastr.error('Problems during task assignment', 'Something went wrong [' + err.data.exceptionMessage + ']');
            });
        }

        function _giveUpTask(taskIndex) {
            RestService.PostData(CostantUrl.urlTask, "giveuptask/" + sessionStorage.getItem('userId') + "/", vm.tasklist[taskIndex].id).then(function (response) {
                toastr.warning('Never give up! Be a MOTHRFKRRRR!!', 'Confirmed');
                console.log(response);
                $state.reload();
            }, function (err) {
                console.log(err)
                toastr.error('Problems during giving up, seems you will work on it forever RAMBO!', 'Something went wrong [' + err.data.exceptionMessage + ']');
            });
        }

        function _closeTask(taskIndex) {
            RestService.PostData(CostantUrl.urlTask, "closetask", vm.tasklist[taskIndex].id).then(function (response) {
                toastr.success('Great! Task correctly closed', 'Confirmed');
                console.log(response);
                $state.reload();
            }, function (err) {
                console.log(err)
                toastr.error('Problems closing the task', 'Something went wrong [' + err.data.exceptionMessage + ']');
            });
        }

        function _riepilogoTaskChiusi() {
            RestService.GetData(CostantUrl.urlTask, "getclosedtask").then(function (response) {
                vm.taskClosedList = response.data;


                var avatars = ApplicationService.getAllAvatars();
                if (avatars != undefined && avatars != null) {
                    for (var i = 0; i < vm.taskClosedList.length; i++) {
                        for (var k = 0; k < vm.taskClosedList[i].users.length; k++) {
                            vm.taskClosedList[i].users[k].avatar = null;
                            for (var j = 0; j < avatars.length; j++) {
                                if (vm.taskClosedList[i].users[k].id == avatars[j].userId) {
                                    vm.taskClosedList[i].users[k].avatar = avatars[j].dataHtml;
                                    break;
                                }
                            }
                            if (vm.taskClosedList[i].users[k].avatar == null) {
                                vm.taskClosedList[i].users[k].avatar = ApplicationService.getDefaultImageSrc();
                            }
                        }
                    }
                }


                console.log(response);
            });
        }


        function _userIsAlreadyAssigned(taskIndex) {
            if (vm.tasklist[taskIndex].users != null && vm.tasklist[taskIndex].users.length > 0) {
                for (var i = 0; i < vm.tasklist[taskIndex].users.length; i++) {
                    if (vm.tasklist[taskIndex].users[i].id == sessionStorage.getItem('userId')) {
                        return true;
                    }
                }
            }
            return false;
        }

    }]);