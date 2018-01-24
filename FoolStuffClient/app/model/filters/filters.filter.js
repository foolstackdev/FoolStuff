"use strict";
angular.module("FoolStackApp")
    .constant("ConstantFilterFunctions", {
        calculateWhen: function (timestamp) {
            var past = "PAST";
            var present = "PRESENT";
            var future = "FUTURE";

            try {
                var d = new Date(timestamp),	// Convert the passed timestamp to milliseconds
                    yyyy = d.getFullYear(),
                    mm = ('0' + (d.getMonth() + 1)).slice(-2),	// Months are zero based. Add leading 0.
                    dd = ('0' + d.getDate()).slice(-2);			// Add leading 0.

                var PREtoday = new Date(),
                    PREyyyy = PREtoday.getFullYear(),
                    PREmm = ('0' + (PREtoday.getMonth() + 1)).slice(-2),
                    PREdd = ('0' + PREtoday.getDate()).slice(-2);


                var today = new Date(PREyyyy + "-" + PREmm + "-" + PREdd);
                var obj = new Date(yyyy + "-" + mm + "-" + dd);

                if (obj.valueOf() < today.valueOf()) {
                    return past;
                }
                if (obj.valueOf() == today.valueOf()) {
                    return present;
                }
                if (obj.valueOf() > today.valueOf()) {
                    return future;
                }
            }
            catch (err) {
                return "";
            }
        },
        calculateRemainingDays: function (timestamp) {
            try {
                var d = new Date(timestamp),	// Convert the passed timestamp to milliseconds
                    yyyy = d.getFullYear(),
                    mm = ('0' + (d.getMonth() + 1)).slice(-2),	// Months are zero based. Add leading 0.
                    dd = ('0' + d.getDate()).slice(-2);			// Add leading 0.

                var PREtoday = new Date(),
                    PREyyyy = PREtoday.getFullYear(),
                    PREmm = ('0' + (PREtoday.getMonth() + 1)).slice(-2),
                    PREdd = ('0' + PREtoday.getDate()).slice(-2);


                var today = new Date(PREyyyy + "-" + PREmm + "-" + PREdd);
                var obj = new Date(yyyy + "-" + mm + "-" + dd);
                var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

                var diffDays = Math.round(Math.abs((obj.getTime() - today.getTime()) / (oneDay)));
                return diffDays;
            }
            catch (err) {
                return "";
            }
        }
    })
    .filter('timestampToHuman', function () {
        return function (timestamp) {
            var d = new Date(timestamp),	// Convert the passed timestamp to milliseconds
                yyyy = d.getFullYear(),
                mm = ('0' + (d.getMonth() + 1)).slice(-2),	// Months are zero based. Add leading 0.
                dd = ('0' + d.getDate()).slice(-2),			// Add leading 0.
                hh = d.getHours(),
                h = hh,
                min = ('0' + d.getMinutes()).slice(-2),		// Add leading 0.
                ampm = 'AM',
                time;

            time = dd + '/' + mm + '/' + yyyy;
            return time;
        };
    })
    .filter('timestampToHumanWithHours', function () {
        return function (timestamp) {
            var d = new Date(timestamp),	// Convert the passed timestamp to milliseconds
                yyyy = d.getFullYear(),
                mm = ('0' + (d.getMonth() + 1)).slice(-2),	// Months are zero based. Add leading 0.
                dd = ('0' + d.getDate()).slice(-2),			// Add leading 0.
                hh = d.getHours(),
                h = hh,
                min = ('0' + d.getMinutes()).slice(-2),		// Add leading 0.
                ampm = 'AM',
                time;

            if (hh > 12) {
                h = hh - 12;
                ampm = 'PM';
            } else if (hh === 12) {
                h = 12;
                ampm = 'PM';
            } else if (hh == 0) {
                h = 12;
            }

            // ie: 2013-02-18, 8:35 AM	
            time = dd + '-' + mm + '-' + yyyy + ', ' + h + ':' + min + ' ' + ampm;

            return time;
        };
    })
    .filter('timestampIsTodayReturningClasses', ["ConstantFilterFunctions", function (ConstantFilterFunctions) {
        return function (timestamp) {
            var past = "panel-warning";
            var present = "panel-success";
            var future = "panel-info";

            var when = ConstantFilterFunctions.calculateWhen(timestamp);
            switch (when) {
                case "PAST":
                    return past;
                case "PRESENT":
                    return present;
                case "FUTURE":
                    return future;
                default:
                    return "";
            }
        };
    }])
    .filter('humanReadableDate', function () {
        return function (obj) {
            for (var i = 0; i < obj.length; i++) {
                var d = new Date(obj[i].dataEvento),	// Convert the passed timestamp to milliseconds
                    yyyy = d.getFullYear(),
                    mm = ('0' + (d.getMonth() + 1)).slice(-2),	// Months are zero based. Add leading 0.
                    dd = ('0' + d.getDate()).slice(-2),			// Add leading 0.
                    hh = d.getHours(),
                    h = hh,
                    min = ('0' + d.getMinutes()).slice(-2),		// Add leading 0.
                    ampm = 'AM',
                    time;

                time = dd + '/' + mm + '/' + yyyy;
                obj[i].dataEventoHumanReadable = time;
            }
            return obj;
        };
    })
    .filter('timestampIsInTimeRetourningSentence', ["ConstantFilterFunctions", function (ConstantFilterFunctions) {
        return function (timestamp) {

            var past = "Past";
            var present = "Today";
            var future = "Future";

            var when = ConstantFilterFunctions.calculateWhen(timestamp);
            switch (when) {
                case "PAST":
                    return past;
                case "PRESENT":
                    return present;
                case "FUTURE":
                    return future;
                default:
                    return "";
            }
        };
    }])
    .filter('timestampIsInTimeRetourningCustom', ["ConstantFilterFunctions", function (ConstantFilterFunctions) {
        return function (timestamp, past, present, future) {
            var when = ConstantFilterFunctions.calculateWhen(timestamp);
            switch (when) {
                case "PAST":
                    return past;
                case "PRESENT":
                    return present;
                case "FUTURE":
                    return future;
                default:
                    return "";
            }
        };
    }])
    .filter('remainingDays', ["ConstantFilterFunctions", function (ConstantFilterFunctions) {
        return function (timestamp) {
            return ConstantFilterFunctions.calculateRemainingDays(timestamp);
        };
    }])
    .filter('checkIfUserIn', ["ConstantFilterFunctions", "ApplicationService", function (ConstantFilterFunctions, ApplicationService) {
        return function (collection) {
            var userId = ApplicationService.getUserId();

            if (collection != undefined && collection.length != undefined && collection.length > 0) {
                for (var i = 0; i < collection.length; i++) {
                    if (collection[i].id == userId)
                        return true;
                }
            }
            return false;
        };
    }])
    .filter('checkIfUserInProgressoFormazione', ["ConstantFilterFunctions", "ApplicationService", function (ConstantFilterFunctions, ApplicationService) {
        return function (collection) {
            var userId = ApplicationService.getUserId();

            if (collection != undefined && collection.length != undefined) {
                for (var i = 0; i < collection.length; i++) {
                    if (collection[i].utente.id == userId)
                        return true;
                }
            }
            return false;
        };
    }])
    .filter('returnConclusionDateProgressoFormazione', ["ConstantFilterFunctions", "ApplicationService", "UtilService", function (ConstantFilterFunctions, ApplicationService, UtilService) {
        return function (collection) {
            var userId = ApplicationService.getUserId();

            if (collection != undefined && collection.length != undefined) {
                for (var i = 0; i < collection.length; i++) {
                    if (collection[i].utente.id == userId)
                        return UtilService.convertTimestampToDate(collection[i].dataCompletamento);
                }
            }
            return "";
        };
    }])

    ;
