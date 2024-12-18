using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static SpaceshipGame.Game;

namespace SpaceshipGame
{

    public class Spaceship
    {
        int health = 100;
        int damage = 10;
        public static float Speed = 1f;

        public static List<Bullet> bullets = new List<Bullet>();

        public static Vector2 Size = new Vector2(40f, 40f);
        public static Vector2 Positions = new Vector2(10f, (Screen.Height - Size.X) / 2);
        public static Rectangle Collision = new Rectangle(Positions, Size);

        public static void control()
        {
            //çapraz yönlerde hız artışı problemi çözülecek

            if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                Positions.X -= Positions.X <= 0 ? 0 : Speed / 10;
            }
            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                Positions.X += Positions.X >= (Screen.Width - Size.X) ? 0 : Speed / 10;
            }
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                Positions.Y -= Positions.Y <= 0 ? 0 : Speed / 10;
            }
            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                Positions.Y += Positions.Y >= (Screen.Height - Size.Y) ? 0 : Speed / 10;
            }

            Collision = new Rectangle(Positions, Size);

        }

        public static void Shoot()
        {
            Bullet bullet = new Bullet(Positions, 1f);
            Spaceship.bullets.Add(bullet);
        }


    }
}
