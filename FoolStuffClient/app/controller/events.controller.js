"use strict";
angular
.module('FoolStackApp')
.controller('EventsController', ["$scope", "RestService", "CostantUrl", "toastr", "ApplicationService", "$sce",
    function ($scope, RestService, CostantUrl, toastr, ApplicationService, $sce) {

        var vm = this;
        vm.users = [];
        vm.events = [];
        vm.numberOfEventToShow = 6;
        vm.selectAllCheckBox = false;
        vm.presence = null;
        vm.eventToManage = "";
        vm.evento = {
            titolo: "",
            note: ""
        };

        //funzioni
        vm.inserisciEvento = _inserisciEvento;
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
                vm.users = ApplicationService.addAvatarToUsers(vm.users);
                console.log(vm.users);
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
            var ostring = "<p>" + item.note +"</p>"
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
            console.log(vm.presence);
        }

    }]);