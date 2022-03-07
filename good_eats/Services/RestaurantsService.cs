using System;
using System.Collections.Generic;
using good_eats.Models;
using good_eats.Repositories;

namespace good_eats.Services
{
  public class RestaurantsService
  {
    private readonly RestaurantsRepository _repo;

    public RestaurantsService(RestaurantsRepository repo)
    {
      _repo = repo;
    }

    internal List<Restaurant> GetAll()
    {
      return _repo.GetAll();
    }

    internal Restaurant GetById(int id)
    {
      Restaurant foundRestaurant = _repo.GetById(id);
      if (foundRestaurant == null)
      {
        throw new Exception("Unable to find that restaurant");
      }
      return foundRestaurant;
    }
  }
}