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

    public static class Spaceship
    {
        public static int health = 100;
        public static int damage = 20;
        public static float Speed = 1f;
        public static double ShootSpeed = 0.2f;

        public static List<Bullet> bullets = new List<Bullet>();

        public static Vector2 Size = new Vector2(40f, 40f);
        public static Vector2 Positions = new Vector2(10f, (Screen.Height - Size.X) / 2f);
        public static Rectangle Collision = new Rectangle(Positions, Size);

        public static Texture2D MainShip = Raylib.LoadTexture("../../../resources/Mainship.png");

        public static Sound ShootSound = Raylib.LoadSound("../../../resources/shootsound.ogg");

        public static void control()
        {
            //çapraz yönlerde hız artışı problemi çözülecek

            if (Raylib.IsKeyDown(KeyboardKey.A))  Positions.X -= Positions.X <= 0 ? 0 : Speed / Raylib.GetFPS() * 300;
            if (Raylib.IsKeyDown(KeyboardKey.D))  Positions.X += Positions.X >= (Screen.Width - Size.X) ? 0 : Speed / Raylib.GetFPS() * 300;
            if (Raylib.IsKeyDown(KeyboardKey.W))  Positions.Y -= Positions.Y <= 0 ? 0 : Speed / Raylib.GetFPS() * 300;
            if (Raylib.IsKeyDown(KeyboardKey.S))  Positions.Y += Positions.Y >= (Screen.Height - Size.Y) ? 0 : Speed / Raylib.GetFPS() * 300;

            Collision = new Rectangle(Positions, Size);

        }

        public static void Shoot()
        {
            Raylib.PlaySound(ShootSound);

            Bullet bullet = new Bullet(Positions);
            bullets.Add(bullet);
        }

        public static void Take_Damage(int amount)
        {
            health -= amount;
        }

        public static void CheckDeath()
        {
            if (health <= 0)
            {
                Raylib.UnloadTexture(MainShip);
                Game.isGameOver = true;
            }
        }

    }
}
