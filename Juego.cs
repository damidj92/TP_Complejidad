using System;

namespace juegoIA
{
	class Juego
	{
		public static void Main(string[] args)
		{
            string resp = "si";

            while (resp == "si") {
                Game game = new Game();
                game.play();
                Console.Write("\nDesea seguir jugando? (si/no): ");
                resp = Console.ReadLine();
            }

			Console.ReadKey();
		}
	}
}