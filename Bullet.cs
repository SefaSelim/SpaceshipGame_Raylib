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
        public Texture2D bulletTexture;

        public int direction = 1; // to the right

        public Bullet(Vector2 position, float speed) // for enemies
        {
            Position = position;
            Position.Y += (Spaceship.Size.Y - Size.Y) / 2;
            Position.X -= 3; 
            direction = -1;
            Speed = speed;
            bulletTexture = Raylib.LoadTexture("../../../resources/basicenemybullet.png");

        }

        public Bullet(Vector2 position)
        {
            Position = position;
            Position.X += Spaceship.Size.X;
            Position.Y += (Spaceship.Size.Y - Size.Y) / 2;
            bulletTexture = Raylib.LoadTexture("../../../resources/shipbullet.png");
        }
        public void Move()
        {
            if (!isAlive)
            {
                Raylib.UnloadTexture(bulletTexture);
            }

            if (direction > 0)
            {
                Position.X += Speed / Game.Screen.fps * 300;
            }
            else
            {
                Position.X -= Speed / Game.Screen.fps * 300;
            }

            hitbox = new Rectangle(Position, Size);

            //Raylib.DrawRectangleRec(hitbox, Color.Red);  //HITBOX CHECK
            if (direction == -1)  Raylib.DrawTextureEx(bulletTexture, new Vector2(Position.X - 5f, Position.Y + Size.Y), 270, 0.7f, Color.White);
            else                  Raylib.DrawTextureEx(bulletTexture, new Vector2(Position.X + 8f, Position.Y - 1f), 90, 0.6f, Color.White);



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
