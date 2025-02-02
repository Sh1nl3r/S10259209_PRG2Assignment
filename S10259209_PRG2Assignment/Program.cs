//==========================================================
// Student Number : S10259209
// Student Name : Chan Shin Ler
// Partner Name : Goh Yong Ze
//==========================================================

using S10259209_PRG2Assignment;
using System.Globalization;

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

















// Feature 7
void option5()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-16}{"Airline Name"}");
    foreach (Airline i in AirlinesDict.Values)
    {
        Console.WriteLine($"{i.Code,-16}{i.Name}");
    }
    Console.WriteLine("Enter Airline Code: ");
    string airlinecode = Console.ReadLine().ToUpper();
    if (AirlinesDict.ContainsKey(airlinecode))
    {
        Airline selectAirline = AirlinesDict[airlinecode];

        Console.WriteLine("=============================================");
        Console.WriteLine($"List of Flights for {selectAirline.Name}");
        Console.WriteLine("=============================================");
        Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}{"Expected Departure/Arrival Time",-31}");
        foreach (Flight i in FlightDict.Values)
        {
            if ($"{i.FlightNumber[0]}" + $"{i.FlightNumber[1]}" == airlinecode)
            {
                Console.WriteLine($"{i.FlightNumber,-16}{selectAirline.Name,-23}{i.Origin,-23}{i.Destination,-23}{i.ExpectedTime,-31}");
            }
        }
        Console.Write("Select a flight number: ");
        string selectedflightnum = Console.ReadLine().ToUpper();
        if (string.IsNullOrEmpty(selectedflightnum) || !FlightDict.ContainsKey(selectedflightnum))
        {
            Console.WriteLine("Invalid Input!");
        }
        else
        {
            if (BoardingGateStatusDict.FirstOrDefault(x => x.Value == selectedflightnum).Key == null)
            {
                Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-21}{"Expected Departure/Arrival Time",-34}{"Special Request",-18}{"Boarding Gate",-13}");
                Console.WriteLine($"{selectedflightnum,-16}{selectAirline.Name,-23}{FlightDict[selectedflightnum].Origin,-23}{FlightDict[selectedflightnum].Destination,-21}{FlightDict[selectedflightnum].ExpectedTime,-34}{FlightRequestCode[selectedflightnum],-18}{"Not Assigned",-13}");
            }
            else
            {
                Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-21}{"Expected Departure/Arrival Time",-34}{"Special Request",-18}{"Boarding Gate",-13}");
                Console.WriteLine($"{selectedflightnum,-16}{selectAirline.Name,-23}{FlightDict[selectedflightnum].Origin,-23}{FlightDict[selectedflightnum].Destination,-21}{FlightDict[selectedflightnum].ExpectedTime,-34}{FlightRequestCode[selectedflightnum],-18}{BoardingGateStatusDict.FirstOrDefault(x => x.Value == selectedflightnum).Key,-13}");
            }
        }
    }
    else
    {
        Console.WriteLine("Invalid Input!");
    }
}

// Feature 8
void option6()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Modify Flight Details");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Airline Code",-16}{"Airline Name"}");
    foreach (Airline i in AirlinesDict.Values)
    {
        Console.WriteLine($"{i.Code,-16}{i.Name}");
    }
    Console.WriteLine("Enter Airline Code: ");
    string airlinecode = Console.ReadLine().ToUpper();
    if (AirlinesDict.ContainsKey(airlinecode))
    {
        Airline selectAirline = AirlinesDict[airlinecode];

        Console.WriteLine($"List of Flights for {selectAirline.Name}");
        Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}{"Expected Departure/Arrival Time",-31}");
        foreach (Flight i in FlightDict.Values)
        {
            if ($"{i.FlightNumber[0]}" + $"{i.FlightNumber[1]}" == airlinecode)
            {
                Console.WriteLine($"{i.FlightNumber,-16}{selectAirline.Name,-23}{i.Origin,-23}{i.Destination,-23}{i.ExpectedTime,-31}");
            }
        }
        while (true)
        {
            Console.WriteLine("Choose an existing Flight to modify or delete: ");
            string flightnumber = Console.ReadLine().ToUpper();
            if (FlightDict.ContainsKey(flightnumber) == false)
            {
                Console.WriteLine("Flight Number does not exist!");
            }
            else if (!string.IsNullOrEmpty(flightnumber))
            {
                while (true)
                {
                    Console.WriteLine("1. Modify Flight Details");
                    Console.WriteLine("2. Delete Flight");
                    Console.WriteLine("Choose an option: ");
                    string option = Console.ReadLine();
                    if (option == "1")
                    {
                        while (true)
                        {
                            Console.WriteLine("1. Modify Basic Information");
                            Console.WriteLine("2. Modify Status");
                            Console.WriteLine("3. Modify Special Request Code");
                            Console.WriteLine("4. Modify Boarding Gate");
                            Console.WriteLine("Choose an option: ");
                            string op = Console.ReadLine();

                            if (op == "1")
                            {
                                DateTime expectedtime;

                                Console.WriteLine("Enter new Origin: ");
                                string origin = Console.ReadLine();
                                while (string.IsNullOrEmpty(origin))
                                {
                                    Console.WriteLine("Origin cannot be empty. Try again: ");
                                    origin = Console.ReadLine();
                                }

                                Console.WriteLine("Enter new Destination: ");
                                string destination = Console.ReadLine();
                                while (string.IsNullOrEmpty(destination))
                                {
                                    Console.WriteLine("Destination cannot be empty. Try again: ");
                                    destination = Console.ReadLine();
                                }

                                Console.WriteLine("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm):");
                                DateTime expectedTime = DateTime.MinValue;
                                bool validT = false;
                                while (!validT)
                                {
                                    Console.Write("Enter new Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                                    string timeInput = Console.ReadLine();

                                    if (DateTime.TryParseExact(timeInput, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out expectedTime))
                                    {
                                        validT = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Format!");
                                    }
                                }

                                FlightDict[flightnumber].Origin = origin;
                                FlightDict[flightnumber].Destination = destination;
                                FlightDict[flightnumber].ExpectedTime = expectedTime;

                                Console.WriteLine("Flight updated!");
                                break;
                            }

                            else if (op == "2")
                            {
                                while (true)
                                {
                                    Console.WriteLine("1. Delayed");
                                    Console.WriteLine("2. Boarding");
                                    Console.WriteLine("3. On Time");
                                    Console.WriteLine("Please select the new status of the flight: ");
                                    string statup = Console.ReadLine();
                                    if (statup == "1")
                                    {
                                        FlightDict[flightnumber].Status = "Delayed";
                                        break;
                                    }
                                    else if (statup == "2")
                                    {
                                        FlightDict[flightnumber].Status = "Boarding";
                                        break;
                                    }
                                    else if (statup == "3")
                                    {
                                        FlightDict[flightnumber].Status = "On Time";
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input! Try again.");
                                    }
                                }
                                break;
                            }
                            else if (op == "3")
                            {
                                while (true)
                                {
                                    Console.WriteLine("Enter new Special Request Code: ");
                                    string specialrequest = Console.ReadLine().ToUpper();
                                    if (specialrequest == "CFFT" || specialrequest == "DDJB" || specialrequest == "LWTT" || specialrequest == "NONE")
                                    {
                                        FlightRequestCode[flightnumber] = specialrequest;
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Input! Try again.");
                                    }
                                }
                                break;
                            }
                            else if (op == "4")
                            {
                                while (true)
                                {
                                    Console.WriteLine("Enter new Boarding Gate Name: ");
                                    string boardinggatename = Console.ReadLine();
                                    if (BoardingGateDict.ContainsKey(boardinggatename) == false)
                                    {
                                        Console.WriteLine("Boarding Gate does not exist!");
                                    }
                                    else if (BoardingGateStatusDict.ContainsKey(boardinggatename) == true)
                                    {
                                        Console.WriteLine("Boarding Gate is already assigned to a flight!");
                                    }
                                    else if (BoardingGateStatusDict.ContainsValue(flightnumber))
                                    {
                                        Console.WriteLine("This Flight is already assigned a boarding gate.");
                                    }
                                    else if (FlightDict[flightnumber] is LWTTFlight && BoardingGateDict[boardinggatename].SupportsLWTT == false)
                                    {
                                        Console.WriteLine($"This gate does not support LWTT flights.");
                                    }
                                    else if (FlightDict[flightnumber] is DDJBFlight && BoardingGateDict[boardinggatename].SupportsDDJB == false)
                                    {
                                        Console.WriteLine($"This gate does not support DDJB flights.");
                                    }
                                    else if (FlightDict[flightnumber] is CFFTFlight && BoardingGateDict[boardinggatename].SupportsCFFT == false)
                                    {
                                        Console.WriteLine($"This gate does not support CFFT flights.");
                                    }
                                    else
                                    {
                                        BoardingGateStatusDict.Add(boardinggatename, flightnumber);
                                        Console.WriteLine($"Flight {flightnumber} has been assigned to Boarding Gate {boardinggatename}!");
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input!");
                                continue;
                            }

                            Console.WriteLine("Flight updated!");
                            Console.WriteLine($"Flight Number: {flightnumber}");
                            Console.WriteLine($"Airline Name: {selectAirline.Name}");
                            Console.WriteLine($"Origin: {FlightDict[flightnumber].Origin}");
                            Console.WriteLine($"Destination: {FlightDict[flightnumber].Destination}");
                            Console.WriteLine($"Expected Departure/Arrival Time: {FlightDict[flightnumber].ExpectedTime}");
                            Console.WriteLine($"Status: {FlightDict[flightnumber].Status}");
                            Console.WriteLine($"Special Request Code: {FlightRequestCode[flightnumber]}");
                            if (BoardingGateStatusDict.FirstOrDefault(x => x.Value == flightnumber).Key != null)
                            {
                                Console.WriteLine($"Boarding Gate: {BoardingGateStatusDict.FirstOrDefault(x => x.Value == flightnumber).Key}");
                            }
                            else
                            {
                                Console.WriteLine("Boarding Gate: Unassigned");
                            }
                        }
                        break;
                    }
                    else if (option == "2")
                    {
                        while (true)
                        {
                            Console.WriteLine("Are you sure you want to delete this flight? (Y/N)");
                            if (Console.ReadLine().ToUpper() == "Y")
                            {
                                cancelledList.Add(FlightDict[flightnumber]);
                                selectAirline.RemoveFlight(FlightDict[flightnumber]);
                                FlightDict.Remove(flightnumber);
                                FlightRequestCode.Remove(flightnumber);
                                if (BoardingGateStatusDict.ContainsValue(flightnumber))
                                {
                                    BoardingGateStatusDict.Remove(BoardingGateStatusDict.FirstOrDefault(x => x.Value == flightnumber).Key);
                                }
                                Console.WriteLine("Flight Deleted!");
                                break;
                            }
                            else if (Console.ReadLine().ToUpper() == "N")
                            {
                                Console.WriteLine("Flight not deleted!");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid Input!");
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input!");
                    }
                }

            }
            else
            {
                Console.WriteLine("Flight Number does not exist!");
                continue;
            }

            break;
        }
    }
    else
    {
        Console.WriteLine("The airline code does not exist.");
    }
}
