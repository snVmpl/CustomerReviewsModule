﻿angular.module('customerReviewsModule')
    .controller('customerReviewsModule.reviewsListController', ['$scope', 'customerReviewsModule.webApi', 'platformWebApp.bladeUtils', 'uiGridConstants', 'platformWebApp.uiGridHelper',
        function ($scope, reviewsApi, bladeUtils, uiGridConstants, uiGridHelper) {
            $scope.uiGridConstants = uiGridConstants;

            var blade = $scope.blade;
            var bladeNavigationService = bladeUtils.bladeNavigationService;

            blade.getSearchCriteria = function () {
                return angular.extend(filter, {
                    searchPhrase: filter.keyword ? filter.keyword : undefined,
                    sort: uiGridHelper.getSortExpression($scope),
                    skip: ($scope.pageSettings.currentPage - 1) * $scope.pageSettings.itemsPerPageCount,
                    take: $scope.pageSettings.itemsPerPageCount
                });
            };

            blade.refresh = function () {
                blade.isLoading = true;
                reviewsApi.search(blade.getSearchCriteria(), function (data) {
                    blade.isLoading = false;
                    $scope.pageSettings.totalItems = data.totalCount;
                    blade.currentEntities = data.results;
                });
            };

            blade.selectNode = function (data) {
                $scope.selectedNodeId = data.id;

                var newBlade = {
                    id: 'reviewDetails',
                    currentEntityId: data.id,
                    currentEntity: data,
                    title: data.authorNickname,
                    controller: 'customerReviewsModule.reviewDetailController',
                    template: 'Modules/$(customerReviewsModule)/Scripts/blades/customer-review-detail.tpl.html'
                };
                bladeNavigationService.showBlade(newBlade, blade);
            };

            function openBladeNew() {
                $scope.selectedNodeId = null;

                var newBlade = {
                    id: 'storeDetails',
                    currentEntity: {},
                    title: 'customerReviews.blades.new-customer-review-wizard.title',
                    subtitle: 'customerReviews.blades.new-customer-review-wizard.subtitle',
                    controller: 'customerReviewsModule.newReviewDetailController',
                    template: 'Modules/$(customerReviewsModule)/Scripts/blades/new-customer-review-wizard.tpl.html'
                };
                bladeNavigationService.showBlade(newBlade, blade);
            }

            blade.headIcon = 'fa-comments';

            blade.toolbarCommands = [
                {
                    name: "platform.commands.refresh", icon: 'fa fa-refresh',
                    executeMethod: blade.refresh,
                    canExecuteMethod: function () {
                        return true;
                    }
                },
                {
                    name: "platform.commands.add", icon: 'fa fa-plus',
                    executeMethod: openBladeNew,
                    canExecuteMethod: function () {
                        return true;
                    },
                    permission: 'store:create'
                }
            ];

            // simple and advanced filtering
            var filter = $scope.filter = blade.filter || {};

            filter.criteriaChanged = function () {
                if ($scope.pageSettings.currentPage > 1) {
                    $scope.pageSettings.currentPage = 1;
                } else {
                    blade.refresh();
                }
            };

            // ui-grid
            $scope.setGridOptions = function (gridOptions) {
                uiGridHelper.initialize($scope, gridOptions, function (gridApi) {
                    uiGridHelper.bindRefreshOnSortChanged($scope);
                });
                bladeUtils.initializePagination($scope.$parent);
            };

        }]);