using System;
using MicrowaveOvenClasses.Interfaces;

namespace MicrowaveOvenClasses.Boundary
{
    public class PowerTube : IPowerTube
    {
        private IOutput myOutput;
        private readonly double MAX_WATTAGE = 700.0;
        private bool IsOn = false;

        public PowerTube(IOutput output)
        {
            myOutput = output;
        }

        public void TurnOn(int power)
        {
            double percentPower = (power/MAX_WATTAGE)*100;//FEJL. Powertube modtog power, men forventede procent. 
            if (percentPower < 1.0 || 100.0 < percentPower)
            {
                throw new ArgumentOutOfRangeException("power", power, "Must be between 1 and 100 % (incl.)");
            }

            if (IsOn)
            {
                throw new ApplicationException("PowerTube.TurnOn: is already on");
            }

            myOutput.OutputLine($"PowerTube works with {(int)percentPower} %");
            IsOn = true;
        }

        public void TurnOff()
        {
            if (IsOn)
            {
                myOutput.OutputLine($"PowerTube turned off");
            }

            IsOn = false;
        }
    }
}