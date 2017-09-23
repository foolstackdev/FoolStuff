"use strict";
angular
.module('FoolStackApp')
.controller('UserPageController', ["$scope", "RestService", "CostantUrl", "toastr", function ($scope, RestService, CostantUrl, toastr) {

    var vm = this;
    vm.users = [];

    init();
    function init() {
        console.log("Inside User Page controller");
        //RestService.GetData(CostantUrl.urlUserAccount, "allusers").then(function (response) {
        //    vm.users = response.data;
        //})
    }
}]);