angular.module('customerReviewsModule')
    .controller('customerReviewsModule.reviewDetailController', ['$scope', 'customerReviewsModule.webApi',
        function ($scope, reviewsApi) {
            var blade = $scope.blade;

            //blade.headIcon = 'fa fa-floppy-o';
            blade.headIcon = 'fa fa-pencil-square-o';

            blade.update = function () {
                blade.loading = true;
                reviewsApi.update([blade.currentEntity], function (data) {
                    //blade.refresh();
                    blade.loading = false;
                });
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