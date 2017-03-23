var INTEGER_REGEXP = /^\-?\d+$/;

angular.module('irecruit.directives')
.directive('integer', function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ngModel) {
            scope.$watch(function () {
                if (elm.attr('disabled')) return true;
                var modelValue = ngModel.$modelValue;
                if (INTEGER_REGEXP.test(modelValue)) {
                    return true;
                }
                return false;
            }, function (validity) {
                ngModel.$setValidity('integer', validity);
            });
        }
    };
})
.directive('username', ['$q', '$timeout', function ($q, $timeout) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ngModel) {
            scope.$watch(function () {
                var modelValue = ngModel.$modelValue;
                var usernames = [];
                $.each(scope.users, function (index, item) {
                    usernames.push(item.label);
                });
                if ($.inArray(modelValue, usernames) > -1) {
                    return true;
                } else {
                    return false;
                }

            },
            function (validity) {
                ngModel.$setValidity('username', validity);
            });

        }
    };
}])
.directive('validTechnicalRating', ['$q', '$timeout', function ($q, $timeout) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ngModel) {
            scope.$watch(function () {
                var modelValue = ngModel.$modelValue;
                if (modelValue && modelValue != '') {
                    if (modelValue >= 1 && modelValue <= 4) return true;
                    return false;
                }
                return true;
            },
            function (validity) {
                ngModel.$setValidity('validTechnicalRating', validity);
            });

        }
    };
}]);


