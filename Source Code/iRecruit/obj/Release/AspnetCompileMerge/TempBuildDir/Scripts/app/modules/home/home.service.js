angular.module('irecruit.home')
.factory('homeService', ['$http', 'persistService', function ($http, persistService) {
    var api = {};
    api.getOpenPositions = function (companyId) {
        return $http.get('/api/OpenPositions/' + companyId)
        .then(function( response){
            return response.data;
        });
    };
    api.getOfferJoiningRatio = function (companyId, isBranchwise) {
        return $http.get('/api/OfferJoiningRatio/' + companyId + '?b=' + isBranchwise)
        .then(function (response) {
            return response.data;
        });
    };
    api.getTopOpenings = function (companyId) {
        return $http.get('/api/topopenings/' + companyId)
        .then(function (response) {
            return response.data;
        });
    };
    api.getResumeSources = function (companyId) {
        return $http.get('/api/ResumeSources/' + companyId)
        .then(function (response) {
            return response.data;
        });
    }; 
    return api;
}]);