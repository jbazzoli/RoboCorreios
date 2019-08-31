var app = angular.module('myFrontCorreio', []);
app.controller('myCtrl', function ($scope, $http) {
    $scope.cep="";
    $scope.endereco;
    $scope.pesquisar = function () {
       
            var baseUrl = 'http://localhost:49697/api/Values/' + $scope.cep;

            $http.get(baseUrl).then(function (response) {
                $scope.endereco = response.data;
                
            }, function (err) {
                console.log(err);
            });
        
        
    }
   
    
    
});

