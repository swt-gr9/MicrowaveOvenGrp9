using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Microwave.Test.Integration
{
   
    public class IT5_UserInterfaceDisplay
    {
        #region Properties
        private UserInterface _uut;
        private IButton _powerButton;
        private IButton _timeButton;
        private IButton _startCancelButton;
        private IDoor _door;
        private IDisplay _display;
        private IOutput _output;
        private ILight _light;
        private ICookController _cookController;
        private IPowerTube _powerTube;
        #endregion

        [SetUp]
        public void SetUp()
        {
            _powerTube = Substitute.For<IPowerTube>();
            _output = Substitute.For<IOutput>();
            _light = new Light(_output);
            _display = new Display(_output);
            _door = new Door();
            _powerButton = new Button();
            _cookController = Substitute.For<ICookController>();
            _timeButton = new Button();
            _startCancelButton = new Button();
            _uut = new UserInterface(_powerButton,
                _timeButton, 
                _startCancelButton,
                _door,
                _display,
                _light,
                _cookController
                );
        }

        #region States

        private void SetStateSetPower()
        {
            _powerButton.Press();
        }

        private void SetStateCooking()
        {
            SetStateSetPower();
            _startCancelButton.Press();
        }

        #endregion

        #region MainScenario

        [TestCase(1, 50)]
        [TestCase(2, 100)]
        [TestCase(3, 150)]
        public void TestDisplaysCorrectPower(int times, int power)
        {
            for(int i = 0; i < times; ++i)
                _powerButton.Press();

            _output.Received(1).OutputLine($"Display shows: {power} W");
        }

        [TestCase(1, 1)]
        [TestCase(5, 5)]
        public void TestDisplaysCorrectTime(int times, int displayTime)
        {
            SetStateSetPower();

            for(int i = 0; i < times; ++i)
                _timeButton.Press();

            _output.Received(1).OutputLine($"Display shows: {displayTime:D2}:{0:D2}");
        }

        [Test]
        public void TestDisplayClears()
        {
            SetStateCooking();

            _output.Received(1).OutputLine($"Display cleared");
        }

        #endregion



    }
}
