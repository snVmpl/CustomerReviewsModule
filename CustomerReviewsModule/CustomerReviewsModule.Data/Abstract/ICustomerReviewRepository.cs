using System.Linq;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.Repositories
{
    public interface ICustomerReviewRepository : IRepository
    {
        IQueryable<CustomerReviewEntity> CustomerReviews { get; }

        IQueryable<RatingProductEntity> Products { get; }

        RatingProductEntity GetProductById(string id);

        CustomerReviewEntity[] GetByIds(string[] ids);

        CustomerReviewEntity[] GetProductReviews(string productId);

        void DeleteCustomerReviews(string[] ids);
    }
}
