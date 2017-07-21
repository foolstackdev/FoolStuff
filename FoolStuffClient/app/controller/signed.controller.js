"use strict";
angular
.module('FoolStackApp')
.controller('SignedController', ["$scope", "RestService", "CostantUrl", "toastr", "$state", function ($scope, RestService, CostantUrl, toastr,$state) {

    var vm = this;
    vm.logout = _logout;

    init();
    function init() {
        console.log("Inside Signed controller");
    }
    function _logout() {
        sessionStorage.clear();
        toastr.success('You are now just mister nobody :)', 'Confirmed');
        $state.go("unlogged.home");
    }

}]);