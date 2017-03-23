angular.module('irecruit.directives')
.directive('appRoleMapping', function () {
    return {
        restrict: 'E',
        scope: {
            Employees: '=list'
        },
        template: '<li class="list-group-item" ng-repeat="t in Titles"><small>{{t.Description}} </small>, {{t.Name}}</li>'
    };
});

