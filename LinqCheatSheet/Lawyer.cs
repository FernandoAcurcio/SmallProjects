namespace LinqCheatSheet;

public class Lawyer
{
    public List<Case> Cases { get; set; } = default!; // null forgiving operator
    public string FirstName { get; set; } = default!; // null forgiving operator
    public string LastName { get; set; } = default!; // null forgiving operator
}
