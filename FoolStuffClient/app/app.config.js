angular
.module('FoolStackApp')
.config(["$locationProvider", "$urlRouterProvider", "$stateProvider", function config($locationProvider, $urlRouterProvider, $stateProvider) {

    $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/index/home');

    $stateProvider
        .state('unlogged', {
            abstract: true,
            url: "/index",
            templateUrl: "app/view/template/common/content.html"
        })
        .state('unlogged.home', {
            url: "/home",
            templateUrl: "app/view/template/public/home.html",
            data: { pageTitle: 'Home view' }
        })
        .state('unlogged.contact', {
            url: "/contact",
            templateUrl: "app/view/template/public/contact.html",
            data: { pageTitle: 'Contact view' }
        })
        .state('unlogged.login', {
            url: "/login",
            templateUrl: "app/view/template/public/login.html",
            controller: "LoginController",
            controllerAs: "loginCtrl",
            data: { pageTitle: 'Login view' }
        })
        .state('unlogged.register', {
            url: "/register",
            templateUrl: "app/view/template/public/register.html",
            controller: "RegisterController",
            controllerAs: "registerCtrl",
            data: { pageTitle: 'Register view' }
        })
        .state('unlogged.users', {
            url: "/users",
            templateUrl: "app/view/template/public/users.html",
            controller: "UsersController",
            controllerAs: "usersCtrl",
            data: { pageTitle: 'Users view' }
        })
}]);