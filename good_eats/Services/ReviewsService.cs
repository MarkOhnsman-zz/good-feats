using System;
using System.Collections.Generic;
using good_eats.Models;
using good_eats.Repositories;

namespace good_eats.Services
{
  public class ReviewsService
  {
    private readonly ReviewsRepository _repo;

    public ReviewsService(ReviewsRepository repo)
    {
      _repo = repo;
    }

    internal List<Review> GetReviewsByRestaurant(int id, string userId)
    {
      List<Review> foundReviews = _repo.GetReviewsByRestaurant(id);
      return foundReviews.FindAll(r => r.Published == true || r.CreatorId == userId);
    }

    internal Review Create(Review newReview)
    {
      // NOTE we could call out to the restaurants service, grab object, and modify it here
      return _repo.Create(newReview);
    }

    internal void Delete(int reviewId, string userId)
    {
      Review foundReview = GetById(reviewId);
      if (foundReview.CreatorId != userId)
      {
        throw new Exception("Unauthorized to delete");
      }
      _repo.Delete(reviewId);
    }

    internal Review GetById(int reviewId)
    {
      Review foundReview = _repo.GetById(reviewId);
      if (foundReview == null)
      {
        throw new Exception("Unable to find that review");
      }
      return foundReview;
    }

    internal List<Review> GetReviewsByProfile(string id, string userId)
    {
      List<Review> reviews = _repo.GetReviewsByProfile(id);
      return reviews.FindAll(r => r.Published == true || r.CreatorId == userId);
    }

    internal Review Update(Review updatedReview, string userId)
    {
      Review originalReview = GetById(updatedReview.Id);
      if (originalReview.CreatorId != userId)
      {
        throw new Exception("Unauthorized to update");
      }
      // NOTE ?? is null coalescence
      originalReview.Body = updatedReview.Body ?? originalReview.Body;
      originalReview.Published = updatedReview.Published ?? originalReview.Published;
      originalReview.Rating = updatedReview.Rating ?? originalReview.Rating;
      return _repo.Update(originalReview);
    }
  }
}