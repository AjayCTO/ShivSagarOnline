'use strict';
app.controller('homeController', ['$scope', function ($scope) {

    $scope.searchcategoriesslider = [];
    var _localCategories = localStorage.getItem("Categories");
    if (_localCategories != null && _localCategories != undefined) {
        _localCategories = JSON.parse(_localCategories);

    }
    else {
        _localCategories = [];
    }

    $scope.searchcategoriesslider = _localCategories;

    $scope.GetcatBgImage = function (Path) {
        if ($.trim(Path) != "") {
            return _GlobalImagePath + "/CategoryImage/" + Path;
        }
        return "../img/nocategory.png";
    }


    function productSlider() {
        // ------------------------------------------------------- //
        // Products Slider
        // ------------------------------------------------------ //
        $('.products-slider').owlCarousel({
            loop: false,
            margin: 20,
            dots: true,
            nav: false,
            smartSpeed: 400,
            responsiveClass: true,
            navText: ['<i class="fa fa-long-arrow-left"></i>', '<i class="fa fa-long-arrow-right"></i>'],
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 2
                },
                1000: {
                    items: 4
                }
            }
        });
    }

    $scope.GetProductImage = function (Path) {
        if ($.trim(Path) != "") {
            return _GlobalImagePath + "/ProductImages/" + Path;
        }
        return "../img/no-image.png";
    }
    $scope.pagedItems = [];
    $scope.total = 0;

    function init() {

        var _model = { displayLength: 1000, displayStart: 0, searchText: "", filtertext: "", Categories: "", lowprice: "", highprice: "", isFeatured: "1" };
        $.ajax({
            url: serviceBase + 'api/Product/Post',
            type: 'POST',
            dataType: 'json',
            data:_model,
            success: function (data, textStatus, xhr) {

                $scope.total = data.iTotalDisplayRecords;
                //var featuredProducts = JSON.parse(data.data);
                // $scope.pagedItems = featuredProducts.aaData;
                $scope.pagedItems = data.aaData;


                debugger;
                console.log("Data");
                console.log($scope.pagedItems);
                $("#loadingmessage").hide();
                $scope.$apply();
                productSlider();
            },
            error: function (xhr, textStatus, errorThrown) {

                alert(xhr.error);
            }
        });
    }


    init();
}]);