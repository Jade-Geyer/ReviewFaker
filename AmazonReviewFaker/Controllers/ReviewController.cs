using AmazonReviewFaker.Models;
using Microsoft.AspNetCore.Mvc;

namespace AmazonReviewFaker.Controllers
{
    [Route("api/getreview")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        readonly IReviewRepository _reviewRepository;
        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        [HttpGet]
        public ActionResult<ReviewResponse> Get()
        {
            return Ok(_reviewRepository.GetReview());
        }
    }
}
