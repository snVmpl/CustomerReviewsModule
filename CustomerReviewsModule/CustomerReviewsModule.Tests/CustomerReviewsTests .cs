using CustomerReviewsModule.Core.Models;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data;
using CustomerReviewsModule.Data.Repositories;
using CustomerReviewsModule.Data.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Testing.Bases;
using Xunit;

namespace CustomerReviewsModule.Tests
{
    public class CustomerReviewsTests : FunctionalTestBase
    {
        private ICustomerReviewSearchService _customerReviewSearchService;
        private Mock<Func<ICustomerReviewRepository>> _customerReviewRepoMock;
        private Mock<IRatingService> _ratingServiceMock;
        private ICustomerReviewService _customerReviewService;

        private string _testProductId => "testProductId";
        private string _testCustomerId => "testCustomerId";

        public CustomerReviewsTests()
        {
            Init();
        }

        private void Init()
        {
            _customerReviewRepoMock = new Mock<Func<ICustomerReviewRepository>>();
            _ratingServiceMock = new Mock<IRatingService>();
            _customerReviewService = new CustomerReviewService(_customerReviewRepoMock.Object, _ratingServiceMock.Object);
            _customerReviewSearchService = new CustomerReviewSearchService(_customerReviewRepoMock.Object, _customerReviewService);

            var customers = new List<CustomerReviewEntity>
            {
                new CustomerReviewEntity
                {
                    Id = _testCustomerId,
                    ProductId = _testProductId,
                    IsActive = true }
            }.AsQueryable();

            _customerReviewRepoMock.Setup(c => c().CustomerReviews).Returns(customers);
            _customerReviewRepoMock.Setup(c => c().UnitOfWork.Commit()).Returns(1);
        }

        [Fact]
        public void SearchCustomerReviewsTest()
        {
            // Arrange
            var criteria = new CustomerReviewSearchCriteria
            {
                IsActive = true,
                ProductIds = new string[1] { _testProductId }
            };

            // Act
            GenericSearchResult<CustomerReview> result = _customerReviewSearchService.SearchCustomerReviews(criteria);

            // Assert
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public void SaveCustomerReviewsTest()
        {
            // Act
            _customerReviewService.SaveCustomerReviews(new CustomerReview[] { new CustomerReview() });

            // Assert
            _customerReviewRepoMock.Verify(c => c().Add(It.IsAny<CustomerReviewEntity>()), Times.Once);
            _customerReviewRepoMock.Verify(c => c().UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public void DeleteCustomerReviewsTest()
        {
            // Act
            _customerReviewService.DeleteCustomerReviews(new string[] { });

            // Assert
            _customerReviewRepoMock.Verify(c => c().DeleteCustomerReviews(It.IsAny<string[]>()), Times.Once);
            _customerReviewRepoMock.Verify(c => c().UnitOfWork.Commit(), Times.Once);
        }
    }
}
