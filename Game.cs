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

        public static class Screen
        {
            public const int Height = 480;
            public const int Width = 800;
            public const string Title = "Spaceship Game";
            public const int fps = 144;

        }



        //list enemies

        public bool isGameOver = false;
        public double timer = 0;




        public void StartGame() {

            Raylib.InitWindow(Screen.Width, Screen.Height , Screen.Title);
            Raylib.SetTargetFPS(Screen.fps);

        }

        public void UpdateGame()
        {
            while (!Raylib.WindowShouldClose())
            {
                timer += 1;

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);



                //Main fuctions
                Spaceship.control();

                if (Raylib.IsKeyDown(KeyboardKey.Space))
                {

                    if (timer >= Spaceship.ShootSpeed * Screen.fps)
                    {
                        Spaceship.Shoot();
                        timer = 0;
                    }

                }
                foreach (Bullet bullet in Spaceship.bullets)
                {
                    bullet.Move();
                }
                Spaceship.bullets.RemoveAll(b => !b.isAlive);


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
