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
        bool isAlive = true;
        public static float Speed = 1f;
        public static float Damage = 1f;
        static Vector2 Position = new Vector2();
        static Vector2 Size = new Vector2(3f, 3f);
        public Rectangle hitbox = new Rectangle(Position,Size);

        public int direction = 1; // to the right

        public Bullet(Vector2 position, float speed)
        {
            Position = position;
            Speed = speed;
        }

        public void Move()
        {
            if (direction > 0)
            {
                Position.X += Speed / 10;
            }
            else
            {
                Position.X -= Speed / 10;
            }

            hitbox = new Rectangle(Position, Size);
            Raylib.DrawRectangleRec(hitbox, Color.Black);
        }




    }
}
