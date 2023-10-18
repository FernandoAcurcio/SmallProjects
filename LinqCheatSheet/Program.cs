using LinqCheatSheet;

internal class Program
{
    private static void Main(string[] args)
    {
        var lawyers = new[]
        {
            new Lawyer()
            {
                FirstName = "Peter",
                LastName = "Parker"
            },
            new Lawyer()
            {
                FirstName = "Sam",
                LastName = "Patrick"
            }
        };

        var clients = new[]
        {
            new Client()
            {
                FirstName = "Paul",
                LastName = "Stone"
            },
            new Client()
            {
                FirstName = "Randy",
                LastName = "Orton"
            },
            new Client()
            {
                FirstName = "Jane",
                LastName = "Doe"
            }
        };

        var cases = new[]
        {
            new Case()
            {
                Title = "Car accident",
                AmountInDispute = 10000,
                CaseType = CaseType.Comercial,
                Client  = clients[0],
                Lawyer = lawyers[0]
            },
            new Case()
            {
                Title = "Molding flat",
                AmountInDispute = 65000,
                CaseType = CaseType.ProBono,
                Client  = clients[0],
                Lawyer = lawyers[0]
            },
            new Case()
            {
                Title = "Threat",
                AmountInDispute = 15000,
                CaseType = CaseType.Comercial,
                Client  = clients[2],
                Lawyer = lawyers[1]
            },new Case()
            {
                Title = "Robbery",
                AmountInDispute = 19000,
                CaseType = CaseType.Comercial,
                Client  = clients[1],
                Lawyer = lawyers[1]
            },
        };

        // where
        foreach (var lawyer in lawyers)
        {
            lawyer.Cases = cases.Where(c => c.Lawyer == lawyer).ToList();
        }

        foreach (var client in clients)
        {
            client.Cases = cases.Where(c => c.Client == client).ToList();
        }

        // first and single
        var first = lawyers.First(x => x.FirstName == "Peter"); // returns the first lawyer with name Peter

        try
        {
            var firstException = lawyers.First(x => x.FirstName == "Steve"); // since I don't have a lawyer named Steve this is going to throw an exception
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("There is no lawyer with that name");
            Console.WriteLine(ex.Message);
        }

        // FirstOrDefault
        var firstOrDefault = lawyers.FirstOrDefault(x => x.FirstName == "Pete<");

        // Single works like first but ensures that only a single elements matches the specified condition
        var single = lawyers.Single(x => x.FirstName == "Peter");

        try
        {
            var singleException = lawyers.Single(x => x.FirstName == "Pete");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("There is no lawyer with that name");
            Console.WriteLine(ex.Message);
        }

        try
        {
            var singleException = lawyers.Single(x => x.LastName.Contains("P"));
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("More than one element matches the condition");
            Console.WriteLine(ex.Message);
        }

        // SingleOrDefault
        // For classes thats null for value types is the default value 
        var singleordefault = lawyers.SingleOrDefault(x => x.FirstName == "Pete");

        // Any and All 
        var probono = lawyers.Where(x => x.Cases.Any(c => c.CaseType == CaseType.ProBono)); // returns any lawyer with pro bono cases
        var commercial = lawyers.Where(x => x.Cases.All(c => c.CaseType == CaseType.Comercial)); // returns all the lawyers with commercial cases

        // working with numbers
        var sumOfAmount = cases.Sum(x => x.AmountInDispute);
        var averageAmount = cases.Average(x => x.AmountInDispute);
        var biggestAmount = cases.Max(x => x.AmountInDispute);

        // OrderBy
        // Ascending
        var amountAscending = lawyers.OrderBy(x => x.Cases.Sum(c => c.AmountInDispute));
        // Descending
        var amountDescending = lawyers.OrderByDescending(x => x.Cases.Sum(c => c.AmountInDispute));

        // Select 
        var casesSelected = cases.Select(x => x.Title);
        var lawyerNames = lawyers.Select(x => $"{x.FirstName}, {x.LastName}");
        // Select returns a list of lists
        var casesPerLawyer = lawyers.Select(x => x.Cases);
        // Select returns a flattened list
        var casesPerLawyerflat = lawyers.SelectMany(x => x.Cases);

        // Fluent - Chaining Linq Queries
        var caseTitlesOfCommercialLawyers = lawyers.Where(x => x.Cases.All(c => c.CaseType == CaseType.Comercial)).SelectMany(l => l.Cases).Select(c => c.Title);


        // Order Lawyers by money in dispute for commercial cases only
        var byMoney = lawyers.OrderBy(x => x.Cases.Where(c => c.CaseType == CaseType.Comercial).Sum(c => c.AmountInDispute));

        // Select all cases from Clients as an IEnumerable<List<Case>>
        var asInumerable = clients.Select(x => x.Cases);

        // Select all cases from Clients as a flattened list
        var flattenedList = clients.SelectMany(x => x.Cases);

        // Select a list of strings containing the following fields comma separated 
        // lawyer.FirstName, lawyer.LastName, client.FirstName, client.LastName
        var selectString = cases.Select(x => $"{x.Lawyer.FirstName}, {x.Lawyer.LastName}, {x.Client.FirstName}, {x.Client.LastName}");

        Console.ReadLine();
    }
}