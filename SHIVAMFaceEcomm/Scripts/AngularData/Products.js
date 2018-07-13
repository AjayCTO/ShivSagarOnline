﻿app.controller("PaginationCtrl", function ($scope,$rootScope, AddToCart, CartToCookieService) {


    $scope.itemsPerPage = 6;
    $scope.currentPage = 0;
    $scope.total = 0;
    $scope.alltotalproduct = 0;
    $scope.search = '';
    $scope.value = '';
    $scope.categoriesarraySelect = [];
    $scope.categoriesobj = '';
    $scope.pagedItems = [];
    $scope.CartProductsCounter = 0;
    $scope.CartItem = { ProductId: 0, Image: '', Quantity: 0, ProductName: "" };
    $scope.AllCartItems = [];
    $scope.Attributes = [];
    $scope.AttributesValue = [];
    $scope.categories = [];
    $scope.AllAttributeFilters = [];
    $scope.Suppliers = [];
    $scope.selectedAttribute = "";
    $scope.showloader = true;
    $scope.CurrentWishList = [];
    $scope.CustomerID = _customerID == undefined ? -1 : _customerID;
    var minpriceval;
    var maxpriceval;
    var searchtext;
    $scope.category = {
        "id": 0,
        "categoryName": null,
        "isActive": false,
        "sort": 0,
        "description": null,
        "notes": null,
        "supplier_Id": null,
        "parentCategory": null,
        "categoryImage": null,
        "categories1": null
    };
   
    $scope.FilterProducts = function () {
        $scope.itemsPerPage = 5;
        $scope.currentPage = 0;
        $scope.total = 0;
        $scope.pagedItems = [];
        $scope.loadData($scope.currentPage * $scope.itemsPerPage, $scope.itemsPerPage);

    };

    $scope.Clearfilter = function () {
    debugger
        $scope.search='',
        FilterText='',
        $scope.categoriesobj='',
        minpriceval='',
        maxpriceval = ''
        $scope.categoriesarraySelect = [];
      
        $scope.loadData($scope.currentPage * $scope.itemsPerPage, $scope.itemsPerPage);
   
    }
    $scope.myFunc = function () {
        debugger;

        $scope.loadData($scope.currentPage * $scope.itemsPerPage, $scope.itemsPerPage);
    };

    $rootScope.$on("CallGetCookieData", function () {
        var items = CartToCookieService.getCookieData();
        if (items != undefined) {

            $scope.AllCartItems = items;
            $scope.CartProductsCounter = $scope.AllCartItems.length;
        }
    });



    $scope.loadData = function (offset, limit) {
        debugger;
        var items = CartToCookieService.getCookieData();
        if (items != undefined) {

            $scope.AllCartItems = items;

            console.log("cart items");
            console.log($scope.AllCartItems);

            $scope.CartProductsCounter = $scope.AllCartItems.length;
        }

        var FilterText = "";
        var newcounter = 0;
        for (var i = 0; i < $scope.AllAttributeFilters.length; i++) {
            if ($scope.AllAttributeFilters[i].Values.length > 0) {
                if (newcounter == 0) {
                    FilterText = FilterText + "(";
                    newcounter++;
                }
                FilterText = FilterText + " [" + $scope.AllAttributeFilters[i].Name + "]  in (";
                for (var k = 0; k < $scope.AllAttributeFilters[i].Values.length; k++) {
                    FilterText = FilterText + "'" + $scope.AllAttributeFilters[i].Values[k] + "'" + ",";
                }
                FilterText = FilterText.replace(/,\s*$/, "");

                FilterText = FilterText + ") OR ";


            }
        }
        if (newcounter > 0) {
            FilterText = FilterText.substring(0, FilterText.length - 4);
            FilterText = FilterText + ") AND";
        }
        debugger;
        if ($.trim($scope.categoriesobj) != "" || $.trim(minpriceval) != "" || $.trim(maxpriceval) != "" || $.trim(FilterText) != "" ||$.trim.searchtext!="")
        {
            $scope.pagedItems = [];
        }

        

        $.ajax({
            url: '/Models/GetProducts.ashx',
            type: 'GET',
            dataType: 'json',
            data: { displayLength: limit, displayStart: offset, searchText: $scope.search, filtertext: FilterText, Categories: $scope.categoriesobj, lowprice: minpriceval, highprice: maxpriceval },
            success: function (data, textStatus, xhr) {
           
                $scope.total = data.iTotalDisplayRecords;
                $scope.pagedItems = $scope.pagedItems.concat(data.aaData);
                debugger;
                $scope.$apply();
            },
            error: function (xhr, textStatus, errorThrown) {
                return items = [];
            }
        });

        $scope.loadFilters();


    };




    $scope.AddAttrToFilter = function (ischecked, name, value) {

        debugger;
        if (ischecked == 1) {
            for (var i = 0; i < $scope.AllAttributeFilters.length; i++) {
                if ($scope.AllAttributeFilters[i].Name === name) {
                    if ($scope.AllAttributeFilters[i].Values.indexOf(value) === -1) {
                        $scope.AllAttributeFilters[i].Values.push(value);
                    }

                }
            }
        } else {
            for (var i = 0; i < $scope.AllAttributeFilters.length; i++) {
                if ($scope.AllAttributeFilters[i].Name === name) {
                    if ($scope.AllAttributeFilters[i].Values.indexOf(value) === 1) {

                        $scope.AllAttributeFilters[i].Values.splice($scope.AllAttributeFilters[i].Values.indexOf(value), 1);
                    }

                }
            }
        }

 
        $scope.loadData($scope.currentPage * $scope.itemsPerPage, $scope.itemsPerPage);
    }

    $scope.loadSuppliers = function () {
        $.ajax({
            url: '/api/Products/GetSuppliers',
            type: 'GET',
            dataType: 'json',
            success: function (data, textStatus, xhr) {
                debugger;
                $scope.Suppliers = data;

                $scope.$apply();
            },
            error: function (xhr, textStatus, errorThrown) {
                $scope.Attributes = [];
            }
        });
    };

    $scope.loadFilters = function () {

        $.ajax({
            url: '/api/Products/GetAttributes',
            type: 'GET',
            dataType: 'json',
            success: function (data, textStatus, xhr) {

                $scope.Attributes = data;
                for (var i = 0; i < $scope.Attributes.length; i++) {
                    $scope.AllAttributeFilters.push({
                        Name: $scope.Attributes[i].AttributeName,
                        Values: []
                    });
                }


                $scope.$apply();
            },
            error: function (xhr, textStatus, errorThrown) {
                $scope.Attributes = [];
            }
        });
        $.ajax({
            url: '/api/Products/GetAttributesValue',
            type: 'GET',
            dataType: 'json',
            success: function (data, textStatus, xhr) {

                $scope.AttributesValue = data;


                $scope.$apply();
            },
            error: function (xhr, textStatus, errorThrown) {
                $scope.Attributes = [];
            }
        });

        $.ajax({
           
            url: '/api/Products/GetCategories',
            type: 'GET',
            dataType: 'json',
            success: function (data, textStatus, xhr) {
                debugger;
                $scope.categories = data;


                console.log("category");
                console.log($scope.categories);

                $scope.showloader = false;

                $scope.$apply();
                setTimeout(function () {


                    new Swiper('.swiper-coverflow', {
                        pagination: '.swiper-pagination',
                        nextButton: '.swiper-button-next',
                        prevButton: '.swiper-button-prev',
                        paginationClickable: true,
                        effect: 'coverflow',
                        centeredSlides: true,
                        slidesPerView: 'auto',
                        loop: true,
                        coverflow: {
                            rotate: 50,
                            stretch: 0,
                            depth: 100,
                            modifier: 1,
                            slideShadows: true
                        }
                    });
                }, 100);


            },
            error: function (xhr, textStatus, errorThrown) {
                $scope.categories = [];
            }
        });


        function reinitSwiper(swiper) {

        }


        $.ajax({
            url: '/api/Products/GetSupplier',
            type: 'GET',
            dataType: 'json',
            success: function (data, textStatus, xhr) {


                $scope.supplierlist = data;
                $scope.$apply();


                console.log($scope.supplierlist);
            },
            error: function (xhr, textStatus, errorThrown) {

            }
        });


    };
    $scope.loadMore = function () {
        debugger;
        $scope.currentPage++;
        $scope.loadData($scope.currentPage * $scope.itemsPerPage, $scope.itemsPerPage);

    };

    $scope.nextPageDisabledClass = function () {
        return $scope.currentPage === $scope.pageCount() - 1 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        return Math.ceil($scope.total / $scope.itemsPerPage);
    };

    $scope.AddToCart = function (productId, product) {


        debugger;

        var item = $scope.AllCartItems.filter(function (item) {
            if (item.ProductId === product.x[8]) {
                item.Quantity = item.Quantity + 1;
            }
            return item.ProductId === product.x[8];
        })[0];
        if (item == undefined) {
            $scope.CartProductsCounter++;
            $('html, body').animate({
                'scrollTop': $(".cart_anchor").position().top
            });
            var itemImg = $("#pid" + productId).parent().parent().find('img').eq(0);
            //AddToCart.flyToElement($(itemImg), $('.cart_anchor'));
            $scope.AllCartItems.push({ ProductId: product.x[8], Image: product.x[2], Quantity: 1, ProductName: product.x[3], Cost: product.x[4], discount: 0 });


        }
        CartToCookieService.setCookieData($scope.AllCartItems);
        debugger;


    };

    $scope.loadData($scope.currentPage * $scope.itemsPerPage, $scope.itemsPerPage);
    $scope.loadSuppliers();

    $scope.RemoveFromwishList = function (ID) {

        $.ajax({
            url: '/Home/DeleteWishList?id=' + ID,
            type: 'GET',
            dataType: 'json',
            success: function (data, textStatus, xhr) {
                if (data.Success == true) {

                    $scope.GetWishList($scope.CustomerID);
                    $scope.$apply();
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("into error");
            }
        });
    };
    $scope.addTowishList = function (productId, customerId) {

        debugger;
        if ($scope.CheckisInWishList(productId) == "active") {
            for (var i = 0; i < $scope.CurrentWishList.length; i++) {
                if ($scope.CurrentWishList[i].ProductId == productId) {
                    $scope.RemoveFromwishList($scope.CurrentWishList[i].Id);
                    break;
                }

            }

        }
        else {


            var wishListmodel = { ProductId: productId, CustomerId: -1, UserID: customerId };
            $.ajax({
                url: '/api/WishLists/PostWishList',
                type: 'POST',
                data: wishListmodel,
                dataType: 'json',
                success: function (data, textStatus, xhr) {
                    $scope.GetWishList($scope.CustomerID);
                    $scope.$apply();
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert("into error");
                }
            });
        }
    };



    $scope.GetWishList = function (customerId) {
        $.ajax({
            url: '/api/WishLists/GetWishLists?UserID=' + customerId,
            type: 'GET',
            dataType: 'json',
            success: function (data, textStatus, xhr) {
                $scope.CurrentWishList = data;
                $scope.$apply();
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("into error");
            }
        });

    }
    $scope.GetWishList($scope.CustomerID);

    $scope.CheckisInWishList = function (ProductID) {
        for (var i = 0; i < $scope.CurrentWishList.length; i++) {
            if ($scope.CurrentWishList[i].ProductId == ProductID) {
                return "active";
            }

        }

        return "";
    }

    $scope.AddCatArray=function(CategoryId)
    {
        debugger;
        var idx = $scope.categoriesarraySelect.indexOf(CategoryId);
        if (idx > -1) {
            // is currently selected
            $scope.categoriesarraySelect.splice(idx, 1);
        }
        else {
            // is newly selected
            $scope.categoriesarraySelect.push(CategoryId);
        }
        
        
  
        $scope.categoriesobj = $scope.categoriesarraySelect.join();

        $scope.loadData($scope.currentPage * $scope.itemsPerPage, $scope.itemsPerPage);

    }

    //function in remove in cart 
    $scope.DeleteCarttolist = function (Product) {
        debugger;
        bootbox.confirm("Are you sure you want to delete this Item ?", function (result) {
            if (result) {
                debugger;
       
                for (var i = 0; i < $scope.AllCartItems.length; i++) {
                    if ($scope.AllCartItems[i].ProductId == Product.ProductId) {
                        $scope.AllCartItems.splice($.inArray(Product, $scope.AllCartItems), 1);
                    }
                }
                CartToCookieService.setCookieData($scope.AllCartItems);
                $scope.CartProductsCounter = $scope.AllCartItems.length;
                $scope.$apply();
            }
       
      
        });
    };
    $("._slider").on("change", function () {
        debugger;

        minpriceval = $("#lowpricevalue").val();
        maxpriceval = $("#highpricevalue").val();
        if (minpriceval != "" || maxpriceval != "") {
            $scope.loadData($scope.currentPage * $scope.itemsPerPage, $scope.itemsPerPage);
        }

    });



});

$("#cart").on("click", function () {
    debugger;
    $(".shopping-cart").fadeToggle("fast");
    document.getElementById("overlay").style.display = "block";
});

$(".fa-angle-double-right").on("click", function () {
    debugger;
    $(".shopping-cart").fadeToggle("fast");
    document.getElementById("overlay").style.display = "none";

});

$("#overlay").on("click", function () {
    debugger;
    $(".shopping-cart").fadeToggle("fast");
    document.getElementById("overlay").style.display = "none";
});


