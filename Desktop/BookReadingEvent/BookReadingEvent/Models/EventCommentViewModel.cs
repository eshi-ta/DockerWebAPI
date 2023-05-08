namespace BookReadingEvent.Models
{
    public class EventCommentViewModel
    {
        public Event Event { get; set; }
        public Comment Comment { get; set; }
        public List<Comment> Comments { get; set; }



    }
}