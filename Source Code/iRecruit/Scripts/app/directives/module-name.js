angular.module('irecruit.directives')
.directive('moduleName', ['$rootScope', '$timeout',  function ($rootScope, $timeout) {
      return {
          link: function (scope, element) {
              var listener = function (event, toState) {
                  var module = 'Featured';
                  if (toState.data && toState.data.moduleName) module = toState.data.moduleName;
                  $timeout(function () {
                      element.text(module);
                  }, 0, false);
              };

              $rootScope.$on('$stateChangeSuccess', listener);
          }
      };
  }
]);

