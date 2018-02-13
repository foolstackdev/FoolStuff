"use strict";
angular
    .module('FoolStackApp')
    .controller('HomeController', ["$scope", "RestService", "CostantUrl", "toastr", "$state", "$rootScope", "ApplicationService",
        function ($scope, RestService, CostantUrl, toastr, $state, $rootScope, ApplicationService) {

            var vm = this;
         

            init();
            function init() {
                console.log("Inside Home controller");
                _reload();
            }

            function _reload() {
                RestService.GetData(CostantUrl.urlHome, "getallinfo").then(function (response) {
                    console.log(response);
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems during insertion', 'Something went wrong [' + err + ']');
                });
            }
          


        }]);