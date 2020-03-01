using System;

namespace Delivery
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("**** DELIVERY SYSTEM ****");
            Console.ResetColor();

            new MessageHanlder().ProcessMessage();
        }
    }
}
