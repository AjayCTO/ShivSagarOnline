app.controller("ShoppingCartCtrl", function ($scope, AddToCart, CartToCookieService) {
    $scope.emailvalidation = /^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$/;
    $scope.CartItems = [];
    $scope.TotalOfCartItems = 0;
    $scope.years;

    $scope.CustomerInfoList = _CustomerInfo;
    $scope.CustomerAddress = {address1:"",address2:"",city:"",state:"",region:"",type:"",country:""};

    $scope.CustomerDetails = { firstName: $scope.CustomerInfoList.FirstName, lastName: $scope.CustomerInfoList.LastName, phone: $scope.CustomerInfoList.Phone, email: $scope.CustomerInfoList.Email, cardType: 0, CreditCard: "", CardExpMo: "", CardExpYr: "", userName: "", password: "" };

    $scope.loadyear = function () {
        debugger;
        var year = new Date().getFullYear();
        var range = [];
     range.push(year);

        for (var i = 0; i < 20; i++) {
            range.push(year + i);
        }

        $scope.years = range;
    }

   
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

        var total = 0;

        for (var i = 0; i < $scope.CartItems.length; i++) {
            var product = $scope.CartItems[i];
            total += (product.Cost * product.Quantity);
        }
        $scope.TotalOfCartItems = total;

        toastr.info("", "Updated");

    };

    $scope.DeleteCartItem = function (Product) {

        //if (confirm("are you sure you want to delete this item from cart ?") == true) {
        //    for (var i = 0; i < $scope.CartItems.length; i++) {
        //        if ($scope.CartItems[i].ProductId == Product.ProductId) {
        //            $scope.CartItems.splice($.inArray(Product, $scope.CartItems), 1);
        //        }
        //    }
        //    debugger;
        //    CartToCookieService.setCookieData($scope.CartItems);

        //    var total = 0;

        //    for (var i = 0; i < $scope.CartItems.length; i++) {
        //        var product = $scope.CartItems[i];
        //        total += (product.Cost * product.Quantity);
        //    }
        //    $scope.TotalOfCartItems = total;

        //}

        bootbox.confirm("are you sure you want to delete this item from cart ?", function (result) {
            if (result) {
                debugger;
                for (var i = 0; i < $scope.CartItems.length; i++) {
                    if ($scope.CartItems[i].ProductId == Product.ProductId) {
                        $scope.CartItems.splice($.inArray(Product, $scope.CartItems), 1);
                    }
                }
                debugger;
                CartToCookieService.setCookieData($scope.CartItems);

                var total = 0;

                for (var i = 0; i < $scope.CartItems.length; i++) {
                    var product = $scope.CartItems[i];
                    total += (product.Cost * product.Quantity);
                }
                $scope.TotalOfCartItems = total; $scope.$apply();
            }
       

        });
        
          
    };



   
    $scope.Continue = function () {
        debugger;
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



    app.directive
     ('creditCardType'
     , function () {
         var directive =
           {
               require: 'ngModel'
           , link: function (scope, elm, attrs, ctrl) {
               ctrl.$parsers.unshift(function (value) {
                   scope.ccinfo.type =
                     (/^5[1-5]/.test(value)) ? "mastercard"
                     : (/^4/.test(value)) ? "visa"
                     : (/^3[47]/.test(value)) ? 'amex'
                     : (/^6011|65|64[4-9]|622(1(2[6-9]|[3-9]\d)|[2-8]\d{2}|9([01]\d|2[0-5]))/.test(value)) ? 'discover'
                     : undefined
                   ctrl.$setValidity('invalid', !!scope.ccinfo.type)
                   return value
               })
           }
           }
         return directive
     }
       )
