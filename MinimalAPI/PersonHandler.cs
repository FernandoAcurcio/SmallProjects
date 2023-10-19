namespace MinimalAPI
{
    public class PersonHandler
    {
        public Person HandlerGet()
        {
            return new Person("John", "Doe");
        }

        public Person HandlerGetById(int id)
        {
            return new Person("Peter", "Parker");
        }
    }

    public record Person(string FirstName, string LastName);
}
