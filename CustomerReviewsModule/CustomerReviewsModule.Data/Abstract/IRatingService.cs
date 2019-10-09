using CustomerReviewsModule.Data.Repositories;

namespace CustomerReviewsModule.Data
{
    public interface IRatingService
    {
        float Count(CustomerReviewEntity review);
    }
}
