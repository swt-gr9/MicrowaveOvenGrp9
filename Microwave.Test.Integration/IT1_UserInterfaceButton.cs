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
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;

namespace Microwave.Test.Integration
{
    [TestFixture]
    public class IT1_UserInterfaceButton
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
            _door = Substitute.For<IDoor>();
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

        #region MainScenario
 
        [TestCase(1,50)]
        [TestCase(2, 100)]
        [TestCase(3, 150)]
        public void Ready_TimesPowerButton_PowerIsResult(int times,int result)
        {
            for (int i = 0; i < times; i++)
            {
                _powerButton.Press();
            }
            _display.Received(1).ShowPower(result);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public void SetPower_TimesTimeButton_TimeIsResult(int times, int result)
        {
            _powerButton.Press();
            for (int i = 0; i < times; i++)
            {
                _timeButton.Press();
            }
            _display.Received(1).ShowTime(result,0);
        }

        [Test]
        public void SetTime_StartButton_LightIsCalled()
        {
            _powerButton.Press();
            // Now in SetPower
            _timeButton.Press();
            // Now in SetTime
            _startCancelButton.Press();
            // Now cooking
            _light.Received(1).TurnOn();
        }

        [TestCase(60,50)]
        public void SetTime_StartButton_CookerIsCalled(int timeS, int power)
        {
            _powerButton.Press();
            // Now in SetPower
            _timeButton.Press();
            // Now in SetTime
            _startCancelButton.Press();
            // Now cooking
            _cookController.Received(1).StartCooking(power,timeS);
        }
        #endregion

        #region Exceptions

        [Test]
        public void SetPower_StartButton_LightIsCalled()
        {
            _powerButton.Press();
            _startCancelButton.Press();

            _light.Received(1).TurnOff();
        }

        [Test]
        public void SetPower_StartButton_DisplayIsCalled()
        {
            _powerButton.Press();
            _startCancelButton.Press();

            _display.Received(1).Clear();
        }

        [Test]
        public void Cooking_StartButton_CookerIsCalled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _startCancelButton.Press();

            _cookController.Received(1).Stop();
        }

        [Test]
        public void Cooking_StartButton_LightIsCalled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _startCancelButton.Press();

            _light.Received(1).TurnOff();
        }

        [Test]
        public void Cooking_StartButton_DisplayIsCalled()
        {
            _powerButton.Press();
            _timeButton.Press();
            _startCancelButton.Press();

            _startCancelButton.Press();

            _display.Received(2).Clear();
        }



        #endregion
    }
}
