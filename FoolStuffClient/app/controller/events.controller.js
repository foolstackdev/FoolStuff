"use strict";
angular
.module('FoolStackApp')
.controller('EventsController', ["$scope", "RestService", "CostantUrl", "toastr", "ApplicationService", "$sce", "$state",
    function ($scope, RestService, CostantUrl, toastr, ApplicationService, $sce, $state) {

        var vm = this;
        vm.users = [];
        vm.events = [];
        vm.numberOfEventToShow = 6;
        vm.selectAllCheckBox = false;
        vm.presence = null;
        vm.eventToManage = {};
        vm.userSelected = [];
        vm.evento = {
            titolo: "",
            note: ""
        };

        //funzioni
        vm.inserisciEvento = _inserisciEvento;
        vm.updatePresenza = _updatePresenza;
        vm.refreshEventView = _refreshEventView;
        vm.notePopover = _notePopover;
        vm.checkAllUsers = _checkAllUsers;
        vm.changeEventToManage = _changeEventToManage;
        ///////////GESTIONE CALENDARIO
        //variabili
        vm.dateSelected;
        vm.popupDPEvento = { opened: false };
        var formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate', 'dd/MM/yyyy'];
        vm.format = formats[4];
        //elenco funzioni
        vm.openDPEvento = _openDPEvento;
        //funzioni
        function _openDPEvento() {
            vm.popupDPEvento.opened = true;
        }
        ///////////END GESTIONE CALENDARIO

        init();
        function init() {
            vm.evento.dataEvento = new Date();
            console.log("Inside Events controller");
            _reload();
        }
        function _inserisciEvento() {
            vm.evento.dataEvento = vm.evento.dataEvento.getTime();
            RestService.PostData(CostantUrl.urlEvents, "insertnewevent", vm.evento).then(function (response) {
                console.log(response);
                toastr.success("Event [" + vm.evento.titolo + "] added successfully", "Confirmed");
                vm.evento = {};
                vm.evento.dataEvento = new Date();
                _reload()
            }, function (err) {
                console.log(err);
            })
        }
        function _reload() {
            vm.evento.dataEvento = new Date();
            RestService.GetData(CostantUrl.urlUserAccount, "allusers").then(function (response) {
                vm.users = response.data;
                vm.userSelected = [];
                vm.users = ApplicationService.addAvatarToUsers(vm.users);
                angular.forEach(vm.users, function (value) {
                    var item = {
                        check: false,
                        user: value
                    }
                    console.log(item);
                    vm.userSelected.push(item);
                });
                console.log(vm.users);
            }, function (err) {
                console.log(err);
                toastr.error("Problems retrieving data [" + err.data.message + "]", "Error");
            })
            _refreshEventView();
        }
        function _refreshEventView() {
            RestService.GetData(CostantUrl.urlEvents, "getfirstnumeventswithusers/" + vm.numberOfEventToShow).then(function (response) {
                vm.events = response.data;
                console.log(response);
            }, function (err) {
                console.log(err);
            })
        }

        //////////////////////POPOVER
        function _notePopover(item) {
            var ostring = "<p>" + item.note + "</p>"
            return $sce.trustAsHtml(ostring);
        }

        //////////////////////CHECKBOX TABLE
        function _checkAllUsers() {
            vm.selectAllCheckBox = !vm.selectAllCheckBox;
            var boolCheck = vm.selectAllCheckBox ? true : false;
            angular.forEach(vm.userSelected, function (value) {
                value.check = boolCheck;
            });
        }

        /////////////////Presence Management
        function _changeEventToManage(item) {
            vm.presence = null;
            vm.presence = angular.copy(JSON.parse(item));
            vm.presence.prenotazioni = ApplicationService.addAvatarToUsers(vm.presence.prenotazioni);

            //Gestisco utenti gia presenti per eventuali update
            angular.forEach(vm.userSelected, function (valueUsers) {
                valueUsers.check = false;
            });
            angular.forEach(vm.presence.presenze, function (valuePresenze) {
                angular.forEach(vm.userSelected, function (valueUsers) {
                    if (valuePresenze.id == valueUsers.user.id) {
                        valueUsers.check = true;
                    }
                });
            });


            console.log(vm.presence);
        }
        function _updatePresenza() {
            vm.presence.presenze = [];
            angular.forEach(vm.userSelected, function (value) {
                if (value.check == true)
                    vm.presence.presenze.push(value.user);
            });
            RestService.PostData(CostantUrl.urlEvents, "adduserstoeventpresence", vm.presence).then(function (response) {
                console.log(response);
                toastr.success("Presence [" + vm.presence.titolo + "] updated successfully", "Confirmed");
                $state.reload();
            }, function (err) {
                console.log(err);
                toastr.error("Problems updating presence [" + err.data.message + "]", "Error");
            })
        }

    }]);