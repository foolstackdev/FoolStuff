"use strict";
angular
    .module('FoolStackApp')
    .controller('ApplicationsController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", "$timeout", function ($scope, RestService, CostantUrl, $state, toastr, $timeout) {

        var vm = this;

        vm.dataOdierna = "";

        init();


        //function init() {
        //    //Contattare il server
        //    RestService.GetData("http://localhost:51989/api/testfoolstack/", "getpresentdate").then(function (response) {
        //        //recuperare dal server la data corrente
        //        console.log(response);
        //        vm.dataOdierna = response.data;
        //    }, function (err) {

        //    });

        //    //inserire la data nela pagina
        //}






















        //$scope.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales", "Tele Sales", "Corporate Sales"];
        //$scope.data = [300, 500, 100, 40, 120];
        //$scope.options = { legend: { display: false } };


        vm.labels = ["January", "February", "March", "April", "May", "June", "July"];
        vm.series = ['Series A', 'Series B'];
        vm.data = [
            [65, 59, 80, 81, 56, 55, 40],
            [28, 48, 40, 19, 86, 27, 90]
        ];
        vm.onClick = function (points, evt) {
            console.log(points, evt);
        };

        // Simulate async data update
        $timeout(function () {
            $scope.data = [
                [28, 48, 40, 19, 86, 27, 90],
                [65, 59, 80, 81, 56, 55, 40]
            ];
        }, 3000);
        //END FIRST

        //BEGIN FIRST
        $scope.labels = ["January", "February", "March", "April", "May", "June", "July"];
        $scope.series = ['Series A', 'Series B'];
        $scope.data = [
            [65, 59, 80, 81, 56, 55, 40],
            [28, 48, 40, 19, 86, 27, 90]
        ];
        $scope.onClick = function (points, evt) {
            console.log(points, evt);
        };
        $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }, { yAxisID: 'y-axis-2' }];
        $scope.options = {
            scales: {
                yAxes: [
                    {
                        id: 'y-axis-1',
                        type: 'linear',
                        display: true,
                        position: 'left'
                    },
                    {
                        id: 'y-axis-2',
                        type: 'linear',
                        display: true,
                        position: 'right'
                    }
                ]
            }
        };
        //END SECOND
        init();

        function init() {
            console.log("Inside Applications Controller");
            console.log($scope);
        }





    }]);