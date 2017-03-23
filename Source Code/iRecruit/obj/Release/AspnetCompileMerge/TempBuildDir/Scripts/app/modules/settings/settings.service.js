angular.module('irecruit.settings')
.factory('settingsService', ['$q', '$http', function ($q, $http) {
    var api = {};
    api.getDepartmentRoles = function () {
        var deferred = $q.defer();
        $http.get('/api/departmentroles/0')
          .success(function (data) {
              deferred.resolve(data);
          });
        return deferred.promise;
    };
    api.saveDepartmentRoles = function (dept) {
        var request = $.parseJSON(JSON.stringify({
            "DepartmentRoleID": dept.DepartmentRoleID,
            "DepartmentID": dept.dept.DepartmentID,
            "BranchID": dept.branch.BranchID,
            "FunctionHead": dept.FunctionHead,
            "SVP": dept.SVP
        }));
        return $http.post('/api/departmentroles', request )
        .then(function (response) {
            return res = response.data;
        });
        
    };
    api.saveSkills = function (skills) {
        var request = $.parseJSON(JSON.stringify({
            "TechnologyAndSkillID": skills.TechnologyAndSkillID,
            "Code": skills.Code,
            "SkillType": skills.SkillType,
            "Name": skills.Name,
            "CompanyID": $scope.data.UserInfo.CompanyID
        }));

        return $http.post('/api/skills', request )
        .then(function (response) {
            return response.data;
        });
        
    };
    api.saveUser = function (companyId, uid, name, title, email, photo, depts, features) {
        var request = $.parseJSON(JSON.stringify({
            "UserID": uid,
            "CompanyID": companyId,
            "Name": name,
            "Title": title,
            "Email": email,
            "Photo":photo,
            "Branches": depts,
            "AccessFeatures": features
        }));
        return $http.post('/api/user', request )
        .then(function (response) {
            return response.data;
        });
        
    };
    return api;
}]);