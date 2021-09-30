/// <reference path="angular.min.js" />
var app = angular.module('ma', []);
app.controller('mc', function ($scope, $http) {
    $scope.GetAllTasks = function () {
        $http({
            method: "get",
            url: "/Home/GetAllTasks"
        }).then(function (response) {
            $scope.tasks = response.data;
        }, function () {
            alert("Lỗi !!!");
        });
    }

    $scope.AddTask = function () {
        $scope.t = new Object();
        $scope.t.Description = $scope.desc;
        $http({
            method: "post",
            url: "/Home/AddTask",
            datatype: "json",
            data: JSON.stringify($scope.t)
        }).then(function (response) {
            $scope.GetAllTasks();
            $scope.desc = "";
        }, function () {
            alert("Lỗi !!!");
        });
    }
});