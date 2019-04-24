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
    class IT9_TimerCookController
    {
        #region properties

        private IDisplay _display;
        private ICookController _cookController;
        private IPowerTube _powerTube;
        private ITimer _uut;
        private IOutput _output;
        private IUserInterface _userInterface;
        #endregion

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _userInterface = Substitute.For<IUserInterface>();
            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _uut = new Timer();
            _cookController = new CookController(_uut,_display,_powerTube,_userInterface);
        }

        #region Main Scenario

        [TestCase(60, "Display shows: 00:59",2000)]
        [TestCase(120,"Display shows: 01:57",5000)]
        public void OnTimerTickCookControllerLogsOutput(int time, string output, int delay)
        {
            _cookController.StartCooking(50, time);
            System.Threading.Thread.Sleep(delay);
            _output.Received(1).OutputLine(output);            
        }
        
        public void OntimerExpire()
        {

        }

        #endregion
    }
}
