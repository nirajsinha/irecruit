angular.module('irecruit.settings')
.controller('DepartmentController', ['$scope', 'settingsService', 'persistService','commonService', '$state', '$stateParams', '$q', 
    function ($scope, settingsService, persistService,commonService, $state, $stateParams, $q) {
    $scope.dept = {};
    $scope.department_roles = [];
    $scope.$formPristine = false;
    $scope.users = [];
    $scope.searchUser = function (val) {
        return $.grep($scope.users, function (a) {
            
            return a.value.toLowerCase().indexOf(val.toLowerCase()) >= 0 || a.label.toLowerCase().indexOf(val.toLowerCase()) >= 0;
        });
    };
    $scope.onFunctionHeadSelected = function (item, model, label) {
        $scope.dept.FunctionHead = item.value;
        $scope.dept.FunctionHeadLabel = item.label;
    }

    $scope.onSVPSelected = function (item, model, label) {
        $scope.dept.SVP = item.value;
        $scope.dept.SVPLabel = item.label;
    }
    $scope.$on('$stateChangeSuccess', function () {
        $q.all([
           commonService.getBranches($scope.data.UserInfo.CompanyID),
           commonService.getDepartments($scope.data.UserInfo.CompanyID),
           settingsService.getDepartmentRoles(),
           commonService.searchUser($scope.data.UserInfo.CompanyID, '')
        ]).then(function (result) {
            var tmp = [];
            angular.forEach(result, function (response) {
                tmp.push(response);
            });
            return tmp;
        }).then(function (data) {
            $scope.data.branches = data[0];
            $scope.data.departments = data[1];
            persistService.setData($scope.data);
            $scope.users = data[3];
            $.each(data[2], function (index, it) {
                var temp = {};

                temp.DepartmentRoleID = it.DepartmentRoleID;
                temp.Active = it.Active;
                temp.FunctionHead = it.FunctionHead;
                temp.SVP = it.SVP;

                var fhlabel = $.grep($scope.users, function (n, i) { // just use arr
                    return n.value == it.FunctionHead;
                });
                temp.FunctionHeadLabel = fhlabel[0].label;

                var svplabel = $.grep($scope.users, function (n, i) { // just use arr
                    return n.value == it.SVP;
                });
                temp.SVPLabel = svplabel[0].label;

                var tba = $.grep($scope.data.branches, function (n, i) { // just use arr
                    return n.BranchID == it.BranchID;
                });
                temp.branch = tba[0];

                var bba = $.grep($scope.data.departments, function (n, i) { // just use arr
                    return n.DepartmentID == it.DepartmentID;
                });
                temp.dept = bba[0];
                $scope.department_roles.push(temp);

            });
            
        });
       
        displayMessages();
    });

    $scope.submitForm = function (isValid) {
        // check to make sure the form is completely valid
        if (isValid) {
            $scope.$formPristine = false;
            var success = settingsService.saveDepartmentRoles($scope.dept);
            success.then(function () {
                settingsService.getDepartmentRoles()
                .then(function (data) {
                    $scope.department_roles = [];
                    $.each(data, function (index, it) {
                        var temp = {};
                        temp.DepartmentRoleID = it.DepartmentRoleID;
                        temp.Active = it.Active;
                        temp.FunctionHead = it.FunctionHead;
                        temp.SVP = it.SVP;

                        var fhlabel = $.grep($scope.users, function (n, i) { // just use arr
                            return n.value == it.FunctionHead;
                        });
                        temp.FunctionHeadLabel = fhlabel[0].label;

                        var svplabel = $.grep($scope.users, function (n, i) { // just use arr
                            return n.value == it.SVP;
                        });
                        temp.SVPLabel = svplabel[0].label;

                        var tba = $.grep($scope.data.branches, function (n, i) { // just use arr
                            return n.BranchID == it.BranchID;
                        });
                        temp.branch = tba[0];

                        var bba = $.grep($scope.data.departments, function (n, i) { // just use arr
                            return n.DepartmentID == it.DepartmentID;
                        });
                        temp.dept = bba[0];
                        $scope.department_roles.push(temp);

                    });
                });

                $scope.dept = {};
                $.toas('Department-Roles assigned successfully', '', "success");
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
        //$scope.dept = $scope.department_roles[index];
        $scope.dept = angular.copy($scope.department_roles[index]);
        var tba = $.grep($scope.data.branches, function (n, i) { // just use arr
            return n.BranchID == $scope.dept.branch.BranchID;
        });
        $scope.dept.branch = tba[0];

        var bba = $.grep($scope.data.departments, function (n, i) { // just use arr
            return n.DepartmentID == $scope.dept.dept.DepartmentID;
        });
        $scope.dept.dept = bba[0];
    };
}]);