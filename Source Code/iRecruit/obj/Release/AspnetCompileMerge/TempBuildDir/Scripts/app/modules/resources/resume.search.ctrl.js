angular.module('irecruit.resources')
.factory('PageData', function () {
    var PageData = function () {
        this.SearchType = "Simple";
        this.PageNo = 0;
        this.PageSize = 10;
        this.SortColumn = 'FirstName';
        this.SortDirection = 'ASC';
        this.busy = true;
        this.totalReached = true;
        this.after = '';
        this.items = [];
        this.searchParams = {};
        this.TotalItems = 0;
        this.SortCriteria = {};
    };
    return PageData;
})
.controller('ResumeSearchController', ['PageData', '$q', '$scope', 'profileService', 'commonService', 'persistService', '$state', '$stateParams', '$timeout', 
    function (PageData, $q, $scope, profileService, commonService, persistService, $state, $stateParams, $timeout) {
    $scope.SortCriterias = [
                { SortColumn: 'FirstName', SortDirection: 'ASC', Label: 'First Name A-Z' },
                { SortColumn: 'FirstName', SortDirection: 'DESC', Label: 'First Name Z-A' },
                { SortColumn: 'MinExperience', SortDirection: 'ASC', Label: 'Minimum Exp. Low to High' },
                { SortColumn: 'MinExperience', SortDirection: 'DESC', Label: 'Minimum Exp. High to Low' }
    ];

    
    $q.all([
        commonService.getTypes(),
        profileService.getConsultancies()

    ]).then(function (result) {
        var tmp = [];
        angular.forEach(result, function (response) {
            tmp.push(response);
        });
        return tmp;
    }).then(function (data) {
        $scope.data.types = data[0];
        persistService.setData($scope.data);
        $scope.resume_types = $.grep($scope.data.types, function (n, i) {
            return (n.TypeClassID == 10);
        });
        $scope.cand_status_types = $.grep($scope.data.types, function (n, i) {
            return (n.TypeClassID == 8);
        });
        $scope.consultancies = data[1];

    }).catch(function error(err) {
        $.toas(err, '', "error");
    });

    $scope.pageData = new PageData();
    $scope.$on('$stateChangeSuccess', function () {
        //handleAjaxMessages();
        var d = persistService.getResumeSearchPageData();
        if (d) {
            $timeout(function () {
                $scope.pageData = d;
            });
        }
        displayMessages();
    });
    $scope.$on('$stateChangeStart', function () {
        persistService.setResumeSearchPageData($scope.pageData);
    });

    $scope.nextPage = function () {
        if ($scope.pageData.searchParams) {
            if ($scope.pageData.busy) return;
            if ($scope.pageData.items && $scope.pageData.items.length > 0 && $scope.pageData.items.length >= $scope.pageData.TotalItems) $scope.pageData.totalReached = true;
            if (!$scope.pageData.totalReached) {
                $scope.pageData.PageNo = $scope.pageData.PageNo + 1;
                $scope.pageData.busy = true;
                var request = angular.copy($scope.pageData.searchParams);
                if ($scope.pageData.searchParams.ResumeStatus) {
                    request.CandidateStatus = $scope.pageData.searchParams.ResumeStatus.Name;
                }
                if ($scope.pageData.searchParams.ResumeSource) {
                    request.ResumeSourceDetail = $scope.pageData.searchParams.ResumeSource.ConsultancyName;
                }
                request.SearchType = $scope.pageData.SearchType;
                request.PageNo = $scope.pageData.PageNo;
                request.PageSize = $scope.pageData.PageSize;
                if ($scope.pageData.SortCriteria != null) {
                    request.SortColumn = $scope.pageData.SortCriteria.SortColumn;
                    request.SortDirection = $scope.pageData.SortCriteria.SortDirection;
                }
                else {
                    request.SortColumn = $scope.pageData.SortColumn;
                    request.SortDirection = $scope.pageData.SortDirection;
                }
                return profileService.search(request)
                .then(function (response) {
                    for (var i = 0; i < response.length; i++) {
                        $scope.pageData.items.push(response[i]);
                        $scope.pageData.TotalItems = response[i].TotalCount;
                    }
                    if (response.length > 0) {
                        $scope.pageData.after = "t3_" + $scope.pageData.items[$scope.pageData.items.length - 1].id;
                    }

                    $scope.pageData.busy = false;

                }, function (err) {
                    $scope.pageData.busy = false;
                    $.toas(err.data, '', "error");
                });
            }
        }
    };
    $scope.doSearch = function () {
        //if (i_form1.$valid) {
        $scope.pageData.items = [];
        $scope.pageData.PageNo = 0;
        $scope.pageData.busy = false;
        $scope.pageData.totalReached = false;
        $scope.pageData.after = '';
        $scope.pageData.TotalItems = 0;
        $scope.nextPage();
        //}
    };
    $scope.downloadResume = function (path) {
        window.open(path, '_blank', '');
        
    }
    $scope.exportCsv = function () {
        if ($scope.pageData.items.length == 0) return;
        ConvertJsonToCsv($scope.pageData.items, "Search Results", true);
    };
    
    
    $scope.cancelForm = function () {
        $scope.pageData.searchParams = {};
    }
}]);

