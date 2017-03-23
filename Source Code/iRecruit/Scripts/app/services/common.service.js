angular.module('irecruit.services')
.factory('commonService', ['$http', 'persistService', function ($http, persistService) {
    
    var api = {};
    api.getMasterData = function (companyId) {
        var data = persistService.getData();
        if (!data) {
            data = {};
            var promise = $http.get('/api/masterdata/' + companyId);
            promise.then(function (response) {
                var d = response.data;
                data.technologies = d.TechnologyAndSkills;
                data.branches = d.Branches;
                data.departments = d.Departments;
                var panel_level1 = jQuery.grep(d.InterviewPanel, function (n, i) {
                    return (n.Level == 1);
                });
                var panel_level2 = jQuery.grep(d.InterviewPanel, function (n, i) {
                    return (n.Level == 2);
                });
                data.interviewer_level1 = panel_level1;
                data.interviewer_level2 = panel_level2;
                data.types = d.Types;
            });
        }
        return data;
    };
    api.getFeatures = function (companyId) {
        return $http.get('/api/features/' + companyId)
        .then(function (result) {
            return result.data;
        });
        
    };
    api.getTechnologies = function (companyId) {
        return $http.get('/api/skills/' + companyId)
        .then(function (result) {
            return result.data;
        });
    };
    api.getBranches = function (companyId) {
        return $http.get('/api/branches/' + companyId)
        .then(function (result) {
            return result.data;
        });
        
    };
    api.getDepartments = function (companyId) {
        return $http.get('/api/departments/' + companyId)
        .then(function (result) {
            return result.data;
        });
    };
    api.getInterviewPanel = function (companyId) {
        return $http.get('/api/interviewpanel/' + companyId)
        .then(function (result) {
            return result.data;
        });
    };
    api.getTypes = function () {
        return $http.get('/api/types')
        .then(function (result) {
            return result.data;
        });
    };
    api.getUserList = function (search) {
        return $http.post('/api/aduser/' + search)
        .then(function (result) {
            return result.data;
        });
    };
    api.getUserDetails = function (userid) {
        return $http.get('/api/user/' + userid)
        .then(function (result) {
            return result.data;
        });
    };
    api.getUserDetailsFromAD = function (userid) {
        return $http.get('/api/aduser/' + userid)
        .then(function (result) {
            return result.data;
        }, function (error) {
            return null;
        });
    }; 
    api.searchUser = function (compid, search) {
        return $http.get('/api/usersearch/' + compid + '?search=' + search)
        .then(function (result) {
            return result.data;
        });
        
    };
    api.getUserAccessFeatures = function (userid) {
        return $http.get('/api/accessfeatures/' + userid)
        .then(function (result) {
            return result.data;
        });
    };
    return api;
}]);