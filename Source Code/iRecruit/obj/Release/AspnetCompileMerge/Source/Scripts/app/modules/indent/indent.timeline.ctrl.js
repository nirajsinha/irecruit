angular.module('irecruit.indent')
.controller('IndentTimelineController', ['$scope', 'commonService', 'indentService', '$state', '$stateParams', function ($scope, commonService, indentService, $state, $stateParams) {
    $scope.selectedIndent = "";
    $scope.logs = [];
    $scope.searchIndents = function (val) {
        return indentService.searchIndents($scope.data.UserInfo.CompanyID, val)
        .then(function (response) {
            return response;
        });
    };
    $scope.onIndentSelected = function (item, model, label) {
        $scope.selectedIndent = item.value;
        var promise = indentService.getActivities($scope.selectedIndent);
        promise.then(function (data) {
            $scope.logs = data.Items;
        });
    }

    $scope.$on('$stateChangeSuccess', function () {
        
        displayMessages();
    });

}]);