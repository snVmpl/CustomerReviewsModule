angular.module('customerReviewsModule')
    .factory('customerReviewsModule.webApi', ['$resource', function ($resource) {
        return $resource('api/CustomerReviewsModule', {}, {
            search: { method: 'POST', url: 'api/CustomerReviewsModule/search' },
            update: { method: 'POST' },
            delete: { method: 'DELETE' }
        });
}]);