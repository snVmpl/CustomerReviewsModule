using CustomerReviewsModule.Core.Models;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data.Repositories;
using System;
using System.Linq;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace CustomerReviewsModule.Data.Services
{
    public class CustomerReviewService : ServiceBase, ICustomerReviewService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;
        private readonly IRatingService _ratingService;

        public CustomerReviewService(Func<ICustomerReviewRepository> repositoryFactory,
            IRatingService ratingService)
        {
            _repositoryFactory = repositoryFactory;
            _ratingService = ratingService;
        }

        public CustomerReview[] GetByIds(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                return repository.GetByIds(ids).Select(x => x.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance())).ToArray();
            }
        }

        public void SaveCustomerReviews(CustomerReview[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = _repositoryFactory())
            {
                using (var changeTracker = GetChangeTracker(repository))
                {
                    var alreadyExistEntities = repository.GetByIds(items.Where(m => !m.IsTransient()).Select(x => x.Id).ToArray());
                    foreach (var item in items)
                    {
                        var sourceEntity = AbstractTypeFactory<CustomerReviewEntity>.TryCreateInstance().FromModel(item, pkMap);
                        var targetEntity = alreadyExistEntities.FirstOrDefault(x => x.Id == sourceEntity.Id);
                        bool needRecountRating = targetEntity == null || targetEntity.Rating != sourceEntity.Rating;
                        if (targetEntity != null)
                        {
                            changeTracker.Attach(targetEntity);
                            sourceEntity.Patch(targetEntity);
                        }
                        else
                        {
                            repository.Add(sourceEntity);
                        }
                        if (needRecountRating)
                        {
                            var product = repository.GetProductById(sourceEntity.ProductId);
                            product.Rating = _ratingService.Count(sourceEntity);
                        }
                    }

                    CommitChanges(repository);
                    pkMap.ResolvePrimaryKeys();
                }
            }
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                repository.DeleteCustomerReviews(ids);
                CommitChanges(repository);
            }
        }

        public double? GetProductsRating(string id)
        {
            double? result;
            using (var repository = _repositoryFactory())
            {
                result = repository.GetProductById(id).Rating;
            }
            return result;
        }
    }
}
