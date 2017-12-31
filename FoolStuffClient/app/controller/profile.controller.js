"use strict";
angular
    .module('FoolStackApp')
    .controller('ProfileController', ["$scope", "RestService", "CostantUrl", "$state", "toastr", "$timeout", "$window", "ApplicationService", "$rootScope",
        function ($scope, RestService, CostantUrl, $state, toastr, $timeout, $window, ApplicationService, $rootScope) {

            var vm = this;
            vm.user = {};
            vm.show = 1;

            vm.imageAvatar = ApplicationService.getSpecificAvatar("LG");
            vm.back = _back;
            vm.changeAvatar = _changeAvatar;
            vm.upload = _upload;

            init();
            var Cropper = window.Cropper;
            var cropper;
            function init() {
                console.log("Inside Profile Controller");
                vm.user = JSON.parse(sessionStorage.getItem('user')).userInfo;
            }

            var fileSelect = document.getElementById('file');

            function _changeAvatar() {
                fileSelect.click();
            }
            var uploadedImageType;
            var uploadedImageName;
            fileSelect.onchange = function (e) {
                vm.show = 2;
                var f = fileSelect.files[0];
                uploadedImageType = f.type;
                uploadedImageName = f.name;
                if ((f.type != "image/jpeg") && (f.type != "image/jpg") && (f.type != "image/png") && (f.type != "image/bmp")) {
                    toastr.warning('Load an IMAGE file', 'Ops..');
                    return;
                }
                if (f.size > 5000000) {
                    toastr.warning('Load an image with a size less than 5 MB', 'Image too big.. Rocco!');
                    return;
                }

                var r = new FileReader();
                r.onloadend = function (e) {
                    $timeout(function () {
                        document.getElementById("imageAvatartoCrop").src = e.target.result;
                        document.getElementById("imageAvatartoCrop-lg").src = e.target.result;
                        document.getElementById("imageAvatartoCrop-md").src = e.target.result;
                        document.getElementById("imageAvatartoCrop-sm").src = e.target.result;
                        document.getElementById("imageAvatartoCrop-xs").src = e.target.result;

                        var URL = window.URL || window.webkitURL;
                        var container = document.querySelector('.img-container');
                        var image = container.getElementsByTagName('img').item(0);
                        var download = document.getElementById('download');
                        var actions = document.getElementById('actions');
                        var dataX = document.getElementById('dataX');
                        var dataY = document.getElementById('dataY');
                        var dataHeight = document.getElementById('dataHeight');
                        var dataWidth = document.getElementById('dataWidth');
                        var dataRotate = document.getElementById('dataRotate');
                        var dataScaleX = document.getElementById('dataScaleX');
                        var dataScaleY = document.getElementById('dataScaleY');

                        var options = {
                            aspectRatio: 1 / 1,
                            preview: '.img-preview',
                            ready: function (e) {
                                console.log(e.type);
                                //Cropper pronto disabilito la progress bar
                                $timeout(function () {
                                    $(".waiter").css('display', 'none');
                                }, 700, true);
                            },
                            cropstart: function (e) {
                                console.log(e.type, e.detail.action);
                            },
                            cropmove: function (e) {
                                console.log(e.type, e.detail.action);
                            },
                            cropend: function (e) {
                                console.log(e.type, e.detail.action);
                            },
                            crop: function (e) {
                                var data = e.detail;

                                console.log(e.type);
                                dataX.value = Math.round(data.x);
                                dataY.value = Math.round(data.y);
                                dataHeight.value = Math.round(data.height);
                                dataWidth.value = Math.round(data.width);
                                dataRotate.value = typeof data.rotate !== 'undefined' ? data.rotate : '';
                                dataScaleX.value = typeof data.scaleX !== 'undefined' ? data.scaleX : '';
                                dataScaleY.value = typeof data.scaleY !== 'undefined' ? data.scaleY : '';
                            },
                            zoom: function (e) {
                                console.log(e.type, e.detail.ratio);
                            }
                        };

                        $timeout(function () {
                            cropper = new Cropper(image, options);
                        }, 20, true);

                        //$timeout(function () {
                        //    //$('#imageAvatartoCrop').cropper({
                        //    //    //aspectRatio: 16 / 9,
                        //    //    aspectRatio: 1 / 1,
                        //    //    preview: '.img-preview',
                        //    //    ready: function (e) {
                        //    //        console.log(e.type);
                        //    //        //Cropper pronto disabilito la progress bar
                        //    //        $timeout(function () {
                        //    //            $(".waiter").css('display', 'none');
                        //    //        }, 700, true);
                        //    //    },
                        //    //    cropstart: function (e) {
                        //    //        console.log(e.type, e.detail.action);
                        //    //    },
                        //    //    cropmove: function (e) {
                        //    //        console.log(e.type, e.detail.action);
                        //    //    },
                        //    //    cropend: function (e) {
                        //    //        console.log(e.type, e.detail.action);
                        //    //    },
                        //    //    crop: function (e) {
                        //    //        // Output the result data for cropping image.
                        //    //        console.log(e.x);
                        //    //        console.log(e.y);
                        //    //        console.log(e.width);
                        //    //        console.log(e.height);
                        //    //        console.log(e.rotate);
                        //    //        console.log(e.scaleX);
                        //    //        console.log(e.scaleY); var data = e.detail;
                        //    //        dataX.value = Math.round(e.x);
                        //    //        dataY.value = Math.round(e.y);
                        //    //        dataHeight.value = Math.round(e.height);
                        //    //        dataWidth.value = Math.round(e.width);
                        //    //        dataRotate.value = typeof e.rotate !== 'undefined' ? e.rotate : '';
                        //    //        dataScaleX.value = typeof e.scaleX !== 'undefined' ? e.scaleX : '';
                        //    //        dataScaleY.value = typeof e.scaleY !== 'undefined' ? e.scaleY : '';
                        //    //    },
                        //    //    zoom: function (e) {
                        //    //        console.log(e.type, e.detail.ratio);
                        //    //    }
                        //    //});
                        //}, 30, true);
                    }, 20, true);



                    //send to SERVER
                    //toastr.info('work in progress..', 'Loading');
                    //var avatar = {
                    //    data: e.target.result.replace(/^data:[a-zA-Z\/]*;base64,/, ""),
                    //    type: f.type,
                    //    name: f.name
                    //}
                    //RestService.PostData(CostantUrl.urlUpload, "addavatar", avatar).then(function (response) {
                    //    console.log(response);
                    //    toastr.success('Avatar correctly loaded', 'Confirmed');
                    //    $state.reload();

                    //}, function (err) {
                    //    console.log(err)
                    //    toastr.error('Problems during Upload', 'Something went wrong [' + err.data.exceptionMessage + ']');
                    //});
                }


                r.readAsDataURL(f);
                //Se un file di testo legge il contenuto.. utile in futuro
                //r.readAsBinaryString(f);
            }

            function _upload() {
                console.log("upload");

                var avatars = [
                    {
                        size: "LG",
                        data: cropper["getCroppedCanvas"]({ width: 144, height: 144, fillColor: "#fff" }, undefined).toDataURL(uploadedImageType).replace(/^data:[a-zA-Z\/]*;base64,/, ""),
                        name: uploadedImageName,
                        type: uploadedImageType
                    },
                    {
                        size: "MD",
                        data: cropper["getCroppedCanvas"]({ width: 72, height: 72, fillColor: "#fff" }, undefined).toDataURL(uploadedImageType).replace(/^data:[a-zA-Z\/]*;base64,/, ""),
                        name: uploadedImageName,
                        type: uploadedImageType
                    },
                    {
                        size: "SM",
                        data: cropper["getCroppedCanvas"]({ width: 36, height: 36, fillColor: "#fff" }, undefined).toDataURL(uploadedImageType).replace(/^data:[a-zA-Z\/]*;base64,/, ""),
                        name: uploadedImageName,
                        type: uploadedImageType
                    },
                    {
                        size: "XS",
                        data: cropper["getCroppedCanvas"]({ width: 18, height: 18, fillColor: "#fff" }, undefined).toDataURL(uploadedImageType).replace(/^data:[a-zA-Z\/]*;base64,/, ""),
                        name: uploadedImageName,
                        type: uploadedImageType
                    }
                ];

                RestService.PostData(CostantUrl.urlUpload, "addavatar", avatars).then(function (response) {
                    console.log(response);
                    toastr.success('Avatar correctly loaded', 'Confirmed');
                    sessionStorage.setItem("userAvatar", JSON.stringify(avatars))
                    ApplicationService.setUserAvatar();
                    $rootScope.$broadcast("SignedControllerTriggerAvatarReload");
                    _back();
                    //$state.reload();

                }, function (err) {
                    console.log(err)
                    toastr.error('Problems during Upload', 'Something went wrong [' + err.data.exceptionMessage + ']');
                });

                //var prop = {
                //    width: 160,
                //    height: 90,
                //    fillColor: "#fff"
                //}
                ////var result = cropper["getCroppedCanvas"](prop, undefined);
                ////var base64ImageMd = result.toDataURL(uploadedImageType);
                ////console.log(base64ImageMd);
                //var result = cropper["getCroppedCanvas"](prop, undefined).toDataURL(uploadedImageType);
            }

            function _back() {
                //fileSelect.value = "";
                //document.getElementById("imageAvatartoCrop").src = "";
                //document.getElementById("imageAvatartoCrop-lg").src = "";
                //document.getElementById("imageAvatartoCrop-md").src = "";
                //document.getElementById("imageAvatartoCrop-sm").src = "";
                //document.getElementById("imageAvatartoCrop-xs").src = "";
                vm.show = 1;
                $state.reload();
            }

        }]);