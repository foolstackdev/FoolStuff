
angular.module("FoolStackApp").directive("footer", function () {
    return {
        restrict: 'A',
        templateUrl: 'app/view/directives/footer/footer.html',
        scope: true,
        transclude: false,
        controller: 'FooterController'
    };
});
