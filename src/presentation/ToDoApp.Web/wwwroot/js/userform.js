var app = angular.module("UserManager", []);
app.controller("UserController", function ($scope, $http) {
    $scope.dataLoading = false;
    $scope.disableBtn = false;
    $scope.loginError = '';
    $scope.registerError = '';

    console.log($scope);


    $scope.login = function () {
        $scope.dataLoading = true;
        $scope.disableBtn = true;
        $scope.loginError = '';

       
            $http({
                method: "POST",
                url: "/user/login",
                data: $scope.loginForm,
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {

                if (!response.data.status) {
                    $scope.loginError = response.data.message;
                }
                else {
                    window.location.href = "/";
                }

                $scope.dataLoading = false;
                $scope.disableBtn = false;
            }
                , function () {
                    // Error callback
                })
        

       
    };

    $scope.register = function () {
        $scope.dataLoading = true;
        $scope.disableBtn = true;
        $scope.registerError = '';

       
        if ($scope.registerForm.Password != $scope.registerForm.confirmPassword) {
            $scope.error = 'Passwords are different!';
        }
        else {
            $http({
                method: "POST",
                url: "/user/register",
                data: $scope.registerForm,
                headers: { 'Content-Type': 'application/json' }
            }).then(function (response) {
                if (!response.data.status) {
                    $scope.registerError = response.data.message;
                }
                else {
                    window.location.href = "/";   
                }
            }
                , function () {
                    // Error callback
                })
            $scope.dataLoading = false;
            $scope.disableBtn = false;

        }



    };
});


$(function () {
    $('#login-form-link').click(function (e) {
        $("#login-form").delay(100).fadeIn(100);
        $("#register-form").fadeOut(100);
        $('#register-form-link').removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
    });
    $('#register-form-link').click(function (e) {
        $("#register-form").delay(100).fadeIn(100);
        $("#login-form").fadeOut(100);
        $('#login-form-link').removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
    });
});