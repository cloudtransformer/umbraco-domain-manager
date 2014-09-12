angular.module("umbraco").controller("AgeBase.DomainManager.IndexController", function ($scope, $http, notificationsService) {

    $http.get("backoffice/AgeBaseDomainManager/DomainManagerApi/Get").then(function (response) {
        $scope.domains = response.data;

        for (var i = 0; i < $scope.domains.length; i++) {
            if ($scope.domains[i].Name.toLowerCase().indexOf("http://") != 0 && $scope.domains[i].Name.toLowerCase().indexOf("https://") != 0)
                $scope.domains[i].Url = window.location.protocol + "//" + $scope.domains[i].Name;
            else
                $scope.domains[i].Url = $scope.domains[i].Name;
        }
    });

    $scope.showNode = function (nodeId) {
        window.location = "/umbraco/#/content/content/edit/" + nodeId;
    };

    $scope.deleteDomain = function (domain) {
        $http.delete("backoffice/AgeBaseDomainManager/DomainManagerApi/Delete/" + domain.Id).then(function (response) {
            if (response && response.data) {
                $scope.domains.pop(domain);
            } else {
                notificationsService.error("ERROR", "It looks like the domain failed to delete correctly. Please try again.");
            }
        });
    };

});