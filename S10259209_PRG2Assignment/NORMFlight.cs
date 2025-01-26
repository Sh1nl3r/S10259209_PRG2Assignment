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
    internal class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status)
            : base(flightNumber, origin, destination, expectedTime, status)
        {
        }
        public override double CalculateFees()
        {
            if (Destination == "Singapore (SIN)")
            {
                return 500.00;
            }
            else
            {
                return 800.00;
            }
        }
        public override string ToString()
        {
            return $"Fees: ${CalculateFees()}";
        }
    }
}
