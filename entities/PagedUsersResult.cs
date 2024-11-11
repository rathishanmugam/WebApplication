using WebApplication.Entities;

namespace WebApplication.Models
{
    public class PagedUsersResult
    {
        public required IEnumerable<User> Users { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
         public bool HasNextPage { get; set; }
      public bool HasPreviousPage { get; set; }
    }
}