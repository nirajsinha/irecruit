angular.module('irecruit.resources')
.factory('profileService', ['$http', 'persistService', function ($http, persistService) {
    var api = {};
    api.getConsultancies = function (companyId) {
        return $http.get('/api/consultants/0')
        .then(function (response) {
            return response.data;
        });
    };
    api.getCandidate = function (candId) {
        return $http.get('/api/candidate/' + candId)
        .then(function (response) {
            return response.data;
        });
    };
    api.saveCandidate = function (request) {
        return $http.post('/api/candidate', request)
        .then(function (response) {
            return response.data;
        });
    };
    api.getInterviewSchedule = function (candId, round) {
        if (!round) round = 1;
        return $http.get('/api/InterviewSchedule/' + candId + '?round=' + round)
        .then(function (response) {
            return response.data;
        });
    };
    api.saveInterviewSchedule = function (request) {
        return $http.post('/api/InterviewSchedule', request)
        .then(function (response) {
            return response.data;
        });
    };
    api.getInterviewFeedback = function (candId) {
        return $http.get('/api/interviewfeedbacks/' + candId)
        .then(function (response) {
            return response.data;
        });
    };
    api.saveInterviewFeedback = function (feedback) {
        return $http.post('/api/interviewfeedbacks', feedback)
        .then(function (response) {
            return response.data;
        });
    };

    api.getResume = function (candId) {
        return $http.get('/api/resume/' + candId)
        .then(function (response) {
            return response.data;
        });
    };
    
    api.search = function (request) {
        return $http.post('/api/resume', request)
        .then(function (response) {
            return response.data;
        });
    };
    return api;
}]);
