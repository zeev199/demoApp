var app = angular.module('myApp', []);

app.controller('appCtrl', function ($scope, Api, $q) {

    $scope.getData = function () {
        Api({}, 'sp_GetQueues', function (data) {
            $scope.queues = data.filter(function (obj) {
                return obj['Status'] === 0;
            });
            $scope.queuesSerice = data.filter(function (obj) {
                return obj['Status'] === 1;
            });
        })
    }
    $scope.addCustomer = function (cus) {
        Api({ CustomerName: cus, Status: 0 }, 'sp_IUQueues', function (data) {
            $scope.CustomerName = undefined;
            $scope.getData();
        })
    }
    $scope.customerNext = function () {
        var next = $scope.queues[0];
        Api({ QueueID: next.QueueID, CustomerName: next.CustomerName, Status: 1 }, 'sp_IUQueues', function (data) {
            var prev = $scope.queuesSerice[0];
            if (prev)
                Api({ QueueID: prev.QueueID, CustomerName: prev.CustomerName, Status: 2 }, 'sp_IUQueues', function (data) {
                    $scope.getData();
                    setTimeout(function () {
                        $scope.$apply();
                    }, 0);
                });
            else {
                $scope.getData();
                setTimeout(function () {
                    $scope.$apply();
                }, 0);
            }
        })


    }
    $scope.customerFinis = function () {
        var prev = $scope.queuesSerice[0];
        if (prev)
            Api({ QueueID: prev.QueueID, CustomerName: prev.CustomerName, Status: 2 }, 'sp_IUQueues', function (data) {
                $scope.getData();
                setTimeout(function () {
                    $scope.$apply();
                }, 0);
            });
    }
    $scope.getData();

});
// service Api
//  DB-קריאה לפרוצדורות ב
app.factory("Api", function ($http) {
    //  sql-ל html של type פונקציה להמרת
    //(מוסבר בקבצים של השרת) ado.net של eunm הפונקצייה מחזירה מספר של  
    function detectType(value) {
        switch (typeof value) {
            case "string":
                return 16;
            case "number":
                if (parseInt(value) === value)
                    return 11;
                else
                    return 7
            case "boolean":
                return 3;
            case "object":
                if (value instanceof Date)
                    return 6;
            default:
                return 16;
        }
    }

    return function (obj, storedProcedure, callback, eroorcallback) {
        // DB-בניית מערך פרמטרים מהאובייקט ל
        var parms = [];
        for (var index in obj) {
            var x = {
                ParameterName: "@" + index,
                Value: obj[index],
                DbType: detectType(obj[index]),
                Direction: 1
            }
            parms.push(x);
        }
        //קריאה לשרת
        return $http.post('/GetData', { StoredProcedure: storedProcedure, Params: parms }).then(function (res) {
            if (res.data)
                callback(res.data['Table']);
        }, eroorcallback ? eroorcallback : function (err) {

        })
    }
})