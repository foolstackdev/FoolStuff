﻿"use strict";
angular
    .module("FoolStackApp")
    .factory("RestService", ["$http", "$q", function ($http, $q) {

        return {
            GetData: function () {
                var deferred = $q.defer();
                $http.get("https://jsonplaceholder.typicode.com/posts/1/comments")
                    .then(function (result) {
                        console.log("Success");
                        deferred.resolve(result);
                    }, function(err) {
                        console.log(err);
                        deferred.reject(err);
                    });
                return deferred.promise;
            },
            PostData: function (json) {
            var deferred = $q.defer();
            $http.post("https://jsonplaceholder.typicode.com/posts/1/comments", json)
                .then(function (result) {
                    console.log("Success");
                    deferred.resolve(result);
                }, function(err) {
                    console.log(err);
                    deferred.reject(err);
                });
            return deferred.promise;
        }
        };

    }]);
