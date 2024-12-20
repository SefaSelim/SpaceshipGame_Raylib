﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace SpaceshipGame
{
    public class CollisionDetector
    {
        public void CheckCollision(Game game)
        {
            foreach (Bullet bullet in Spaceship.bullets)
            {

                if (Raylib.CheckCollisionRecs(bullet.hitbox , Spaceship.Collision))
                {
                    Spaceship.Take_Damage(10);
                    bullet.isAlive = false;
                }

                foreach (Enemy enemy in game.enemies)
                {
                    if (Raylib.CheckCollisionRecs(bullet.hitbox, enemy.Hitbox))
                    {
                        enemy.TakeDamage(Spaceship.damage);
                        bullet.isAlive = false;
                    }
                }

            }
        }
    }
}