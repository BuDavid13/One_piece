using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace monoGame_Project.levels
{

    public class Enemy : Character
    {
        public bool key = false;
        public int[] position = new int[2];
        public string around;
        public Enemy(Texture2D CharacterTexture, int level, int x, int y) : base(CharacterTexture)
        {
            this.level = spawnLevel(level);
            this.maxHP = 2 * level * D6(1);
            this.HP = maxHP;
            this.maxShield = level * D6(1);
            this.shield = maxShield;
            this.damage = level;
            position[0] = x;
            position[1] = y;

        }

        public int Damage(Enemy enemy)
        {
            int attackDamage = enemy.damage + D6(1);
            return attackDamage;
        }

        public void Attack(int attackDamage, Character character)
        {
            int tempShield = character.shield;
            int tempDamage = attackDamage;
            if (character.shield > 0)
            {
                character.shield -= attackDamage;
                if (character.shield > 0)
                    tempDamage = 0;
                else
                {
                    tempDamage = attackDamage - tempShield;
                    character.shield = 0;
                }
            }


            if (tempDamage > 0)
            {
                character.HP = character.HP - (tempDamage);
                if (character.HP < 0)
                    character.HP = 0;
            }
        }

        protected int spawnLevel(int level)
        {
            Random rnd = new Random();
            int chance = (rnd.Next(0, 9));
            if (chance <= 4)
            {
            }
            else if (chance > 4 || chance <= 8)
            {
                level++;
            }
            else
            {
                level += 2;
            }

            return level;
        }
        public void Advance(int[] playerXY, int[,] levelGrid, List<Enemy> enemyList, Enemy enemy, Boss boss)
        {
            var item = enemy;
                int[] enemyXY = item.position;
                bool x;
                bool y;
                switch (playerXY[0])
                {
                    case int n when n > enemyXY[0]:
                        x = true; 
                        break;
                    default:
                        x = false;
                    break;
                }
                switch (playerXY[1])
                {
                    case int n when n > enemyXY[1]:
                        y = true;
                        break;
                    default:
                        y = false;
                        break;
                }
                Random rnd = new Random();
                if (rnd.Next(1, 3) == 1)
                {
                    switch (x)
                    {
                        case true:
                            if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1) // HA X TRUE �S NINCS K�
                            {
                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                {
                                    // HA X TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                    item.position[0] += 1;
                                }
                                else    // HA X TRUE DE NEM SZABAD
                                {
                                    switch (y)
                                    {
                                        case true:
                                            if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1)       // HA Y TRUE �S NINCS K�
                                            {
                                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    // HA Y TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                    item.position[1] += 1;
                                                }
                                                else    // HA NEM SZABAD
                                                {
                                                    if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                        item.position[1] -= 1;
                                                    }
                                                    else if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                        item.position[0] -= 1;
                                                    }
                                                }
                                            }
                                            else    // HA Y TRUE DE VAN K�
                                            {
                                                if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] -= 1;
                                                }
                                                else if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] -= 1;
                                                }
                                            }
                                            break;
                                        case false:
                                            if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1)     // HA Y FALSE �S NINCS K�
                                            {
                                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // HA Y FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                    item.position[1] -= 1;
                                                }
                                                else    // HA NEM SZABAD
                                                {
                                                    if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                    {
                                                        //MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                        item.position[1] += 1;
                                                    }
                                                    else if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                        item.position[0] -= 1;
                                                    }
                                                }
                                            }
                                            else    // HA Y FALSE �S VAN K�
                                            {
                                                if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    //MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] += 1;
                                                }
                                                else if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] -= 1;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            else    // HA X TRUE DE VAN K�
                            {
                                switch (y)
                                {
                                    case true:
                                        if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1)       // HA Y TRUE �S NINCS K�
                                        {
                                            if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                            {
                                                // HA Y TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                item.position[1] += 1;
                                            }
                                            else    // HA NEM SZABAD
                                            {
                                                if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] -= 1;
                                                }
                                                else if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] -= 1;
                                                }
                                            }
                                        }
                                        else    // HA Y TRUE DE VAN K�
                                        {
                                            if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                item.position[1] -= 1;
                                            }
                                            else if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                item.position[0] -= 1;
                                            }
                                            
                                        }
                                        break;
                                    case false:
                                        if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1)     // HA Y FALSE �S NINCS K�
                                        {
                                            if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                            {
                                                // HA Y FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                item.position[1] -= 1;
                                            }
                                            else    // HA NEM SZABAD
                                            {
                                                if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    //MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] += 1;
                                                }
                                                else if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] -= 1;
                                                }
                                            }
                                        }
                                        else    // HA Y FALSE �S VAN K�
                                        {
                                            if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                            {
                                                //MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                item.position[1] += 1;
                                            }
                                            else if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                item.position[0] -= 1;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                        case false:
                            if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1) // HA X FALSE �S NINCS K�
                            {
                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                {
                                    // HA X FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                    item.position[0] -= 1;
                                }
                                else    // HA X FALSE DE NEM SZABAD
                                {
                                    switch (y)
                                    {
                                        case true:
                                            if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1)       // HA Y TRUE �S NINCS K�
                                            {
                                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    // HA Y TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                    item.position[1] += 1;
                                                }
                                                else    // HA NEM SZABAD
                                                {
                                                    if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                        item.position[1] -= 1;
                                                    }
                                                    else if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                        item.position[0] += 1;
                                                    }
                                                }
                                            }
                                            else    // HA Y TRUE DE VAN K�
                                            {
                                                if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] -= 1;
                                                }
                                                else if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] += 1;
                                                }                                               
                                            }
                                            break;
                                        case false:
                                            if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1)     // HA Y FALSE �S NINCS K�
                                            {
                                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // HA Y FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                    item.position[1] -= 1;
                                                }
                                                else    // HA NEM SZABAD
                                                {
                                                    if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                    {
                                                        //MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                        item.position[1] += 1;
                                                    }
                                                    else if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                        item.position[0] += 1;
                                                    }
                                                }
                                            }
                                            else    // HA Y FALSE �S VAN K�
                                            {
                                                if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    //MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] += 1;
                                                }
                                                else if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] += 1;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            else    // HA X FALSE DE VAN K�
                            {
                                switch (y)
                                {
                                    case true:
                                        if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1)       // HA Y TRUE �S NINCS K�
                                        {
                                            if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                            {
                                                // HA Y TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                item.position[1] += 1;
                                            }
                                            else    // HA NEM SZABAD
                                            {
                                                if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] -= 1;
                                                }
                                                else if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] += 1;
                                                }
                                            }
                                        }
                                        else    // HA Y TRUE DE VAN K�
                                        {
                                            if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                item.position[1] -= 1;
                                            }
                                            else if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                item.position[0] += 1;
                                            }
                                        }
                                        break;
                                    case false:
                                        if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1)     // HA Y FALSE �S NINCS K�
                                        {
                                            if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                            {
                                                // HA Y FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                item.position[1] -= 1;
                                            }
                                            else    // HA NEM SZABAD
                                            {
                                                if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    //MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] += 1;
                                                }
                                                else if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] += 1;
                                                }
                                            }
                                        }
                                        else    // HA Y FALSE �S VAN K�
                                        {
                                            if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                            {
                                                //MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                item.position[1] += 1;
                                            }
                                            else if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                item.position[0] += 1;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                }






                else
                {
                    switch (y)
                    {
                        case true:
                            if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1) // HA Y TRUE �S NINCS K�
                            {
                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                {
                                    // HA Y TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                    item.position[1] += 1;
                                }
                                else    // HA Y TRUE DE NEM SZABAD
                                {
                                    switch (x)
                                    {
                                        case true:
                                            if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1)       // HA X TRUE �S NINCS K�
                                            {
                                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // HA X TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                    item.position[0] += 1;
                                                }
                                                else    // HA NEM SZABAD
                                                {
                                                    if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1) && playerXY[1] == enemyXY[1] && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                        item.position[0] -= 1;
                                                    }
                                                    else if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                        item.position[1] -= 1;
                                                    }
                                                }
                                            }
                                            else    // HA X TRUE DE VAN K�
                                            {
                                                if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] -= 1;
                                                }
                                                else if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] -= 1;
                                                }
                                            }
                                            break;
                                        case false:
                                            if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1)     // HA X FALSE �S NINCS K�
                                            {
                                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // HA X FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                    item.position[0] -= 1;
                                                }
                                                else    // HA NEM SZABAD
                                                {
                                                    if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                    {
                                                        //MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                        item.position[0] += 1;
                                                    }
                                                    else if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                        item.position[1] -= 1;
                                                    }
                                                }
                                            }
                                            else    // HA X FALSE �S VAN K�
                                            {
                                                if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    //MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] += 1;
                                                }
                                                else if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] -= 1;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            else    // HA Y TRUE DE VAN K�
                            {
                                switch (x)
                                {
                                    case true:
                                        if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1)       // HA X TRUE �S NINCS K�
                                        {
                                            if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // HA X TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                item.position[0] += 1;
                                            }
                                            else    // HA NEM SZABAD
                                            {
                                                if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] -= 1;
                                                }
                                                else if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] -= 1;
                                                }
                                            }
                                        }
                                        else    // HA X TRUE DE VAN K�
                                        {
                                            if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                item.position[0] -= 1;
                                            }
                                            else if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                item.position[1] -= 1;
                                            }
                                         
                                        }
                                        break;
                                    case false:
                                        if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1)     // HA X FALSE �S NINCS K�
                                        {
                                            if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // HA X FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                item.position[0] -= 1;
                                            }
                                            else    // HA NEM SZABAD
                                            {
                                                if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    //MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] += 1;
                                                }
                                                else if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] -= 1;
                                                }
                                            }
                                        }
                                        else    // HA X FALSE �S VAN K�
                                        {
                                            if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                //MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                item.position[0] += 1;
                                            }
                                            else if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                item.position[1] -= 1;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                        case false:
                            if (levelGrid[enemyXY[0], enemyXY[1] - 1] != 1) // HA Y FALSE �S NINCS K�
                            {
                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] - 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] - 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] - 1))
                                {
                                    // HA Y FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                    item.position[1] -= 1;
                                }
                                else    // HA Y FALSE DE NEM SZABAD
                                {
                                    switch (x)
                                    {
                                        case true:
                                            if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1)       // HA X TRUE �S NINCS K�
                                            {
                                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // HA X TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                    item.position[0] += 1;
                                                }
                                                else    // HA NEM SZABAD
                                                {
                                                    if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                        item.position[0] -= 1;
                                                    }
                                                    else if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                        item.position[1] += 1;
                                                    }
                                                }
                                            }
                                            else    // HA X TRUE DE VAN K�
                                            {
                                                if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] -= 1;
                                                }
                                                else if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] += 1;
                                                }
                                            }
                                            break;
                                        case false:
                                            if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1)     // HA X FALSE �S NINCS K�
                                            {
                                                if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // HA X FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                    item.position[0] -= 1;
                                                }
                                                else    // HA NEM SZABAD
                                                {
                                                    if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                    {
                                                        //MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                        item.position[0] += 1;
                                                    }
                                                    else if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                    {
                                                        // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                        item.position[1] += 1;
                                                    }
                                                }
                                            }
                                            else    // HA X FALSE �S VAN K�
                                            {
                                                if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    //MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] += 1;
                                                }
                                                else if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] += 1;
                                                }
                                            }
                                            break;
                                    }
                                }
                            }
                            else    // HA Y TRUE DE VAN K�
                            {
                                switch (x)
                                {
                                    case true:
                                        if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1)       // HA X TRUE �S NINCS K�
                                        {
                                            if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // HA X TRUE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                item.position[0] += 1;
                                            }
                                            else    // HA NEM SZABAD
                                            {
                                                if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] -= 1;
                                                }
                                                else if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] += 1;
                                                }
                                            }
                                        }
                                        else    // HA X TRUE DE VAN K�
                                        {
                                            if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                item.position[0] -= 1;
                                            }
                                            else if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                item.position[1] += 1;
                                            }

                                        }
                                        break;
                                    case false:
                                        if (levelGrid[enemyXY[0] - 1, enemyXY[1]] != 1)     // HA X FALSE �S NINCS K�
                                        {
                                            if (!enemyList.Exists(xy => xy.position[0] == enemyXY[0] - 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] - 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] - 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                // HA X FALSE �S SE ENEMY SE PLAYER, MOZOGHAT
                                                item.position[0] -= 1;
                                            }
                                            else    // HA NEM SZABAD
                                            {
                                                if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                                {
                                                    //MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                    item.position[0] += 1;
                                                }
                                                else if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                                {
                                                    // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                    item.position[1] += 1;
                                                }
                                            }
                                        }
                                        else    // HA X FALSE �S VAN K�
                                        {
                                            if (levelGrid[enemyXY[0] + 1, enemyXY[1]] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] + 1 && xy.position[1] == enemyXY[1]) && !(playerXY[0] == enemyXY[0] + 1 && playerXY[1] == enemyXY[1]) && !(boss.position[0] == enemyXY[0] + 1 && boss.position[1] == enemyXY[1]))
                                            {
                                                //MEGN�ZI HOGY ELLENKEZ� X SZABAD
                                                item.position[0] += 1;
                                            }
                                            else if (levelGrid[enemyXY[0], enemyXY[1] + 1] != 1 && !enemyList.Exists(xy => xy.position[0] == enemyXY[0] && xy.position[1] == enemyXY[1] + 1) && !(playerXY[0] == enemyXY[0] && playerXY[1] == enemyXY[1] + 1) && !(boss.position[0] == enemyXY[0] && boss.position[1] == enemyXY[1] + 1))
                                            {
                                                // MEGN�ZI HOGY ELLENKEZ� Y SZABAD
                                                item.position[1] += 1;
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
                    }
                
            }
        }
    }
}
