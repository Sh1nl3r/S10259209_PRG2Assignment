//==========================================================
// Student Number : S10259209
// Student Name : Chan Shin Ler
// Partner Name : Goh Yong Ze
//==========================================================

using S10259209_PRG2Assignment;

Dictionary<string, Airline> AirlinesDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> BoardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> FlightDict = new Dictionary<string, Flight>();
Dictionary<string, string> BoardingGateStatusDict = new Dictionary<string, string>();
Dictionary<string, string?> FlightRequestCode = new Dictionary<string, string?>();
List<Flight> cancelledList = new List<Flight>();
new Terminal("Terminal 5");

using (StreamReader sr = new StreamReader("airlines.csv"))
{
    sr.ReadLine();
    string line;
    while ((line = sr.ReadLine()) != null)
    {
        string[] data = line.Split(",");
        AirlinesDict.Add(data[1], new Airline(data[0], data[1]));
    }
}
using (StreamReader sr = new StreamReader("boardinggates.csv"))
{
    sr.ReadLine();
    string line;
    while ((line = sr.ReadLine()) != null)
    {
        string[] data = line.Split(",");
        BoardingGateDict.Add(data[0], new BoardingGate(data[0], bool.Parse(data[1]), bool.Parse(data[2]), bool.Parse(data[3])));
    }
}
using (StreamReader sr = new StreamReader("flights.csv"))
{
    sr.ReadLine();
    string line;
    while ((line = sr.ReadLine()) != null)
    {
        string[] data = line.Split(",");
        FlightRequestCode.Add(data[0], data[4]);
        if (data[4] == "LWTT")
        {
            Flight newflight = new LWTTFlight(data[0], data[1], data[2], DateTime.Parse(data[3]), "Scheduled", 500.00);
            FlightDict.Add(data[0], newflight);
        }
        else if (data[4] == "DDJB")
        {
            Flight newflight = new DDJBFlight(data[0], data[1], data[2], DateTime.Parse(data[3]), "Scheduled", 300.00);
            FlightDict.Add(data[0], newflight);
        }
        else if (data[4] == "CFFT")
        {
            Flight newflight = new CFFTFlight(data[0], data[1], data[2], DateTime.Parse(data[3]), "Scheduled", 150.00);
            FlightDict.Add(data[0], newflight);
        }
        else
        {
            Flight newflight = new NORMFlight(data[0], data[1], data[2], DateTime.Parse(data[3]), "Scheduled");
            FlightDict.Add(data[0], newflight);
        }
    }
}


void displaymenu()
{
    Console.WriteLine("\n=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("8. Auto-Assign Boarding Gates");
    Console.WriteLine("9. Display Fees of Each Airlines");
    Console.WriteLine("10. Display flights by location");
    Console.WriteLine("11. Generate a flight Report");
    Console.WriteLine("0. Exit");
}


// Feature 4
void option2()
{
    Console.WriteLine($"{"Gate Name",-16}{"DDJB",-23}{"CFFT",-23}{"LWTT",-17}{"Status",-10}");
    foreach (BoardingGate i in BoardingGateDict.Values)
    {
        if (BoardingGateStatusDict.ContainsKey(i.GateName) == false)
        {
            Console.WriteLine($"{i.GateName,-16}{i.SupportsDDJB,-23}{i.SupportsCFFT,-23}{i.SupportsLWTT,-17}{"Open",-10}");
        }
        else
        {
            Console.WriteLine($"{i.GateName,-16}{i.SupportsDDJB,-23}{i.SupportsCFFT,-23}{i.SupportsLWTT,-17}{BoardingGateStatusDict[i.GateName],-10}");
        }
    }
}