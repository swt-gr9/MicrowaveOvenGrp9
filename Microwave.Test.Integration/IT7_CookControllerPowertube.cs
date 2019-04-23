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
            _uut = new CookController(_timer, _display, _powerTube);
        }

        #region States

        private void SetStateCooking()
        {
        }

        #endregion


        #region MainScenario


        public void TestTurnsOn()
        {

        }

        #endregion
    }
}
