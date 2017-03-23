angular.module('irecruit.settings')
.controller('SkillsController', ['$scope', 'commonService', 'settingsService', 'persistService', '$state', '$stateParams', function ($scope, commonService, settingsService, persistService, $state, $stateParams) {
    $scope.ts = {};
    $scope.$on('$stateChangeSuccess', function () {
        if (!$scope.data.technologies) {
            var p = commonService.getTechnologies($scope.data.UserInfo.CompanyID);
            p.then(function (response) {
                $scope.data.technologies = response;
                persistService.setData($scope.data);
            }, function (error) {
                $.toas(error.data, '', "error");
            });
        }
        displayMessages();
    });
    $scope.$formPristine = false;

    $scope.submitForm = function (isValid) {
        if (isValid) {
            $scope.$formPristine = false;
            var success = settingsService.saveSkills($scope.ts);
            success.then(function () {
                var tba = $.grep($scope.data.technologies, function (n, i) { // just use arr
                    return n.TechnologyAndSkillID == $scope.ts.TechnologyAndSkillID;
                });
                if (tba.length == 0) {
                    $scope.data.technologies.push($scope.ts);
                }
                $scope.ts = {};
                $.toas('Skills added successfully', '', "success");
            },
            function (error) {
                $.toas(error.data, '', "error");
            });
        }
        else {
            $scope.$formPristine = true;
        }

    };

    $scope.selectData = function (index) {
        $scope.ts = $scope.data.technologies[index];
    };
}]);