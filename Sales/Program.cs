using System;

namespace Sales
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sales System");

            var handler = new MessageHandler();
            handler.ProcessMessage();
        }
    }
}
