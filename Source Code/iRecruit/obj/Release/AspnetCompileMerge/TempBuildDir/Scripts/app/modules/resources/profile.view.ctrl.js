angular.module('irecruit.resources')
.controller('ViewProfileController', ['$q', '$scope', 'persistService', 'commonService', 'indentService', 'profileService', '$state', '$stateParams', 'dialogs',
    function ($q, $scope, persistService, commonService, indentService, profileService, $state, $stateParams, dialogs) {
    var candId = $stateParams.CandidateID;
    $scope.cand = {};

    if (candId && candId != '') {
        $q.all([
           profileService.getCandidate(candId),
           profileService.getResume(candId)
        ]).then(function (result) {
            var tmp = [];
            angular.forEach(result, function (response) {
                tmp.push(response);
            });
            return tmp;
        }).then(function (data) {
            $scope.cand = data[0].Candidate;
            $scope.cand.ResumeFileName = data[1].ResumeFileName;
            $scope.cand.ResumeVirtualPath = data[1].ResumeVirtualPath;
        }).catch(function error(err) {
            $.toas(err, '', "error");
        });

    }
    $scope.searchCandidates = function (val) {
        var request = { 'SearchType': 'Simple', 'PageNo': 1, 'PageSize': 200, 'SortColumn': 'FirstName', 'SortDirection': 'ASC', 'search': val };
        return profileService.search(request)
        .then(function (response) {
            $scope.loadingIndent = false;
            return response;
        });
    };
    $scope.onCandidateSelected = function (item, model, label) {
        $state.transitionTo("profile", { CandidateID: item.CandidateID }, { notify: true });
    }
    $scope.$on('$stateChangeSuccess', function () {
        //handleAjaxMessages();
        displayMessages();
        
    });
    $scope.downloadResume = function () {
        window.open($scope.cand.ResumeVirtualPath, '_blank', '');

    }

}]);






