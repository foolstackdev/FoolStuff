"use strict";
angular
    .module("FoolStackApp")
    .factory("ApplicationService", ["$rootScope", "RestService", "CostantUrl", function ($rootScope, RestService, CostantUrl) {

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
        application.getDefaultImageSrc = function () {
            return "app/view/assets/img/XS/user.png";
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
            return "app/view/assets/img/XS/user.png";
        }

        application.getAllAvatars = function () {
            var avatars = JSON.parse(sessionStorage.getItem("usersAvatar"));
            return avatars;
        }

        application.loadUsersAvatar = function (size) {
            if (size == undefined || size == null)
                size = 0;
            RestService.GetData(CostantUrl.urlUserAccount, "allusersavatar/" + size).then(function (responseUser) {
                console.log(responseUser);
                var resp = responseUser.data;
                if (resp != undefined && resp != null) {
                    for (var i = 0; i < resp.length; i++) {
                        if (resp[i].avatars == null) {
                            resp[i].avatars = [
                                {
                                    data: "app/view/assets/img/XS/user.png",
                                    name: "default.png",
                                    size: "XS",
                                    type: "image/png"
                                }
                            ];

                        }
                        else {
                            resp[i].dataHtml = "data:" + resp[i].avatars[0].type + ";base64," + resp[i].avatars[0].data;
                        }
                    }
                    sessionStorage.setItem("usersAvatar", JSON.stringify(resp));
                }

            }, function (err) {
                console.log(err);

            });
        }

        return application;

    }]);
