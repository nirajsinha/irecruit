angular.module('irecruit.settings')
.directive('validAdUser', ['$q', '$timeout', 'commonService', function ($q, $timeout, commonService) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ngModel) {
            elm.on('blur', function () {
                ngModel.$setValidity('validAdUser', false);
                var modelValue = ngModel.$modelValue;
                $q.all([
                    commonService.getUserDetailsFromAD(modelValue)
                ])
                .then(function (results) {
                    ngModel.$setValidity('validAdUser', true);
                }, function (errors) {
                    ngModel.$setValidity('validAdUser', false);
                });
            });
        }
    };
}])
.controller('UserController', ['$scope', 'commonService', 'persistService', 'settingsService', '$state', '$stateParams', function ($scope, commonService, persistService, settingsService, $state, $stateParams) {
    $scope.userid = '';
    $scope.username = '';
    $scope.photo = '';
    $scope.brans = null;
    $scope.feature = null;
    $scope.$formPristine = false;
    // setup autocomplete function pulling from currencies[] array
    $scope.searchUser = function (val) {
        return commonService.searchUser($scope.data.UserInfo.CompanyID, val)
        .then(function (response) {
            if (response.length) {
                $scope.admessage = "";
            }
            return response;
        });
    };
    $scope.onUserSelected = function (item, model, label) {
        commonService.getUserDetails(item.value)
        .then(function (result) {
            if (result) {
                $scope.userid = result.UserID;
                $scope.username = result.Name;
                $scope.title = result.Title;
                $scope.email = result.Email;
                if (result.Photo) $scope.photo = result.Photo;
                // set branches
                var arr_branches = result.Branches.split(',');
                var temp_branches = [];
                for (var i = 0; i < arr_branches.length; i++) {
                    $.each($scope.data.branches, function (index, item) {
                        if (arr_branches[i] == item.Code) {
                            temp_branches.push(item);
                            $('#brans option:contains("' + arr_branches[i] + '")').prop("selected", true);
                        }
                    });

                }
                $scope.brans = temp_branches;

                // set access features
                var arr = result.AccessFeatures.split(',');
                var temp = [];
                for (var i = 0; i < arr.length; i++) {
                    $.each($scope.features, function (index, item) {
                        if (arr[i] == item.Code) {
                            temp.push(item);
                            $('#feature option:contains("' + arr[i] + '")').prop("selected", true);
                        }
                    });
                }
                $scope.feature = temp;
                //$("#feature").trigger("chosen:updated");
            }
        }, function (error) {
            $.toas(error.data, '', "error");
        });
    }
    $scope.searchADUser = function (val) {
        return commonService.getUserList(val)
        .then(function (data) {
            if (data) {
                $scope.admessage = "";
                return data;
            }
            else {
                $scope.admessage = "UserID does not exists or invalid.";
            }
        }, function (error) {
            $.toas(error.data, '', "error");
        });
    };
    $scope.onADUserSelected = function (item, model, label) {
        return commonService.getUserDetailsFromAD(item.value)
        .then(function (result) {
            if (result) {
                $scope.userid = result.UserID;
                $scope.username = result.Name;
                $scope.title = result.Title;
                $scope.email = result.Email;
                $scope.photo = result.Photo;
                $scope.brans = null;
                $scope.feature = null;
            }
        }, function (error) {
            $.toas(error.data, '', "error");
        });
    }

    $scope.$on('$stateChangeSuccess', function () {
        var p = commonService.getFeatures($scope.data.UserInfo.CompanyID);
        p.then(function (data) {
            $scope.features = data;
        });
        if (!$scope.data.branches) {
            var p = commonService.getBranches($scope.data.UserInfo.CompanyID);
            p.then(function (response) {
                $scope.data.branches = response;
                persistService.setData($scope.data);
            }, function (error) {
                $.toas(error.data, '', "error");
            });
        }
        
        //handleAjaxMessages();
        displayMessages();

    });
    $scope.submitForm = function (isValid) {
        // check to make sure the form is completely valid
        if (isValid) {
            $scope.$formPristine = false;
            var branches = [];
            $.each($scope.brans, function (index, item) {
                branches.push(item.Code);
            });
            var accessfeatures = [];
            $.each($scope.feature, function (index, item) {
                accessfeatures.push(item.Code);
            });
            var success = settingsService.saveUser($scope.data.UserInfo.CompanyID, $scope.userid, $scope.username, $scope.title, $scope.email, $scope.photo, branches.join(), accessfeatures.join());
            success.then(function () {
                $scope.userid = '';
                $scope.username = '';
                $scope.title = '';
                $scope.email = '';
                $scope.photo = '';
                $scope.brans = null;
                $scope.feature = null;
                $.toas('User addedd successfully', '', "success");
            },
            function (err) {
                $.toas(err.data, '', "error");
            });
        }
        else {
            $scope.$formPristine = true;
        }

    };
}]);
