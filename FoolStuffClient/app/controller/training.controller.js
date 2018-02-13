"use strict";
angular
    .module('FoolStackApp')
    .controller('TrainingController', ["$scope", "RestService", "CostantUrl", "toastr", "UtilService", "$sce", "$state", "ApplicationService","$timeout",
        function ($scope, RestService, CostantUrl, toastr, UtilService, $sce, $state, ApplicationService, $timeout) {

            var vm = this;

            var userId = ApplicationService.getUserId();

            vm.corso = {};
            vm.corsi = [];
            vm.capitolo = {};
            vm.capitolo = [];
            vm.visualizzaCorso = {};
            vm.visualizzaCorsoPersonale = {};
            vm.corsiPersonali = [];
            //messaggi
            vm.nuovoMessaggio = {};
            vm.nuovaRisposta = {};

            vm.isAdmin = ApplicationService.isAdmin();

            //functions
            vm.reload = _reload;
            //vm.caricaCorsi = _caricaCorsi;
            vm.switchMessaggiInit = _switchMessaggiInit;
            vm.switchMessaggi = _switchMessaggi;

            //button functions
            vm.inserisciCorso = _inserisciCorso;
            vm.inserisciCapitolo = _inserisciCapitolo;
            vm.switchCorso = _switchCorso;
            vm.seguiCorso = _seguiCorso;
            vm.switchCorsoPersonale = _switchCorsoPersonale
            vm.capitoloConcluso = _capitoloConcluso;
            //Messaggi
            vm.inserisciMessaggio = _inserisciMessaggio;
            vm.inserisciRisposta = _inserisciRisposta;
            vm.manageProgress = _manageProgress;

            init();
            function init() {
                console.log("Inside Training controller");
            }

            function _reload() {
                RestService.GetData(CostantUrl.urlFormazione, "getallcorsi").then(function (response) {
                    vm.corsi = response.data;
                    //vm.visualizzaCorso = angular.equals(vm.visualizzaCorso, {}) ? vm.corsi[0] : vm.corsi[vm.visualizzaCorso.id - 1];
                    var showCorso = angular.equals(vm.visualizzaCorso, {}) ? vm.corsi[0] : vm.corsi[vm.visualizzaCorso.id - 1];
                    _caricaCorso(showCorso);
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
                _caricaCorso(item)
                _switchMessaggiInit(vm.visualizzaCorso);
            }

            function _switchCorsoPersonale(item) {
                vm.visualizzaCorsoPersonale = item;
            }

            function _seguiCorso(item) {
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

            function _caricaCorso(item) {
                RestService.PostData(CostantUrl.urlFormazione, "getcorso", item).then(function (response) {
                    //vm.visualizzaCorso = response.data[0];
                    vm.visualizzaCorso = response.data;

                    for (var i = 0; i < vm.visualizzaCorso.capitoli.length; i++) {
                        vm.visualizzaCorso.capitoli[i].isLoading = false;
                    }
                    _manageProgress();
                    _switchMessaggiInit(vm.visualizzaCorso);
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems duringl loading', 'Something went wrong [' + err + ']');
                });
            }

            function _capitoloConcluso(item) {

                _asyncApply(function () {
                    item.isLoading = true;
                });

                var progressiFormazione = {
                    Utente: ApplicationService.getUser().userInfo,
                    Capitolo: item
                }
                RestService.PostData(CostantUrl.urlFormazione, "addprogressoformazione", progressiFormazione).then(function (response) {
                    toastr.success('Registrazione effettuata correttamente', 'Confirmed');
                    _reload();
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems during insertion', 'Something went wrong [' + err.data.message + ']');
                });
            }

            //MESSAGGI
            vm.capitoliMessaggi = {};
            function _switchMessaggiInit(item) {
                if (item == undefined)
                    return;
                vm.capitoliMessaggi = item.capitoli[0];
                _getMessaggiPerCapitolo(vm.capitoliMessaggi);
            }

            function _switchMessaggi(item) {
                if (item == undefined)
                    return;
                vm.capitoliMessaggi = item;
                _getMessaggiPerCapitolo(vm.capitoliMessaggi);
            }

            function _inserisciMessaggio() {
                vm.capitoliMessaggi.messaggi = [];
                vm.capitoliMessaggi.messaggi.push(vm.nuovoMessaggio);

                RestService.PostData(CostantUrl.urlFormazione, "addusermessageforcapitolo", vm.capitoliMessaggi).then(function (response) {
                    toastr.success('Inserimento messaggio effettuato correttamente', 'Confirmed');
                    vm.nuovoMessaggio = {};
                    _getMessaggiPerCapitolo(vm.capitoliMessaggi);
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems during insertion', 'Something went wrong [' + err.data.message + ']');
                });
            }

            function _getMessaggiPerCapitolo(capitolo) {
                RestService.PostData(CostantUrl.urlFormazione, "getmessaggipercapitolo", capitolo).then(function (response) {
                    vm.capitoliMessaggi.messaggi = response.data.messaggi;
                    if (vm.capitoliMessaggi.messaggi.length > 0) {
                        for (var i = 0; i < vm.capitoliMessaggi.messaggi.length; i++) {
                            vm.capitoliMessaggi.messaggi[i].submitter = ApplicationService.addAvatarToSingleUser(vm.capitoliMessaggi.messaggi[i].submitter);
                            for (var j = 0; j < vm.capitoliMessaggi.messaggi[i].risposte.length; j++) {
                                vm.capitoliMessaggi.messaggi[i].risposte[j].utente = ApplicationService.addAvatarToSingleUser(vm.capitoliMessaggi.messaggi[i].risposte[j].utente);
                            }
                        }
                    }
                    vm.nuovoMessaggio = {};
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems retrieving messages', 'Something went wrong [' + err.data.message + ']');
                });
            }

            function _inserisciRisposta(messaggio, capitolo) {
                messaggio.risposte = [];
                messaggio.risposte.push(vm.nuovaRisposta);
                RestService.PostData(CostantUrl.urlFormazione, "addrispostatomessaggio", messaggio).then(function (response) {
                    vm.nuovaRisposta = {};
                    _getMessaggiPerCapitolo(capitolo);
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems retrieving messages', 'Something went wrong [' + err.data.message + ']');
                });
            }

            function _manageProgress() {
                var obj = {
                    idCorso: vm.visualizzaCorso.id
                }
                RestService.PostData(CostantUrl.urlFormazione, "getprogressoformazionebyidcorso", obj).then(function (response) {
                    console.log(response);
                    vm.visualizzaCorso.utenti = response.data;
                    vm.visualizzaCorso.utenti = ApplicationService.addAvatarToUsers(vm.visualizzaCorso.utenti);
                }, function (err) {
                    console.log(err)
                    toastr.error('Problems duringl loading', 'Something went wrong [' + err + ']');
                });
            }

            function _asyncApply(func) {
                $timeout(function () {
                    func();
                }, 0, true)
            }

            //END MESSAGGI

        }]);