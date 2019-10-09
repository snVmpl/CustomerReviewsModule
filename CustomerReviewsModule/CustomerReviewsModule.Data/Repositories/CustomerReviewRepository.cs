using System.Data.Entity;
using System.Linq;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace CustomerReviewsModule.Data.Repositories
{
    public class CustomerReviewRepository : EFRepositoryBase, ICustomerReviewRepository
    {
        public CustomerReviewRepository()
        {
        }

        public CustomerReviewRepository(string nameOrConnectionString, params IInterceptor[] interceptors)
            : base(nameOrConnectionString, null, interceptors)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public IQueryable<CustomerReviewEntity> CustomerReviews => GetAsQueryable<CustomerReviewEntity>();

        public IQueryable<RatingProductEntity> Products => GetAsQueryable<RatingProductEntity>();

        public RatingProductEntity GetProductById(string id)
        {
            return Products.FirstOrDefault(c => c.Id == id);
        }

        public CustomerReviewEntity[] GetByIds(string[] ids)
        {
            return CustomerReviews.Where(x => ids.Contains(x.Id)).ToArray();
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            var items = GetByIds(ids);
            foreach (var item in items)
            {
                Remove(item);
            }
        }

        public CustomerReviewEntity[] GetProductReviews(string productId)
        {
            return CustomerReviews.Where(c => c.ProductId == productId).ToArray();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerReviewEntity>().ToTable("CustomerReview").HasKey(x => x.Id).Property(x => x.Id);

            modelBuilder.Entity<RatingProductEntity>().ToTable("RatingProduct").HasKey(x => x.Id).Property(x => x.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
