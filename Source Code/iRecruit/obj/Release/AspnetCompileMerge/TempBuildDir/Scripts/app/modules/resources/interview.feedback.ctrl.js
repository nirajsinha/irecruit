angular.module('irecruit.resources')
.factory('FeedbackPageData', function () {
    var FeedbackPageData = function () {
        this.InterviewRound = 1;
        this.CandidateName = '';
        this.TotalExperience = '';
        this.PositionFor = '';
        this.feedback = {};
        this.Round1Feedback = {};

    };
    return FeedbackPageData;
})
.controller('InterviewFeedbackController', ['FeedbackPageData', '$q', '$scope', 'persistService', 'commonService', 'profileService', '$state', '$stateParams', '$modal', 'dialogs',
    function (FeedbackPageData, $q, $scope, persistService, commonService, profileService, $state, $stateParams, $modal, dialogs) {
        $scope.pageData = new FeedbackPageData();

    $q.all([
        commonService.getTypes()
    ]).then(function (result) {
        var tmp = [];
        angular.forEach(result, function (response) {
            tmp.push(response);
        });
        return tmp;
    }).then(function (data) {
        $scope.data.types = data[0];
        persistService.setData($scope.data);
        $scope.rating_interpretations = $.grep($scope.data.types, function (n, i) {
            return (n.TypeClassID == 11);
        });
    }).catch(function error(err) {
        $.toas(err, '', "error");
    });
    $scope.$watchCollection('[pageData.feedback.Status]', function (newValues, oldValues) {
        var disableFeedback = newValues[0] == "1";
        $("#i_form1 :input").attr("disabled", disableFeedback);
        
        $("#btnsave").attr("disabled", disableFeedback);
        $("#btnsubmit").attr("disabled", disableFeedback);
        $("#btncancel").attr("disabled", disableFeedback);
        $("#round1feedback").removeAttr("disabled");
        $("#btn_interpretations").removeAttr("disabled");
    });
    $scope.executeSearch = function (e)
    {
        if (e.keyCode == 13) $scope.loadCandidate();
    }
    $scope.loadCandidate = function () {
        if ($scope.pageData.CandidateID && $scope.pageData.CandidateID != '') {
            profileService.getInterviewFeedback($scope.pageData.CandidateID)
            .then(function (d) {
                $scope.pageData.CandidateName = d.CandidateName;
                $scope.pageData.TotalExperience = d.TotalExperience;
                $scope.pageData.Round1Feedback = d.FeedbackRound1;
                // no interview happened till now
                if (d.FeedbackRound1 == null) {
                    $scope.pageData.InterviewRound = 1;
                    $scope.pageData.feedback = {};
                    $scope.pageData.feedback.PositionFor = d.CurrentPosition;
                    $scope.pageData.feedback.ReleventExperience = d.TotalExperience;
                }
                else if (d.FeedbackRound1.Status == 0) // first round interview happend but not submitted
                {
                    $scope.pageData.InterviewRound = 1;
                    $scope.pageData.feedback = d.FeedbackRound1;
                }
                else if (d.FeedbackRound2 == null) { // first round completed, second not happened
                    $scope.pageData.Round1Feedback = d.FeedbackRound1;
                    $scope.pageData.InterviewRound = 2;
                    $scope.pageData.feedback = {};
                    $scope.pageData.feedback.PositionFor = d.FeedbackRound1.PositionFor;
                    $scope.pageData.feedback.ReleventExperience = d.FeedbackRound1.ReleventExperience;
                    $scope.pageData.feedback.ReleventExperienceDiscountReason = d.FeedbackRound1.ReleventExperienceDiscountReason;
                    $scope.pageData.feedback.PositionSuggested = d.FeedbackRound1.PositionSuggested;
                }
                else // second round interview happend but not submitted
                {
                    $scope.pageData.Round1Feedback = d.FeedbackRound1;
                    $scope.pageData.InterviewRound = 2;
                    $scope.pageData.feedback = d.FeedbackRound2;
                }
                
                $scope.pageData.feedback.CandidateID = $scope.pageData.CandidateID;
                //$timeout(function () {
                //    if ($scope.cand.ResumeSourceTypeID == 35) {
                //        var i = $.map($scope.consultancies, function (obj, index) {
                //            if (obj.ConsultancyName == $scope.cand.ResumeSourceDetail) return index;
                //        })[0];
                //        $scope.cand.ResumeSource = $scope.consultancies[i];
                //    }
                //    var s = $.map($scope.data.types, function (obj, index) {
                //        if (obj.TypeID == $scope.cand.CandidateStatusTypeID) return index;
                //    })[0];
                //    $scope.cand.CandidateStatus = $scope.data.types[s];
                //});
            },
            function (error) {
                $.toas(error.data, '', "error");
            });
        }
    };
    var candId = $stateParams.CandidateID;
    if (candId && candId != '') {
        $scope.pageData.CandidateID = candId;
        $scope.loadCandidate();
    }
    $scope.openOldFeedback = function (size) {
        var modalInstance = $modal.open({
            templateUrl: 'oldFeedback.html',
            controller: 'ModalInstanceController',
            size: size,
            resolve: {
                modalData: function () {
                    return $scope.pageData.Round1Feedback;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            // ok click 
        }, function () {
            // cancel click
        });
    };
    $scope.$formPristine = true;
    $scope.submitForm = function (form, status) {
        if (status == 0 || form.$valid) {
            $scope.$formPristine = true;
            $scope.pageData.feedback.InterviewRound = $scope.pageData.InterviewRound;
            $scope.pageData.feedback.Status = status;
            profileService.saveInterviewFeedback($scope.pageData.feedback)
            .then(function (response) {
                $scope.pageData.feedback = {};
                $.toas('Interview feedback saved.', '', "success");
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
            $scope.pageData = new FeedbackPageData();
        }, function (btn) {
            //alert('No clicked');
        });
    }
    $scope.$on('$stateChangeSuccess', function () {
        displayMessages();
        
    });

}])
.controller('ModalInstanceController', ['$scope', '$modalInstance', 'modalData', function ($scope, $modalInstance, modalData) {

    $scope.feedback = modalData;
    $scope.ok = function () {
        $modalInstance.close($scope.feedback);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss('cancel');
    };
}])
.directive('popOver', ['$compile', function ($compile) {
    var itemsTemplate = '<table class="table table-hover"><tbody><tr><th class="col-md-1">Level</th><th class="col-md-3">Definition</th><th class="text-center col-md-8">Brief Description</th></tr><tr ng-repeat="t in items"><td class="col-lg-2">{{t.Code}}</td><td class="col-lg-2">{{t.Name}}</td><td class="col-lg-4">{{t.Description}}</td> </tr> </tbody> </table>';
    var getTemplate = function (contentType) {
        var template = '';
        switch (contentType) {
            case 'items':
                template = itemsTemplate;
                break;
        }
        return template;
    };
    
    $('body').on('click', function (e) {
        $(".infowindow").popover('destroy');
    });
    return {
        restrict: "A",
        transclude: true,
        template: "<span ng-transclude></span>",
        link: function (scope, element, attrs) {
            element.bind('click', function (e) {
                var popOverContent;
                if (scope.items) {
                    var html = getTemplate("items");
                    popOverContent = $compile(html)(scope);
                }
                var options = {
                    content: popOverContent,
                    placement: "left",
                    html: true,
                    trigger: "manual",
                    title: scope.title
                };

                e.stopPropagation();
                $(element).popover(options);
                element.popover('show');
            });
        },
        scope: {
            items: '=',
            title: '@'
        }
    };
}]);






