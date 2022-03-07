using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using good_eats.Models;
using good_eats.Services;
using Microsoft.AspNetCore.Mvc;

namespace good_eats.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ReviewsService _reviewsService;
    private readonly AccountService _accountService;

    public ProfilesController(ReviewsService reviewsService, AccountService accountService)
    {
      _reviewsService = reviewsService;
      _accountService = accountService;
    }

    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      Profile profile = _accountService.GetProfileById(id);
      return Ok(profile);
    }


    [HttpGet("{id}/reviews")]
    public async Task<ActionResult<List<Review>>> GetReviewsByProfileAsync(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_reviewsService.GetReviewsByProfile(id, userInfo?.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}