using System;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    public class IT7_CookControllerPowertube
    {
        #region Properties

        private IUserInterface _userInterface;
        private ICookController _uut;
        private IDisplay _display;
        private IPowerTube _powerTube;
        private ITimer _timer;
        private IOutput _output;

        #endregion

        [SetUp]
        public void SetUp()
        {
            _output = Substitute.For<IOutput>();
            _timer = Substitute.For<ITimer>();
            _display = new Display(_output);
            _powerTube = new PowerTube(_output);
            _userInterface = Substitute.For<IUserInterface>();
            _uut = new CookController(_timer, _display, _powerTube, _userInterface);
        }
        
        #region MainScenario
        
        [TestCase(50)]
        [TestCase(700)]
        public void TestTurnsOn(int power)
        {
            _uut.StartCooking(power, 120);
            
            
            _output.Received(1).OutputLine($"PowerTube works with {power} W");
        }

        [Test]
        public void TestTurnOff()
        {
            _uut.StartCooking(50, 120);
            
            _timer.Expired += Raise.EventWith<EventArgs>();
            _output.Received(1).OutputLine($"PowerTube turned off");
        }

        #endregion

        #region Exception

        [Test]
        public void TestTurnsOffWhenButtonPressed()
        {
            _uut.StartCooking(50, 120);
            _uut.Stop();

            _output.Received(1).OutputLine($"PowerTube turned off");
        }

        #endregion



    }
}
