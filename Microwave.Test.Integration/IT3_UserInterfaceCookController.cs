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
    public class IT3_UserInterfaceCookController
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
            _display = Substitute.For<IDisplay>();
            _light = Substitute.For<ILight>();
            _powerTube = Substitute.For<IPowerTube>();
            _timer = Substitute.For<ITimer>();
            _userInterface = Substitute.For<IUserInterface>();

            _cookController = new CookController(
                _timer,
                _display,
                _powerTube,
                _userInterface);

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

        [TestCase(1)]
        public void SetTime_StartCooking_TimerStart(int timeS)
        {
            //Initiating
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Test
            _timer.Received(1).Start(timeS*60);
        }

        [TestCase(50)]
        public void SetTime_StartCooking_PowerTubeTurnOn(int power)
        {
            //Initiating
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            //Test
            _powerTube.Received(1).TurnOn(power);
        }
        #endregion

        #region Exceptions

        [Test]
        public void Cooking_OpenDoor_TimerStop()
        {
            //Initiating
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();
            

            //Test
            _door.Open();
            _timer.Received(1).Stop();
        }

        [Test]
        public void Cooking_OpenDoor_PowerTubeTurnOff()
        {
            //Initiating
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();


            //Test
            _door.Open();
            _powerTube.Received(1).TurnOff();
        }

        [Test]
        public void Cooking_StartButton_TimerStop()
        {
            //Initiating
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();


            //Test
            _startCancelButton.Press();
            _timer.Received(1).Stop();
        }

        [Test]
        public void Cooking_StartButton_PowerTubeTurnOff()
        {
            //Initiating
            _door.Open();
            _door.Close();
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();


            //Test
            _startCancelButton.Press();
            _powerTube.Received(1).TurnOff();
        }



        #endregion
    }
}
