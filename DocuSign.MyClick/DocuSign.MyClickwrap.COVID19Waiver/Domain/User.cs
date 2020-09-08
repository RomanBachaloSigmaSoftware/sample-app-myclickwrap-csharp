namespace DocuSign.MyClickwrap.COVID19Waiver.Domain
{
    public class User
    {
        public User(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }

        public string Name { get; }
    }
}