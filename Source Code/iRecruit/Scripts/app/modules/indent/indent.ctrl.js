angular.module('irecruit.indent')

.controller('IndentController', ['$rootScope', '$scope', '$http', '$filter', 'commonService', 'persistService', 'indentService', 'WizardHandler', '$state', '$stateParams', 'dialogs', '$q',
    function ($rootScope, $scope, $http, $filter, commonService, persistService, indentService, WizardHandler, $state, $stateParams, dialogs, $q) {
    $q.all([
       commonService.getTechnologies($scope.data.UserInfo.CompanyID),
       commonService.getBranches($scope.data.UserInfo.CompanyID),
       commonService.getDepartments($scope.data.UserInfo.CompanyID),
       commonService.getInterviewPanel($scope.data.UserInfo.CompanyID),
       commonService.getTypes()
    ]).then(function(result) {
        var tmp = [];
        angular.forEach(result, function(response) {
            tmp.push(response);
        });
        return tmp;
    }).then(function (data) {
         $scope.data.technologies = data[0];
         $scope.data.branches = data[1];
         $scope.data.departments = data[2];
         $scope.data.interviewer_level1 = jQuery.grep(data[3], function (n, i) {
             return (n.Level == 1);
         });
         $scope.data.interviewer_level2 = jQuery.grep(data[3], function (n, i) {
             return (n.Level == 2);
         });

         $scope.data.types = data[4];
         persistService.setData($scope.data);
         $scope.project_statuses = $.grep($scope.data.types, function (n, i) {
             return (n.TypeClassID == 1);
         });
         $scope.indent_reasons = $.grep($scope.data.types, function (n, i) {
             return (n.TypeClassID == 2);
         });
         $scope.resource_locations = $.grep($scope.data.types, function (n, i) {
             return (n.TypeClassID == 3);
         });
         $scope.employment_types = $.grep($scope.data.types, function (n, i) {
             return (n.TypeClassID == 4);
         });
         $scope.staffing_mode = $.grep($scope.data.types, function (n, i) {
             return (n.TypeClassID == 5);
         });
         $scope.visa_types = $.grep($scope.data.types, function (n, i) {
             return (n.TypeClassID == 6);
         });
         $scope.indent_statuses = $.grep($scope.data.types, function (n, i) {
             return (n.TypeClassID == 7);
         });
    }).catch(function error(err) {
        $.toas(err, '', "error");
    });
    
    $scope.indent = {};
    $scope.indent.DisableIndenterAccess = 0;
    $scope.indent.DisableFHAccess = 0;
    $scope.indent.DisableSVPAccess = 0;
    $scope.files = [];
    $scope.$formPristine = true;
    $scope.$watchCollection('[indent.DisableIndenterAccess, indent.DisableFHAccess, indent.DisableSVPAccess]',function(newValues,oldValues){
        var disableInd = newValues[0] == "1";
        var disableFh = newValues[1] == "1";
        var disableSvp = newValues[2] == "1";
        $("#ind_section1 :input").attr("disabled", disableInd);
        $("#ind_section2 :input").attr("disabled", disableInd);
        $("#ind_section3 :input").attr("disabled", disableInd);
        $("#ind_section4 :input").attr("disabled", disableInd);
        $("#fh_section :input").attr("disabled", disableFh);
        $("#svp_section :input").attr("disabled", disableSvp);

        $("#idate").attr("disabled", "disabled");
        $("#idate_btn").attr("disabled", "disabled");
        $("#fhdate").attr("disabled", "disabled");
        $("#fhdate_btn").attr("disabled", "disabled");
        $("#svpdate").attr("disabled", "disabled");
        $("#svpdate_btn").attr("disabled", "disabled");
        
    });
    $scope.searchIndents = function (val) {
        return indentService.searchIndents($scope.data.UserInfo.CompanyID, val)
        .then(function (response) {
            return response;
        });
    };
    $scope.getIndent = function (indentNumber) {
        indentService.getIndent(indentNumber)
            .then(function (d) {
                var data = d.Indent;
                $scope.indent = angular.copy(d.Indent);
                
                $scope.indent.IndentDate = $filter('moment')(d.Indent.IndentDate, $rootScope.ldf);
                $scope.indent.FunctionHeadStatusDate = $filter('moment')(d.Indent.FunctionHeadStatusDate, $rootScope.ldf);
                $scope.indent.SeniorVicePresidentStatusDate = $filter('moment')(d.Indent.SeniorVicePresidentStatusDate, $rootScope.ldf);
                $scope.indent.TargetJoinDate = $filter('moment')(d.Indent.TargetJoinDate, $rootScope.ldf);
    
                $scope.indent.DisableIndenterAccess = d.DisableIndenterAccess;
                $scope.indent.DisableFHAccess = d.DisableFHAccess;
                $scope.indent.DisableSVPAccess = d.DisableSVPAccess;
                $scope.indent.JDUrl = d.JDUrl;
                var b = $.map($scope.data.branches, function (obj, index) {
                    if (obj.BranchID == data.BranchID) return index;
                })[0];
                $scope.indent.branch = $scope.data.branches[b];

                var d = $.map($scope.data.departments, function (obj, index) {
                    if (obj.DepartmentID == data.DepartmentID) return index;
                })[0];
                $scope.indent.dept = $scope.data.departments[d];

                // set interview panel 1
                if (data.InterviewPanel1) {
                    var arr_panel1 = data.InterviewPanel1.split(';');
                    var temp_panel1 = [];
                    for (var i = 0; i < arr_panel1.length; i++) {
                        $.each($scope.data.interviewer_level1, function (index, item) {
                            if (arr_panel1[i] == item.Name) {
                                temp_panel1.push(item);
                                $('#tech1 option:contains("' + arr_panel1[i] + '")').prop("selected", true);
                            }
                        });
                    }
                    $scope.indent.InterviewPanel1 = temp_panel1;
                }
                // set interview panel 2
                if (data.InterviewPanel2) {
                    var arr_panel2 = data.InterviewPanel2.split(';');
                    var temp_panel2 = [];
                    for (var i = 0; i < arr_panel2.length; i++) {
                        $.each($scope.data.interviewer_level2, function (index, item) {
                            if (arr_panel2[i] == item.Name) {
                                temp_panel2.push(item);
                                $('#tech2 option:contains("' + arr_panel2[i] + '")').prop("selected", true);
                            }
                        });
                    }
                    $scope.indent.InterviewPanel2 = temp_panel2;
                }
                // set technologies
                
                commonService.getTechnologies($scope.data.UserInfo.CompanyID).then(function (result) {
                    $scope.data.technologies = result;
                    if (data.Technologies) {
                        var arr_tech = data.Technologies.split(';');
                        var temp_tech = [];
                        for (var i = 0; i < arr_tech.length; i++) {
                            $.each($scope.data.technologies, function (index, item) {
                                if (arr_tech[i].trim() == item.Name) {
                                    temp_tech.push(item);
                                    $('#tech option:contains("' + arr_tech[i] + '")').prop("selected", true);
                                }
                            });
                        }
                        $scope.indent.Technologies = temp_tech;
                    }
                });

            },
            function (error) {
                $.toas(error.data, '', "error");
            });
    }
    var indentNo = $stateParams.indentNumber;
    if (indentNo && indentNo != '') {
        $scope.getIndent(indentNo);
    }
    $scope.loadSelectedIndent = function (item, model, label)
    {
        $state.transitionTo("indent", { 'indentNumber': item.value }, { notify: true });
        //if ($state.current.name != 'indent.details') {
        //    $state.go(".details", { 'indentNumber': item.value });
        //}
        //else {
        //    $state.params.indentNumber = item.value;
        //    $scope.getIndent(item.value);
        //}
    }
    $scope.deleteFile = function (index)
    {
        $scope.indent.UploadFile_Indents = null;
        $scope.files.splice(index, 1);
    }
    $scope.downloadJobDescription = function () {
        window.open($scope.indent.JDUrl, '_blank', '');
    }
    //listen for the file selected event
    $scope.$on("fileSelected", function (event, args) {
        $('#i_form').ajaxSubmit({
            url: '/api/upload/' + $scope.data.UserInfo.UserID,
            type: 'POST',
            dataType: 'text',
            success: function (response, status, xhr, $form) {
                $scope.$apply(function () {
                    $scope.indent.UploadFile_Indents = response;
                    if ($scope.files.length > 0) {
                        $scope.files[0] = args.file;
                    }
                    else $scope.files.push(args.file);
                });
            },
            error: function (xhr, status, error) {
                $.toas(error, '', "error");
            }
        });
        
    });
    
    $scope.$on('$stateChangeSuccess', function () {
        //if ($state.params && $state.params.indentNumber) {
        //    var item = { value: $state.params.indentNumber };
        //    $scope.loadSelectedIndent(item);
        //}
        displayMessages();
    });
    $scope.resetForm = function () {
        $scope.indent = {};
    };
    $scope.cancelForm = function () {
        var dlg = dialogs.confirm('Please Confirm', 'This action will cancel all inputs and you will lose all unsaved data. <br />Do you want to continue?', { size: 'md' });
        dlg.result.then(function (btn) {
            $scope.resetForm();
        }, function (btn) {
            //alert('No clicked');
        });
    }
    $scope.finished = function () {
        alert("Wizard finished :)");
    }
    $scope.validateStep = function () {
        console.log("Step continued");
    }
    $scope.changeEmployeeType = function (checked) {
        $scope.EmployeeTypeDisabled = true;
    }
    $scope.goBack = function () {
        //WizardHandler.wizard().goTo(0);
        WizardHandler.wizard().previous();
        //WizardHandler.wizard().next();
    }

    $scope.submitForm = function (form) {
        // If Save is selected then make isValid true to bypass validation 
        var result = $.grep($scope.indent_statuses, function (e) {
            return e.TypeID == $scope.indent.Indent_Status && e.Code == 1;
        });
        if (result.length > 0 || form.$valid) {
            $scope.$formPristine = true;
            var technologies = null;
            if ($('#tech').val()) technologies = $('#tech').val().join('; ');

            var techPanel1 = null;
            if ($scope.indent.InterviewPanel1) {
                techPanel1 = $.map($scope.indent.InterviewPanel1, function (obj) {
                    return obj.Name
                }).join('; ');
            }
            var techPanel2 = null;
            if ($scope.indent.InterviewPanel2) {
                var techPanel2 = $.map($scope.indent.InterviewPanel2, function (obj) {
                    return obj.Name
                }).join('; ');
            }
			var request = angular.copy($scope.indent);
			request.BranchID = $scope.indent.branch.BranchID;
			request.DepartmentID = $scope.indent.dept.DepartmentID;
			request.Technologies = technologies;
			request.InterviewPanel1 = techPanel1;
			request.InterviewPanel2 = techPanel2;
			request.TargetJoinDate = $scope.getDateString($scope.indent.TargetJoinDate);
			
			indentService.saveIndent(request)
            .then(function (response) {
                $scope.indent = {};
                $scope.files = [];
                $.toas('Indent added successfully. Search and track your Indent status anytime on timeline window.', '', "success");
            },
            function (error) {
                $.toas(error.data, '', "error");
            });
        }
        else {
            $scope.$formPristine = false;
            $.toas('Form is not filled completely. Please save it temporarily or complete before submitting.', '', "exclaimation");
        }
    };

    $scope.datepickers = {
        dtDoj: false,
        dtIndent: false,
        dtFh: false,
        dtSvp: false
    }
    
    $scope.showWeeks = true;
    $scope.toggleWeeks = function () {
        $scope.showWeeks = !$scope.showWeeks;
    };

    //// Disable weekend selection
    //$scope.disabled = function (date, mode) {
    //    return (mode === 'day' && (date.getDay() === 0 || date.getDay() === 6));
    //};

    $scope.toggleMin = function () {
        $scope.minDate = ($scope.minDate) ? null : new Date();
    };
    $scope.toggleMin();

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
