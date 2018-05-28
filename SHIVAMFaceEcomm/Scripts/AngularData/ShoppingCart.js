app.controller("ShoppingCartCtrl", function ($scope, AddToCart, CartToCookieService) {

    $scope.CartItems = [];
    $scope.TotalOfCartItems = 0;

    $scope.CustomerAddress = {address1:"",address2:"",city:"",state:"",region:"",type:"",country:""};

    $scope.CustomerDetails = {firstName:"",lastName:"",phone:"",email:"",cardType:0,CreditCard:"",CardExpMo:"",CardExpYr:"",userName:"",password:""};

    $scope.loadItemsFromCookie = function () {
       
        $scope.CartItems = CartToCookieService.getCookieData();
        var total = 0;
        for (var i = 0; i < $scope.CartItems.length; i++) {
            var product = $scope.CartItems[i];
            total += (product.Cost * product.Quantity);
        }
        $scope.TotalOfCartItems = total;
        
       
    };

    $scope.updateQuantityOfCartItem = function (Product) { 
        CartToCookieService.setCookieData($scope.CartItems);
    };

    $scope.DeleteCartItem = function (Product) {

        if (confirm("are you sure you want to delete this item from cart ?") == true) {
            for (var i = 0; i < $scope.CartItems.length; i++) {
                if ($scope.CartItems[i].ProductId == Product.ProductId) {
                    $scope.CartItems.splice($.inArray(Product, $scope.CartItems), 1);
                }
            }
            debugger;
            CartToCookieService.setCookieData($scope.CartItems);
        }
        
          
    };




    $scope.Continue = function () {

        var allDataToSend = { CartItems: $scope.CartItems, CustomerData: $scope.CustomerDetails, CustAddress: $scope.CustomerAddress };
        var saveData = $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: "/ShoppingCart/CompletePurchaseAndCreateSessionForUser",
            data: JSON.stringify(allDataToSend),
            dataType: "json",
            success: function (resultData) {
                
                alert("Save Complete");
                window.location.href = "/ShoppingCart/OrderConfirmation";
            }
        });
        saveData.error(function () { alert("Something went wrong"); });

    };
    $scope.loadItemsFromCookie();
});