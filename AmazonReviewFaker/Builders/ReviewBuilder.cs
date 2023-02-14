using AmazonReviewFaker.Models;
using Newtonsoft.Json;

namespace AmazonReviewFaker.Builders
{
    /// <summary>
    /// Builds review data from a streamreader
    /// </summary>
    public class ReviewBuilder
    {
        public ReviewData BuildReviewData(StreamReader reader)
        {
            ReviewData reviewData = new ReviewData();
            if (reader.Peek != null)
            {
                string line = reader.ReadLine();
                dynamic obj = JsonConvert.DeserializeObject(line);
                reviewData.ReviewSummary = obj.summary;
                reviewData.ReviewText = obj.reviewText;
                reviewData.Stars = obj.overall;
            }
            return reviewData;
        }
    }
}
