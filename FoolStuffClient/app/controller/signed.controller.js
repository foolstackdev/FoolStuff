"use strict";
angular
.module('FoolStackApp')
.controller('SignedController', ["$scope", "RestService", "CostantUrl", "toastr", "$state", "ApplicationService",
    function ($scope, RestService, CostantUrl, toastr, $state, ApplicationService) {

        var vm = this;
        vm.logout = _logout;
        vm.userAvatar = ApplicationService.getSpecificAvatar("MD");

        vm.userName = {};
        $scope.$on("SignedControllerTriggerAvatarReload", function (event, args) {
            vm.userAvatar = ApplicationService.getSpecificAvatar("MD");
        });

        init();
        function init() {
            console.log("Inside Signed controller");
            vm.userName = JSON.parse(sessionStorage.getItem('user'));
        }
        function _logout() {
            sessionStorage.clear();
            toastr.success('You are now just mister nobody :)', 'Confirmed');
            $state.go("unlogged.home");
        }

    }]);