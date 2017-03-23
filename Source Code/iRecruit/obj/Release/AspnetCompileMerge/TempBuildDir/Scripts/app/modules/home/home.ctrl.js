angular.module('irecruit.home')
.controller('HomeController', ['$scope', 'commonService', 'homeService', '$state', '$stateParams', function ($scope, commonService, homeService, $state, $stateParams) {
    $scope.OpenPositions = 0;
    $scope.OffersMade = 0;
    $scope.OnBoard = 0;
    $scope.RejectedDenied = 0;
    $scope.InProcess = 0;

    $scope.OfferJoiningRatioChartType = 'bar';
    $scope.OfferJoiningRatioChartConfig = {
        labels: false,
        title: "",
        legend: {
            display: true,
            position: 'right'
        },
        innerRadius: 0
    };
    $scope.OfferJoiningRatioData = {
        series: [],
        data: []
    }
    $scope.RefreshOfferJoiningRatio = function () {
        var promise = homeService.getOfferJoiningRatio($scope.data.UserInfo.CompanyID, false);
        promise.then(function (data) {
            $scope.OfferJoiningRatioData.data = data.Items;
            $scope.OfferJoiningRatioData.series = data.Series;
        },function (error) {
            $.toas(error.data, '', "error");
        });
    };
    
    $scope.TopOpeningsChartType = 'bar';
    $scope.TopOpeningsChartConfig = {
        labels: false,
        title: "",
        legend: {
            display: true,
            position: 'left'
        },
        innerRadius: 0
    };
    $scope.TopOpeningsData = {
        data: []
    }
    $scope.RefreshTopOpenings = function () {
        var promise = homeService.getTopOpenings($scope.data.UserInfo.CompanyID);
        promise.then(function (data) {
            $scope.TopOpeningsData.data = data.Items;
            $scope.TopOpeningsData.series = data.Series;
        }, function (error) {
            $.toas(error.data, '', "error");
        });
    };

    $scope.EmployeeReferalChartType = 'pie';
    $scope.EmployeeReferalChartConfig = {
        labels: false,
        title: "",
        legend: {
            display: true,
            position: 'right'
        },
        innerRadius: 50
    };
    $scope.EmployeeReferalData = {
        series: [],
        data: []
    };
    $scope.RefreshEmployeeReferalData = function () {
        var promise = homeService.getResumeSources($scope.data.UserInfo.CompanyID);
        promise.then(function (data) {
            $scope.EmployeeReferalData.data = data.Items;
            $scope.EmployeeReferalData.series = data.Series;
        }, function (error) {
            $.toas(error.data, '', "error");
        });
    };
    
    
    $scope.$on('$stateChangeSuccess', function () {
        // load user photo
        if (!$scope.data.UserInfo.Photo) {
            var p = commonService.getUserDetails($scope.data.UserInfo.UserID);
            p.then(function (result) {
                $scope.data.UserInfo.Photo = result.Photo;
            }, function (err) { $.toas(err.data, '', "error"); });
        }
        var opPromise = homeService.getOpenPositions($scope.data.UserInfo.CompanyID);
        opPromise.then(function (response) {
            if (response) {
                $scope.OpenPositions = response.OpenPositions;
                $scope.OffersMade = response.OffersMade;
                $scope.OnBoard = response.OnBoard;
                $scope.RejectedDenied = response.RejectedDenied;
                $scope.InProcess = response.InProcess;
                $scope.BounceRate = 0
                if (response.OffersMade > 0) {
                    var b = Math.round((response.OffersMade - response.OnBoard) * 100 / response.OffersMade);
                    if (isNaN(b)) b = 0;
                    $scope.BounceRate = b;
                }

            }
        }, function (err) { $.toas(err.data, '', "error"); });
               
        // offer joining ratio
        $scope.RefreshOfferJoiningRatio();
        // top opening chart
        $scope.RefreshTopOpenings();
        // resume sources
        $scope.RefreshEmployeeReferalData();

        
        //Make the dashboard widgets sortable Using jquery UI
        $(".connectedSortable").sortable({
            placeholder: "sort-highlight",
            connectWith: ".connectedSortable",
            handle: ".box-header, .nav-tabs",
            forcePlaceholderSize: true,
            zIndex: 999999
        }).disableSelection();
        $(".box-header, .nav-tabs").css("cursor", "move");

        $("[data-widget='collapse']").click(function () {
            //Find the box parent        
            var box = $(this).parents(".box").first();
            //Find the body and the footer
            var bf = box.find(".box-body, .box-footer");
            if (!box.hasClass("collapsed-box")) {
                box.addClass("collapsed-box");
                //Convert minus into plus
                $(this).children(".fa-minus").removeClass("fa-minus").addClass("fa-plus");
                bf.slideUp();
            } else {
                box.removeClass("collapsed-box");
                //Convert plus into minus
                $(this).children(".fa-plus").removeClass("fa-plus").addClass("fa-minus");
                bf.slideDown();
            }
        });
        //handleAjaxMessages();
        displayMessages();
        
    });
    
}]);
