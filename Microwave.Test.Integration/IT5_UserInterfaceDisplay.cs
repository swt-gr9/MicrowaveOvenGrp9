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

        #region MainScenario

        [TestCase(50)]
        public void Test(int power)
        {
            _door.Open();
            _door.Close();
            _powerButton.Press();

            _output.Received(1).OutputLine($"Display shows: {power} W");
        }

        #endregion



    }
}
