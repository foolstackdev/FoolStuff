"use strict";
angular
.module('FoolStackApp')
.controller('TaskController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", function ($scope, RestService, CostantUrl, $state, toastr) {

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
            console.log(response);
        });
    }

    function _insertTask() {
        RestService.PostData(CostantUrl.urlTask, "insertnewtask", vm.task).then(function (response) {
            vm.tasklist = response.data;
            toastr.success('Task inserted', 'Confirmed');
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
            vm.tasklist = response.data;
        }, function (err) {
            console.log(err)
            toastr.error('Problems during task assignment', 'Something went wrong [' + err.data.exceptionMessage + ']');
        });
    }

    function _giveUpTask(taskIndex) {
        RestService.PostData(CostantUrl.urlTask, "giveuptask/" + sessionStorage.getItem('userId') + "/", vm.tasklist[taskIndex].id).then(function (response) {
            toastr.warning('Never give up! Be a MOTHRFKRRRR!!', 'Confirmed');
            console.log(response);
            vm.tasklist = response.data;
        }, function (err) {
            console.log(err)
            toastr.error('Problems during giving up, seems you will work on it forever RAMBO!', 'Something went wrong [' + err.data.exceptionMessage + ']');
        });
    }

    function _closeTask(taskIndex) {
        RestService.PostData(CostantUrl.urlTask, "closetask", vm.tasklist[taskIndex].id).then(function (response) {
            toastr.success('Great! Task correctly closed', 'Confirmed');
            console.log(response);
            vm.tasklist = response.data;
        }, function (err) {
            console.log(err)
            toastr.error('Problems closing the task', 'Something went wrong [' + err.data.exceptionMessage + ']');
        });
    }

    function _riepilogoTaskChiusi() {
        RestService.GetData(CostantUrl.urlTask, "getclosedtask").then(function (response) {
            vm.taskClosedList = response.data;
            console.log(response);
        });
    }


    function _userIsAlreadyAssigned(taskIndex) {
        if (vm.tasklist[taskIndex].userInfo.length > 0) {
            for (var i = 0; i < vm.tasklist[taskIndex].userInfo.length; i++) {
                if (vm.tasklist[taskIndex].userInfo[i].id == sessionStorage.getItem('userId')) {
                    return true;
                }
            }
        }
        return false;
    }

}]);