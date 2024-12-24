using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace SpaceshipGame
{
    public class Powerups
    {
        Texture2D Texture2D;
        public bool isAlive = true;

        Random random = new Random();
        Rectangle size = new Rectangle();

        int Powtype;


        Vector2 Position = new Vector2();
        public Powerups(int type) {
            Powtype = type;
        Position = new Vector2(random.Next(200,Game.Screen.Width - 250),random.Next(50,Game.Screen.Height - 50));
        size = new Rectangle((int)Position.X, (int)Position.Y, 20, 20);

            switch (type)
            {
                    case 1:
                    Texture2D = Raylib.LoadTexture("../../../resources/healthpill.png");
                    break;

                    case 2:
                    Texture2D = Raylib.LoadTexture("../../../resources/shootspeedpill.png");
                    break;

                    case 3:
                    Texture2D = Raylib.LoadTexture("../../../resources/damagepill.png");
                    break;

                default:
                    break;
            }
        }

        public void DrawPowerup()
        {
            //Raylib.DrawRectangleRec(size, Color.Red);
            Raylib.DrawTextureEx(Texture2D, Position, 0, 1f, Color.White);

            if (Raylib.CheckCollisionRecs(Spaceship.Collision,size))
            {
                if (Powtype == 1)
                {
                    if (Spaceship.health < 200)
                    {
                        Spaceship.health += 30;
                    }
                }
                if (Powtype == 2)
                {
                    if (Spaceship.ShootSpeed > 0.09f)
                    {
                        Spaceship.ShootSpeed -= 0.04f;
                    }
                }
                if (Powtype == 3)
                {
                    if (Spaceship.damage < 40)
                    {
                        Spaceship.damage += 5;
                    }
                }
                isAlive = false;
            }
        }
    }
}
