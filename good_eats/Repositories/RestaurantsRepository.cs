using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using good_eats.Models;

namespace good_eats.Repositories
{
  public class RestaurantsRepository
  {
    private readonly IDbConnection _db;

    public RestaurantsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Restaurant> GetAll()
    {
      // NOTE left join will keep the data from the left table intact if there is no match, rather than failing like an join would if the data was null
      string sql = @"
      SELECT
      rs.*,
      AVG(rv.rating) AS AverageRating,
      COUNT(rv.id) AS TotalReviews
      FROM restaurants rs
      LEFT JOIN reviews rv on rv.restaurantId = rs.id AND rv.published = 1
      GROUP BY rs.id;";
      return _db.Query<Restaurant>(sql).ToList();
    }

    internal Restaurant GetById(int id)
    {
      string sql = @"
      SELECT
      rs.*,
      AVG(rv.rating) AS AverageRating,
      COUNT(rv.id) AS TotalReviews
      FROM restaurants rs
      JOIN reviews rv on rv.restaurantId = rs.id
      WHERE rs.id = @id;";
      return _db.Query<Restaurant>(sql, new { id }).FirstOrDefault();
    }
  }
}