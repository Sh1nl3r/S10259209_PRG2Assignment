using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10259209_PRG2Assignment
{
    internal class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }

        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee)
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