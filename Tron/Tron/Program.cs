using System;
using Tron.Logic;

namespace Tron
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
            Console.ReadKey();
        }
    }
}
