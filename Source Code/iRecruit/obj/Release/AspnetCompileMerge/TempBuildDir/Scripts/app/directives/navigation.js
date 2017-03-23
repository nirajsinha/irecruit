angular.module('irecruit.directives')
.controller('NavigationController', ['$scope', function ($scope) {
    $scope.isIndentOpen = false;
    $scope.isResourcesOpen = false;
    $scope.isConfiguratiosOpen = false;
    $scope.iconSize = 80;
    
    $scope.handleIndentClick = function () {
        $scope.isResourcesOpen = false;
        $scope.isConfiguratiosOpen = false;
        $scope.isReportsOpen = false;
        $("#sidebarResources").hide();
        $("#sidebarConfig").hide();
        $("#sidebarReports").hide();

        if (!$scope.isIndentOpen) {
            $("#sidebarIndent").show();
            var NewElementWidth = 300;
            var animateWidthTo = NewElementWidth + $scope.iconSize;
            $("div.sideBar").animate({ width: animateWidthTo, duration: 600 });
            $scope.isIndentOpen = !$scope.isIndentOpen;
        }
        else {
            $("div.sideBar").animate({ width: $scope.iconSize, duration: 600 }, function () {
                $("#sidebarIndent").hide();
            });
            $scope.isIndentOpen = !$scope.isIndentOpen;
        }
        if ($scope.isIndentOpen) $("div.sidebarLiveDisplay").show();
        else $("div.sidebarLiveDisplay").hide();
        return false;
		    
    };
    $scope.handleResourceClick = function () {
		    
        $scope.isIndentOpen = false;
        $scope.isConfiguratiosOpen = false;
        $scope.isReportsOpen = false;
        $("#sidebarIndent").hide();
        $("#sidebarConfig").hide();
        $("#sidebarReports").hide();

        if (!$scope.isResourcesOpen) {
            $("#sidebarResources").show();
            var NewElementWidth = 300;
            var animateWidthTo = NewElementWidth + $scope.iconSize;
            $("div.sideBar").animate({ width: animateWidthTo, duration: 600 });
            $scope.isResourcesOpen = !$scope.isResourcesOpen;
        }
        else {
            $("div.sideBar").animate({ width: $scope.iconSize, duration: 600 }, function () {
                $("#sidebarResources").hide();
            });
            $scope.isResourcesOpen = !$scope.isResourcesOpen;
        }
        if ($scope.isResourcesOpen) $("div.sidebarLiveDisplay").show();
        else $("div.sidebarLiveDisplay").hide();
        return false;
		    
    },
    $scope.handleConfigClick = function () {
		    
        $scope.isIndentOpen = false;
        $scope.isResourcesOpen = false;
        $scope.isReportsOpen = false;
        $("#sidebarIndent").hide();
        $("#sidebarResources").hide();
        $("#sidebarReports").hide();

        if (!$scope.isConfiguratiosOpen) {
            $("#sidebarConfig").show();
            var NewElementWidth = 300;//$("#sidebarConfig img").width();
            var animateWidthTo = NewElementWidth + $scope.iconSize;
            $("div.sideBar").animate({ width: animateWidthTo, duration: 600 });
            $scope.isConfiguratiosOpen = !$scope.isConfiguratiosOpen;

        }
        else {
            $("div.sideBar").animate({ width: $scope.iconSize, duration: 600 }, function () {
                $("#sidebarConfig").hide();
            });
            $scope.isConfiguratiosOpen = !$scope.isConfiguratiosOpen;
        }
        if ($scope.isConfiguratiosOpen) $("div.sidebarLiveDisplay").show();
        else $("div.sidebarLiveDisplay").hide();
        return false;
		    
    },
    $scope.closeAll = function () {
        $("div.sideBar").animate({ width: $scope.iconSize, duration: 600 }, function () {
            $scope.isIndentOpen = false;
            $scope.isResourcesOpen = false;
            $scope.isConfiguratiosOpen = false;
            $scope.isReportsOpen = false;
            $("#sidebarIndent").hide();
            $("#sidebarResources").hide();
            $("#sidebarConfig").hide();
            $("#sidebarReports").hide();
            $("div.sidebarLiveDisplay").hide();
        });
    };
    
    $scope.get_isOpen = function() {
        return $scope.isIndentOpen ||
               $scope.isResourcesOpen ||
               $scope.isConfiguratiosOpen ||
               $scope.isReportsOpen;
    }
    $scope.init = function () {
        $("div.sidebarLiveDisplay").hide();
        $("#sidebarIndent").hide();
        $("#sidebarResources").hide();
        $("#sidebarConfig").hide();
        //$("#indentLink").on('click', function (e) {
        //    $scope.handleIndentClick();
        //});
        //$("#resourceLink").on('click', function (e) {
        //    $scope.handleResourceClick();
        //});
        //$("#configLink").on('click', function (e) {
        //    $scope.handleConfigClick();
        //});
        $('div.sidebarLiveDisplay ul li a').mouseover(function () {
            $(this).css('color', 'black');
            $(this).parent().css('background-color', 'rgb(239, 244, 255)');
        });
        $('div.sidebarLiveDisplay ul li a').mouseout(function () {
            $(this).css('color', 'ghostwhite');
            $(this).parent().css('background-color', 'transparent');
        });
        $("div.sideBar").on('click', function (e) {
            if ($scope.get_isOpen()) {
                e.stopPropagation();
            }
        });
        $(document).click(function (e) {
            if ($scope.get_isOpen()) {
                $scope.closeAll();
            }
        });
    }
}])
.directive("navigation", ['$log', '$compile', function ($log, $compile) {
    return {
        restrict: 'E',
        replace: true,
        scope: {
            menu: '=',
        },
        templateUrl: '/scripts/app/directives/navigation.tmpl.html',
        controller: 'NavigationController',
        //link: function ($scope, elem, attrs) {}
        compile: function (el) {
            var contents = el.contents().remove();
            return function(scope,el){
                $compile(contents)(scope,function(clone){
                    el.append(clone);
                });
                scope.init();
            };
            
          
        }

 
    };
}]);

