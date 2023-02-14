using AmazonReviewFaker.Extensions;
using AmazonReviewFaker.Models;

namespace UnitTests
{
    public class FunctionTests
    {
        private ReviewRepository _testRepo = new ReviewRepository();

        [Fact]
        public void When_Getting_Review_Should_Have_Valid_Data()
        {
            ReviewResponse review = _testRepo.GetReview();
            Assert.NotNull(review);
            Assert.NotNull(review.SummaryText);
            Assert.NotNull(review.ReviewText);
            Assert.NotNull(review.Stars);
        }

        [Fact]
        public void When_Mapping_AmazonFakeReview_Expect_Valid_Data()
        {
            AmazonFakeReview testReview = new AmazonFakeReview()
            {
                Id = "123",
                SummaryText = "summary text",
                ReviewText = "review text",
                Stars = 1
            };

            var result = testReview.ToReviewResponse();
            Assert.NotNull(result);
            Assert.Equal(result.SummaryText, testReview.SummaryText);
            Assert.Equal(result.ReviewText, testReview.ReviewText);
            Assert.Equal(result.Stars, testReview.Stars);
        }
    }
}