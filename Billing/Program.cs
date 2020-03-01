using System;

namespace Billing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("**** BILLING SYSTEM ****");
            Console.ResetColor();

            new MessageHandler().ProcessMessage();
        }
    }
}
