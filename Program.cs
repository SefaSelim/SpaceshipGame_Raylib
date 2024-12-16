using Raylib_cs;
using SpaceshipGame;

namespace HelloWorld;

class Program
{
    
    public static void Main()
    {

        Game game = new Game();

        game.StartGame();
        game.UpdateGame();
        game.EndGame();

    }
}