using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipGame
{
    abstract public class Enemy
    {
        public int MaxHealth;
        public int Health;
        public float Speed = 1;
        public int Damage { get; set; }
        static Random random = new Random();
        Vector2 SpawnPoint = new Vector2(Game.Screen.Width + 50,random.Next(0,Game.Screen.Height-40));
        Vector2 Position = new Vector2();
        Vector2 Size = new Vector2(40f, 40f);
        public Rectangle Hitbox = new Rectangle();
        public bool isEnemyAlive = true;
        public  Texture2D EnemyTexture;
        public Texture2D EnemyBullet;

        abstract public void Move();
        abstract public void Attack();

        public void DrawHealthBar(int val)
        {
            Raylib.DrawRectangleV(new Vector2(Position.X, Position.Y - 20), new Vector2((float)Health / MaxHealth * val, 10), Color.Red);
            Raylib.DrawTextPro(Raylib.GetFontDefault(),Health.ToString(),new Vector2(Position.X,Position.Y - 40),new Vector2(0,0),0,20,1,Color.White);
        }

        public void CheckOutOfBounds()
        {
            if (Position.X < -30)
            {
                Spaceship.Take_Damage(Damage);
                Destroy();
            }
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Destroy();
            }
        }

        public void Destroy()
        {
            Raylib.UnloadTexture(EnemyTexture);
            isEnemyAlive = false;
        }

        private float find_shortest_path()
        {
            Vector2 Distances = new Vector2(Position.X-Spaceship.Positions.X,Position.Y - Spaceship.Positions.Y);
            double ShortestPath = Math.Sqrt (Math.Pow(Distances.X, 2) + Math.Pow(Distances.Y, 2)); //UNNECCESSARY
            float VerticalStep = Distances.Y / Distances.X * Speed / Game.Screen.fps * 100;

            return VerticalStep;
        }

        private void move_to_target(float step)
        {
            Position.X -= Speed / Game.Screen.fps * 100;

            if (Position.X - Spaceship.Positions.X > 75)
            {
                Position.Y -= step;
            }
        }

        public class BasicEnemy : Enemy
        {

            public override void Move()
            {
                Position.X -= Speed / Game.Screen.fps * 100;

                Hitbox = new Rectangle(Position, Size);

                //Raylib.DrawRectangleRec(Hitbox, Color.Red);  //HITBOX CHECK
                Raylib.DrawTextureEx(EnemyTexture, new Vector2 (Position.X + Size.X , Position.Y), 90, 0.4f, Color.White);
                DrawHealthBar(50);

                CheckOutOfBounds();
            }

            public override void Attack()
            {
                Bullet bullet = new Bullet(Position,1.5f);
               
                Spaceship.bullets.Add(bullet);
            }
            public BasicEnemy()
            {
                this.Health = 100;
                this.MaxHealth = this.Health;
                this.Position = SpawnPoint;
                this.Hitbox = new Rectangle(Position, Size);
                this.Speed = 1f;
                this.Damage = 20;
                this.EnemyTexture = Raylib.LoadTexture("../../../resources/BasicEnemy.png");


            }

        }

        public class FastEnemy : Enemy
        {

            public FastEnemy()
            {
                this.Health = 40;
                this.MaxHealth = this.Health;
                this.Position = SpawnPoint;
                this.Hitbox = new Rectangle(Position, Size);
                this.Damage = 10;
                this.EnemyTexture = Raylib.LoadTexture("../../../resources/FastEnemy.png");
                this.Speed = 2f;
            }

            public override void Move()
            {
                move_to_target(find_shortest_path());

                Hitbox = new Rectangle(Position, Size);

                //Raylib.DrawRectangleRec(Hitbox, Color.Red);  //HITBOX CHECK
                Raylib.DrawTextureEx(EnemyTexture, new Vector2(Position.X + Size.X, Position.Y), 90, 0.4f, Color.White);
                DrawHealthBar(50);

                CheckOutOfBounds();
            }

            public override void Attack()
            {
                Bullet bullet = new Bullet(Position, 1.5f);

                Spaceship.bullets.Add(bullet);
            }

        }


        public class StrongEnemy : Enemy
        {

            public StrongEnemy()
            {
                this.Health = 200;
                this.MaxHealth = this.Health;
                this.Position = SpawnPoint;
                this.Hitbox = new Rectangle(Position, Size);
                this.Damage = 30;
                this.EnemyTexture = Raylib.LoadTexture("../../../resources/StrongEnemy.png");
                this.Speed = 0.75f;
            }

            public override void Move()
            {
                move_to_target(find_shortest_path());

                Hitbox = new Rectangle(Position, Size);

                //Raylib.DrawRectangleRec(Hitbox, Color.Red);  //HITBOX CHECK
                Raylib.DrawTextureEx(EnemyTexture, new Vector2(Position.X + Size.X, Position.Y), 90, 0.5f, Color.White);
                DrawHealthBar(50);

                CheckOutOfBounds();
            }

            public override void Attack()
            {
                Bullet bullet = new Bullet(Position, 1.5f);

                Spaceship.bullets.Add(bullet);
            }

        }


        public class BossEnemy : Enemy
        {
            public BossEnemy()
            {
                this.Size *= 2.1f;
                this.Health = 1000;
                this.MaxHealth = this.Health;
                this.Position = new Vector2(850,240-Size.Y/2);
                this.Hitbox = new Rectangle(Position, Size);
                this.Speed = 0.5f;
                this.Damage = 50;
                this.EnemyTexture = Raylib.LoadTexture("../../../resources/BossEnemy.png");
            }
            public override void Move()
            {
                if (Position.X > 500)
                {
                    Position.X -= Speed / Game.Screen.fps * 100;
                }

                Hitbox = new Rectangle(Position, Size);

                //Raylib.DrawRectangleRec(Hitbox, Color.Red);  //HITBOX CHECK
                Raylib.DrawTextureEx(EnemyTexture, new Vector2(Position.X, Position.Y + Size.Y +6), 270, 1f, Color.White);
                DrawHealthBar(100);
            }

            public override void Attack()
            {
                Bullet bullet = new Bullet(Position, 1.5f);

                Spaceship.bullets.Add(bullet);
            }

        }



    }
}
