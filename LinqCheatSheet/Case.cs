namespace LinqCheatSheet;

public enum CaseType
{
    Comercial,
    ProBono
}

public class Case
{
    public Client Client { get; set; } = default!;
    public Lawyer Lawyer { get; set; } = default!;
    public string Title { get; set; } = default!;
    public CaseType CaseType { get; set; }
    public decimal AmountInDispute  { get; set; } 
}