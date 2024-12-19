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

        abstract public void Move();
        abstract public void Attack();

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
            isEnemyAlive = false;
        }

        public class BasicEnemy : Enemy
        {

            public override void Move()
            {
                Position.X -= Speed / Game.Screen.fps * 100;
                Hitbox = new Rectangle(Position, Size);
                Raylib.DrawRectangleRec(Hitbox, Color.Red);
            }

            public override void Attack()
            {
                throw new NotImplementedException();
            }
            public BasicEnemy()
            {
                this.Health = 100;
                this.Position = SpawnPoint;
                this.Hitbox = new Rectangle(Position, Size);

               
            }

        }



    }
}
