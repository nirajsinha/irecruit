
angular.module('irecruit')
.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    //Setup routes to load partial templates from server. TemplateUrl is the location for the server view (Razor .cshtml view)
    $urlRouterProvider.otherwise('/home/');
    $stateProvider
	.state('home', {
	    url: '/home/',
	    templateUrl: '/home/main',
	    data : { moduleName: 'Dashboard' },
	    controller: 'HomeController'
	})
	
	.state('indent', {
	    url: '/indent/{indentNumber}/',
	    templateUrl: '/home/indent',
	    data: { moduleName: 'Indent' },
	    controller: 'IndentController'
	})
	//.state('indent.details', {
	//	url: '/:indentNumber',
	//	templateUrl: '/home/indent',
	//	controller: 'IndentController'
	//})
    .state('indenttracker', {
        url: '/indenttracker/',
        templateUrl: '/home/indenttracker',
        data: { moduleName: 'Indent' },
        controller: 'IndentTrackerController'
    })
	.state('timeline', {
	    url: '/timeline/',
	    templateUrl: '/home/timeline',
	    data: { moduleName: 'Indent' },
	    controller: 'IndentTimelineController'
	})
    .state('profile', {
        url: '/profile/{CandidateID}/',
        templateUrl: '/home/Profile',
        data: { moduleName: 'Profiles' },
        controller: 'ViewProfileController'
    })
    .state('resumesearch', {
        url: '/resumesearch/',
        templateUrl: '/home/ResumeSearch',
        data: { moduleName: 'Profiles' },
        controller: 'ResumeSearchController'
    })
    .state('candidate', {
        url: '/candidate/{CandidateID}/',
        templateUrl: '/home/candidate',
        data: { moduleName: 'Profiles' },
        controller: 'EditProfileController'
    })
    .state('interviewschedule', {
        url: '/interviewschedule/{CandidateID}/',
        templateUrl: '/home/interviewschedule',
        data: { moduleName: 'Profiles' },
        controller: 'InterviewScheduleController'
    })
	.state('interviewfeedback', {
	    url: '/interviewfeedback/{CandidateID}/',
	    templateUrl: '/home/interviewfeedback',
	    data: { moduleName: 'Profiles' },
	    controller: 'InterviewFeedbackController'
	})
	.state('departments', {
	    url: '/departments/',
	    templateUrl: '/home/departments',
	    data: { moduleName: 'Settings' },
	    controller: 'DepartmentController'
	})
	.state('skills', {
	    url: '/skills/',
	    templateUrl: '/home/skills',
	    data: { moduleName: 'Settings' },
	    controller: 'SkillsController'
	})
	.state('users', {
	    url: '/users/',
	    templateUrl: '/home/users',
	    data: { moduleName: 'Settings' },
	    controller: 'UserController'
	})
	.state('contactus', {
	    url: '/contactus/',
	    templateUrl: '/home/contactus',
	    data: { moduleName: 'Home' }
	})
	.state('privacy', {
	    url: '/privacy/',
	    templateUrl: '/home/privacy',
	    data: { moduleName: 'Home' }
	})
	.state('accessdenied', {
	    url: '/access-denied/',
	    templateUrl: '/home/accessdenied'
	})
	.state('cookiepolicy', {
	    url: '/cookiepolicy/',
	    templateUrl: '/home/cookiepolicy',
	    data: { moduleName: 'Home' }
	})
	.state('signout', {
	    url: '/account/signout/',
	    controller: function () {
	        location.href = '/account/logon';
	    }
	});
            
}]);