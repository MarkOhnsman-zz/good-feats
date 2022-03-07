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
  public class RestaurantsController : ControllerBase
  {
    private readonly RestaurantsService _restaurantsService;
    private readonly ReviewsService _reviewsService;

    public RestaurantsController(RestaurantsService restaurantsService, ReviewsService reviewsService)
    {
      _restaurantsService = restaurantsService;
      _reviewsService = reviewsService;
    }

    [HttpGet]
    public ActionResult<Restaurant> GetAll()
    {
      try
      {
        return Ok(_restaurantsService.GetAll());
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Restaurant> GetById(int id)
    {
      try
      {
        return Ok(_restaurantsService.GetById(id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}/reviews")]

    public async Task<ActionResult<List<Review>>> GetReviewsByRestaurantAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_reviewsService.GetReviewsByRestaurant(id, userInfo?.Id));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}