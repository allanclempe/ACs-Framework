(function (angular) {
    'use strict';

    
    angular
        .module("framework",[])
        .config(function () {
            //cfg.
        })
        .controller("mainCtrl", function ($scope, $http) {
            $http.get("/API/foo/notfound", {cache: false})
            .then(function (response) {
                console.log(response);
                console.log("ok response");
            }, function () {
                console.log("error response");
            })
        })
        .run();
    
})(angular);
