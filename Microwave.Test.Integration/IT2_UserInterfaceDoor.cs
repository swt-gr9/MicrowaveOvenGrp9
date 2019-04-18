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
    public class IT2_UserInterfaceDoor
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
        #endregion


        [SetUp]
        public void SetUp()
        {
            _powerButton = new Button();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _door = new Door();
            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();
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

        #region Main Scenario

        [Test]
        public void Ready_DoorOpen_LightOn()
        {
            _door.Open();
            _light.Received(1).TurnOn();
        }

        [Test]
        public void DoorIsOpen_DoorClose_LightOff()
        {
            _door.Open();

            _door.Close();
            _light.Received(1).TurnOff();
        }




        #endregion

        #region Exception   
        [Test]
        public void Cooking_DoorOpen_CookingStop()
        {
            //Main Scenario
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Exception Scenario
            _door.Open();
            _cookController.Received(1).Stop();
        }

        [Test]
        public void Cooking_DoorOpen_DisplayClear()
        {
            //Main Scenario
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Exception Scenario
            _door.Open();
            _display.Received(1).Clear();
        }

        [Test]
        public void Cooking_StartButton_CookingStop()
        {
            //Main Scenario
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Exception Scenario
            _startCancelButton.Press();
            _cookController.Received(1).Stop();
        }

        [Test]
        public void Cooking_StartButton_DisplayClear()
        {
            //Main Scenario
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Exception Scenario
            _startCancelButton.Press();
            _display.Received(2).Clear();
        }

        [Test]
        public void Cooking_StartButton_LightTurnOff()
        {
            //Main Scenario
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Exception Scenario
            _startCancelButton.Press();
            _light.Received(2).TurnOff();
        }
        #endregion
    }
}
