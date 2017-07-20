"use strict";
angular
.module('FoolStackApp')
.controller('RegisterController', ["$scope", "RestService", "CostantUrl", function ($scope, RestService, CostantUrl) {

    var vm = this;

    vm.user = {
        name: "",
        surname: "",
        phone: "",
        address: "",
        email: "",
        password: ""
        
    };



    vm.signUser = _signUser;


    init();
    function init() {
        console.log("Inside Register controller");
        //chiamo un servizio rest per recuperare dei dati
        //RestService.GetData().then(function (response) {
        //    console.log(response);
        //    $scope.datiScaricati = response.data;
        //}, function (err) {
        //    console.log(err);
        //});
        //console.log("asincrono");
        //elaboro i dati per il binding
    }

    function _signUser() {
        console.log(vm.user);

        //RestService.PostData(CostantUrl.urlAccount, "register", vm.user).then(function (response) {
        //    console.log(response);
        //})


    }

}]);