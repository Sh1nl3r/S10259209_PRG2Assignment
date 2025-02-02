//==========================================================
// Student Number : S10259209
// Student Name : Chan Shin Ler
// Partner Name : Goh Yong Ze
//==========================================================

using S10259209_PRG2Assignment;
using System.Globalization;
using System.Text.RegularExpressions;

Dictionary<string, Airline> AirlinesDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> BoardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> FlightDict = new Dictionary<string, Flight>();
Dictionary<string, string> BoardingGateStatusDict = new Dictionary<string, string>();
Dictionary<string, string?> FlightRequestCode = new Dictionary<string, string?>();
List<Flight> cancelledList = new List<Flight>();
new Terminal("Terminal 5");

// Feature 1
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

// Feature 2
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

// Feature 3
void option1()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-17}{"Expected Departure/Arrival Time",-31}");
    foreach (Flight i in FlightDict.Values)
    {
        Console.WriteLine($"{i.FlightNumber,-16}{AirlinesDict[$"{i.FlightNumber[0]}" + $"{i.FlightNumber[1]}"].Name,-23}{i.Origin,-23}{i.Destination,-17}{i.ExpectedTime,-31}");
    }
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

// Feature 5
void option3()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");
    Console.WriteLine("Enter Flight Number: \n");
    string flightnumber = Console.ReadLine().ToUpper();
    if (FlightDict.ContainsKey(flightnumber) == false)
    {
        Console.WriteLine("Flight Number does not exist!");
    }
    else if (BoardingGateStatusDict.ContainsValue(flightnumber) == true)
    {
        Console.WriteLine("This Flight is already assigned a boarding gate.");
    }
    else
    {
        while (true)
        {
            Console.WriteLine("Enter Boarding Gate Name: \n");
            string boardinggatename = Console.ReadLine().ToUpper();
            if (BoardingGateDict.ContainsKey(boardinggatename) == false)
            {
                Console.WriteLine("Boarding Gate does not exist!");
            }
            else if (BoardingGateStatusDict.ContainsKey(boardinggatename) == true)
            {
                Console.WriteLine("Boarding Gate is already assigned to a flight!");
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
                Console.WriteLine($"Flight Number: {FlightDict[flightnumber].FlightNumber}");
                Console.WriteLine($"Origin: {FlightDict[flightnumber].Origin}");
                Console.WriteLine($"Destination: {FlightDict[flightnumber].Destination}");
                Console.WriteLine($"Expected Time: {FlightDict[flightnumber].ExpectedTime}");
                if (FlightDict[flightnumber] is LWTTFlight)
                {
                    Console.WriteLine("Special Request Code: LWTT");
                }
                else if (FlightDict[flightnumber] is DDJBFlight)
                {
                    Console.WriteLine("Special Request Code: DDJB");
                }
                else if (FlightDict[flightnumber] is CFFTFlight)
                {
                    Console.WriteLine("Special Request Code: CFFT");
                }
                else
                {
                    Console.WriteLine($"Special Request Code: NONE");
                }
                Console.WriteLine($"Boarding Gate Name: {BoardingGateDict[boardinggatename].GateName}");
                Console.WriteLine($"Supports DDJB: {BoardingGateDict[boardinggatename].SupportsDDJB}");
                Console.WriteLine($"Supports CFFT: {BoardingGateDict[boardinggatename].SupportsCFFT}");
                Console.WriteLine($"Supports LWTT: {BoardingGateDict[boardinggatename].SupportsLWTT}");
                Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
                string ans = Console.ReadLine().ToUpper();
                if (ans == "Y")
                {
                    int option;
                    bool valid = false;
                    do
                    {
                        Console.WriteLine("1. Delayed");
                        Console.WriteLine("2. Boarding");
                        Console.WriteLine("3. On Time");
                        Console.Write("Please select the new status of the flight: ");

                        if (int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 3)
                        {
                            valid = true;
                            switch (option)
                            {
                                case 1:
                                    FlightDict[flightnumber].Status = "Delayed";
                                    break;
                                case 2:
                                    FlightDict[flightnumber].Status = "Boarding";
                                    break;
                                case 3:
                                    FlightDict[flightnumber].Status = "On Time";
                                    break;
                            }
                            Console.WriteLine($"Flight status updated to: {FlightDict[flightnumber].Status}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid input. Please try again!");
                        }

                    } while (!valid);
                }
                else if (ans == "N")
                {
                    FlightDict[flightnumber].Status = "On Time";
                }
                else
                {
                    Console.WriteLine("Invalid Input! The status has not been changed.");
                }
                BoardingGateStatusDict.Add(boardinggatename, flightnumber);
                Console.WriteLine($"Flight {flightnumber} has been assigned to Boarding Gate {boardinggatename}!");
                break;
            }
        }
    }
}

// Feature 6    
void option4()
{
    string flightnumber;
    Regex flightPattern = new Regex(@"^[A-Z]{2}\s\d+$");
    bool addflight = true;
    while (addflight)
    {
        do
        {
            Console.Write("Enter flight number: ");
            flightnumber = Console.ReadLine().ToUpper();

            if (string.IsNullOrEmpty(flightnumber))
            {
                Console.WriteLine("Invalid Input!");
            }
            else if (!flightPattern.IsMatch(flightnumber))
            {
                Console.WriteLine("Invalid format! (ex: MH 298)");
                flightnumber = "";
            }
            else if (FlightDict.ContainsKey(flightnumber))
            {
                Console.WriteLine("Flight number already exists! Try again.");
                flightnumber = "";
            }
        } while (string.IsNullOrEmpty(flightnumber));

        string pattern = @"^[a-zA-Z\s]+ \([A-Z]{3}\)$";

        string origin;
        do
        {
            Console.Write("Origin (ex: Singapore (SIN)): ");
            origin = Console.ReadLine();

            if (string.IsNullOrEmpty(origin) || !Regex.IsMatch(origin, pattern))
            {
                Console.WriteLine("Invalid Input!");
            }
        } while (string.IsNullOrEmpty(origin) || !Regex.IsMatch(origin, pattern));

        string destination;
        do
        {
            Console.Write("Destination (ex: Singapore (SIN)): ");
            destination = Console.ReadLine();

            if (string.IsNullOrEmpty(destination) || !Regex.IsMatch(destination, pattern))
            {
                Console.WriteLine("Invalid Input!");
            }
        } while (string.IsNullOrEmpty(destination) || !Regex.IsMatch(destination, pattern));

        bool validInput = false;
        DateTime expectedtime = DateTime.MinValue;
        while (!validInput)
        {
            try
            {
                Console.WriteLine("Expected Departure/Arrival Time (dd/MM/yyyy HH:mm): ");
                string i = Console.ReadLine();
                expectedtime = DateTime.ParseExact(i, "d/M/yyyy HH:mm", CultureInfo.InvariantCulture);
                validInput = true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Format! Try again.");
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Input! Try again.");
            }
        }

        string specialrequest;
        string[] validreq = { "CFFT", "DDJB", "LWTT", "NONE" };
        do
        {
            Console.WriteLine("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
            specialrequest = Console.ReadLine().ToUpper();
            if (!validreq.Contains(specialrequest))
            {
                Console.WriteLine("Invalid Input! Try again.");
            }
        } while (!validreq.Contains(specialrequest));

        Flight newflight;
        if (specialrequest == "LWTT")
        {
            newflight = new LWTTFlight(flightnumber, origin, destination, expectedtime, "Scheduled", 500.00);
        }
        else if (specialrequest == "DDJB")
        {
            newflight = new DDJBFlight(flightnumber, origin, destination, expectedtime, "Scheduled", 300.00);
        }
        else if (specialrequest == "CFFT")
        {
            newflight = new CFFTFlight(flightnumber, origin, destination, expectedtime, "Scheduled", 150.00);
        }
        else
        {
            newflight = new Flight(flightnumber, origin, destination, expectedtime, "Scheduled");
        }

        FlightRequestCode.Add(flightnumber, specialrequest);
        FlightDict.Add(flightnumber, newflight);
        File.AppendAllText("flights.csv", $"{newflight.FlightNumber},{newflight.Origin},{newflight.Destination},{newflight.ExpectedTime},{specialrequest}\n");

        Console.WriteLine($"Flight {flightnumber} has been added!");

        Console.WriteLine("Would you like to add another flight? (Y/N)");
        string ans = Console.ReadLine().ToUpper();
        if (ans != "Y")
        {
            addflight = false;
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

// Feature 9
void option7()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine($"{"Flight Number",-16}{"Airline Name",-23}{"Origin",-23}{"Destination",-23}{"Expected Departure/Arrival Time",-35}{"Status",-16}{"Boarding Gate"}");
    List<KeyValuePair<string, Flight>> sortedByTime = FlightDict.OrderBy(x => x.Value.ExpectedTime).ToList();
    foreach (var i in sortedByTime)
    {
        if (BoardingGateStatusDict.ContainsValue(i.Value.FlightNumber))
        {
            Console.WriteLine($"{i.Value.FlightNumber,-16}{AirlinesDict[$"{i.Value.FlightNumber[0]}" + $"{i.Value.FlightNumber[1]}"].Name,-23}{i.Value.Origin,-23}{i.Value.Destination,-23}{i.Value.ExpectedTime,-35}{i.Value.Status,-16}{BoardingGateStatusDict.FirstOrDefault(x => x.Value == i.Value.FlightNumber).Key}");
        }
        else
        {
            Console.WriteLine($"{i.Value.FlightNumber,-16}{AirlinesDict[$"{i.Value.FlightNumber[0]}" + $"{i.Value.FlightNumber[1]}"].Name,-23}{i.Value.Origin,-23}{i.Value.Destination,-23}{i.Value.ExpectedTime,-35}{i.Value.Status,-16}{"Not Assigned"}");
        }
    }

}

// Advance Feature 1
void option8()
{
    Queue<string> unassignedFlights = new Queue<string>();
    int totalFlights = FlightDict.Count;
    int totalGates = BoardingGateDict.Count;
    int asFlight = BoardingGateStatusDict.Count;
    int asGate = BoardingGateStatusDict.Values.Distinct().Count();

    foreach (var flight in FlightDict.Keys)
    {
        if (!BoardingGateStatusDict.ContainsValue(flight))
        {
            unassignedFlights.Enqueue(flight);
        }
    }
    Console.WriteLine($"Total Unassigned Flights: {unassignedFlights.Count}");

    List<string> availGate = new List<string>();
    foreach (var gate in BoardingGateDict.Keys)
    {
        if (!BoardingGateStatusDict.ContainsKey(gate))
        {
            availGate.Add(gate);
        }
    }
    Console.WriteLine($"Total Unassigned Boarding Gates: {availGate.Count}");

    int flightsAssign = 0;
    while (unassignedFlights.Count > 0 && availGate.Count > 0)
    {
        string flightNumber = unassignedFlights.Dequeue();

        Flight flight = FlightDict[flightNumber];
        string assignedGate = null;
        foreach (var gate in availGate)
        {
            if (flight is LWTTFlight && BoardingGateDict[gate].SupportsLWTT || flight is DDJBFlight && BoardingGateDict[gate].SupportsDDJB || flight is CFFTFlight && BoardingGateDict[gate].SupportsCFFT || flight is NORMFlight)
            {
                assignedGate = gate;
                break;
            }
        }
        if (assignedGate != null)
        {
            BoardingGateStatusDict[assignedGate] = flightNumber;
            availGate.Remove(assignedGate);
            flightsAssign++;
        }
    }
    Console.WriteLine($"Total Flights Assigned: {flightsAssign}");

    int finalAsFlight = BoardingGateStatusDict.Count;
    int finalAsGate = BoardingGateStatusDict.Values.Distinct().Count();
    double percentageFlights = (finalAsFlight - asFlight) / (double)totalFlights * 100;
    double percentageGates = (finalAsGate - asGate) / (double)totalGates * 100;

    Console.WriteLine($"Percentage of Flights Automatically Assigned: {percentageFlights:F2}%");
    Console.WriteLine($"Percentage of Gates Automatically Assigned: {percentageGates:F2}%");
}

//Advance Feature 2
void option9()
{
    if (BoardingGateStatusDict.Count != FlightDict.Count)
    {
        Console.WriteLine("Some Flights has not been assigned a boarding gate.");
    }
    else
    {
        foreach (Airline airline in AirlinesDict.Values)
        {
            double totalFees = 0.0;
            double feetodeduct = 0.0;
            int numofflights = 0;
            foreach (Flight flight in FlightDict.Values)
            {
                if ($"{flight.FlightNumber[0]}" + $"{flight.FlightNumber[1]}" == airline.Code)
                {
                    totalFees = totalFees + flight.CalculateFees() + 300;
                    DateTime before11 = DateTime.Today.AddHours(11);
                    DateTime after9 = DateTime.Today.AddHours(21);
                    if (flight.ExpectedTime < before11 && flight.ExpectedTime > after9)
                    {
                        feetodeduct += 110.00;
                    }
                    if (flight.Origin == "Dubai (DXB)" || flight.Origin == "Bangkok (BKK)" || flight.Origin == "Tokyo (NRT)")
                    {
                        feetodeduct += 25;
                    }
                    if (flight.Status == "")
                    {
                        feetodeduct += 50;
                    }
                    numofflights++;
                }
            }

            if (numofflights > 5)
            {
                totalFees *= 0.97;
            }
            totalFees -= feetodeduct;
            if (numofflights >= 3)
            {
                totalFees = totalFees - ((numofflights % 3) * 350);
            }
            Console.WriteLine($"Total Fees for {airline.Name}: ${totalFees:N}");

        }
    }
}

//Additional Feature 2
void option11()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Daily Flight Report");
    Console.WriteLine("=============================================");

    int totalFlights = FlightDict.Count;
    int onTime = 0;
    int delayed = 0;
    int cancelled = 0;

    foreach (var flight in FlightDict.Values)
    {
        switch (flight.Status.ToLower())
        {
            case "scheduled":
                onTime++;
                break;
            case "delayed":
                delayed++;
                break;
        }
    }
    foreach (var flight in cancelledList)
    {
        cancelled++;
        totalFlights++;
    }

    Console.WriteLine($"Total Active Flights: {totalFlights}");
    Console.WriteLine($"On Time: {onTime} | Delayed: {delayed} | Cancelled: {cancelled}");
    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Status Percentages");
    Console.WriteLine($"On Time: {(double)onTime / totalFlights * 100:F2}% | Delayed: {(double)delayed / totalFlights * 100:F2}% | Cancelled: {(double)cancelled / totalFlights * 100:F2}%");
    Console.WriteLine("=============================================");

    Console.WriteLine("1. Show Flight Report by Airline");
    Console.WriteLine("2. Show Cancelled Flights by Airline");
    string option = Console.ReadLine();

    if (option == "1")
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
            int numofflights = 0;
            int ontimef = 0;
            int delayedf = 0;
            int cancelledf = 0;
            Airline airline = AirlinesDict[airlinecode];
            foreach (Flight flight in FlightDict.Values)
            {
                if ($"{flight.FlightNumber[0]}" + $"{flight.FlightNumber[1]}" == airlinecode)
                {
                    numofflights++;
                    switch (flight.Status.ToLower())
                    {
                        case "scheduled":
                            ontimef++;
                            break;
                        case "delayed":
                            delayedf++;
                            break;
                    }
                }
            }

            foreach (var flight in cancelledList)
            {
                if (flight.FlightNumber.StartsWith(airlinecode, StringComparison.OrdinalIgnoreCase))
                {
                    cancelledf++;
                    numofflights++;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Airline: {airline.Name}");
            Console.WriteLine("=============================================");
            Console.WriteLine($"Total Flights: {numofflights}");
            Console.WriteLine($"On Time: {ontimef} | Delayed: {delayedf} | Cancelled: {cancelledf}");
            Console.WriteLine($"On Time: {(double)ontimef / numofflights * 100:F2}% | Delayed: {(double)delayedf / numofflights * 100:F2}% | Cancelled: {(double)cancelledf / numofflights * 100:F2}%");
            Console.WriteLine("=============================================");
        }
        else
        {
            Console.WriteLine("Invalid Input!");
        }
    }
    else if (option == "2")
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
            if (cancelledList.Count == 0)
            {
                Console.WriteLine("No Cancelled Flights!");
            }
            else
            {
                Console.WriteLine(" ");
                Console.WriteLine("=============================================");
                Console.WriteLine($"Airline: {AirlinesDict[airlinecode].Name}");
                Console.WriteLine("=============================================");
                foreach (var flight in cancelledList)
                {
                    if (flight.FlightNumber.StartsWith(airlinecode, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                        Console.WriteLine($"Origin: {flight.Origin}");
                        Console.WriteLine($"Destination: {flight.Destination}");
                        Console.WriteLine($"Expected Time: {flight.ExpectedTime}");
                        Console.WriteLine("=============================================");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid Input!");
        }
    }
    else
    {
        Console.WriteLine("Invalid Input!");
    }
}


Console.WriteLine("Loading Airlines...");
Console.WriteLine($"{AirlinesDict.Count} Airlines Loaded!");
Console.WriteLine("Loading Boarding Gates...");
Console.WriteLine($"{BoardingGateDict.Count} Boarding Gates Loaded!");
Console.WriteLine("Loading Flights...");
Console.WriteLine($"{FlightDict.Count} Flights Loaded!\n\n\n");

while (true)
{
    displaymenu();
    Console.Write("\nPlease select your option: ");
    string? option = Console.ReadLine();
    if (option == "1")
    {
        option1();
    }
    else if (option == "2")
    {
        option2();
    }
    else if (option == "3")
    {
        option3();
    }
    else if (option == "4")
    {
        option4();
    }
    else if (option == "5")
    {
        option5();
    }
    else if (option == "6")
    {
        option6();
    }
    else if (option == "7")
    {
        option7();
    }
    else if (option == "8")
    {
        option8();
    }
    else if (option == "9")
    {
        option9();
    }
    else if (option == "10")
    {
        option10();
    }
    else if (option == "11")
    {
        option11();
    }
    else if (option == "0")
    {
        Console.WriteLine("GoodBye!");
        break;
    }
    else
    {
        Console.WriteLine("Invalid Input! Please try again!");
    }
}