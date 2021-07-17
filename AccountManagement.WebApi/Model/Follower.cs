namespace AccountManagement.WebApi.Model
{
    public class Follower
    {
        public string Name { get; }

        public Follower(string name) => Name = name;
    }
}