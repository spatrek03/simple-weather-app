using Newtonsoft.Json;
using System.Net.Http;

/* 
    Author: Sápi József Patrik
    Description: It's the simplest C# console weather application. This is my first C# project.
*/

bool success = false;

while (!success)
{

    Console.WriteLine("Kérlek add meg a hely pontos megnevezését:");
    string location = Console.ReadLine();

    string query = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{location}?unitGroup=metric&key=ZJBKKEUVSP3GNJW6V4ZFA2DCJ&contentType=json&lang=hu";

    var client = new HttpClient();

    var request = new HttpRequestMessage(HttpMethod.Get, query);

    var response = await client.SendAsync(request);
    if(response.IsSuccessStatusCode)
    { 
        var body = await response.Content.ReadAsStringAsync(); 

        dynamic weather = JsonConvert.DeserializeObject(body);

        foreach (var day in weather.days)
        {
            string weather_address = weather.resolvedAddress;
            string weather_date = day.datetime;
            string weather_desc = day.description;
            string weather_tmax = day.tempmax;
            string weather_tmin = day.tempmin;

            Console.WriteLine("Hely: " + weather_address);
            Console.WriteLine("Dátum: " + weather_date);
            Console.WriteLine("Általános információk: " + weather_desc);
            Console.WriteLine("A legnagyobb hőmérséklet: " + weather_tmax + " °C");
            Console.WriteLine("A legkisebb hőmérséklet:: " + weather_tmin + " °C");
            Console.WriteLine("");
        }
    }
    else
    {
        Console.WriteLine("Nem található ilyen hely. Szeretnél másik helyet megadni? (igen/nem)");
        string answer = Console.ReadLine();
        if (answer.ToLower() == "nem")
        {
            success = true;
        }
    }
}