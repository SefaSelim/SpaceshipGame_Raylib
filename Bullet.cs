using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
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
        public double Angle;
        double timer = 0;

        public int direction = 1; // to the right

        public Bullet(Vector2 position, float speed) // for enemies
        {
            Position = position;
            Position.Y += (Spaceship.Size.Y - Size.Y) / 2;
            Position.X -= 3; 
            direction = -1;
            Speed = speed;
            Angle = 270;
            bulletTexture = Raylib.LoadTexture("../../../resources/basicenemybullet.png");

        }

        public Bullet(Vector2 position, float speed, int a) // for boss
        {
            Position = position;
            Position.Y += (Spaceship.Size.Y - Size.Y) / 2 +20;
            Position.X -= 3;
            direction = -2;
            Speed = speed / 2;
            bulletTexture = Raylib.LoadTexture("../../../resources/rocket.png");

        }

        public Bullet(Vector2 position)
        {
            Position = position;
            Position.X += Spaceship.Size.X;
            Position.Y += (Spaceship.Size.Y - Size.Y) / 2;
            Angle = 90;
            bulletTexture = Raylib.LoadTexture("../../../resources/shipbullet.png");
        }



        private void find_shortest_path()
        {
            Vector2 Distances = new Vector2(Position.X-Spaceship.Positions.X,Position.Y - Spaceship.Positions.Y);
            float VerticalStep;

            timer++;

            if (Math.Abs(Distances.X) > Math.Abs(Distances.Y)  && Distances.X > 0 )
            {
               VerticalStep = Distances.Y / Distances.X * Speed / Game.Screen.fps * 100;
                Position.X -= Speed / Game.Screen.fps * 300;
                Position.Y -= VerticalStep;
            }
            else if (Math.Abs(Distances.X) > Math.Abs(Distances.Y) && Distances.X < 0)
            {
                VerticalStep = Distances.Y / Distances.X * Speed / Game.Screen.fps * 100;
                Position.X += Speed / Game.Screen.fps * 300;
                Position.Y += VerticalStep;
            }

            else if (Distances.Y > 0)
            {
                VerticalStep = Distances.X / Distances.Y * Speed / Game.Screen.fps * 100;
                Position.Y -= Speed / Game.Screen.fps * 300;
                Position.X -= VerticalStep;
            }
            else
            {
                VerticalStep = Distances.X / Distances.Y * Speed / Game.Screen.fps * 100;
                Position.Y += Speed / Game.Screen.fps * 300;
                Position.X += VerticalStep;
            }


            Angle = Math.Atan2(Distances.Y, Distances.X) * 180 / Math.PI - 90;

            if (timer > 5 * Game.Screen.fps)
            {
                isAlive = false;
                timer = 0;
            }


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
                if (direction == -2 )
                {

                find_shortest_path();

                }
                else
                {
                    Position.X -= Speed / Game.Screen.fps * 300;
                }

                
            }

            hitbox = new Rectangle(Position, Size);

            //Raylib.DrawRectangleRec(hitbox, Color.Red);  //HITBOX CHECK
            if (direction == -1 || direction == -2)  Raylib.DrawTextureEx(bulletTexture, new Vector2(Position.X - 5f, Position.Y + Size.Y), (float)Angle, 0.7f, Color.White);
            else                  Raylib.DrawTextureEx(bulletTexture, new Vector2(Position.X + 8f, Position.Y - 1f), (float) Angle, 0.6f, Color.White);



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
