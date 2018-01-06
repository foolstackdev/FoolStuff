"use strict";
angular
.module('FoolStackApp')
.controller('TesoreriaController', ["$scope", "RestService", "CostantUrl", "toastr", "UtilService", "$sce", "$state", "ApplicationService",
function ($scope, RestService, CostantUrl, toastr, UtilService, $sce, $state, ApplicationService) {

    var vm = this;

    vm.dateSelected;

    vm.users = [];
    vm.userSelected = [];
    vm.versamento = {
        quota: 1,
        note: ""
    };
    vm.spesa = {
        quota: 1,
        note: ""
    };
    vm.infoEntrate = [];
    vm.infoUscite = [];
    vm.saldo = {};

    //vm.pushUserSelected = _pushUserSelected;
    vm.updateVersamento = _updateVersamento;
    vm.getUsers = _getUsers;
    vm.riepilogoUscite = _riepilogoUscite;
    //vm.getSaldo = _getSaldo();
    vm.openDPVersamento = _openDPVersamento;
    vm.openDPSpesa = _openDPSpesa;
    vm.checkAllUsers = _checkAllUsers;
    vm.reload = _reload;
    vm.prepareSpesa = _prepareSpesa;
    vm.updateSpesa = _updateSpesa;

    vm.popupDPVersamento = {
        opened: false
    };
    vm.popupDPSpesa = {
        opened: false
    };
    var formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate', 'dd/MM/yyyy'];
    vm.format = formats[4];
    vm.selectAllCheckBox = false;

    //Chart Functions
    vm.getContributoUtentiChart = _getContributoUtentiChart;




    init();
    function init() {
        console.log("Inside Tesoreria controller");
        //Date initialization
        vm.versamento.dataOperazione = new Date();
        vm.spesa.dataOperazione = new Date();
    }

    function _reload() {
        _riepilogoEntrate();
        _getSaldo();
    }

    //function _pushUserSelected(index) {
    //    console.log(index);
    //    console.log(vm.userSelected[index].check);
    //    vm.userSelected[index].check = !vm.userSelected[index].check;
    //    console.log(vm.userSelected[index].check);
    //}

    function _updateVersamento() {
        if (vm.versamento.quota == undefined) {
            toastr.warning('Problems with amount [' + vm.versamento.quota + ']', 'Something went wrong');
            return;
        }
        var itemToSend = {
            dataOperazione: vm.versamento.dataOperazione.getTime(),
            operazione: "SPESA",
            quota: vm.versamento.quota,
            note: vm.versamento.note,
            users: []
        }

        angular.forEach(vm.userSelected, function (value) {
            if (value.check == true)
                itemToSend.users.push(value.user);
        });
        console.log(itemToSend);

        if (itemToSend.users.length == 0) {
            toastr.warning('Select one or more user', 'Something went wrong');
            return;
        }
        RestService.PostData(CostantUrl.urlTesoreria, "insertversamento", itemToSend).then(function (response) {
            toastr.success('Payments updated', 'Confirmed');
            $state.reload();
        }, function (err) {
            console.log(err)
            toastr.error('Problems during payments', 'Something went wrong [' + err + ']');
        });
    }

    function _updateSpesa() {
        if (vm.spesa.quota == undefined) {
            toastr.warning('Problems with amount [' + vm.spesa.quota + ']', 'Something went wrong');
            return;
        }
        if (vm.spesa.note == undefined || vm.spesa.note == "") {
            toastr.warning('Please insert a note', 'Something went wrong');
            return;
        }
        var itemToSend = {
            dataOperazione: vm.spesa.dataOperazione.getTime(),
            operazione: "SPESA",
            quota: vm.spesa.quota,
            note: vm.spesa.note,
            users: []
        }
        RestService.PostData(CostantUrl.urlTesoreria, "insertspesa", itemToSend).then(function (response) {
            toastr.success('Payments updated', 'Confirmed');
            $state.reload();
        }, function (err) {
            console.log(err)
            toastr.error('Problems during payments', 'Something went wrong [' + err + ']');
        });
    }

    function _riepilogoEntrate() {
        RestService.GetData(CostantUrl.urlTesoreria, "getallentry").then(function (response) {
            console.log(response);
            vm.infoEntrate = response.data;
            for (var i = 0; i < vm.infoEntrate.length; i++) {
                vm.infoEntrate[i] = ApplicationService.addAvatarToUsers(vm.infoEntrate[i]);
            }
            //var avatars = ApplicationService.getAllAvatars();
            //if (avatars != undefined && avatars != null) {
            //    for (var i = 0; i < vm.infoEntrate.length; i++) {
            //        for (var k = 0; k < vm.infoEntrate[i].user.length; k++) {
            //            vm.infoEntrate[i].user[k].avatar = null;
            //            for (var j = 0; j < avatars.length; j++) {
            //                if (vm.infoEntrate[i].user[k].id == avatars[j].userId) {
            //                    vm.infoEntrate[i].user[k].avatar = avatars[j].dataHtml;
            //                    break;
            //                }
            //            }
            //            if (vm.infoEntrate[i].user[k].avatar == null) {
            //                vm.infoEntrate[i].user[k].avatar = ApplicationService.getDefaultImageSrc();
            //            }
            //        }
            //    }
            //}


            //vm.infoEntrate = _manageRiepilogoentrate(response.data);
            console.log(vm.infoEntrate);
            //vm.infoEntrate = response.data;
        });
    }

    //function _manageRiepilogoentrate(obj) {
    //    var arr = [];
    //    angular.forEach(obj, function (value) {

    //        if (arr.filter(function (e) { return e.dataOperazione === value.dataOperazione }).length > 0) {
    //            var fff = arr.filter(function (e) { return e.dataOperazione === value.dataOperazione })[0];
    //            fff.totale += value.quota;
    //        }
    //        else {
    //            value.totale = value.quota;
    //            value.dataVersamento = UtilService.convertTimestampToDate(value.dataOperazione);
    //            arr.push(value)
    //        }
    //    });
    //    return arr;
    //}



    function _riepilogoUscite() {
        RestService.GetData(CostantUrl.urlTesoreria, "getallexit").then(function (response) {
            console.log(response);
            vm.infoUscite = response.data;
        });
    }

    function _prepareSpesa() {
        vm.spesa.dataOperazione = new Date();
    }

    function _getUsers() {
        //Date Reinitialization
        vm.versamento.dataOperazione = new Date();

        RestService.GetData(CostantUrl.urlUserAccount, "allusers").then(function (response) {
            vm.userSelected = [];
            vm.users = response.data;

            vm.users = ApplicationService.addAvatarToUsers(vm.users);

            angular.forEach(vm.users, function (value) {
                var item = {
                    check: false,
                    user: value
                }
                console.log(item);
                vm.userSelected.push(item);
            });
        });
    }

    function _openDPVersamento() {
        vm.popupDPVersamento.opened = true;
    }
    function _openDPSpesa() {
        vm.popupDPSpesa.opened = true;
    }




    //////////////////////////////////////////VM UTILS AND GRAPHICAL EFFECTS

    /*
    Manage CheckBox selection for users
    */
    function _checkAllUsers() {
        vm.selectAllCheckBox = !vm.selectAllCheckBox;
        var boolCheck = vm.selectAllCheckBox ? true : false;
        angular.forEach(vm.userSelected, function (value) {
            value.check = boolCheck;
        });
    }
    //////////////////////////////////////////VM UTILS AND GRAPHICAL EFFECTS
    function _getSaldo() {
        RestService.GetData(CostantUrl.urlTesoreria, "getsaldo").then(function (response) {
            console.log(response);
            vm.saldo = response.data;
        });
    }

    $scope.htmlPopover = function (item) {
        var ostring = "<ul style='list-style-type: none;'>"
        for (var i = 0; i < item.user.length; i++) {
            ostring += "<li><img src='" + item.user[i].avatar + "' \> " + item.user[i].name + " " + item.user[i].surname + "</li>";
        }
        ostring += "</ul>"
        return $sce.trustAsHtml(ostring);
    }
    //////////////////////////////////////////Charts
    function _getContributoUtentiChart() {
        //Preparo Label utenti
        RestService.GetData(CostantUrl.urlUserAccount, "allusers").then(function (response) {
            vm.users = response.data;

            vm.contributoUtenti = [];
            //for (var i = 0; i < vm.users.length; i++) {
            //    vm.contributoUtenti.push(vm.users);
            //}
            vm.contributoUtenti = vm.users;
            for (var i = 0; i < vm.infoEntrate.length; i++) {
                for (var j = 0; j < vm.infoEntrate[i].user.length; j++) {
                    for (var k = 0; k < vm.contributoUtenti.length; k++) {
                        if (vm.infoEntrate[i].user[j].email == vm.contributoUtenti[k].email) {
                            vm.contributoUtenti[k].contributoTotale != undefined ? vm.contributoUtenti[k].contributoTotale += vm.infoEntrate[i].quota : vm.contributoUtenti[k].contributoTotale = vm.infoEntrate[i].quota
                        }
                    }
                }
            }

            console.log(vm.contributoUtenti);
            vm.contributoUtenti.labels = [];
            vm.contributoUtenti.data = [];
            for (var i = 0; i < vm.contributoUtenti.length; i++) {
                vm.contributoUtenti.labels.push(vm.contributoUtenti[i].name + " " + vm.contributoUtenti[i].surname);
                vm.contributoUtenti.data.push(vm.contributoUtenti[i].contributoTotale);
            }

        });

        //vm.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
        //vm.data = [300, 500, 100];
    }

    //////////////////////////////////////////Charts



}]);