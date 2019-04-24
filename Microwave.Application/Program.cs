using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using Timer = MicrowaveOvenClasses.Boundary.Timer;

namespace Microwave.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Comment to test webhook
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);

            Light light = new Light(output);

            Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker);

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence from sequence diagram

            door.Open(); //Opens door
            door.Close(); //Closes door
            Console.WriteLine("\n-----Test rollover-----\n");
            for(int i = 0; i < 15; i++)
                powerButton.Press(); //Press power button

            Console.WriteLine("\n-----Test increment time-----\n");
            for(int i = 0; i < 3; ++i)
                timeButton.Press(); //Press time button


            Console.WriteLine("\n-----Test door open-----\n");
            startCancelButton.Press(); //Press start-cancel 

            Thread.Sleep(3000);
            door.Open();
            door.Close();


            Console.WriteLine("\n-----Test startcancelbutton-----\n");
            powerButton.Press();
            timeButton.Press();

            startCancelButton.Press();

            Thread.Sleep(3000);
            
            startCancelButton.Press();


            Console.WriteLine("\n-----Test normal run-----\n");
            powerButton.Press();
            timeButton.Press();
            startCancelButton.Press();
            System.Console.WriteLine("When you press enter, the program will stop");
            // Wait for input

            System.Console.ReadLine();
        }
    }
}
