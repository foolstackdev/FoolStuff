﻿"use strict";
angular
    .module("FoolStackApp")
    .factory("UtilService", [function () {
        return {
            convertTimestampToDate: function (timestamp) {
                var d = new Date(timestamp),	// Convert the passed timestamp to milliseconds
                      yyyy = d.getFullYear(),
                      mm = ('0' + (d.getMonth() + 1)).slice(-2),	// Months are zero based. Add leading 0.
                      dd = ('0' + d.getDate()).slice(-2),			// Add leading 0.
                      hh = d.getHours(),
                      h = hh,
                      min = ('0' + d.getMinutes()).slice(-2),		// Add leading 0.
                      ampm = 'AM',
                      time;

                time = yyyy + '/' + mm + '/' + dd;

                return time;
            },
            convertTimestampToDateAndHours: function (timestamp) {
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
                time = yyyy + '-' + mm + '-' + dd + ', ' + h + ':' + min + ' ' + ampm;

                return time;
            },
            getDateInTimemillis: function (timemillis) {
                var d = new Date(timemillis),	// Convert the passed timestamp to milliseconds
                     yyyy = d.getFullYear(),
                     mm = ('0' + (d.getMonth() + 1)).slice(-2),	// Months are zero based. Add leading 0.
                     dd = ('0' + d.getDate()).slice(-2);
                var oDate = new Date(yyyy + "-" + mm + "-" + dd);
                return oDate.getTime();
            }
        }

    }]);
