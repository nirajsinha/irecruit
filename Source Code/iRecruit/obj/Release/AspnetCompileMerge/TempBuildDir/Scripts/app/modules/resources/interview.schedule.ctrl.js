angular.module('irecruit.resources')
.controller('InterviewScheduleController', ['$q', '$scope', 'persistService', 'commonService', 'profileService', '$state', '$stateParams', '$timeout', 
    function ($q, $scope, persistService, commonService, profileService, $state, $stateParams, $timeout) {
        $scope.interview_panel = [];
    $q.all([
       commonService.getInterviewPanel($scope.data.UserInfo.CompanyID)
    ]).then(function (result) {
        var tmp = [];
        angular.forEach(result, function (response) {
            tmp.push(response);
        });
        return tmp;
    }).then(function (data) {
        $scope.interview_panel = data[0];
        $scope.data.interviewer_level1 = jQuery.grep(data[0], function (n, i) {
            return (n.Level == 1);
        });
        $scope.data.interviewer_level2 = jQuery.grep(data[0], function (n, i) {
            return (n.Level == 2);
        });
        persistService.setData($scope.data);
    }).catch(function error(err) {
        $.toas(err, '', "error");
    });
    $scope.cand = {};
    $scope.executeSearch = function (e) {

        if (e.keyCode == 13) {
            $state.go('interviewschedule', { CandidateID: $scope.cand.CandidateID });
        }
    }
    $scope.loadCandidate = function () {
        profileService.getInterviewSchedule($scope.cand.CandidateID, $scope.cand.InverviewRound)
        .then(function (d) {
            $scope.cand = d;
            // set access features
            var arr = d.ScheduledInterviewers.split(',');
            var temp = [];
            for (var i = 0; i < arr.length; i++) {
                $.each($scope.interview_panel, function (index, item) {
                    if (arr[i] == item.Name) {
                        temp.push(item);
                        $('#interviewers option:contains("' + arr[i] + '")').prop("selected", true);
                    }
                });
            }
            $scope.interviewers = temp;
        },
        function (error) {
            $.toas(error.data, '', "error");
        });
    };
    var candId = $stateParams.CandidateID;
    if (candId && candId != '') {
        $scope.cand.CandidateID = candId;
        $scope.loadCandidate();
    }
    $scope.$formPristine = true;
    $scope.$on('$stateChangeSuccess', function () {
        //handleAjaxMessages();
        displayMessages();
    });
    
    $scope.submitForm = function (form) {
        $scope.DateError = false;
        if ($scope.cand.StartTime < new Date() || $scope.cand.EndTime < new Date() || $scope.cand.EndTime < $scope.cand.StartTime) {
            $scope.DateError = true;
            $scope.$formPristine = false;
            return;
        }
        if (form.$valid) {
            $scope.$formPristine = true;
            var interviewers = [];
            $.each($scope.interviewers, function (index, item) {
                interviewers.push(item.Name);
            });
            $scope.cand.ScheduledInterviewers = interviewers.join();
            profileService.saveInterviewSchedule($scope.cand)
            .then(function (response) {
                $scope.cand = {};
                $.toas('Interview scheduled.', '', "success");
            },
            function (error) {
                $.toas(error.data, '', "error");
            });
        }
        else {
            
            $scope.$formPristine = false;
        }
    };
    
    $scope.resetForm = function () {
        //$scope.cand = {};
        $state.go('interviewschedule', { CandidateID: null});
    };

        // date pickers
    $scope.dateTimeNow = function () {
        $scope.date = new Date();
    };
    $scope.dateTimeNow();
    $scope.toggleMinDate = function () {
        $scope.minDate = $scope.minDate ? null : new Date();
    };
    $scope.toggleMinDate();
    $scope.dateOptions = {
        showWeeks: false,
        'year-format': "'yy'",
        'starting-day': 1
    };
    // Disable weekend selection
    $scope.disabled = function (calendarDate, mode) {
        return mode === 'day' && (calendarDate.getDay() === 0 || calendarDate.getDay() === 6);
    };

    $scope.hourStep = 1;
    $scope.minuteStep = 15;

    $scope.timeOptions = {
        hourStep: [1, 2, 3],
        minuteStep: [1, 5, 10, 15, 25, 30]
    };

    $scope.showMeridian = true;
    $scope.timeToggleMode = function () {
        $scope.showMeridian = !$scope.showMeridian;
    };
}]);






