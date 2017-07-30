"use strict";
angular
.module('FoolStackApp')
.controller('TesoreriaController', ["$scope", "RestService", "CostantUrl", "toastr", function ($scope, RestService, CostantUrl, toastr) {

    var vm = this;

    vm.dateSelected;

    vm.users = [];
    vm.userSelected = [];
    vm.versamento = {};
    vm.infoEntrate = [];
    vm.infoUscite = [];

    vm.pushUserSelected = _pushUserSelected;
    vm.updatePayment = _updatePayment;
    vm.getUsers = _getUsers;
    vm.riepilogoEntrate = _riepilogoEntrate;
    vm.riepilogoUscite = _riepilogoUscite;
    vm.openDP = _openDP;
    vm.insertVersamento = _insertVersamento;

    vm.popupDP = {
        opened: false
    };
    var formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate','dd/MM/yyyy'];
    vm.format = formats[4];

    init();
    function init() {
        console.log("Inside Tesoreria controller");

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

        RestService.PostData(CostantUrl.urlTesoreria, "insertpayment/"+vm.dateSelected, itemToSend).then(function (response) {
            toastr.success('Payments updated', 'Confirmed');
        }, function (err) {
            console.log(err)
            toastr.error('Problems during payments', 'Something went wrong [' + err + ']');
        });
    }

    function _riepilogoEntrate() {
        RestService.GetData(CostantUrl.urlTesoreria, "getallentry").then(function (response) {
            console.log(response);
            vm.infoEntrate = response.data;
        });
    }

    function _riepilogoUscite() {
        RestService.GetData(CostantUrl.urlTesoreria, "getallexit").then(function (response) {
            console.log(response);
            vm.infoUscite = response.data;
        });
    }

    function _getUsers() {
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
        });
    }


    function _insertVersamento() {
        console.log(vm.versamento);
        vm.versamento.dataOperazione.setHours(vm.versamento.dataOperazione.getHours() + 2);
        RestService.PostData(CostantUrl.urlTesoreria, "insertpaymentdate", vm.versamento).then(function (response) {
            toastr.success('New Payment updated', 'Confirmed');
            vm.infoEntrate = response.data;
        }, function (err) {
            console.log(err)
            toastr.error('Problems during payments', 'Something went wrong [' + err + ']');
        });
        //_riepilogoEntrateUscite()
    }

    function _openDP() {
        vm.popupDP.opened = true;
    }

}]);