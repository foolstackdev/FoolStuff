"use strict";
angular
.module('FoolStackApp')
.controller('SignedController', ["$scope", "RestService", "CostantUrl", "toastr", "$state", "ApplicationService",
    function ($scope, RestService, CostantUrl, toastr, $state, ApplicationService) {

        var vm = this;
        vm.logout = _logout;
        vm.userAvatar = ApplicationService.getSpecificAvatar("MD");
        vm.activeMenu = _activateMenu;
        vm.setLastActive = _setLastActive;
        var lastActive;

        vm.menu = [];

        vm.userName = {};
        $scope.$on("SignedControllerTriggerAvatarReload", function (event, args) {
            vm.userAvatar = ApplicationService.getSpecificAvatar("MD");
        });

        init();
        function init() {
            console.log("Inside Signed controller");
            vm.userName = ApplicationService.getUser();
            var userRoles = ApplicationService.getUserRoleList();
            userRoles.indexOf("SuperAdmin") != -1 ? _manageUserRole("SuperAdmin") : userRoles.indexOf("FoolStackUser") != -1 ? _manageUserRole("FoolStackUser") : _logout();
        }
        function _logout() {
            sessionStorage.clear();
            toastr.success('You are now just mister nobody :)', 'Confirmed');
            $state.go("unlogged.home");
        }

        function _manageUserRole(value) {
            switch (value) {
                case "SuperAdmin":

                    vm.menu = [
                        {
                            url: "signed.homepage",
                            title: "HomePage",
                            active: true
                        },
                        {
                            url: "signed.users",
                            title: "Users",
                            active: false
                        },
                        {
                            url: "signed.tesoreria",
                            title: "Tesoreria",
                            active: false
                        },
                        {
                            url: "signed.task",
                            title: "Task",
                            active: false
                        }, {
                            url: "signed.applications",
                            title: "Applications",
                            active: false
                        }, {
                            url: "signed.roles",
                            title: "Ruoli",
                            active: false
                        }, {
                            url: "signed.events",
                            title: "Eventi",
                            active: false
                        }, {
                            url: "signed.logs",
                            title: "Logs",
                            active: false
                        }
                    ];
                    break;
                case "FoolStackUser":
                    vm.menu = [
                       {
                           url: "signed.homepage",
                           title: "HomePage",
                           active: true
                       },
                       {
                           url: "signed.users",
                           title: "Users",
                           active: false
                       },
                       {
                           url: "signed.tesoreria",
                           title: "Tesoreria",
                           active: false
                       },
                       {
                           url: "signed.events",
                           title: "Eventi",
                           active: false
                       },
                       {
                           url: "signed.task",
                           title: "Task",
                           active: false
                       }
                    ];
                    break;
                default:
                    _logout();
            }
        }


        $scope.attivaMenu = function () {
            //var id = $(this).attr('ui-sref');

            var activePage = this.menu.title;
            this.menu.active = true;
            //console.log(activePage); // il titolo della pagina
            alert(activePage);
            

            var itemsCount = this.menu.length;
             
        }

        function _setLastActive(elem) {
            if (elem.active)
                lastActive = elem;

        }

        function  _activateMenu(elem) {
            elem.active = true;

            lastActive.active = false;
            lastActive = elem;
            
        }



    }]);