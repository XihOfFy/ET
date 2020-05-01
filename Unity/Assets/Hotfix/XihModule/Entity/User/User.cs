namespace ETHotfix
{
    public class User : Entity
    {
        public string Name { get; set; }
        public User(string name) {
            Name = name;
        }
    }
}
