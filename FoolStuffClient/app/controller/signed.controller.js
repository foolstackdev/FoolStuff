"use strict";
angular
.module('FoolStackApp')
.controller('SignedController', ["$scope", "RestService", "CostantUrl", "toastr", "$state", "usSpinnerService", function ($scope, RestService, CostantUrl, toastr, $state, usSpinnerService) {

    var vm = this;
    vm.logout = _logout;

    vm.userName = JSON.parse(sessionStorage.getItem('user'));
    var obj = this.userName;
    
    init();
    function init() {
        console.log("Inside Signed controller");
    }
    function _logout() {
        sessionStorage.clear();
        toastr.success('You are now just mister nobody :)', 'Confirmed');
        $state.go("unlogged.home");
    }

    $scope.$on('start-spin', function (event, args) {
        usSpinnerService.spin('spinner-1');
    });
    $scope.$on('stop-spin', function (event, args) {
        usSpinnerService.stop('spinner-1');
    });


}]);