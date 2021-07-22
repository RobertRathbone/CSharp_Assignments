using System;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class WeddingAttendees
    {
        [Key]
        public int WeddingAttendeeId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public int UserId {get; set; }
        public User Attendee { get; set; }
        public int WeddingId { get; set; }
        public Wedding Wedding { get; set; }
    }
}