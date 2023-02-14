using AmazonReviewFaker.Models;

public interface IReviewRepository
{
    public ReviewResponse GetReview(int count = 0);
}