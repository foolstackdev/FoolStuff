
angular.module("FoolStackApp").controller("FooterController", function ($scope) {

    $scope.submitFeedback = function (action) {
        //good or bad...
        if (action) {
            //good

            //submit using $http or service

        }
        else {
            //bad

            //submit using $http or service
        }
    }

});
