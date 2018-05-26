
 
app.controller("CustomerCtrl", function ($scope, AddToCart, CartToCookieService) {

    $scope.CustomerId = customerId;
    $scope.wishList=[];

    $scope.GetWishListItems = function () {
        $.ajax({
            url: '/api/Products/GetWishList',
            type: 'GET',
            dataType: 'json',
            data: { CustomerId: $scope.CustomerId },
            success: function (data, textStatus, xhr) {
                debugger;
                $scope.wishList = data;
              

                $scope.$apply();
            },
            error: function (xhr, textStatus, errorThrown) {
                return items = [];
            }
        });
    };
    $scope.GetWishListItems();
});

$("#cart").on("click", function () {
    $(".shopping-cart").fadeToggle("fast");
});