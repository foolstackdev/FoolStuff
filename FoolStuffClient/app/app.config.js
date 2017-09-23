angular
.module('FoolStackApp')
.config(["$locationProvider", "$urlRouterProvider", "$stateProvider", "$httpProvider", "usSpinnerConfigProvider", function config($locationProvider, $urlRouterProvider, $stateProvider, $httpProvider, usSpinnerConfigProvider) {

    $httpProvider.defaults.useXDomain = true;
    $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/index/home');

    $httpProvider.interceptors.push('httpRequestInterceptor');

    usSpinnerConfigProvider.setDefaults({ color: 'blue' });

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
        .state('signed.userPage', {
            url: "/userPage",
            templateUrl: "app/view/template/private/userPage.html",
            controller: "UserPageController",
            controllerAs: "userPageCtrl",
            data: { pageTitle: 'Users Page view' }
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
    console.log(service);
    service.request = function (config) {
        if (sessionStorage.getItem('accessToken') != null)
            config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('accessToken');
        return config;
    };


    //return {
    //    'request': function (config) {
    //        //$rootScope.$broadcast('start-spin');
    //        if (sessionStorage.getItem('accessToken') != null)
    //            config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('accessToken');
    //        return config;
    //    },
    //    'response': function (response) {
    //        console.log("response")
    //    },
    //};

    //return {
    //    'request': function (config) {
    //        //$rootScope.$broadcast('start-spin');
    //        if (sessionStorage.getItem('accessToken') != null)
    //            config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('accessToken');
    //        return config;
    //    },
    //    'response': function (response) {
    //        console.log("response")
    //    },
    //    'responseError': function (rejection) {
    //        console.log("responseerror")
    //    }
    //};







    console.log(service);
    //service.response = function (rrr) {
    //    $rootScope.$broadcast('stop-spin');
    //};

    //service.requestError = function (error) {
    //    $rootScope.$broadcast('stop-spin');
    //};

    //service.responseError = function (responseError) {
    //    $rootScope.$broadcast('stop-spin');
    //};

}]);
//.service('httpRequestInterceptor', ["$rootScope",function ($rootScope) {
//    var service = this;

//    service.request = function (config) {
//        $rootScope.$broadcast('start-spin');
//        if (sessionStorage.getItem('accessToken') != null)
//            config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('accessToken');
//        return config;
//    };

//    service.response = function (response) {
//        $rootScope.$broadcast('stop-spin');
//    };

//    service.requestError = function (error) {
//        $rootScope.$broadcast('stop-spin');
//    };

//    service.responseError = function (responseError) {
//        $rootScope.$broadcast('stop-spin');
//    };

//}]);