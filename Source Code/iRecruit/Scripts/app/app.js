// Defining a module
var app = angular.module('irecruit', [
    'ui.router',
    'ngCookies',
    'ui.bootstrap',
    'irecruit.ui',
    'dialogs.main',
    'dialogs.default-translations',
    'pascalprecht.translate',
    'mgo-angular-wizard',
    'LocalStorageModule',
    'angular-loading-bar', 
    'irecruit.directives',
    'irecruit.services',
    'irecruit.home',
    'irecruit.indent',
    'irecruit.resources',
    'irecruit.settings'
]);
angular.element(document).ready(function () {
    angular.bootstrap(document, ['irecruit']);
});

app.factory('formDataObject', function () {
    return function(data) {
        var fd = new FormData();
        angular.forEach(data, function(value, key) {
            fd.append(key, value);
        });
        return fd;
    };
})
.factory('httpRequestInterceptor', ['$rootScope', function($rootScope){
	return {
		request: function($config) {
		    if ($rootScope.AuthToken) {
				$config.headers['Auth-Token'] = $rootScope.AuthToken;
			}
			return $config;
		}
	};
}])
.config(['$httpProvider', function ($httpProvider) {
	$httpProvider.interceptors.push('httpRequestInterceptor');
}])
.config(['$locationProvider', function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}])
.config(['$provide', function ($provide) {
    $provide.decorator('$exceptionHandler', ['$log', '$delegate', function ($log, $delegate) {
        return function (exception, cause) {
            $log.debug('iRecruit exception handler: ' + exception);
            $delegate(exception, cause);
        };
    }
    ]);
}])
.filter('moment', ['$rootScope', function($rootScope) {
    return function (dateString, format) {
        if(dateString && dateString != '')
            return moment.utc(dateString).tz($rootScope.ltz).format(format);
        return null;
    };
}])
.controller('RootController', ['$rootScope', '$scope', '$state', '$stateParams', '$location', function ($rootScope, $scope, $state, $stateParams, $location) {
    $scope.$on('$stateChangeSuccess', function (e, current, previous) {
        $scope.activeViewPath = $location.path();
    });
}]);

app.run(['$rootScope', 'commonService', '$cookies', function ($rootScope, commonService, $cookies) {
    $rootScope.data = {};
    var userinfo = $.parseJSON($cookies.UserInfo);
    $rootScope.AuthToken = $cookies.AuthToken;
    $rootScope.ltz = jstz.determine().name();
    $rootScope.ldf = 'DD/MM/YYYY';
    $rootScope.dtpf = 'dd/MM/yyyy';// datepicker format, angular is not supporting moment formats
    if ($rootScope.ltz.indexOf('America') != -1) {
        $rootScope.ldf = 'MM/DD/YYYY';
        $rootScope.dtpf = 'MM/dd/yyyy';
    }
    $rootScope.ltf = $rootScope.ldf + ' HH:MM A z';
    $rootScope.menu = {
        DashboardEnabled: false,
        IndentEnabled: false,
        ResumeManagementEnabled: false,
        SettingsEnabled: false,
        ReportsEnabled: false,
        InterviewsEnabled: false
    };
    var p = commonService.getUserAccessFeatures(userinfo.UserID)
    p.then(function (data) {
        $rootScope.menu = data;
    });
    
    if (userinfo) {
        $rootScope.data.UserInfo = userinfo;
    }
    $rootScope.getDateString = function (dt)
    {
        if (dt) {
            var t = moment(dt, $rootScope.ldf).utc();
            if (t.isValid()) return t._d;
            else return t._i;
        }
        return null;
    }
    $rootScope.NotIE = function () {
        if (navigator.appName != 'Microsoft Internet Explorer') return true;
        return false;
    }
    
}]);

    





