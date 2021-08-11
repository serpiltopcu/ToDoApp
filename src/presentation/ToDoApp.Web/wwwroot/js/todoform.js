var d;
var app = angular.module("TaskManager", []);
app.controller("taskController", function ($scope, $http) {
    $scope.dataLoading = false;
    $scope.disableBtn = false;
    $scope.error = '';

    $scope.newTask = {
        Assign: null,
        AssignedToUser: null,
        users: []
    };

    $scope.newTasks = {};
 
    $scope.getUserList = function () {
        $http({
            method: "GET",
            url: "/user/getlist",
        }).then(function (response) {
            $scope.newTask.users = response.data.list;
            if ($scope.newTask.users.length>0) {
                $scope.newTask.Assign = $scope.newTask.users[0].id;
            }
            
        });
    };

    $scope.getTodoList = function () {
        $http({
            method: "GET",
            url: "/todo/getlist",
        }).then(function (response) {
            $scope.newTasks = response.data.list;
            
        });
    };



    $scope.addNew = function () {

        if ($scope.newTask.$valid && $scope.newTask.Title != undefined) {
            $scope.dataLoading = true;
            $scope.disableBtn = true;

            $scope.newTask.AssignedToUser = {
                'Id': $('#assignSelect').find(":selected").val(),
                'Name': $('#assignSelect').find(":selected").data('name'),
            };

            $http({
                method: "POST",
                url: "/todo/add",
                data: $scope.newTask,
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {

                if (!response.data.status) {
                    $scope.error = response.data.message;
                }
                else {
                    window.location.href = "/todo";
                }

                $scope.dataLoading = false;
                $scope.disableBtn = false;
            }
                , function () {
                    // Error callback
                })
        } else {
            $scope.error = "Form is not valid!";
        }
       
    }

    $scope.getUserList();
    $scope.getTodoList();

    d = $scope;
});

