using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipGame
{
    public class Game
    {
        public void SpaceShip()
        {
            Rectangle spaceship = new Rectangle();
            spaceship.X = 0;
            spaceship.Y = 0;
            spaceship.Width = 50;
            spaceship.Height = 50;
            
        }

        public void StartGame() {

            Raylib.InitWindow(800, 480, "Hello World");


        }

        public void UpdateGame()
        {
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);

                Raylib.EndDrawing();
            }
        }




        public void EndGame()
        {
            Raylib.CloseWindow();
        }

    }
}
