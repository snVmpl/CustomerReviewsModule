using CustomerReviewsModule.Core.Models;
using CustomerReviewsModule.Core.Services;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Web.Security;
using static CustomerReviewsModule.Core.ModuleConstants.Security;

namespace CustomerReviewsModule.Web.Controllers.Api
{
    [RoutePrefix("api/CustomerReviewsModule")]
    public class CustomerReviewsModuleController : ApiController
    {
        private readonly ICustomerReviewSearchService _customerReviewSearchService;
        private readonly ICustomerReviewService _customerReviewService;
        public CustomerReviewsModuleController(ICustomerReviewSearchService customerReviewSearchService, ICustomerReviewService customerReviewService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
        }

        // GET: api/CustomerReviewsModule
        [HttpGet]
        [Route("")]
        [CheckPermission(Permission = Permissions.Read)]
        public IHttpActionResult Get()
        {
            return Ok(new { result = "Hello world!" });
        }

        [HttpPost]
        [Route("search")]
        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]
        [CheckPermission(Permission = Permissions.Read)]
        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            GenericSearchResult<CustomerReview> result = _customerReviewSearchService.SearchCustomerReviews(criteria);
            return Ok(result);
        }

        /// <summary>
        ///  Create new or update existing customer review
        /// </summary>
        /// <param name="customerReviews">Customer reviews</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = Permissions.Update)]
        public IHttpActionResult Update(CustomerReview[] customerReviews)
        {
            _customerReviewService.SaveCustomerReviews(customerReviews);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete Customer Reviews by IDs
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = Permissions.Delete)]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            _customerReviewService.DeleteCustomerReviews(ids);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
