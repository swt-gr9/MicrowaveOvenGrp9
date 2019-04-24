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
        private CookController _uut;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IOutput _output;
        private IUserInterface _userInterface;
        private ILight _light;
        private IDoor _door;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;

        #endregion

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();


            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();

            _door = new Door();

            _powerTube = new PowerTube(_output);
            _display = new Display(_output);
            _light = new Light(_output);

            _timer = new Timer();

            _uut = new CookController(_timer, _display, _powerTube);

            _userInterface = new UserInterface(_powerButton, _timeButton, _startCancelButton, _door, _display, _light, _uut);

            _uut.UI = _userInterface;
        }

        #region Main Scenario

        [TestCase(1, "Display shows: 00:59",2000)]
        [TestCase(2,"Display shows: 01:57",5000)]
        public void OnTimerTickCookControllerLogsOutput(int times, string output, int delay)
        {
            _door.Open();
            _door.Close();

            _powerButton.Press();
            for (int i = times; (i > 0); i--)
            {
                _timeButton.Press();
            }
           
            _startCancelButton.Press();

            
            System.Threading.Thread.Sleep(delay);
            _output.Received(1).OutputLine(output);            
        }
        
        public void OntimerExpire()
        {

        }

        #endregion
    }
}
