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
        $state.transitionTo("timeline", { 'indentNumber': item.value }, { notify: true });
    }
    $scope.getActivities = function (indentNo) {
        $scope.selectedIndent = indentNo;
        var promise = indentService.getActivities($scope.selectedIndent);
        promise.then(function (data) {
            $scope.logs = data.Items;
        });
    }
    var indentNo = $stateParams.indentNumber;
    if (indentNo && indentNo != '') {
        $scope.getActivities(indentNo);
    }
    

    $scope.$on('$stateChangeSuccess', function () {
        
        displayMessages();
    });

}]);