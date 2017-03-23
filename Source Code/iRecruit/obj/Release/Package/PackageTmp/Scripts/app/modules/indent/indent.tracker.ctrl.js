angular.module('irecruit.indent')
.controller('IndentTrackerController', ['$compile', '$scope', 'commonService', 'persistService', 'indentService', '$state', '$stateParams', function ($compile, $scope, commonService, persistService, indentService, $state, $stateParams) {
    $scope.indents = [];
    $scope.sortPredicate = 'IndentDate';
    $scope.busy = false;
    $scope.totalReached = false;
    $scope.after = '';
    $scope.TotalItems = 0;
    $scope.PageNo = 0;
    $scope.PageSize = 0;
    $scope.nextPage = function () {
        if ($scope.busy) return;
        if ($scope.indents && $scope.indents.length > 0 && $scope.indents.length >= $scope.TotalItems) $scope.totalReached = true;
        if (!$scope.totalReached) {
            $scope.PageNo = $scope.PageNo + 1;
            $scope.busy = true;
            return indentService.getIndentTrackerList($scope.data.UserInfo.CompanyID, $scope.PageNo, $scope.PageSize)
            .then(function (response) {
                for (var i = 0; i < response.length; i++) {
                    $scope.indents.push(response[i]);
                    $scope.TotalItems = response[i].TotalCount;
                }
                if (response.length > 0) {
                    $scope.after = "i3_" + $scope.indents[$scope.indents.length - 1].id;
                }

                $scope.busy = false;

            }, function (err) {
                $scope.busy = false;
                $.toas(err.data, '', "error");
            });
        }
    }
    $scope.redirect = function (number)
    {
        $(".info").popover('destroy');
        $state.go("indent", { 'indentNumber': number });
    }
    $scope.$on('$stateChangeSuccess', function () {
        displayMessages();
    });
    $scope.moment = function (date, format) {
        if (date) {
            if (format) return moment(date).format(format);
            return moment(date).fromNow();
        }
    }
    
    
    
}])
.directive('summaryPopOver', ['$compile', 'indentService', function ($compile, indentService) {
    //var itemsTemplate = "<ul class='unstyled'><li ng-repeat='item in items'>{{item}}</li></ul>";
    var itemsTemplate = '<div class="row text-center"  style="margin-right:0px; width:750px;">' +
    '<div class="col-lg-2 col-xs-4"><div class="small-box bg-aqua"><div class="inner"><h3>{{it.NoOfPositions}}</h3><p>Required Positions</p></div></div></div>' +
    '<div class="col-lg-2 col-xs-4"><div class="small-box bg-primary"><div class="inner"><h3>{{it.InProcess}}</h3><p>In Process</p></div></div></div>' +
    '<div class="col-lg-2 col-xs-4"><div class="small-box bg-yellow"><div class="inner"><h3>{{it.OfferedMade}}</h3><p>Offered Made</p></div></div></div>' +
    '<div class="col-lg-2 col-xs-4"><div class="small-box bg-danger"><div class="inner"><h3>{{it.Rejected}}</h3><p>Rejected</p></div></div></div>' +
    '<div class="col-lg-2 col-xs-4"><div class="small-box bg-green"><div class="inner"><h3>{{it.OnBoard}}</h3><p>On Board</p></div></div></div>' +
    '<div class="col-lg-2 col-xs-4"><div class="small-box bg-red"><div class="inner"><h3>{{it.OfferDenied}}</h3><p>Offer Denied</p></div></div></div>' +
    '</div>';
    var getTemplate = function (contentType) {
        var template = '';
        switch (contentType) {
            case 'items':
                template = itemsTemplate;
                break;
        }
        return template;
    }
    var closePopovers = function () {
        $(".info").popover('destroy');
    }
    $('body').on('click', function (e) {
        closePopovers();
    });
    return {
        restrict: "A",
        transclude: true,
        template: "<span ng-transclude></span>",
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                closePopovers();
                var popOverContent;
                
                var p = indentService.getIndentTrackerInfo(scope.indentNumber);
                p.then(function (response) {
                    scope.it = response;
                    if (scope.it) {
                        var html = getTemplate("items");
                        popOverContent = $compile(html)(scope);
                    }
                    var options = {
                        content: popOverContent,
                        placement: "left",
                        html: true,
                        trigger: "manual",
                        title: 'Indent - ' + scope.indentNumber
                    };
                    e.stopPropagation();
                    $(element).popover(options);
                    element.popover('show');
                });
                
            });

        },
        scope: {
            //item: '=',
            indentNumber: '=item',
            title: '@'
        }
    };
}]);
