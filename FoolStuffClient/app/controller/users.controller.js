"use strict";
angular
.module('FoolStackApp')
.controller('UsersController', ["$scope", "RestService", "CostantUrl", "toastr", "ApplicationService",
    function ($scope, RestService, CostantUrl, toastr, ApplicationService) {

        var vm = this;
        vm.users = [];

        init();
        function init() {
            console.log("Inside Users controller");
            RestService.GetData(CostantUrl.urlUserAccount, "allusers").then(function (response) {
                vm.users = response.data;
                vm.users = ApplicationService.addAvatarToUsers(vm.users);
                console.log(vm.users);
            })

        }
    }]);