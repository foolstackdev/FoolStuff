"use strict";
angular
.module('FoolStackApp')
.controller('TaskController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", function ($scope, RestService, CostantUrl, $state, toastr) {

    var vm = this;
    vm.task;
    vm.tasklist = [];

    vm.riepilogoTask = _riepilogoTask;
    vm.insertTask = _insertTask;
    vm.takeTask = _takeTask;

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
            toastr.error('Problems during task insert', 'Something went wrong [' + err + ']');
        });
    }
    function _takeTask(index) {
        console.log(index);
    }


}]);