angular
.module('FoolStackApp')
.config(["$locationProvider", "$urlRouterProvider", "$stateProvider", "$httpProvider", function config($locationProvider, $urlRouterProvider, $stateProvider, $httpProvider) {

    $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/index/home');
    $httpProvider.interceptors.push('httpRequestInterceptor');

    $stateProvider
        .state('unlogged', {
            abstract: true,
            url: "/index",
            templateUrl: "app/view/template/common/content.html"
        })
        .state('signed', {
            abstract: true,
            url: "/foolstaff",
            templateUrl: "app/view/template/common/contentSigned.html",
            controller: "SignedController",
            controllerAs: "signedCtrl"
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
        .state('signed.homepage', {
            url: "/homepage",
            templateUrl: "app/view/template/private/homepage.html",
            data: { pageTitle: 'Users view' }
        })
        .state('signed.users', {
            url: "/users",
            templateUrl: "app/view/template/private/users.html",
            controller: "UsersController",
            controllerAs: "usersCtrl",
            data: { pageTitle: 'Users view' }
        })
        .state('signed.tesoreria', {
            url: "/tesoreria",
            templateUrl: "app/view/template/private/tesoreria.html",
            controller: "TesoreriaController",
            controllerAs: "tesoreriaCtrl",
            data: { pageTitle: 'Users view' }
        })
         .state('signed.update', {
             url: "/update",
             templateUrl: "app/view/template/private/update.html",
             controller: "UpdateController",
             controllerAs: "updateCtrl",
             data: { pageTitle: 'Users view' }
         })
         .state('signed.task', {
             url: "/task",
             templateUrl: "app/view/template/private/task.html",
             controller: "TaskController",
             controllerAs: "taskCtrl",
             data: { pageTitle: 'Users view' }
         })
     .state('signed.updatePass', {
         url: "/updatePass",
         templateUrl: "app/view/template/private/updatePass.html",
         controller: "UpdatePassController",
         controllerAs: "updatePassCtrl",
         data: { pageTitle: 'Users view' }
     })
}])
.run([function () {
    //sessionStorage.clear();
}])
.service('httpRequestInterceptor', [function () {
    var service = this;

    service.request = function (config) {
        if (sessionStorage.getItem('accessToken') != null)
            config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('accessToken');
        return config;
    };
}]);