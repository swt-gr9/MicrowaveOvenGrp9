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
    class IT8_CookControllerUserInterface
    {
        #region Properties

        
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IDisplay _display;
        private ILight _light;
        private CookController _uut;
        private IPowerTube _powerTube;
        private IOutput _output;
        private ITimer _timer;
        private IUserInterface _userInterface;

        #endregion

        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _timer = Substitute.For<ITimer>();
            _uut = new CookController(
                _timer,
                _display,
                _powerTube,
                _userInterface);
            _userInterface = new UserInterface(
                _powerButton,
                _timeButton,
                _startCancelButton,
                _door,
                _display,
                _light,
                _uut);

            _uut.UI = _userInterface;

        }

        #region MainScenario
        [TestCase("Display cleared",2)]
        [TestCase("Light is turned off",1)]
        
        public void onTimerExpired_CookingIsDoneFires(string text, int times)
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            
            _timer.Expired += Raise.EventWith<EventArgs>();
            
            _output.Received(times).OutputLine(text);
        }

        #endregion


    }
}

