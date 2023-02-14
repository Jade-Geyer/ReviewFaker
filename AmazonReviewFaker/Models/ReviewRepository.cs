using AmazonReviewFaker.Builders;
using AmazonReviewFaker.Extensions;

namespace AmazonReviewFaker.Models
{
    public class ReviewRepository : IReviewRepository
    {
        //must track initialization for unit tests because of the database.
        static bool initialized = false;

        /// <summary>
        /// The repository is populated upon initialization by the ReviewBuilder.
        /// The apicontext creates the database object and populates it.
        /// </summary>
        public ReviewRepository()
        {
            if(initialized) return;

            List<ReviewData> ReviewsList = new List<ReviewData>();
            using (StreamReader reader = new StreamReader("Data/AMAZON_FASHION.json"))
            {
                do
                {
                    ReviewData reviewData = new ReviewBuilder().BuildReviewData(reader);
                    ReviewsList.Add(reviewData);
                } while (!reader.EndOfStream);
                reader.Close();
            }
            
            using (var context = new ApiContext())
            {
                int count = 0;
                foreach(ReviewData rData in ReviewsList)
                {
                    AmazonFakeReview review = new AmazonFakeReview()
                    {
                        Id = ""+count,
                        SummaryText = rData.ReviewSummary,
                        ReviewText = rData.ReviewText,
                        Stars = rData.Stars,
                    };
                    context.Reviews.Add(review);
                    count++;
                    if (count > 4000) break;
                    
                }
                context.SaveChanges();
            }
            initialized = true;
        }

        /// <summary>
        /// Randomly selects a review and stars
        /// if the randomly selected one has no summarytext,
        /// it will retry 2x recurvisely
        /// </summary>
        /// <returns></returns>
        public ReviewResponse GetReview(int count = 0)
        {
            using (var context = new ApiContext()) 
            {
                Random rnd = new Random();
                int randomReview = rnd.Next(context.Reviews.Count());
                var result = context.Reviews.ToList()[randomReview];
                if(result.SummaryText == null || result.ReviewText == null)
                {
                    ReviewResponse r = GetReview(count+1);
                    if(count > 2 && r == null)
                    {
                        result.SummaryText = "none";
                    }
                }
                result.Stars = DetermineStarWords(result);
                if(result.Stars == -1)
                {
                    result.Stars = rnd.Next(1, 6);
                }
                ReviewResponse response = result.ToReviewResponse();
                return response;
            }
        }


        /// <summary>
        /// Looks for obvious keywords that indicate that the reviewer gave the review {X} stars. 
        /// If found, will return an int. If not, will return -1.
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public int? DetermineStarWords(AmazonFakeReview review)
        {
            string[] oneStarText = new string[] { "1 star", "one star", "total crap", "ripoff", "scam" };
            string[] twoStarText = new string[] { "2 stars", "two stars", "pretty bad", "junky" };
            string[] threeStarText = new string[] { "3 stars", "three stars", "meh product", "sub-par", "fell short" };
            string[] fourStarText = new string[] { "4 stars", "four stars", "pretty good", "almost perfect" };
            string[] fiveStarText = new string[] { "5 stars", "five stars", "perfect", "amazing" };
            string[][] NumberWords = new string[][] { oneStarText, twoStarText, threeStarText, fourStarText, fiveStarText };

            int foundNumber = 0;
            foreach( string[] Words in NumberWords)
            {
                foundNumber++;
                foreach( string word in Words )
                {
                    if( (bool)review.SummaryText?.ToLower().Contains(word)) return foundNumber;
                    if( (bool)review.ReviewText?.ToLower().Contains(word)) return foundNumber;
                }
               
            }
            return -1;
        }
    }
}