"use strict";
angular
.module('FoolStackApp')
.controller('UsersController', ["$scope", "RestService", "CostantUrl", "toastr", function ($scope, RestService, CostantUrl, toastr) {

    var vm = this;
    vm.users = [];

    init();
    function init() {
        console.log("Inside Users controller");
        RestService.GetData(CostantUrl.urlAccount, "allusers").then(function (response) {
            vm.users = response.data;
        })
    }
}]);