using System;

namespace DisplayServiceLib
{
    public class ConsoleDisplayService : IDisplayService
    {
        public bool SendDisplayMessage(string displayMessage)
        {
            Console.WriteLine(displayMessage);
            return true;
        }
    }
}