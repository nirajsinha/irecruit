angular.module('irecruit.indent')
.factory('indentService', ['$http', 'persistService', function ($http, persistService) {
    var api = {};
    api.getIndents = function (companyId) {
        return $http.get('/api/indent')
        .then(function (response) {
            return response.data;
        });
    };
    api.getIndent = function (indentNumber) {
        return $http.get('/api/indent/' + indentNumber)
        .then(function (response) {
            return response.data;
        });
    };
    api.getIndentTrackerInfo = function (indentNumber) {
        return $http.get('/api/indenttracker/' + indentNumber)
        .then(function (response) {
            return response.data;
        });
    };
    api.searchIndents = function (companyId, query) {
        return $http.get('/api/indentsearch/' + companyId + '?search=' + query + '&type=')
        .then(function (response) {
            return response.data;
        });
    };
    api.searchApprovedIndents = function (companyId, query) {
        return $http.get('/api/indentsearch/' + companyId + '?search=' + query + '&type=3')
        .then(function (response) {
            return response.data;
        });
    };
    api.getIndentTrackerList = function (companyId, page, pageSize) {
        return $http.get('/api/indenttracker/' + companyId + '?page=' + page + '&pageSize=' + pageSize)
        .then(function (response) {
            return response.data;
        });
    };
    api.getActivities = function (indentNumber) {
        return $http.get('/api/indentactivities/' + indentNumber)
        .then(function (response) {
            return response.data;
        });
    };
    api.saveIndent = function (request) {
        return $http.post('/api/indent', request)
        .then(function (response) {
            return response.data;
        });
    };
    return api;
}]);