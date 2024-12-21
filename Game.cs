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

        public static class Timers
        {
            public static double timer = 0;
            public static double timerForEnemies = 0;
            public static double basicEnemyShootTimer = 0;
            public static double fastEnemyShootTimer = 0;
            public static double strongEnemyShootTimer = 0;


            public static void IncreaseTimers()
            {
                timer++;
                timerForEnemies++;
                basicEnemyShootTimer++;
                fastEnemyShootTimer++;
                strongEnemyShootTimer++;
            }
        }

        public static Texture2D background = Raylib.LoadTexture("../../../resources/background.png");

        //list enemies

        public static bool isGameOver = false;

        public List<Enemy> enemies = new List<Enemy>();


        private void BasicEnemyShoot()
        {
            if (Timers.basicEnemyShootTimer >= 2 * Screen.fps)
            {
                foreach (Enemy basicenemy in enemies)
                {
                    if (basicenemy is Enemy.BasicEnemy)
                    {
                        basicenemy.Attack();
                    }
                }
                Timers.basicEnemyShootTimer = 0;
            }
        }

        private void FastEnemyShoot()
        {
            if (Timers.fastEnemyShootTimer >= 1 * Screen.fps)
            {
                foreach (Enemy fastenemy in enemies)
                {
                    if (fastenemy is Enemy.FastEnemy)
                    {
                        fastenemy.Attack();
                    }
                }
                Timers.fastEnemyShootTimer = 0;
            }
        }

        private void StrongEnemyShoot()
        {
            if (Timers.strongEnemyShootTimer >= 3 * Screen.fps)
            {
                foreach (Enemy strongenemy in enemies)
                {
                    if (strongenemy is Enemy.StrongEnemy)
                    {
                        strongenemy.Attack();
                    }
                }
                Timers.strongEnemyShootTimer = 0;
            }
        }

        private void DrawEnemies()
        {
            if (Timers.timerForEnemies  >= 4 * Screen.fps)
            {

                FastEnemy fastEnemy = new FastEnemy();
                enemies.Add(fastEnemy);

                BasicEnemy basicEnemy = new BasicEnemy();
                enemies.Add(basicEnemy);

                StrongEnemy strongEnemy = new StrongEnemy();
                enemies.Add(strongEnemy);

                Timers.timerForEnemies = 0;
            }

            foreach (Enemy enemy in enemies) {
                enemy.Move();
            }

            BasicEnemyShoot();
            FastEnemyShoot();
            StrongEnemyShoot();

            enemies.RemoveAll(b => !b.isEnemyAlive);
        }


        public void StartGame() {

            Raylib.InitWindow(Screen.Width, Screen.Height , Screen.Title);
            Raylib.SetTargetFPS(Screen.fps);
            Raylib.InitAudioDevice();


        }

        public void UpdateGame()
        {
            while (!Raylib.WindowShouldClose())
            {
                collisionDetector.CheckCollision(this);

                Timers.IncreaseTimers();

                Raylib.BeginDrawing();
                
                DrawBackground();


                //Main fuctions
                DrawEnemies();
                Spaceship.control();

                if (Raylib.IsKeyDown(KeyboardKey.Space))
                {

                    if (Timers.timer >= Spaceship.ShootSpeed * Screen.fps)
                    {
                        Spaceship.Shoot();
                        Timers.timer = 0;
                    }

                }
                foreach (Bullet bullet in Spaceship.bullets)
                {
                    bullet.Move();
                }
                Spaceship.bullets.RemoveAll(b => !b.isAlive);


                Raylib.DrawText("Health: "+ Spaceship.health, 12, 12, 20, Color.Black);


                //Raylib.DrawRectangleV(Spaceship.Positions, Spaceship.Size, Color.Red);   // HITBOX CHECK
                Raylib.DrawTextureEx(Spaceship.MainShip, new Vector2(Spaceship.Positions.X + Spaceship.Size.X, Spaceship.Positions.Y - 5f), 90, 0.45f, Color.White);

                Raylib.EndDrawing();
            }
        }




        public void EndGame()
        {
            Raylib.CloseWindow();
        }

        public void DrawBackground()
        {
            Raylib.ClearBackground(Color.White);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Raylib.DrawTextureEx(background, new Vector2(j * background.Width / 2, i * background.Height / 2), 0, 0.5f, Color.White);
                }

            }
        }

    }
}
