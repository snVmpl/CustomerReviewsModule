using CustomerReviewsModule.Data.Repositories;
using System;
using System.Linq;

namespace CustomerReviewsModule.Data.Services
{
    public class RatingService : IRatingService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;
        public RatingService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public float Count(CustomerReviewEntity review)
        {
            using (var repository = _repositoryFactory())
            {
                var reviews = repository.GetProductReviews(review.ProductId);
                float rating = (float)(reviews.Sum(c => (int)c.Rating) + (int)review.Rating) / (reviews.Length + 1);
                return rating;
            }
        }
    }
}
