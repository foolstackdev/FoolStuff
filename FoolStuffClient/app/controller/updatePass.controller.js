"use strict";
angular
.module('FoolStackApp')
.controller('UpdatePassController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", function ($scope, RestService, CostantUrl, $state, toastr) {

    var vm = this;

     vm.model = {
        oldPassword: "",
        newPassword: "",
        confirmPassword: ""
    };

     vm.updatePass = _updatePass;

    init();
    function init() {
        console.log("Inside update Password controller");
    }

    function _updatePass() {
        console.log(vm.model);

        RestService.PostData(CostantUrl.urlAccount, "ChangePassword", vm.model).then(function (response) {
            toastr.success('Cool, your password has been updated', 'Confirmed');
            
        }, function (err) {
            console.log(err)
            toastr.error('Problems during update', 'Something went wrong [' + err + ']');
        });
    }

}]);