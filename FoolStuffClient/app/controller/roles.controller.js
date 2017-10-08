"use strict";
angular
    .module('FoolStackApp')
    .controller('RolesController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", function ($scope, RestService, CostantUrl, $state, toastr) {

        var vm = this;

        vm.roles = [];
        vm.newRole = {
            "Name": ""
        };

        vm.addRole = _addRole;

        init();

        function init() {
            console.log("Inside Roles Controller");
            _getAllRoles();
        }

        function _getAllRoles() {
            RestService.GetData(CostantUrl.urlRoles, "getallroles").then(function (response) {
                vm.roles = response.data;
                console.log(response);
            });
        }

        function _addRole() {
            if (vm.newRole.Name == '')
                return;
            RestService.PostData(CostantUrl.urlRoles, "addrole", vm.newRole).then(function (response) {
                console.log(response);
                toastr.success('Role inserted', 'Confirmed');
                vm.newRole.Name = '';
                _getAllRoles();
            }, function (err) {
                console.log(err);
                toastr.error('Something went wrong [' + err.data.message + ']', 'Problems..');
            });
        }




    }]);