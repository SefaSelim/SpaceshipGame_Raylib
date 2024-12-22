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
            public static double bossEnemyShootTimer = 0;

            public static double WaveTimer = 0;


            public static void IncreaseTimers()
            {
                timer++;
                timerForEnemies++;
                basicEnemyShootTimer++;
                fastEnemyShootTimer++;
                strongEnemyShootTimer++;
                bossEnemyShootTimer++;
                WaveTimer++;
            }
        }

        public static Texture2D background = Raylib.LoadTexture("../../../resources/background.png");

        //list enemies

        public static bool isGameOver = false;
        bool inMenu = true;
        Rectangle startGame = new Rectangle(300, 215, 200, 50);

        public static int Score = 0;

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

        private void BossEnemyShoot()
        {
            if (Timers.bossEnemyShootTimer >= 3 * Screen.fps)
            {
                foreach (Enemy bossenemy in enemies)
                {
                    if (bossenemy is Enemy.BossEnemy)
                    {
                        bossenemy.Attack();
                    }
                }
                Timers.bossEnemyShootTimer= 0;
            }
        }

        private void DrawEnemies()
        {
            if (Timers.timerForEnemies >= 2 * Screen.fps)
            {

                if (Timers.WaveTimer < 10 * Screen.fps)
                {
                    FastEnemy fastEnemy = new FastEnemy();
                    enemies.Add(fastEnemy);
                }

                if (Timers.WaveTimer < 20 * Screen.fps && Timers.WaveTimer > 10 * Screen.fps)
                {
                    BasicEnemy basicEnemy = new BasicEnemy();
                    enemies.Add(basicEnemy);
                }

                if (Timers.WaveTimer < 30 * Screen.fps && Timers.WaveTimer > 20 * Screen.fps )
                {
                    StrongEnemy strongEnemy = new StrongEnemy();
                    enemies.Add(strongEnemy);
                }

                if (Timers.WaveTimer < 50 * Screen.fps && Timers.WaveTimer > 47 * Screen.fps)
                {
                    BossEnemy bossEnemy = new BossEnemy();
                    enemies.Add(bossEnemy);
                }

                Timers.timerForEnemies = 0;
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Move();
            }

            BasicEnemyShoot();
            FastEnemyShoot();
            StrongEnemyShoot();
            BossEnemyShoot();

            enemies.RemoveAll(b => !b.isEnemyAlive);
        }

        public void DrawMenu ()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.White);


            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), startGame))
            {
                Raylib.DrawRectangle(300, 215, 200, 50, Color.LightGray);
                Raylib.DrawText("Start Game", 340, 230, 20, Color.Black);

                if (Raylib.IsMouseButtonPressed(0))
                {
                    inMenu = false;
                }
            }
            else
            {
                Raylib.DrawRectangle(300, 215, 200, 50, Color.Gray);
                Raylib.DrawText("Start Game", 340, 230, 20, Color.White);
            }


            Raylib.EndDrawing();
        }

        public void DrawDeathScreen()
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.White);



            Raylib.EndDrawing();
        }


        public void StartGame()
        {

            Raylib.InitWindow(Screen.Width, Screen.Height, Screen.Title);
            Raylib.SetTargetFPS(Screen.fps);
            Raylib.InitAudioDevice();


        }


        public void UpdateGame()
        {
            while (!Raylib.WindowShouldClose())
            {

                if (inMenu)
                {
                   DrawMenu();
                }
                else if (!isGameOver)
                {

                    collisionDetector.CheckCollision(this);

                    Timers.IncreaseTimers();

                    Raylib.BeginDrawing();

                    DrawBackground();

                    Raylib.DrawText("" + Score, Screen.Width / 2 - 10, 15, 40, Color.White);


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


                    Raylib.DrawText("Health: " + Spaceship.health, 12, 12, 20, Color.Black);


                    //Raylib.DrawRectangleV(Spaceship.Positions, Spaceship.Size, Color.Red);   // HITBOX CHECK
                    Raylib.DrawTextureEx(Spaceship.MainShip, new Vector2(Spaceship.Positions.X + Spaceship.Size.X, Spaceship.Positions.Y - 5f), 90, 0.45f, Color.White);

                    Raylib.EndDrawing();

                    Spaceship.CheckDeath();
                }
                else
                {
                DrawDeathScreen();
                }


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
