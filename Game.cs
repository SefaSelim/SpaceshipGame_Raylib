using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipGame
{
    public class Game
    {
        Spaceship spaceship = new Spaceship();

        public static class Screen
        {
            public const int Height = 480;
            public const int Width = 800;
            public const string Title = "Spaceship Game";

        }



        //list enemies

        public bool isGameOver = false;






        public void StartGame() {

            Raylib.InitWindow(Screen.Width, Screen.Height , Screen.Title);


        }

        public void UpdateGame()
        {
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);



                //Main fuctions
                Spaceship.control();

                if (Raylib.IsKeyPressed(KeyboardKey.Space))
                {

                   Spaceship.Shoot();

                }
                foreach (Bullet bullet in Spaceship.bullets)
                {
                    bullet.Move();
                }


                //   Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
                Raylib.DrawRectangleV(Spaceship.Positions, Spaceship.Size, Color.Red);

                Raylib.EndDrawing();
            }
        }




        public void EndGame()
        {
            Raylib.CloseWindow();
        }

    }
}
