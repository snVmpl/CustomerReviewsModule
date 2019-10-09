using CustomerReviewsModule.Core.Enums;
using CustomerReviewsModule.Core.Models;
using System;
using System.ComponentModel.DataAnnotations;
using VirtoCommerce.Platform.Core.Common;

namespace CustomerReviewsModule.Data.Repositories
{
    public class CustomerReviewEntity : AuditableEntity
    {
        [StringLength(128)]
        public string AuthorNickname { get; set; }

        [Required]
        [StringLength(1024)]
        public string Content { get; set; }

        [StringLength(256)]
        public string Pros { get; set; }

        [StringLength(256)]
        public string Cons { get; set; }

        [Required]
        public Rating Rating { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [StringLength(128)]
        public string ProductId { get; set; }

        public virtual CustomerReview ToModel(CustomerReview customerReview)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            customerReview.Id = Id;
            customerReview.CreatedBy = CreatedBy;
            customerReview.CreatedDate = CreatedDate;
            customerReview.ModifiedBy = ModifiedBy;
            customerReview.ModifiedDate = ModifiedDate;

            customerReview.AuthorNickname = AuthorNickname;
            customerReview.Content = Content;
            customerReview.IsActive = IsActive;
            customerReview.ProductId = ProductId;

            customerReview.Pros = Pros;
            customerReview.Cons = Cons;
            customerReview.Rating = Rating;
            customerReview.RatingNumber = (int)Rating;
            customerReview.RatingTypes = Enum.GetNames(typeof(Rating));

            return customerReview;
        }

        public virtual CustomerReviewEntity FromModel(CustomerReview customerReview, PrimaryKeyResolvingMap pkMap)
        {
            if (customerReview == null)
                throw new ArgumentNullException(nameof(customerReview));

            pkMap.AddPair(customerReview, this);

            Id = customerReview.Id;
            CreatedBy = customerReview.CreatedBy;
            CreatedDate = customerReview.CreatedDate;
            ModifiedBy = customerReview.ModifiedBy;
            ModifiedDate = customerReview.ModifiedDate;

            AuthorNickname = customerReview.AuthorNickname;
            Content = customerReview.Content;
            IsActive = customerReview.IsActive;
            ProductId = customerReview.ProductId;

            Pros = customerReview.Pros;
            Cons = customerReview.Cons;
            Rating = customerReview.Rating;

            return this;
        }

        public virtual void Patch(CustomerReviewEntity target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            target.AuthorNickname = AuthorNickname;
            target.Content = Content;
            target.IsActive = IsActive;
            target.ProductId = ProductId;

            target.Pros = Pros;
            target.Cons = Cons;
            target.Rating = Rating;
        }
    }
}