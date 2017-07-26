"use strict";
angular
.module('FoolStackApp')
.controller('LoginController', ["$scope", "RestService", "CostantUrl", "toastr", "$state", function ($scope, RestService, CostantUrl, toastr, $state) {

    var vm = this;
    vm.login = _login;
    vm.user = {
        username: "",
        password: "",
        grant_type: "password"
    }



    init();
    function init() {
        console.log("Inside Login controller");
    }

    function _login() {

        var JsonObj = "userName=" + vm.user.username + "&password=" + vm.user.password + "&grant_type=password";

        RestService.PostContentTypeText(CostantUrl.urlToken, "token", JsonObj).then(function (response) {
            sessionStorage.setItem('accessToken', response.data.access_token);
            RestService.GetData(CostantUrl.urlUserAccount, "getuserinfo/" + vm.user.username + "/").then(function (responseUser) {
                console.log(responseUser);
                sessionStorage.setItem('userId', responseUser.data.id);
                sessionStorage.setItem('user', responseUser.data);
                toastr.success('Cool, you\' re now logged', 'Confirmed');
                $state.go("signed.homepage");
            }, function (err) {
                console.log(err);
                throw err;
            });
        }, function (err) {
            console.log(err)
            if (err.data == null)
                toastr.error('Problems during login, maybe username or password are incorrect', 'Something went wrong');
            else
                toastr.error('Problems during login [' + err.data.error_description + ']', 'Something went wrong');
        });

    }


}]);