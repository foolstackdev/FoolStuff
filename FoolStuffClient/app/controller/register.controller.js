"use strict";
angular
.module('FoolStackApp')
.controller('RegisterController', ["$scope", "RestService", function ($scope, RestService) {

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

   

}]);