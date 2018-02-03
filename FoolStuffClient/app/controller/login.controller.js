"use strict";
angular
.module('FoolStackApp')
.controller('LoginController', ["$scope", "RestService", "CostantUrl", "toastr", "$state", "$rootScope", "ApplicationService",
    function ($scope, RestService, CostantUrl, toastr, $state, $rootScope, ApplicationService) {

        var vm = this;
        vm.login = _login;
        vm.isLoading = false;

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
            sessionStorage.clear();
            var JsonObj = "userName=" + vm.user.username + "&password=" + vm.user.password + "&grant_type=password";
            vm.isLoading = true;


            //RestService.PostContentTypeText(CostantUrl.urlToken, "token", JsonObj).then(function (response) {
            //    sessionStorage.setItem('accessToken', response.data.access_token);
            //    RestService.GetData(CostantUrl.urlUserAccount, "getuserinfo/" + vm.user.username + "/").then(function (responseUser) {
            //        console.log(responseUser);
            //        if (responseUser.data.userRolesList.indexOf("SimpleUser") != -1) {
            //            toastr.warning('Wait for your confirmation', 'Be patient..');
            //            return;
            //        }
            //        if (responseUser.data.userAvatar != null) {
            //            sessionStorage.setItem("userAvatar", JSON.stringify(responseUser.data.userAvatar))
            //            ApplicationService.setUserAvatar();
            //        }
            //        ApplicationService.setUserId(responseUser.data.userInfo.id);
            //        ApplicationService.setUser(JSON.stringify(responseUser.data));
            //        ApplicationService.setUserRoleList(JSON.stringify(responseUser.data.userRolesList));
            //        ApplicationService.loadUsersAvatar("XS");
            //        toastr.success('Cool, you\' re now logged', 'Confirmed');
            //        $rootScope.$broadcast('stop-spin');
            //        $state.go("signed.homepage");
            //    }, function (err) {
            //        console.log(err);
            //        sessionStorage.clear();
            //        $rootScope.$broadcast('stop-spin');
            //        throw err;
            //    });
            //}, function (err) {
            //    console.log(err);
            //    $rootScope.$broadcast('stop-spin');
            //    sessionStorage.clear();
            //    if (err.data == null)
            //        toastr.error('Problems during login, maybe username or password are incorrect', 'Something went wrong');
            //    else
            //        toastr.error('Problems during login [' + err.data.error_description + ']', 'Something went wrong');
            //});

        }


    }]);