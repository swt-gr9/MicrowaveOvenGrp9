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
    [TestFixture]
    class IT4_UserInterfaceLight
    {

        #region Properties

        private UserInterface _uut;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IDisplay _display;
        private ILight _light;
        private ICookController _cookController;
        private IPowerTube _powerTube;
        private IOutput _output;

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
            _display = Substitute.For<IDisplay>();
            _cookController = Substitute.For<ICookController>();
            _uut = new UserInterface(
                _powerButton,
                _timeButton,
                _startCancelButton,
                _door,
                _display,
                _light,
                _cookController);

        }

        #region MainScenario

        [TestCase("Light is turned on")]
        public void onDoorOpen_LightTurnsOn(string text)
        {
            _door.Open();
            _output.Received(1).OutputLine(text);
        }

        [TestCase("Light is turned off")]
        public void onDoorClosed_LightTurnsOff(string text)
        {
            
            _door.Open();
            _door.Close();
            _output.Received(1).OutputLine(text);
        }

        [TestCase("Light is turned on")]
        public void onPressStartCancelButtonToStart_LightTurnsOn(string text)
        {

            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _output.Received(2).OutputLine(text);
        }


        [TestCase("Light is turned off")]
        public void onUserInterfaceCookingIsDone_LightTurnsOff(string text)
        {

           _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
           _uut.CookingIsDone();
            _output.Received(2).OutputLine(text);
        }
        #endregion


        #region Extension

        [TestCase("Light is turned off")]
        public void onStartCancelButtonPressedWhileCooking_LightTurnsOff(string text)
        {

            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            _startCancelButton.Press();
            _output.Received(1).OutputLine(text);
        }

        [TestCase("Light is turned on")]
        public void onDoorOpenWhilePowerIsSat_LightTurnsOn(string text)
        {

            _powerButton.Press();
            _door.Open();
            _output.Received(1).OutputLine(text);
        }

        [TestCase("Light is turned on")]
        public void onDoorOpenWhileTimeIsSat_LightTurnsOn(string text)
        {

            _powerButton.Press();
            _timeButton.Press();
            _door.Open();
            _output.Received(1).OutputLine(text);
        }
        #endregion
    }
}    
