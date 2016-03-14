//**
// * Created by Vladyslav on 26.02.2016.
// */
//
//(function() {
//    'use strict';
//
//    angular.module('app.roles').factory('Authorization', Authorization);
//
//    Authorization.$inject = ['$http'];
//
//    // service responsible for authorization
//    function Authorization($http) {
//        // Base64 encoding service used by AuthenticationService
//        // Took from http://jasonwatmore.com/post/2015/03/10/AngularJS-User-Registration-and-Login-Example.aspx
//        var Base64 = {
//
//            keyStr: 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=',
//
//            encode: function (input) {
//                var output = '';
//                var chr1, chr2, chr3 = '';
//                var enc1, enc2, enc3, enc4 = '';
//                var i = 0;
//
//                do {
//                    chr1 = input.charCodeAt(i++);
//                    chr2 = input.charCodeAt(i++);
//                    chr3 = input.charCodeAt(i++);
//
//                    enc1 = chr1 >> 2; // jshint ignore:line
//                    enc2 = ((chr1 & 3) << 4) | (chr2 >> 4); // jshint ignore:line
//                    enc3 = ((chr2 & 15) << 2) | (chr3 >> 6); // jshint ignore:line
//                    enc4 = chr3 & 63; // jshint ignore:line
//
//                    if (isNaN(chr2)) {
//                        enc3 = enc4 = 64;
//                    } else if (isNaN(chr3)) {
//                        enc4 = 64;
//                    }
//
//                    output = output +
//                        this.keyStr.charAt(enc1) +
//                        this.keyStr.charAt(enc2) +
//                        this.keyStr.charAt(enc3) +
//                        this.keyStr.charAt(enc4);
//                    chr1 = chr2 = chr3 = '';
//                    enc1 = enc2 = enc3 = enc4 = '';
//                } while (i < input.length);
//
//                return output;
//            },
//
//            decode: function (input) {
//                var output = '';
//                var chr1, chr2, chr3 = '';
//                var enc1, enc2, enc3, enc4 = '';
//                var i = 0;
//
//                // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
//                var base64test = /[^A-Za-z0-9\+\/\=]/g;
//                if (base64test.exec(input)) {
//                    window.alert('There were invalid base64 characters in the input text.\n' +
//                        'Valid base64 characters are A-Z, a-z, 0-9, \'+\', \'/\',and \'=\'\n' +
//                        'Expect errors in decoding.');
//                }
//                input = input.replace(/[^A-Za-z0-9\+\/\=]/g, '');
//
//                do {
//                    enc1 = this.keyStr.indexOf(input.charAt(i++));
//                    enc2 = this.keyStr.indexOf(input.charAt(i++));
//                    enc3 = this.keyStr.indexOf(input.charAt(i++));
//                    enc4 = this.keyStr.indexOf(input.charAt(i++));
//
//                    chr1 = (enc1 << 2) | (enc2 >> 4); // jshint ignore:line
//                    chr2 = ((enc2 & 15) << 4) | (enc3 >> 2); // jshint ignore:line
//                    chr3 = ((enc3 & 3) << 6) | enc4; // jshint ignore:line
//
//                    output = output + String.fromCharCode(chr1);
//
//                    if (enc3 !== 64) {
//                        output = output + String.fromCharCode(chr2);
//                    }
//                    if (enc4 !== 64) {
//                        output = output + String.fromCharCode(chr3);
//                    }
//
//                    chr1 = chr2 = chr3 = '';
//                    enc1 = enc2 = enc3 = enc4 = '';
//
//                } while (i < input.length);
//
//                return output;
//            }
//        };
//
//        return {
//            signIn: signIn,
//            getRepos: getRepos
//        };
//
//        function signIn(user) {
//            var credentials = Base64.encode(user.username + ':' + user.password);
//
//            return $http({
//                method  : 'GET',
//                url     : 'https://api.github.com/user',
//                headers : {'Authorization': 'Basic ' + credentials}
//            });
//        }//
//        function getRepos(user) {
//            return $http({
//                method  : 'GET',
//                url: 'http://localhost:51382/api/user/repos',
//            }).then(function (response) { return response.data;});
//        }
//    }
//})();
