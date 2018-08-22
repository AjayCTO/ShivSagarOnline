'use strict';
app.controller('productController', ['$scope', '$rootScope', function ($scope, $rootScope) {

    $scope.pagedItems = [];
    $scope.total = 0;
    $scope.itemsPerPage =15;
    $scope.currentPage = 1;
    $scope.startpage = 0;
    $scope.items = [];
    $scope.TotalItems = 0;

    $scope.categories = [];

    $scope.childmethod = function () {
        $rootScope.$emit("GetCategories", {});
    }
    var _localCategories = localStorage.getItem("Categories");
    if (_localCategories != null && _localCategories != undefined) {

        _localCategories = JSON.parse(_localCategories);

    }
    else {
        $scope.childmethod();
        _localCategories = localStorage.getItem("Categories")
        _localCategories = JSON.parse(_localCategories);

    }
    $scope.categories = _localCategories;
   

//Get Product 
    function init() {
        debugger;
        var _model = { displayLength: $scope.itemsPerPage, displayStart: $scope.startpage, searchText: "", filtertext: "", Categories: "", lowprice: "", highprice: "", isFeatured: "0" };
        $.ajax({
            url: serviceBase + 'api/Product/Post',
            type: 'POST',
            dataType: 'json',
            data: _model,
            success: function (data, textStatus, xhr) {

                $scope.total = data.iTotalDisplayRecords;
        
                $scope.pagedItems = data.aaData;


                debugger;
                console.log("Data");
                console.log($scope.pagedItems);
                $("#loadingmessage").hide();
                $scope.$apply();
     
                getcategory();
            },
            error: function (xhr, textStatus, errorThrown) {

                alert(xhr.error);
            }
        });
    }


    init();



    //Get Image Path 

    $scope.GetProductImage = function (Path) {
        if ($.trim(Path) != "") {
            return _GlobalImagePath + "/ProductImages/" + Path;
        }
        return "../img/no-image.png";
    }


    $scope.range = function () {
        var rangeSize = $scope.itemsPerPage/3;
        var ret = [];
        var start;

        start = $scope.currentPage;
        if (start > $scope.pageCount() - rangeSize - 1) {
            start = $scope.pageCount() - rangeSize + 1;
        }

        for (var i = start; i < start + (rangeSize - 1) ; i++) {
            if (i > -1) {
                ret.push(i);

            }
        }
        return ret;
    };

    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
            CheckScopeBeforeApply();
          
        }
    };

    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 1 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        return Math.ceil($scope.total / $scope.itemsPerPage);
    };


    $scope.DisableCursor = function (path) {
        for (var i = 0; i < $scope.AlreadySelectedImages.length; i++) {
            if ($.trim($scope.AlreadySelectedImages[i]) == $.trim(path)) {
                return "NonePointer";
            }
        }

        return "";
    }

    $scope.nextPage = function () {
        debugger;
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
            CheckScopeBeforeApply();
 

        }
    };

    $scope.nextPageDisabled = function () {
        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

    $scope.setPage = function (n) {
        debugger;
        $scope.startpage = n;
        CheckScopeBeforeApply();
        $scope.startpage= ($scope.startpage * $scope.itemsPerPage)+1;
        init();
    };


    function CheckScopeBeforeApply() {
        if (!$scope.$$phase) {
            $scope.$apply();
        }
    };



}]);