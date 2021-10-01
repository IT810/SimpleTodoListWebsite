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

    $scope.GetAllDeleted = function () {
        $http({
            method: "get",
            url: "/Home/GetAllDeleted"
        }).then(function (response) {
            $scope.deletedtasks = response.data;
        }, function () {
            alert("Lỗi !!!");
        });
    }

    $scope.AddTask = function () {
        if ($scope.desc == "" || $scope.desc == null) {
            alertify.alert("Thông báo","Vui lòng nhập đầy đủ thông tin!", function () {});
        }
        else {
            var type = $("#insert").val();
            if (type == "Thêm") {
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
                    alertify.success('Thêm thành công !!!');
                }, function () {
                    alert("Lỗi !!!");
                });
            }
            else {
                $scope.t = new Object();
                $scope.t.Id = $scope.id;
                $scope.t.Description = $scope.desc;
                $http({
                    method: "post",
                    url: "/Home/UpdateTask",
                    datatype: "json",
                    data: JSON.stringify($scope.t)
                }).then(function (response) {
                    $scope.GetAllTasks();
                    $scope.id = "";
                    $scope.desc = "";
                    $("#insert").val("Thêm");
                    alertify.success('Cập nhật thành công !!!');
                }, function () {
                    alertify.error('Lỗi !!!');
                });
            }
        }
    }

    $scope.UpdateTask = function (item) {
        $scope.id = item.Id;
        $scope.desc = item.Description;
        $("#insert").val("Cập nhật");
    }

    $scope.UpdateStatus = function (Id) {
        $http({
            method: "get",
            url: "/Home/UpdateStatus/" + Id,
        }).then(function (response) {
            $scope.GetAllTasks();
            alertify.success('Cập nhật tình trạng thành công !!!');
        }, function () {
            alertify.error('Lỗi !!!');
        });
    }

    $scope.DeleteTask = function(Id) {
        alertify.confirm("Thông báo", "Bạn có muốn xóa không ???",
            function () {
                $http({
                    method: "get",
                    url: "/Home/DeleteTask/" + Id,
                }).then(function (response) {
                    $scope.GetAllTasks();
                    alertify.success('Xóa thành công !!!');
                }, function () {
                    alertify.error('Lỗi !!!');
                });
            },
            function () {
                alertify.error('Đã hủy');
            });
    }

    $scope.Undo = function (Id) {
        alertify.confirm("Thông báo", "Bạn có hoàn tác không ???",
            function () {
                $http({
                    method: "get",
                    url: "/Home/Undo/" + Id,
                }).then(function (response) {
                    $scope.GetAllDeleted();
                    alertify.success('Hoàn tác thành công !!!');
                }, function () {
                    alertify.error('Lỗi !!!');
                });
            },
            function () {
                alertify.error('Đã hủy');
            });
    }

    $scope.DeleteCompletely = function (Id) {
        alertify.confirm("Thông báo", "Bạn có muốn xóa hoàn toàn không ???",
            function () {
                $http({
                    method: "get",
                    url: "/Home/DeleteCompletely/" + Id,
                }).then(function (response) {
                    $scope.GetAllDeleted();
                    alertify.success('Xóa thành công !!!');
                }, function () {
                    alertify.error('Lỗi !!!');
                });
            },
            function () {
                alertify.error('Đã hủy');
            });
    }
});