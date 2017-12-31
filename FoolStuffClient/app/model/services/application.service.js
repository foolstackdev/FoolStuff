"use strict";
angular
    .module("FoolStackApp")
    .factory("ApplicationService", ["$rootScope", function ($rootScope) {



        var application = {}
        var entities = {};

        application.getUserAvatar = function () {
            return entities.userAvatar;
        }
        application.setUserAvatar = function () {
            entities.userAvatar = JSON.parse(sessionStorage.getItem("userAvatar"));
            if (entities.userAvatar != undefined && entities.userAvatar != null) {
                for (var i = 0; i < entities.userAvatar.length; i++) {
                    entities.userAvatar[i].dataHtml = "data:" + entities.userAvatar[i].type + ";base64," + entities.userAvatar[i].data;
                }
                sessionStorage.setItem("userAvatar", JSON.stringify(entities.userAvatar))
            }
        }
        application.getSpecificAvatar = function (value) {
            entities.userAvatar = JSON.parse(sessionStorage.getItem("userAvatar"));
            var avatar = entities.userAvatar;
            if (avatar != undefined && avatar != null) {
                for (var i = 0; i < avatar.length; i++) {
                    if (avatar[i].size == value) {
                        return avatar[i].dataHtml;
                    }
                }
            }
            return "app/view/assets/img/user.png";
        }

        return application;

    }]);
