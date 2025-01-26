using S10259209_PRG2Assignment;


Dictionary<string, Airline> AirlinesDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> BoardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> FlightDict = new Dictionary<string, Flight>();

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
        FlightDict.Add(data[0], new Flight(data[0], data[1], data[2], DateTime.Parse(data[3]), data[4]));
    }
}