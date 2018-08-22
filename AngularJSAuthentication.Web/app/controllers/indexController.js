'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    $scope.searchcategories = [];
    var _localCategories = localStorage.getItem("Categories");
    if (_localCategories != null && _localCategories != undefined) {
        _localCategories = JSON.parse(_localCategories);

    }
    else {
        _localCategories = [];
    }
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }
    $scope.GetCategories = function () {
        $.ajax({
            url: serviceBase+'/api/Categories/GetCategories',
            type: 'GET',
            dataType: 'json',
            success: function (data, textStatus, xhr) {
                $scope.searchcategories = data;

                localStorage.setItem("Categories", JSON.stringify(data));

                $scope.$apply();



            },
            error: function (xhr, textStatus, errorThrown) {
                $scope.categories = [];
            }
        });
    };




    if (_localCategories.length == 0) {
        $scope.GetCategories();

    }
    else {
        $scope.searchcategories = _localCategories;
    }
    $scope.authentication = authService.authentication;

}]);