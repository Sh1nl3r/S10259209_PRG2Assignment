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
    internal class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }

        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee)
            : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }
        public override double CalculateFees()
        {
            if (Destination == "Singapore (SIN)")
            {
                return 500.00 + RequestFee;
            }
            else
            {
                return 800.00 + RequestFee;
            }
        }
        public override string ToString()
        {
            return $"Fees: {CalculateFees()}";
        }
    }
}
