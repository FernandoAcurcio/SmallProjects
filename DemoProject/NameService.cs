namespace DemoProject
{
    public class NameService
    {
        private string[] _names { get; } = new[]
        {
            "Patrick",
            "John",
            "Steve",
            "Maria",
            "Silvia"
        };

        private Random _random = new Random();

        public string GetRandomName()
        {
            return _names[_random.Next(_names.Length)];
        }
    }
}
