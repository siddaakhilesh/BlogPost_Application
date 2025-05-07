namespace MVC_Application.Models.Domain
{
    public class BlogPostLike
    {
        public Guid Id { get; set; }
        public Guid BlogPostId { get; set; }
        public string BlogPostTitle { get; set; }

    }
}
