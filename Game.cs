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

        }


        public class Spaceship
        {
            int health = 100;
            int damage = 10;
            public static float Speed = 1f;

            //List<Bullet> bullets = new List<Bullet>();

            public static Vector2 Size = new Vector2(40f, 40f);
            public static Vector2 Positions = new Vector2(10f, (Screen.Height - Size.X) / 2);
            public static Rectangle Collision = new Rectangle(Positions,Size);

            public static void control()
            {
                //çapraz yönlerde hız artışı problemi çözülecek

                if (Raylib.IsKeyDown(KeyboardKey.A))
                {
                    Positions.X -= Positions.X <= 0 ? 0 : Speed / 10;
                }
                if (Raylib.IsKeyDown(KeyboardKey.D))
                {
                    Positions.X += Positions.X >= (Screen.Width - Size.X )? 0 : Speed / 10;
                }
                if (Raylib.IsKeyDown(KeyboardKey.W))
                {
                    Positions.Y -= Positions.Y <= 0 ? 0 : Speed / 10;
                }
                if (Raylib.IsKeyDown(KeyboardKey.S))
                {
                    Positions.Y += Positions.Y >= (Screen.Height - Size.Y) ? 0 : Speed / 10;
                }

                Collision = new Rectangle(Positions,Size);

            }


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
