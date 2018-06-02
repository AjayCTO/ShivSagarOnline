app.controller("PaginationCtrl", function ($scope, AddToCart, CartToCookieService) {

    $scope.itemsPerPage = 12;
    $scope.currentPage = 0;
    $scope.total = 0;
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

    $scope.loadData = function (offset, limit) {
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

        $.ajax({
            url: '/Models/GetProducts.ashx',
            type: 'GET',
            dataType: 'json',
            data: { displayLength: limit, displayStart: offset, searchText: "", filtertext: FilterText },
            success: function (data, textStatus, xhr) {

                $scope.total = data.iTotalDisplayRecords;
                $scope.pagedItems = $scope.pagedItems.concat(data.aaData);

                $scope.$apply();
            },
            error: function (xhr, textStatus, errorThrown) {
                return items = [];
            }
        });

        $scope.loadFilters();


    };





    $scope.AddAttrToFilter = function (ischecked, name, value) {


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

        return true;
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
            AddToCart.flyToElement($(itemImg), $('.cart_anchor'));
            $scope.AllCartItems.push({ ProductId: product.x[8], Image: product.x[2], Quantity: 1, ProductName: product.x[3], Cost: product.x[4], discount: 0 });


        }
        CartToCookieService.setCookieData($scope.AllCartItems);
        debugger;


    };

    $scope.loadData($scope.currentPage * $scope.itemsPerPage, $scope.itemsPerPage);
    $scope.loadSuppliers();
    $scope.addTowishList = function (productId, customerId) {

    };

    $scope.DeleteCarttolist = function (Product) {

        debugger;

        if (confirm("are you sure you want to delete this item from cart ?") == true) {
            for (var i = 0; i < $scope.AllCartItems.length; i++) {
                if ($scope.AllCartItems[i].ProductId == Product.ProductId) {
                    $scope.AllCartItems.splice($.inArray(Product, $scope.AllCartItems), 1);
                }
            }
            debugger;
            CartToCookieService.setCookieData($scope.AllCartItems);
        }


    };


});

$("#cart").on("click", function () {
    $(".shopping-cart").fadeToggle("fast");
    document.getElementById("overlay").style.display = "block";
});

$(".fa-angle-double-right").on("click", function () {
    $(".shopping-cart").fadeToggle("fast");
    document.getElementById("overlay").style.display = "none";
});
