using CustomerReviewsModule.Core.Enums;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Core.Models
{
    public class CustomerReview : AuditableEntity
    {
        public string AuthorNickname { get; set; }

        public string Content { get; set; }

        public bool IsActive { get; set; }

        public string ProductId { get; set; }

        public string Pros { get; set; }

        public string Cons { get; set; }

        public Rating Rating { get; set; }

        public int RatingNumber { get; set; }

        public string[] RatingTypes { get; set; }
    }
}