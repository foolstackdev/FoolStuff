"use strict";
angular
    .module('FoolStackApp')
    .controller('TrainingController', ["$scope", "RestService", "CostantUrl", "toastr", "UtilService", "$sce", "$state", "ApplicationService",
        function ($scope, RestService, CostantUrl, toastr, UtilService, $sce, $state, ApplicationService) {

            var vm = this;

            var userId = ApplicationService.getUserId();

            vm.corso = {};
            vm.corsi = [];
            vm.capitolo = {};
            vm.capitolo = [];
            vm.visualizzaCorso = {};
            vm.corsiPersonali = [];


            vm.isAdmin = ApplicationService.isAdmin();

            //functions
            vm.reload = _reload;
            vm.caricaCorsi = _caricaCorsi;

            //button functions
            vm.inserisciCorso = _inserisciCorso;
            vm.inserisciCapitolo = _inserisciCapitolo;
            vm.switchCorso = _switchCorso;
            vm.seguiCorso = _seguiCorso;

            init();
            function init() {
                console.log("Inside Training controller");
            }

            function _reload() {
                RestService.GetData(CostantUrl.urlFormazione, "getallcorsi").then(function (response) {
                    console.log(response);
                    vm.corsi = response.data;
                    for (var i = 0; i < vm.corsi.length; i++) {
                        vm.corsi[i].utenti = ApplicationService.addAvatarToUsers(vm.corsi[i].utenti);
                    }
                    vm.visualizzaCorso = vm.corsi[0];
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems during insertion', 'Something went wrong [' + err + ']');
                });
            }

            function _inserisciCorso() {
                RestService.PostData(CostantUrl.urlFormazione, "addcorso", vm.corso).then(function (response) {
                    toastr.success('Corso inserito correttamente', 'Confirmed');
                    vm.corso = {};
                    _reload();
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems during insertion', 'Something went wrong [' + err + ']');
                });
            }

            function _inserisciCapitolo(item) {
                var corsoCopy = angular.copy(item);
                corsoCopy.capitoli = [];
                console.log(corsoCopy);
                var obj = {
                    numeroCapitolo: item.capitoli.length + 1,
                    titolo: item.capitolo
                };
                corsoCopy.capitoli.push(obj);
                RestService.PostData(CostantUrl.urlFormazione, "addcapitolo", corsoCopy).then(function (response) {
                    toastr.success('Capitolo inserito correttamente', 'Confirmed');
                    _reload();
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems during insertion', 'Something went wrong [' + err + ']');
                });
            }

            function _switchCorso(item) {
                vm.visualizzaCorso = item;
            }

            function _seguiCorso(item) {
                console.log(item);
                var userCorso = {
                    corso: item,
                    userId: userId
                }
                RestService.PostData(CostantUrl.urlFormazione, "addusertocourse", userCorso).then(function (response) {
                    toastr.success('Registrazione effettuata correttamente', 'Confirmed');
                    _reload();
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems during insertion', 'Something went wrong [' + err.data.message + ']');
                });
            }

            function _caricaCorsi() {
                RestService.GetData(CostantUrl.urlFormazione, "getcorsi/" + userId).then(function (response) {
                    console.log(response);
                    vm.corsiPersonali = response.data;
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems duringl loading', 'Something went wrong [' + err + ']');
                });
            }

        }]);