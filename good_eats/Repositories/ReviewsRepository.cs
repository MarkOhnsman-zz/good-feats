using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using good_eats.Models;

namespace good_eats.Repositories
{
  public class ReviewsRepository
  {
    private readonly IDbConnection _db;

    public ReviewsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Review> GetReviewsByRestaurant(int id)
    {
      string sql = @"
      SELECT
      rv.*,
      a.*
      FROM reviews rv 
      JOIN accounts a on a.id = rv.creatorId 
      WHERE rv.restaurantId = @id;";
      return _db.Query<Review, Profile, Review>(sql, (rv, a) =>
      {
        rv.Creator = a;
        return rv;
      }, new { id }).ToList();
    }

    internal Review Create(Review newReview)
    {
      string sql = @"
      INSERT INTO reviews(body, rating, restaurantId, creatorId, published)
      VALUES(@Body, @Rating, @RestaurantId, @CreatorId, @Published);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, newReview);
      newReview.Id = id;
      return newReview;
    }

    internal void Delete(int reviewId)
    {
      string sql = @"DELETE FROM reviews WHERE id = @reviewId LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new { reviewId });
      if (rowsAffected == 0)
      {
        throw new Exception("Delete failed");
      }
    }

    internal List<Review> GetReviewsByProfile(string id)
    {
      string sql = @"
      SELECT
      rv.*,
      a.*
      FROM reviews rv
      JOIN accounts a on a.id = rv.creatorId
      WHERE rv.creatorId = @id";
      return _db.Query<Review, Profile, Review>(sql, (rv, a) =>
      {
        rv.Creator = a;
        return rv;
      }, new { id }).ToList();
    }

    internal Review GetById(int reviewId)
    {
      string sql = @"
      SELECT
      rv.*,
      a.*
      FROM reviews rv
      JOIN accounts a on a.id = rv.creatorId
      WHERE rv.id = @reviewId;";
      return _db.Query<Review, Profile, Review>(sql, (rv, a) =>
      {
        rv.Creator = a;
        return rv;
      }, new { reviewId }).FirstOrDefault();
    }

    internal Review Update(Review originalReview)
    {
      string sql = @"
      UPDATE reviews
      SET
      body = @Body,
      published = @Published,
      rating = @Rating
      WHERE id = @Id;";
      var rowsAffected = _db.Execute(sql, originalReview);
      if (rowsAffected == 0)
      {
        throw new Exception("Update failed");
      }
      return originalReview;
    }
  }
}