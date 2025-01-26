//==========================================================
// Student Number : S10259209
// Student Name : Chan Shin Ler
// Partner Name : Goh Yong Ze
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10259209_PRG2Assignment
{
    internal class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }

        public Airline() { }
        public Airline(string name, string code)
        {
            this.Name = name;
            this.Code = code;
            Flights = new Dictionary<string, Flight>();
        }
        public bool AddFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                return false;
            }
            else
            {
                Flights.Add(flight.FlightNumber, flight);
                return true;
            }
        }
        public double CalculateFees()
        {
            double totalFees = 0.0;
            double feetodeduct = 0.0;
            foreach (Flight flight in Flights.Values)
            {
                totalFees += flight.CalculateFees();
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
            }
            if (Flights.Count > 5)
            {
                totalFees *= 0.97;
            }
            totalFees -= feetodeduct;
            if (Flights.Count >= 3)
            {
                totalFees = totalFees - ((Flights.Count % 3) * 350);
            }
            return totalFees;
        }
        public bool RemoveFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Remove(flight.FlightNumber);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $"Name: {Name}, Code: {Code}, Total Fees: {CalculateFees():F2}";
        }
    }
}
