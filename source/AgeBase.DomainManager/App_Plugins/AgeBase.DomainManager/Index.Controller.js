angular.module("umbraco").controller("AgeBase.DomainManager.IndexController", function ($scope, $http, contentResource) {
    $http.get("backoffice/AgeBaseDomainManager/DomainManagerApi/List").then(function (response) {
        $scope.domains = response.data;

        for (var i = 0; i < $scope.domains.length; i++) {
            if ($scope.domains[i].Name.toLowerCase().indexOf("http://") != 0 && $scope.domains[i].Name.toLowerCase().indexOf("https://") != 0)
                $scope.domains[i].Url = window.location.protocol + "//" + $scope.domains[i].Name;
            else
                $scope.domains[i].Url = $scope.domains[i].Name;
        }
    });
});