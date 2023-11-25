namespace SchoolProject.Core.Feature.User.Queries.Result
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }
}