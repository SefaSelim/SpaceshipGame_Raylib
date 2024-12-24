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
        Random random = new Random();

        CollisionDetector collisionDetector = new CollisionDetector();
        Scoreboard scoreboard = new Scoreboard();
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


            public static double WaveTime = 4f;


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
        public List<Powerups> powerups = new List<Powerups>();


        #region ENEMY SHOOTERS
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
        #endregion

        #region ENEMY SPAWNERS
        private void FastEnemySpawner(int start, int finish)
        {
            if (Timers.WaveTimer < finish * Screen.fps && Timers.WaveTimer > start * Screen.fps)
            {
                FastEnemy fastEnemy = new FastEnemy();
                enemies.Add(fastEnemy);
            }
        }
        private void BasicEnemySpawner(int start, int finish)
        {
            if (Timers.WaveTimer < finish * Screen.fps && Timers.WaveTimer > start * Screen.fps)
            {
                BasicEnemy basicEnemy = new BasicEnemy(true);
                enemies.Add(basicEnemy);
            }
        }
        private void MeteorSpawner(int start, int finish)
        {
            if (Timers.WaveTimer < finish * Screen.fps && Timers.WaveTimer > start * Screen.fps)
            {
                BasicEnemy basicEnemy = new BasicEnemy(false);
                enemies.Add(basicEnemy);
            }
        }

        private void StrongEnemySpawner(int start, int finish)
        {
            if (Timers.WaveTimer < finish * Screen.fps && Timers.WaveTimer > start * Screen.fps)
            {
                StrongEnemy strongEnemy = new StrongEnemy();
                enemies.Add(strongEnemy);
            }
        }
        private void BossEnemySpawner(int start, int finish)
        {
            if (Timers.WaveTimer < finish * Screen.fps && Timers.WaveTimer > start * Screen.fps)
            {
                BossEnemy bossEnemy = new BossEnemy();
                enemies.Add(bossEnemy);
            }
        }

#endregion  //ENEMY SPAWNERS


        private void DrawEnemies()
        {
            if (Timers.timerForEnemies >= Timers.WaveTime * Screen.fps)
            {
                Powerups pow = new Powerups(random.Next(0,5));
                powerups.Add(pow);

                #region WAVE STRUCTURE
                if (Timers.WaveTimer > 10 && Timers.WaveTimer < 40)
                {
                    Timers.WaveTime = 3.5f;
                }
                if (Timers.WaveTimer > 40 && Timers.WaveTimer < 70)
                {
                    Timers.WaveTime = 3f;   
                }
                if (Timers.WaveTimer > 70 && Timers.WaveTimer < 100)
                {
                    Timers.WaveTime = 2.5f;
                }
                if (Timers.WaveTimer > 100 && Timers.WaveTimer < 150)
                {
                    Timers.WaveTime = 2f;
                }
                if (Timers.WaveTimer > 150 && Timers.WaveTimer < 200)
                {
                    Timers.WaveTime = 1.5f;
                }
                if (Timers.WaveTimer > 200 && Timers.WaveTimer < 250)
                {
                    Timers.WaveTime = 1f;
                }
                if (Timers.WaveTimer > 250 && Timers.WaveTimer < 300)
                {
                    Timers.WaveTime = 0.6f;
                }
                if (Timers.WaveTimer > 300 && Timers.WaveTimer < 350)
                {
                    Timers.WaveTime = 0.3f;
                }

                #endregion

                FastEnemySpawner(0, 30);

                BasicEnemySpawner(25,50);
                
                MeteorSpawner(20, 30);

                StrongEnemySpawner(40, 60);

                FastEnemySpawner(50, 80);

                BossEnemySpawner(90, 95);

                FastEnemySpawner(120, 500);

                StrongEnemySpawner(120, 500);

                BasicEnemySpawner(120, 500);
                
                MeteorSpawner(120, 500);




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

            if (Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                inMenu = false;
            }

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

        public void DrawSpaceshipSpecialities()
        {

            Raylib.DrawRectangle(10, 10, 200, 20, Color.Gray);
            Raylib.DrawRectangle(10, 10, Spaceship.health, 20, Color.Red); // max 200
            Raylib.DrawText("Health",230 ,10,20,Color.White);

            Raylib.DrawRectangle(10, 40, 200, 20, Color.Gray);
            Raylib.DrawRectangle(10, 40, Convert.ToInt16(16/Spaceship.ShootSpeed), 20, Color.Blue); // max 0.1f
            Raylib.DrawText("Shoot Speed", 230, 40, 20, Color.White);

            Raylib.DrawRectangle(10, 70, 200, 20, Color.Gray);
            Raylib.DrawRectangle(10, 70, Convert.ToInt16(Spaceship.damage*200/30), 20, Color.Yellow); // max 30
            Raylib.DrawText("Damage", 230, 70, 20, Color.White);
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

                    Raylib.DrawText("" + Score, Screen.Width / 2 - Score.ToString().Length * 15, 15, 40, Color.White);


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

                    foreach (Powerups pow in powerups)
                    {
                        pow.DrawPowerup();
                    }
                    powerups.RemoveAll(b => !b.isAlive);

                    DrawSpaceshipSpecialities();


                    //Raylib.DrawRectangleV(Spaceship.Positions, Spaceship.Size, Color.Red);   // HITBOX CHECK
                    Raylib.DrawTextureEx(Spaceship.MainShip, new Vector2(Spaceship.Positions.X + Spaceship.Size.X, Spaceship.Positions.Y - 5f), 90, 0.45f, Color.White);

                    Raylib.EndDrawing();

                    Spaceship.CheckDeath();
                }
                else
                {
                    if (!scoreboard.saveSuccessful)
                    {
                        scoreboard.DrawScoreSavePanel();
                    }
                    else
                    {
                       scoreboard.DrawScoreboard();
                    }



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
