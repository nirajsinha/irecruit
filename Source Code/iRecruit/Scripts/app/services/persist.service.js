angular.module('irecruit.services')
.factory("persistService", ['$window', '$rootScope', 'localStorageService', function ($window, $rootScope, localStorageService) {
    return {
        getData: function () {
            return localStorageService.get('data');
        },
        setData: function (val) {
            if (val == 'null') val = '';
            localStorageService.set('data', JSON.stringify(val));
            return this;
        },
        getResumeSearchPageData: function () {
            return localStorageService.get('ResumeSearchPage');
        },
        setResumeSearchPageData: function (val) {
            if (val == 'null') val = '';
            localStorageService.set('ResumeSearchPage', val);
            return this;
        }
    };

}]);
