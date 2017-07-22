"use strict";
angular
    .module("FoolStackApp")
    .factory("RestService", ["$http", "$q", "$state", "toastr", function ($http, $q, $state, toastr) {

        return {
            GetData: function (path, method) {
                var deferred = $q.defer();
                var urltoCall = path + method;
                $http.get(urltoCall)
                    .then(function (result) {
                        console.log("Success");
                        deferred.resolve(result);
                    }, function (err) {
                        if (err.status == 401) {
                            toastr.error('Not allowed, you should first login', 'Unauthorized');
                            $state.go("unlogged.login");
                        }
                        console.log(err);
                        deferred.reject(err);
                    });
                return deferred.promise;
            },
            PostData: function (path, method, json) {
                var deferred = $q.defer();
                $http.post(path + method, json)
                    .then(function (result) {
                        console.log("Success");
                        deferred.resolve(result);
                    }, function (err) {
                        if (err.status == 401) {
                            toastr.error('Not allowed, you should first login', 'Unauthorized');
                            $state.go("unlogged.login");
                        }
                        console.log(err);
                        deferred.reject(err);
                    });
                return deferred.promise;
            },
            PostContentTypeText: function (path, method, text) {
                var deferred = $q.defer();
                var urltoCall = path + method;
                $http({
                    method: 'POST',
                    url: urltoCall,
                    data: text,
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    }
                }).then(function (result) {
                    console.log(result);
                    deferred.resolve(result);
                }, function (error) {
                    console.log(error);
                    deferred.reject(error);
                })



                //$http.post(path + method, text)
                //.then(function (result) {
                //    console.log("Success");
                //    deferred.resolve(result);
                //}, function (err) {
                //    if (err.status == 401) {
                //        toastr.error('Not allowed, you should first login', 'Unauthorized');
                //        $state.go("unlogged.login");
                //    }
                //    console.log(err);
                //    deferred.reject(err);
                //});
                return deferred.promise;
            }
        };

    }]);
