"use strict";
angular
    .module('FoolStackApp')
    .controller('ProfileController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", function ($scope, RestService, CostantUrl, $state, toastr) {

        var vm = this;

        vm.user = {
          
        };

        init();

        function init() {
            console.log("Inside Profile Controller");
        }
        


        

    }]);