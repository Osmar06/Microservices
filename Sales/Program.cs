using System;

namespace Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("**** SALES SYSTEM ****");
            Console.ResetColor();

            new MessageHandler().ProcessMessage();
        }
    }
}
