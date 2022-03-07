using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using good_eats.Models;
using good_eats.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace good_eats.Controllers
{
  [ApiController]
  // NOTE this is locking down the entire controller
  // NOTE this is the equivalent of putting .use() above all routes in the node super
  [Authorize]
  [Route("api/[controller]")]
  public class ReviewsController : ControllerBase
  {
    private readonly ReviewsService _reviewsService;

    public ReviewsController(ReviewsService reviewsService)
    {
      _reviewsService = reviewsService;
    }

    [HttpPost]
    public async Task<ActionResult<Review>> Create([FromBody] Review newReview)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        // NOTE never trust the client! IE - req.body.creatorId = req.userInfo.id
        newReview.CreatorId = userInfo.Id;
        Review createdReview = _reviewsService.Create(newReview);
        // NOTE populating the creator info before sending the new review back to the client
        createdReview.Creator = userInfo;
        return createdReview;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _reviewsService.Delete(id, userInfo.Id);
        return Ok("Review deleted!");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Review>> Update([FromBody] Review updatedReview, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedReview.Id = id;
        updatedReview.CreatorId = userInfo.Id;
        return Ok(_reviewsService.Update(updatedReview, userInfo.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}