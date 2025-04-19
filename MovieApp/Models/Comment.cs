namespace MovieApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DatePosted { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}