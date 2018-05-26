var app = angular.module("MyApp", []);
app.filter('unique', function () {
    // we will return a function which will take in a collection
    // and a keyname
    return function (collection, keyname) {
        // we define our output and keys array;
        var output = [],
            keys = [];
        
        // we utilize angular's foreach function
        // this takes in our original collection and an iterator function
        angular.forEach(collection, function (item) {
            // we check to see whether our object exists
            var key = item.AttributeValue;
            // if it's not already part of our keys array
            if (keys.indexOf(key) === -1) {
                // add it to our keys array
                keys.push(key);
                // push this item to our final output array
                output.push(item);
            }
        });
        // return our array which should be devoid of
        // any duplicates
        return output;
    };
});
app.factory("AddToCart", function () {


    return {
        flyToElement: function (flyer, flyingTo) {
            var $func = $(this);
            var divider = 3;
            var flyerClone = $(flyer).clone();
            $(flyerClone).css({ position: 'absolute', top: $(flyer).offset().top + "px", left: $(flyer).offset().left + "px", opacity: 1, 'z-index': 1000 });
            $('body').append($(flyerClone));
            var gotoX = $(flyingTo).offset().left + ($(flyingTo).width() / 2) - ($(flyer).width() / divider) / 2;
            var gotoY = $(flyingTo).offset().top + ($(flyingTo).height() / 2) - ($(flyer).height() / divider) / 2;

            $(flyerClone).animate({
                opacity: 0.4,
                left: gotoX,
                top: gotoY,
                width: $(flyer).width() / divider,
                height: $(flyer).height() / divider
            }, 700,
            function () {
                $(flyingTo).fadeOut('fast', function () {
                    $(flyingTo).fadeIn('fast', function () {
                        $(flyerClone).fadeOut('fast', function () {
                            $(flyerClone).remove();
                        });
                    });
                });
            });
        }
    };
});
app.factory("CartToCookieService", function () {
   
    var CartProducts = [];
    return {
        setCookieData: function (CartProducts) {
            Cookies.remove('Products');
            Cookies.set('Products', JSON.stringify(CartProducts), { expires: 7 });
            
        },
        getCookieData: function () {
            CartProducts = Cookies.get('Products');  
            return CartProducts===undefined ? CartProducts : JSON.parse(CartProducts);
        },
        clearCookieData: function () {
            CartProducts = "";
           Cookies.remove('Products');
        }
    };

});