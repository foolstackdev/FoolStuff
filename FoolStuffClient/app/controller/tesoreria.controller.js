"use strict";
angular
.module('FoolStackApp')
.controller('TesoreriaController', ["$scope", "RestService", "CostantUrl", "toastr", function ($scope, RestService, CostantUrl, toastr) {

    var vm = this;
    vm.users = [];
    vm.userSelected = [];

    vm.pushUserSelected = _pushUserSelected;
    vm.updatePayment = _updatePayment;

    init();
    function init() {
        console.log("Inside Tesoreria controller");
        RestService.GetData(CostantUrl.urlUserAccount, "allusers").then(function (response) {
            vm.userSelected = [];
            vm.users = response.data;
            angular.forEach(vm.users, function (value) {
                var item = {
                    check: false,
                    user: value
                }
                console.log(item);
                vm.userSelected.push(item);
            });
        })
    }

    function _pushUserSelected(index) {
        console.log(index);
        console.log(vm.userSelected[index].check);
        vm.userSelected[index].check = !vm.userSelected[index].check;
        console.log(vm.userSelected[index].check);
    }

    function _updatePayment() {
        var itemToSend = [];
        angular.forEach(vm.userSelected, function (value) {
            if (value.check == true)
                itemToSend.push(value.user);
        });

        RestService.PostData(CostantUrl.urlUserAccount, "insertpayment", itemToSend).then(function (response) {
            toastr.success('Payments updated', 'Confirmed');
            $state.go("unlogged.tesoreria");
        }, function (err) {
            console.log(err)
            toastr.error('Problems during payments', 'Something went wrong [' + err + ']');
        });

    }

}]);