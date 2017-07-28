"use strict";
angular
.module('FoolStackApp')
.controller('UpdateController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", function ($scope, RestService, CostantUrl, $state, toastr) {

    var vm = this;

    vm.user = {
        name: "",
        surname: "",
        phone: "",
        email: ""
    };

    vm.updateUser = _updateUser;


    init();
    function init() {
        console.log("Inside Update controller");
    }

    function _updateUser() {
        console.log(vm.user);

        RestService.PostData(CostantUrl.urlUserAccount, "updateuserinfo",sessionStorage.getItem('userId'), vm.user).then(function (response) {
            toastr.success('Cool, your personal data has been updated!', 'Confirmed');
            $state.go("unlogged.home");
        }, function (err) {
            console.log(err)
            toastr.error('Problems during update', 'Something went wrong [' + err + ']');
        });
    }

}]);