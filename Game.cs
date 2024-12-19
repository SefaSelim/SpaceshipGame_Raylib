using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static SpaceshipGame.Enemy;

namespace SpaceshipGame
{
    public class Game
    {
        CollisionDetector collisionDetector = new CollisionDetector();
        public static class Screen
        {
            public const int Height = 480;
            public const int Width = 800;
            public const string Title = "Spaceship Game";
            public const int fps = 200;

        }



        //list enemies

        public bool isGameOver = false;
        private double timer = 0;
        private double timerForEnemies = 0;

        public List<Enemy> enemies = new List<Enemy>();


        private void DrawEnemies()
        {
            if (timerForEnemies  >= 3 * Screen.fps)
            {
                BasicEnemy basicEnemy = new BasicEnemy();
                enemies.Add(basicEnemy);
                timerForEnemies = 0;
            }

            foreach (Enemy enemy in enemies) {
                enemy.Move();
            }
        }


        public void StartGame() {

            Raylib.InitWindow(Screen.Width, Screen.Height , Screen.Title);
            Raylib.SetTargetFPS(Screen.fps);


        }

        public void UpdateGame()
        {
            while (!Raylib.WindowShouldClose())
            {
                collisionDetector.CheckCollision(this);

                timer += 1;
                timerForEnemies++;

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                DrawEnemies();

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
