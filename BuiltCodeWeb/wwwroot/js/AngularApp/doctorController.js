﻿var app = angular.module('app', []);

app.controller('doctorControllerRead', ['$scope', '$http', doctorControllerRead]);
app.controller('doctorControllerCreate', ['$scope', '$http', doctorControllerCreate]);
app.controller('doctorControllerUpdate', ['$scope', '$http', doctorControllerUpdate]);
app.controller('doctorControllerDelete', ['$scope', '$http', doctorControllerDelete]);

function doctorControllerRead($scope, $http) {

    $http.get('https://localhost:44387/api/v1/doctors').then(
        function successCallback(data) {
            var test = data.data;
            $scope.listdoctors = data.data;
        }, function errorCallback(data) {
            $scope.erro = "Erro: Não foi possivel Listar Doctors";
        }
    )
}

function doctorControllerCreate($scope, $http) {

    $http.post('https://localhost:44387/api/v1/doctors').then(
        function successCallback(data) {
            var test = data.data;
            $scope.listdoctors = data.data;
        }, function errorCallback(data) {
            $scope.erro = "Erro: Não foi possivel Listar Doctors";
        }
    )
}

function doctorControllerUpdate($scope, $http) {

    $http.get('https://localhost:44387/api/v1/doctors').then(
        function successCallback(data) {
            var test = data.data;
            $scope.listdoctors = data.data;
        }, function errorCallback(data) {
            $scope.erro = "Erro: Não foi possivel Listar Doctors";
        }
    )
}

function doctorControllerDelete($scope, $http) {

    $http.get('https://localhost:44387/api/v1/doctors').then(
        function successCallback(data) {
            var test = data.data;
            $scope.listdoctors = data.data;
        }, function errorCallback(data) {
            $scope.erro = "Erro: Não foi possivel Listar Doctors";
        }
    )
}