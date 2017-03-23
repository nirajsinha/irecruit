angular.module('irecruit.resources')
.controller('EditProfileController', ['$q', '$scope', 'persistService', 'commonService', 'indentService', 'profileService', '$state', '$stateParams', '$timeout', 'dialogs',
    function ($q, $scope, persistService, commonService, indentService, profileService, $state, $stateParams, $timeout, dialogs) {
    $q.all([
       commonService.getTypes(),
       profileService.getConsultancies()

    ]).then(function (result) {
        var tmp = [];
        angular.forEach(result, function (response) {
            tmp.push(response);
        });
        return tmp;
    }).then(function (data) {
        $scope.data.types = data[0];
        persistService.setData($scope.data);
        $scope.resume_types = $.grep($scope.data.types, function (n, i) {
            return (n.TypeClassID == 10);
        });
        $scope.cand_status_types = $.grep($scope.data.types, function (n, i) {
            return (n.TypeClassID == 8);
        });
        $scope.consultancies = data[1];

    }).catch(function error(err) {
        $.toas(err, '', "error");
    });
    $scope.cand = {};
    
    var candId = $stateParams.CandidateID;
    if (candId && candId != '') {
        profileService.getCandidate(candId)
        .then(function (d) {
            $scope.cand = d.Candidate;
            $timeout(function () {
                if ($scope.cand.ResumeSourceTypeID == 35) {
                    var i = $.map($scope.consultancies, function (obj, index) {
                        if (obj.ConsultancyName == $scope.cand.ResumeSourceDetail) return index;
                    })[0];
                    $scope.cand.ResumeSource = $scope.consultancies[i];
                }
                var s = $.map($scope.data.types, function (obj, index) {
                    if (obj.TypeID == $scope.cand.CandidateStatusTypeID) return index;
                })[0];
                $scope.cand.CandidateStatus = $scope.data.types[s];
            });
        },
        function (error) {
            $.toas(error.data, '', "error");
        });
    }

    $scope.$formPristine = true;
    $scope.searchIndents = function (val) {
        $scope.loadingIndent = true;
        return indentService.searchApprovedIndents($scope.data.UserInfo.CompanyID, val)
        .then(function (response) {
            $scope.loadingIndent = false;
            return response;
        });
    };
    $scope.onIndentSelected = function (item, model, label) {
        $scope.cand.IndentNumber = item.value;
    }

    $scope.$on('$stateChangeSuccess', function () {
        //handleAjaxMessages();
        displayMessages();
    });
    $scope.$on("fileSelected", function (event, args) {
        $('#i_form1').ajaxSubmit({
            url: '/api/upload/' + $scope.data.UserInfo.UserID,
            type: 'POST',
            dataType: 'text',
            success: function (response, status, xhr, $form) {
                $scope.$apply(function () {
                    $scope.cand.ResumePath = response;
                });
            },
            error: function (xhr, status, error) {
                $.toas(error, '', "error");
            }
        });

    });
    $scope.downloadResume = function () {
        window.open($scope.cand.ResumeVirtualPath, '_blank', '');
    }
    $scope.submitForm = function (form) {
        if (form.$valid) {
            $scope.$formPristine = true;
            $scope.cand.DOB = $scope.getDateString($scope.cand.DOB);
            if ($scope.cand.ResumeSourceTypeID == 35) $scope.cand.ResumeSourceDetail = $scope.cand.ResumeSource.ConsultancyName;
            $scope.cand.CandidateStatusTypeID = $scope.cand.CandidateStatus.TypeID;
            var request = { Candidate: $scope.cand, Resume: { ResumePath: $scope.cand.ResumePath } };
            profileService.saveCandidate(request)
            .then(function (response) {
                $scope.cand = {};
                $.toas('Candidate added successfully.', '', "success");
            },
            function (error) {
                $.toas(error.data, '', "error");
            });
        }
        else {
            $scope.$formPristine = false;
        }
    };
    $scope.cancelForm = function () {
        var dlg = dialogs.confirm('Please Confirm', 'This action will cancel all inputs and you will lose all unsaved data. <br />Do you want to continue?', { size: 'md' });
        dlg.result.then(function (btn) {
            $scope.cand = {};
        }, function (btn) {
            //alert('No clicked');
        });
    }
    $scope.resetForm = function () {
        //$scope.cand = {};
        $state.go('candidate', { CandidateID: null});
    };
    // date pickers
    $scope.datepickers = {
        dtDob: false
    }
    
    $scope.open = function ($event, which) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.datepickers[which] = true;
    };
    $scope.dateOptions = {
        'year-format': "'yy'",
        'starting-day': 1
    };
}]);






