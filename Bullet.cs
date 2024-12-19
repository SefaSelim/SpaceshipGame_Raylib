using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpaceshipGame
{
    public class Bullet
    {
        public bool isAlive = true;
        public float Speed = 2f; // ana gemi için speed içeren constructer kullanma, enemyler için speedli constructer kullanılacak
        public float Damage = 1f;
        Vector2 Position = new Vector2();
        Vector2 Size = new Vector2(5f, 5f);
        public Rectangle hitbox = new Rectangle();

        public int direction = 1; // to the right

        public Bullet(Vector2 position, float speed) // for enemies
        {
            Position = position;
            Position.Y += (Spaceship.Size.Y - Size.Y) / 2;
            Position.X -= 20; 
            direction = -1;
            Speed = speed;
        }

        public Bullet(Vector2 position)
        {
            Position = position;
            Position.X += Spaceship.Size.X;
            Position.Y += (Spaceship.Size.Y - Size.Y) / 2;
        }
        public void Move()
        {
            if (direction > 0)
            {
                Position.X += Speed / Game.Screen.fps * 300;
            }
            else
            {
                Position.X -= Speed / Game.Screen.fps * 300;
            }

            hitbox = new Rectangle(Position, Size);
            Raylib.DrawRectangleRec(hitbox, Color.Black);
            ChechOutofbounds();
        }

        public void ChechOutofbounds()
        {
            if (Position.X > Game.Screen.Width || Position.X < 0 || Position.Y < 0 || Position.Y > Game.Screen.Height)
            {
                isAlive = false;
            }
        }


    }
}
