using System;
using System.ComponentModel.DataAnnotations;

namespace good_eats.Models
{
  public class Review
  {
    public int Id { get; set; }
    public string Body { get; set; }
    [Range(1, 5)]
    public int? Rating { get; set; }
    public int RestaurantId { get; set; }
    public string CreatorId { get; set; }
    public bool? Published { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Profile Creator { get; set; }
  }
}