"use strict";
angular
    .module('FoolStackApp')
    .controller('RolesController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", "$sce",
        function ($scope, RestService, CostantUrl, $state, toastr, $sce) {

            var vm = this;

            vm.roles = [];
            vm.rolesList = [];
            vm.newRole = {
                "Name": ""
            };

            vm.addRole = _addRole;
            vm.changeRoleToUser = _changeRoleToUser;
            vm.changeRole = _changeRole;

            init();

            function init() {
                console.log("Inside Roles Controller");
                _reload();
            }

            function _reload() {
                _getAllUsersWithRoles();

            }

            function _getAllUsersWithRoles() {
                RestService.GetData(CostantUrl.urlRoles, "getalluserswithroles").then(function (response) {
                    vm.roles = response.data;
                    console.log(response);
                    _getRolesList();
                });
            }
            function _getRolesList() {
                RestService.GetData(CostantUrl.urlRoles, "getroleslist").then(function (response) {
                    //Inserisco id ruolo nell'oggetto vm.roles per garantire integrità con la select nella vista
                    var resproles = response.data;
                    for (var i = 0; i < vm.roles.length; i++) {
                        for (var j = 0; j < resproles.length; j++) {
                            if (vm.roles[i].roles == resproles[j].name) {
                                vm.roles[i].roleId = resproles[j].id
                            }
                            vm.roles[i].roleChanged = false;
                            vm.roles[i].oldRole = vm.roles[i].roles;
                        }
                    }
                    vm.rolesList = response.data;
                    console.log(response);
                });
            }

            function _changeRole(item) {
                for (var i = 0; i < vm.rolesList.length; i++) {
                    if (vm.rolesList[i].id == item.roleId) {
                        item.roles = vm.rolesList[i].name;
                    }
                }
                item.roleChanged = true;
            }

            function _changeRoleToUser(item) {
                console.log(item);

                var oUser = {
                    userId: item.user.id,
                    roleId: item.roleId,
                    role: item.roles,
                    oldRole: item.oldRole
                }
                RestService.PostData(CostantUrl.urlRoles, "changerole", oUser).then(function (response) {
                    console.log(response);
                    toastr.success('Role Canged', 'Confirmed');
                    _reload();
                }, function (err) {
                    console.log(err);
                    toastr.error('Something went wrong [' + err.data.message + ']', 'Problems..');
                });
            }

            function _addRole() {
                if (vm.newRole.Name == '')
                    return;
                RestService.PostData(CostantUrl.urlRoles, "addrole", vm.newRole).then(function (response) {
                    console.log(response);
                    toastr.success('Role inserted', 'Confirmed');
                    vm.newRole.Name = '';
                    _getAllUsersWithRoles();
                }, function (err) {
                    console.log(err);
                    toastr.error('Something went wrong [' + err.data.message + ']', 'Problems..');
                });
            }

            $scope.popoverSingleItem = function (item) {
                var ostring = "<b>" + item + "<b>"
                return $sce.trustAsHtml(ostring);
            }



        }]);