"use strict";
angular
    .module("FoolStackApp")
    .factory("RestService", ["$http", "$q", "$state", "toastr", function ($http, $q, $state, toastr) {

        return {
            GetData: function (path, method) {
                var deferred = $q.defer();
                $http.get(path + method)
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
            }
        };

    }]);
