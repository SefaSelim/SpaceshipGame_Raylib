using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace SpaceshipGame
{
    public class CollisionDetector
    {
        bool bulletCollided = false;
        public void CheckCollision(Game game)
        {
            foreach (Bullet bullet in Spaceship.bullets)
            {

                foreach (Enemy enemy in game.enemies)
                {
                    if (Raylib.CheckCollisionRecs(bullet.hitbox, Spaceship.Collision))
                    {
                        Spaceship.Take_Damage(enemy.Damage);
                        bullet.isAlive = false;
                        bulletCollided = true;
                        break;
                    }

                    if (Raylib.CheckCollisionRecs(bullet.hitbox, enemy.Hitbox))
                    {
                        if (bullet.direction == 1)
                        {
                            enemy.TakeDamage(Spaceship.damage);
                            bullet.isAlive = false;
                            bulletCollided = true;
                            break;
                        }
                    }

                    if (Raylib.CheckCollisionRecs(enemy.Hitbox , Spaceship.Collision))
                    {
                        enemy.isEnemyAlive = false;
                        Spaceship.Take_Damage(enemy.Damage);
                        bulletCollided = true;
                        break;
                    }

                    if (bulletCollided)
                    {

                        continue;
                    }
                }
                game.enemies.RemoveAll(b => !b.isEnemyAlive);

            }
                Spaceship.bullets.RemoveAll(b => !b.isAlive);


        }
    }
}