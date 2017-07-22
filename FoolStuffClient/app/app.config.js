angular
.module('FoolStackApp')
.config(["$locationProvider", "$urlRouterProvider", "$stateProvider", "$httpProvider", function config($locationProvider, $urlRouterProvider, $stateProvider, $httpProvider) {

    $locationProvider.html5Mode(true);
    $urlRouterProvider.otherwise('/index/home');
    //$httpProvider.interceptors.push('authInterceptor');
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
        .state('signed.users', {
            url: "/users",
            templateUrl: "app/view/template/public/users.html",
            controller: "UsersController",
            controllerAs: "usersCtrl",
            data: { pageTitle: 'Users view' }
        })
}])
.run([function () {
    sessionStorage.clear();
}])
.service('httpRequestInterceptor', [function () {
    var service = this;

    service.request = function (config) {
        if (sessionStorage.getItem('accessToken') != null)
            config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('accessToken');
        return config;
    };
}]);
//.factory('httpRequestInterceptor', function ($http) {
//    return {
//        request: function (config) {
//            $http.defaults.headers.common['Authorization'] = 'Bearer ' + sessionStorage.getItem('accessToken');
//            //config.headers['Authorization'] = 'Basic d2VudHdvcnRobWFuOkNoYW5nZV9tZQ==';
//            //config.headers['Accept'] = 'application/json;odata=verbose';

//            return config;
//        }
//    };
//});
//.factory('authInterceptor', function () {
//    return {
//        request: function (config) {
//            config.headers = config.headers || {};
//            if (sessionStorage.getItem('accessToken')) {
//                config.headers.Authorization = 'Bearer ' + sessionStorage.getItem('accessToken');
//            }
//        },
//        responseError: function (response) {

//            //if (response.status === 401) {
//            //    sessionStorage.clear();
//            //}
//        }
//    }
//});



//.run(['$http', function ($http) {
//    if (sessionStorage.getItem('accessToken') != null)
//        $http.defaults.headers.common['Authorization'] = 'Bearer ' + sessionStorage.getItem('accessToken') || '';
//}
//]);
//.factory('restInterceptor', ["ConstantTimeSession", "$location", "$q", "$cookieStore", "$filter", function (ConstantTimeSession, $location, $q, $cookieStore, $filter) {
//}])
//.config(['$httpProvider', function ($httpProvider) {
//    $httpProvider.interceptors.push('restInterceptor');
//}]);