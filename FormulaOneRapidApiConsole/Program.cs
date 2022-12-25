using Newtonsoft.Json;

namespace FormulaOneRapidApiConsole;

class Program
{
    public static async Task Main(string[] args)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://fia-formula-1-championship-statistics.p.rapidapi.com/api/teams"),
            Headers =
    {
        { "X-RapidAPI-Key", "KEY" },
        { "X-RapidAPI-Host", "fia-formula-1-championship-statistics.p.rapidapi.com" },
    },
        };
        using (var response = await client.SendAsync(request))
        {
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            dynamic jsonObj = JsonConvert.DeserializeObject<Rootobject>(body) ?? throw new Exception();

            foreach (var obj in jsonObj.teams)
            {
                Console.WriteLine("Team Name: " + obj.teamName);
                Console.WriteLine("Rank: " + obj.rank.standing);
                Console.WriteLine("Point: " + obj.points.pts);
                Console.WriteLine("1. Driver Name: " + obj.drivers[0].firstname);
                Console.WriteLine("2. Driver Name: " + obj.drivers[1].firstname);
                Console.WriteLine("-------------------");
            }
        }
    }
}

public class Rootobject
{
    public Team[]? teams { get; set; }
    public int httpStatusCode { get; set; }
}

public class Team
{
    public Rank? rank { get; set; }
    public Points? points { get; set; }
    public string? teamName { get; set; }
    public Driver[]? drivers { get; set; }
}

public class Rank
{
    public int standing { get; set; }
}

public class Points
{
    public int pts { get; set; }
}

public class Driver
{
    public string? firstname { get; set; }
    public string? lastname { get; set; }
    public object? abbr { get; set; }
}
