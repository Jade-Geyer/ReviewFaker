using AmazonReviewFaker.Models;

namespace AmazonReviewFaker.Extensions
{
    public static class AmazonFakeReviewExtensions
    {
        public static ReviewResponse ToReviewResponse(this AmazonFakeReview review)
        {
            return new ReviewResponse()
            {
                SummaryText = review.SummaryText,
                ReviewText = review.ReviewText,
                Stars = review.Stars,
            };
        }
    }
}
