using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10259209_PRG2Assignment
{
    internal class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        public BoardingGate(string gateName, bool supportsDDJB, bool supportsCFFT, bool supportsLWTT)
        {
            this.GateName = gateName;
            this.SupportsDDJB = supportsDDJB;
            this.SupportsCFFT = supportsCFFT;
            this.SupportsLWTT = supportsLWTT;
        }

        public double CalculateFees()
        {
            double fees = 300.0;

            if (SupportsCFFT == true)
            {
                fees += 150.0;
            }
            else if (SupportsDDJB == true)
            {
                fees += 300.0;
            }
            else if (SupportsLWTT == true)
            {
                fees += 500.0;
            }

            return fees;
        }
        public override string ToString()
        {
            if (SupportsCFFT == true)
            {
                return $"Gate Name: {GateName}, Supports CFFT Fees: {CalculateFees()}";
            }
            else if (SupportsDDJB == true)
            {
                return $"Gate Name: {GateName}, Supports DDJB Fees: {CalculateFees()}";
            }
            else if (SupportsLWTT == true)
            {
                return $"Gate Name: {GateName}, Supports LWTT Fees: {CalculateFees()}";
            }
            else
            {
                return $"Gate Name: {GateName}, Fee: {CalculateFees()}";
            }
        }
    }
}
