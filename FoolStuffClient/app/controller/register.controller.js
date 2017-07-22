"use strict";
angular
.module('FoolStackApp')
.controller('RegisterController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", function ($scope, RestService, CostantUrl, $state, toastr) {

    var vm = this;

    vm.user = {
        name: "",
        surname: "",
        phone: "",
        email: "",
        password: ""
    };

    vm.signUser = _signUser;


    init();
    function init() {
        console.log("Inside Register controller");
    }

    function _signUser() {
        console.log(vm.user);

        RestService.PostData(CostantUrl.urlAccount, "register", vm.user).then(function (response) {
            toastr.success('Cool, you\' re now a FoolStack member', 'Confirmed');
            $state.go("unlogged.home");
        }, function (err) {
            console.log(err)
            toastr.error('Problems during registration', 'Something went wrong [' + err + ']');
        });
    }

}]);