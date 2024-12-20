using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipGame
{
    abstract public class Enemy
    {
        public int Health;
        public float Speed = 1;
        public int Damage;
        static Random random = new Random();
        Vector2 SpawnPoint = new Vector2(Game.Screen.Width + 50,random.Next(0,Game.Screen.Height));
        Vector2 Position = new Vector2();
        Vector2 Size = new Vector2(40f, 40f);
        public Rectangle Hitbox = new Rectangle();
        public bool isEnemyAlive = true;
        public  Texture2D EnemyTexture;
        public Texture2D EnemyBullet;

        abstract public void Move();
        abstract public void Attack();

        public void CheckOutOfBounds()
        {
            if (Position.X < -30)
            {
                // gemi hasar alacak
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

        public class BasicEnemy : Enemy
        {

            public override void Move()
            {
                Position.X -= Speed / Game.Screen.fps * 100;
                Hitbox = new Rectangle(Position, Size);

                //Raylib.DrawRectangleRec(Hitbox, Color.Red);  //HITBOX CHECK
                Raylib.DrawTextureEx(EnemyTexture, new Vector2 (Position.X + Size.X , Position.Y), 90, 0.4f, Color.White);

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
                this.Position = SpawnPoint;
                this.Hitbox = new Rectangle(Position, Size);
                this.EnemyTexture = Raylib.LoadTexture("../../../resources/BasicEnemy.png");


            }

        }



    }
}
