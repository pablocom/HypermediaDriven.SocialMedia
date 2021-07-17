namespace AccountManagement.WebApi.Controllers
{
    public class Link
    {
        public string Href { get; init; }
        public string Relationship { get; init; }

        public Link(string href, string relationship)
        {
            Href = href;
            Relationship = relationship;
        }
    }
}