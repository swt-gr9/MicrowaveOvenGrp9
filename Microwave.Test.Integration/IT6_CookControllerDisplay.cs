using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    class IT6_CookControllerDisplay
    {
        #region Properties
        private ICookController _uut;
        private IUserInterface _userInterface;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IOutput _output;
        #endregion

        [SetUp]
        public void SetUp()
        {
            _userInterface = Substitute.For<IUserInterface>();
            _powerTube = Substitute.For<IPowerTube>();
            _timer = Substitute.For<ITimer>();
            _output = Substitute.For<IOutput>();
            _display = new Display(_output);
            _uut= new CookController(_timer, _display, _powerTube, _userInterface);
        }

        #region Main Scenario
        [TestCase(60,1, "Display shows: 00:59")]
        public void TimeRemainingOnTimerTick(int time, int tick, string output)
        {
            _uut.StartCooking(50, time);
            _timer.TimeRemaining.Returns(time-tick);
            _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            _output.Received(1).OutputLine(output);
        }

        public void OnManyTicks(int time, int tick, string output)
        {
            _uut.StartCooking(50,time);

            for (int i = 1; i < tick+1; i++)
            {
                _timer.TimeRemaining.Returns(time - i);
                _timer.TimerTick += Raise.EventWith(this, EventArgs.Empty);
            }
            _output.Received(1).OutputLine(output);
        }
        #endregion

    }
}
