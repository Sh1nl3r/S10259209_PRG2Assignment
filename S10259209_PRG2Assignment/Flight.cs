//==========================================================
// Student Number : S10259209
// Student Name : Chan Shin Ler
// Partner Name : Goh Yong Ze
//==========================================================

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10259209_PRG2Assignment
{
    internal class Flight : IComparable<Flight>
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }

        public Flight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
        {
            this.FlightNumber = flightNumber;
            this.Origin = origin;
            this.Destination = destination;
            this.ExpectedTime = expectedTime;
            if (status == "" || status == "NONE")
            {
                status = "Scheduled";
                this.Status = status;
            }
            else
            {
                this.Status = status;
            }
        }

        public virtual double CalculateFees()
        {
            return 0;
        }
        public int CompareTo(Flight other)
        {
            return this.ExpectedTime.CompareTo(other.ExpectedTime);
        }
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Expected Time: {ExpectedTime}, Status: {Status}";
        }
    }
}
