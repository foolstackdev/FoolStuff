"use strict";
angular
    .module("FoolStackApp")
    .factory("ApplicationService", ["$rootScope", "RestService", "CostantUrl", function ($rootScope, RestService, CostantUrl) {

        var application = {}
        var entities = {};
        var userAdministrator = false;
        var defaultAvatarLG = "app/view/assets/img/DEFAULTAVATAR/LG/user.png";
        var defaultAvatarMD = "app/view/assets/img/DEFAULTAVATAR/MD/user.png";
        var defaultAvatarSM = "app/view/assets/img/DEFAULTAVATAR/SM/user.png";
        var defaultAvatarXS = "app/view/assets/img/DEFAULTAVATAR/XS/user.png";



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
            return defaultAvatarXS;
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
            switch (value) {
                case "LG":
                    return defaultAvatarLG;
                case "MD":
                    return defaultAvatarMD;
                case "SM":
                    return defaultAvatarSM;
                case "XS":
                    return defaultAvatarXS;
                default:
                    return defaultAvatarXS;
            }
        }

        application.getAllAvatars = function () {
            var avatars = JSON.parse(sessionStorage.getItem("usersAvatar"));
            return avatars;
        }

        application.addAvatarToUsers = function (collection) {
            var avatars = JSON.parse(sessionStorage.getItem("usersAvatar"));

            if (collection.length != undefined) {
                if (avatars != undefined && avatars != null) {
                    for (var i = 0; i < collection.length; i++) {
                        collection[i].avatar = null;
                        for (var j = 0; j < avatars.length; j++) {
                            if (collection[i].hasOwnProperty("user")) {
                                if (collection[i].user.hasOwnProperty("id")) {
                                    if (collection[i].user.id == avatars[j].userId) {
                                        collection[i].avatar = avatars[j].dataHtml;
                                        break;
                                    }
                                }
                                else if (collection[i].user.hasOwnProperty("Id")) {
                                    if (collection[i].user.Id == avatars[j].userId) {
                                        collection[i].avatar = avatars[j].dataHtml;
                                        break;
                                    }
                                }
                            } else {
                                if (collection[i].hasOwnProperty("users")) {
                                    if (collection[i].users.hasOwnProperty("id")) {
                                        if (collection[i].users.id == avatars[j].userId) {
                                            collection[i].avatar = avatars[j].dataHtml;
                                            break;
                                        }
                                    }
                                    else if (collection[i].users.hasOwnProperty("Id")) {
                                        if (collection[i].users.Id == avatars[j].userId) {
                                            collection[i].avatar = avatars[j].dataHtml;
                                            break;
                                        }
                                    }
                                }
                                else {
                                    if (collection[i].hasOwnProperty("id")) {
                                        if (collection[i].id == avatars[j].userId) {
                                            collection[i].avatar = avatars[j].dataHtml;
                                            break;
                                        }
                                    }
                                    else if (collection[i].hasOwnProperty("Id")) {
                                        if (collection[i].Id == avatars[j].userId) {
                                            collection[i].avatar = avatars[j].dataHtml;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (collection[i].avatar == null) {
                            collection[i].avatar = defaultAvatarXS;
                        }
                    }
                }
                else {
                    for (var i = 0; i < collection.length; i++) {
                        collection[i].avatar = defaultAvatarXS;
                    }
                }
            }
            else {
                if (avatars != undefined && avatars != null) {
                    for (var j = 0; j < avatars.length; j++) {
                        if (collection.hasOwnProperty("user")) {
                            if (collection.user.length != undefined) {
                                for (var i = 0; i < collection.user.length; i++) {
                                    if (collection.user[i].avatar == undefined) {
                                        collection.user[i].avatar = defaultAvatarXS;
                                    }
                                    if (collection.user[i].hasOwnProperty("id")) {
                                        if (collection.user[i].id == avatars[j].userId) {
                                            collection.user[i].avatar = avatars[j].dataHtml;
                                            break;
                                        }
                                    }
                                    else if (collection[i].hasOwnProperty("Id")) {
                                        if (collection.user[i].Id == avatars[j].userId) {
                                            collection.user[i].avatar = avatars[j].dataHtml;
                                            break;
                                        }
                                    }
                                }
                            } else {
                                collection.user.avatar = defaultAvatarXS;
                                if (collection.user.hasOwnProperty("id")) {
                                    if (collection.user.id == avatars[j].userId) {
                                        collection.user.avatar = avatars[j].dataHtml;
                                        break;
                                    }
                                }
                                else if (collection.user.hasOwnProperty("Id")) {
                                    if (collection.user.Id == avatars[j].userId) {
                                        collection.user.avatar = avatars[j].dataHtml;
                                        break;
                                    }
                                }
                            }
                        } else {
                            if (collection.hasOwnProperty("users")) {
                                if (collection.users.length != undefined) {
                                    for (var i = 0; i < collection.users.length; i++) {
                                        if (collection.users[i].avatar == undefined) {
                                            collection.users[i].avatar = defaultAvatarXS;
                                        }
                                        if (collection.users[i].hasOwnProperty("id")) {
                                            if (collection.users[i].id == avatars[j].userId) {
                                                collection.users[i].avatar = avatars[j].dataHtml;
                                                break;
                                            }
                                        }
                                        else if (collection[i].hasOwnProperty("Id")) {
                                            if (collection.users[i].Id == avatars[j].userId) {
                                                collection.users[i].avatar = avatars[j].dataHtml;
                                                break;
                                            }
                                        }
                                    }
                                } else {
                                    collection.users.avatar = defaultAvatarXS;
                                    if (collection.users.hasOwnProperty("id")) {
                                        if (collection.users.id == avatars[j].userId) {
                                            collection.users.avatar = avatars[j].dataHtml;
                                            break;
                                        }
                                    }
                                    else if (collection.users.hasOwnProperty("Id")) {
                                        if (collection.users.Id == avatars[j].userId) {
                                            collection.users.avatar = avatars[j].dataHtml;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return collection;
        }

        application.addAvatarToUsersWithPromise = function (collection) {
            return new Promise(function (resolve, reject) {
                try {
                    if (collection.length != undefined) {
                        if (avatars != undefined && avatars != null) {
                            for (var i = 0; i < collection.length; i++) {
                                collection[i].avatar = null;
                                for (var j = 0; j < avatars.length; j++) {
                                    if (collection[i].hasOwnProperty("user")) {
                                        if (collection[i].user.hasOwnProperty("id")) {
                                            if (collection[i].user.id == avatars[j].userId) {
                                                collection[i].avatar = avatars[j].dataHtml;
                                                break;
                                            }
                                        }
                                        else if (collection[i].hasOwnProperty("Id")) {
                                            if (collection[i].user.Id == avatars[j].userId) {
                                                collection[i].avatar = avatars[j].dataHtml;
                                                break;
                                            }
                                        }
                                    }
                                    if (collection[i].hasOwnProperty("users")) {
                                        if (collection[i].user.hasOwnProperty("id")) {
                                            if (collection[i].users.id == avatars[j].userId) {
                                                collection[i].avatar = avatars[j].dataHtml;
                                                break;
                                            }
                                        }
                                        else if (collection[i].hasOwnProperty("Id")) {
                                            if (collection[i].users.Id == avatars[j].userId) {
                                                collection[i].avatar = avatars[j].dataHtml;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (collection[i].avatar == null) {
                                    collection[i].avatar = defaultAvatarXS;
                                }
                            }
                        }
                        else {
                            for (var i = 0; i < collection.length; i++) {
                                collection[i].avatar = defaultAvatarXS;
                            }
                        }
                    }
                    else {
                        if (avatars != undefined && avatars != null) {
                            for (var j = 0; j < avatars.length; j++) {
                                if (collection.hasOwnProperty("user")) {
                                    if (collection.user.length != undefined) {
                                        for (var i = 0; i < collection.user.length; i++) {
                                            collection.user[i].avatar = defaultAvatarXS;
                                            if (collection.user[i].hasOwnProperty("id")) {
                                                if (collection.user[i].id == avatars[j].userId) {
                                                    collection.user[i].avatar = avatars[j].dataHtml;
                                                    break;
                                                }
                                            }
                                            else if (collection[i].hasOwnProperty("Id")) {
                                                if (collection.user[i].Id == avatars[j].userId) {
                                                    collection.user[i].avatar = avatars[j].dataHtml;
                                                    break;
                                                }
                                            }
                                        }
                                    } else {
                                        collection.user.avatar = defaultAvatarXS;
                                        if (collection.user.hasOwnProperty("id")) {
                                            if (collection.user.id == avatars[j].userId) {
                                                collection.user.avatar = avatars[j].dataHtml;
                                                break;
                                            }
                                        }
                                        else if (collection.user.hasOwnProperty("Id")) {
                                            if (collection.user.Id == avatars[j].userId) {
                                                collection.user.avatar = avatars[j].dataHtml;
                                                break;
                                            }
                                        }
                                    }
                                } else {
                                    if (collection.hasOwnProperty("users")) {
                                        if (collection.users.length != undefined) {
                                            for (var i = 0; i < collection.users.length; i++) {
                                                collection.users[i].avatar = defaultAvatarXS;
                                                if (collection.users[i].hasOwnProperty("id")) {
                                                    if (collection.users[i].id == avatars[j].userId) {
                                                        collection.users[i].avatar = avatars[j].dataHtml;
                                                        break;
                                                    }
                                                }
                                                else if (collection[i].hasOwnProperty("Id")) {
                                                    if (collection.users[i].Id == avatars[j].userId) {
                                                        collection.users[i].avatar = avatars[j].dataHtml;
                                                        break;
                                                    }
                                                }
                                            }
                                        } else {
                                            collection.users.avatar = defaultAvatarXS;
                                            if (collection.users.hasOwnProperty("id")) {
                                                if (collection.users.id == avatars[j].userId) {
                                                    collection.users.avatar = avatars[j].dataHtml;
                                                    break;
                                                }
                                            }
                                            else if (collection.users.hasOwnProperty("Id")) {
                                                if (collection.users.Id == avatars[j].userId) {
                                                    collection.users.avatar = avatars[j].dataHtml;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    resolve(collection);
                }
                catch (err) {
                    reject(Error(err));
                }
            });
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
                                    data: defaultAvatarXS,
                                    name: "default.png",
                                    size: "XS",
                                    type: "image/png"
                                }
                            ];
                            resp[i].dataHtml = defaultAvatarXS;

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
        };

        application.isUserInList = function (coll) {
            var isInList = false;
            var _id = application.getUserId();
            for (var i = 0; i < coll.length; i++) {
                if (coll[i].id == _id) {
                    isInList = true;
                }
            }
            return isInList;
        };

        application.setUserId = function (item) {
            _setItemInSession("userId", item);
        }
        application.getUserId = function () {
            return _getItemInSession("userId");
        };
        application.setUser = function (item) {
            _setItemInSession("user", item);
        }
        application.getUser = function () {
            return _getItemInSession("user");
        };
        application.setUserRoleList = function (item) {
            userAdministrator = item.indexOf("SuperAdmin") != -1 ? true : false;
            _setItemInSession("userRolesList", item);
        };
        application.getUserRoleList = function () {
            return _getItemInSession("userRolesList");
        };

        application.isAdmin = function () {
            return userAdministrator;
        }

        //local functions
        function _getItemInSession(item) {
            return JSON.parse(sessionStorage.getItem(item))
        }

        function _setItemInSession(itemName, item) {
            if (typeof itemName != "string") {
                return Error("Insert a correct Type Of String");
            }
            if (typeof item != "string") {
                item = JSON.stringify(item);
            }
            sessionStorage.setItem(itemName, item);
        }





        return application;

    }]);
