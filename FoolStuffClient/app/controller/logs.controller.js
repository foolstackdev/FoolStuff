"use strict";
angular
.module('FoolStackApp')
.controller('LogsController', ["$scope", "RestService", "CostantUrl", "toastr", "$state", "$rootScope", "ApplicationService",
    function ($scope, RestService, CostantUrl, toastr, $state, $rootScope, ApplicationService) {

        var vm = this;

        vm.logs = [];
        vm.fileContent = "";

        init();
        function init() {
            console.log("Inside Logs controller");

            RestService.GetData(CostantUrl.urlFile, "getlogfilelist").then(function (response) {
                console.log(response);
                vm.logs = response.data;
                var isFirst = 0;
                var theFirst = "";
                for (var i = 0; i < vm.logs.length; i++) {
                    if (vm.logs[i].dateLastUpdate > isFirst) {
                        isFirst = vm.logs[i].dateLastUpdate;
                        theFirst = vm.logs[i].fileName;
                    }
                }
                _getFileLogByFileName(theFirst);

            }, function (err) {
                console.log(err);
            })
        }

        function _getFileLogByFileName(filename) {
            RestService.GetData(CostantUrl.urlFile, "getlogfile/" + filename + "/").then(function (response) {
                console.log(response);
                vm.fileContent = response.data.content;
            }, function (err) {
                console.log(err);
            })
        }





    }]);