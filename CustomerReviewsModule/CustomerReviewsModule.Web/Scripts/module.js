// Call this to register your module to main application
var moduleName = "customerReviewsModule";

if (AppDependencies !== undefined) {
    AppDependencies.push(moduleName);
}

angular.module(moduleName, [])
    .config(['$stateProvider', '$urlRouterProvider',
        function ($stateProvider, $urlRouterProvider) {
            $stateProvider
                .state('workspace.customerReviewsModuleState', {
                    url: '/customerReviewsModule',
                    templateUrl: '$(Platform)/Scripts/common/templates/home.tpl.html',
                    controller: [
                        '$scope', 'platformWebApp.bladeNavigationService', function ($scope, bladeNavigationService) {
                            var newBlade = {
                                id: 'reviewsList',
                                controller: 'customerReviewsModule.reviewsListController',
                                template: 'Modules/$(customerReviewsModule)/Scripts/blades/reviews-list.tpl.html',
                                isClosingDisabled: true
                            };
                            bladeNavigationService.showBlade(newBlade);
                        }
                    ]
                });
        }
    ])
    .run(['$rootScope', 'platformWebApp.mainMenuService', 'platformWebApp.widgetService', '$state',
        function ($rootScope, mainMenuService, widgetService, $state) {
            //Register module in main menu
            var menuItem = {
                path: 'browse/customerReviewsModule',
                icon: 'fa fa-comments',
                title: 'Customer Reviews',
                priority: 100,
                action: function () { $state.go('workspace.customerReviewsModuleState') },
                permission: 'customerReview:read'
            };
            mainMenuService.addMenuItem(menuItem);

            //Register reviews widget inside product blade
            var itemReviewsWidget = {
                controller: 'customerReviewsModule.customerReviewWidgetController',
                template: 'Modules/$(customerReviewsModule)/Scripts/widgets/customerReviewWidget.tpl.html'
            };
            widgetService.registerWidget(itemReviewsWidget, 'itemDetail');
        }
    ]);
