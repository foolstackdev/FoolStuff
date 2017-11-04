"use strict";
angular
    .module('FoolStackApp')
    .controller('TestLoadController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", function ($scope, RestService, CostantUrl, $state, toastr) {

        var vm = this;
        vm.isVisible = false;

        vm.LaMiaFunzione = _LaMiaFunzione;
                  
        

        init();

        function init() {
            console.log("Inside TestLoad Controller");            
        }        

        function _LaMiaFunzione() {
            console.log("Mi hai cliccato");
            vm.isVisible = !vm.isVisible;

        }

    }]);