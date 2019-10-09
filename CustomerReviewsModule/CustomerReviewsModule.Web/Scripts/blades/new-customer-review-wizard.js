angular.module('customerReviewsModule')
    .controller('customerReviewsModule.newReviewDetailController', ['$scope', 'customerReviewsModule.webApi',
        function ($scope, reviewsApi) {
            var blade = $scope.blade;

            blade.headIcon = 'fa fa-floppy-o';

            blade.update = function () {
                alert("update");
                //blade.loading = true;
                //reviewsApi.update("", function (data) {
                //    blade.refresh();
                //});
            }

            blade.toolbarCommands = [
                {
                    name: "platform.commands.save",
                    icon: 'fa fa-floppy-o',
                    executeMethod: blade.update,
                    canExecuteMethod: function () {
                        return true;
                    },
                    permission: 'CustomerReviewsModule:update'
                }
            ];
            blade.isLoading = false;
        }]);